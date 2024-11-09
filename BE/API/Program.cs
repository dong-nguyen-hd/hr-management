using System.Text;
using API.Controllers.Config;
using API.Controllers.Middlewares;
using API.Extensions;
using API.Extensions.AddConfig;
using API.Extensions.JsonConverter;
using Business.Extensions.AddConfig;
using Business.Resources.SystemData;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Serilog;

const string _hostingSourceContext = "Microsoft.Hosting.Lifetime";

try
{
    Console.OutputEncoding = Encoding.UTF8;

    var builder = WebApplication.CreateBuilder(args);

    // Declare external-file
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Configuration.AddJsonFile("response-message.json", optional: false, reloadOnChange: true);
    builder.Configuration.AddJsonFile("administrative-province.json", optional: false, reloadOnChange: true);
    builder.Configuration.AddUserSecrets<Program>(false); // Explicit use secrets.json in env production, staging. By default it only use in development

    SystemGlobal.IsDebug = builder.Environment.IsDevelopment();
    builder.Services.GetSystemData(builder.Configuration);

    // Declare log
    builder.Services.AddLog(builder.Configuration);
    _hostingSourceContext.LogWithContext().Information("Starting up");
    builder.Host.UseSerilog();

    // Force convert dateTime with kind in PostgreSQL
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    #region Add services to the container.

    // Multipart body length limit
    builder.Services.Configure<FormOptions>(options =>
    {
        // Set the limit to 10 MB
        options.MultipartBodyLengthLimit = 10485760; // Bytes
    });

    builder.Services.ApplyTimeoutProfile();
    builder.Services.AddSignalR();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers(opt => { opt.ApplyCacheProfile(); }).ConfigureApiBehaviorOptions(options =>
    {
        // Adds a custom error response factory when Model-State is invalid
        options.InvalidModelStateResponseFactory = InvalidResponseFactory.ProduceErrorResponse;
    }).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new MyDateTimeConverter());
    });

    // Add redis / mem cache
    if (CacheConfig.UseRedis)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = CacheConfig.RedisUri;
            options.InstanceName = CacheConfig.RedisInstanceName;
        });
    }
    else
    {
        builder.Services.AddDistributedMemoryCache();
    }

    //builder.Services.RegisterCronJob();

    builder.Services.AddResponseCaching();
    builder.Services.AddJwtBearerAuthentication();
    //builder.Services.AddHealthCheck();
    builder.Services.AddCustomizeSwagger();
    builder.Services.AddEndpointsApiExplorer(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<CoreContext>(opts =>
    {
        opts.UseNpgsql(SystemGlobal.PostgresqlConnectionString, o =>
        {
            o.EnableRetryOnFailure();

            if (SystemGlobal.IsDebug)
            {
                opts.EnableDetailedErrors();
                opts.EnableSensitiveDataLogging();
            }
        }).UseSnakeCaseNamingConvention();
    });

    builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

    //builder.Services.AddPolices(); // Policy-based authorization
    builder.Services.AddDependencyInjection(builder.Configuration);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
    });

    #endregion

    #region Configure the HTTP request pipeline.

    var app = builder.Build();

    app.UseStaticFiles(new StaticFileOptions
    {
        RequestPath = "/resources",
        HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
        OnPrepareResponse = (context) =>
        {
            var headers = context.Context.Response.GetTypedHeaders();
            headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromHours(1)
            };
        }
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => { options.DefaultModelsExpandDepth(-1); });
    }

    app.UseResponseCompression();
    app.UseSerilogRequestLogging();
    if (app.Environment.IsProduction())
        app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseRouting();
    app.UseResponseCaching();
    app.UseRequestTimeouts();
    app.UseMiddleware<LoggerMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.Use((context, next) => // No-caching explicit
    {
        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
        {
            NoCache = true,
            NoStore = true
        };
        return next.Invoke();
    });
    //app.MapHealthCheck();
    app.MapControllers();
    app.Run();

    #endregion
}
catch (Exception ex)
{
    if (ex is HostAbortedException) // Ex throw by ef-core when migration
        return;

    _hostingSourceContext.LogWithContext().Fatal($"Unhandled exception: {ex.Message}", ex);
}
finally
{
    _hostingSourceContext.LogWithContext().Information("Shut down complete");
    Log.CloseAndFlush();
}