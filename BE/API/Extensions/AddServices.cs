using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Mapping.Account;
using Business.Resources;
using Business.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class AddServices
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IEducationService, EducationService>();

            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ICertificateService, CertificateService>();

            services.AddScoped<IWorkHistoryRepository, WorkHistoryRepository>();
            services.AddScoped<IWorkHistoryService, WorkHistoryService>();

            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IPositionService, PositionService>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICategoryPersonRepository, CategoryPersonRepository>();
            services.AddScoped<ICategoryPersonService, CategoryPersonService>();

            services.AddScoped<ITechnologyRepository, TechnologyRepository>();
            services.AddScoped<ITechnologyService, TechnologyService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ITokenManagementService, TokenManagementService>();

            services.AddScoped<ITimesheetService, TimesheetService>();
            services.AddScoped<ITimesheetRepository, TimesheetRepository>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IPayService, PayService>();
            services.AddScoped<IPayRepository, PayRepository>();

            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<IImageService, ImageService>(); // ImageCrossPlatformService (use for other OSs except Windows)

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(ModelToResourceProfile));
        }

    public static void AddCustomizeSwagger(this IServiceCollection services)
    {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Human Resource Management for IT Company", Version = "v1.0" });
                c.OperationFilter<SwaggerFileOperationFilter>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Human Resource Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // Must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
        }

    public static void AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
    {
            if (options == null)
                throw new ArgumentNullException(nameof(options), "Please provide Schedule Configurations.");

            var config = new ScheduleConfig<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), "Empty Cron Expression is not allowed.");

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
        }
}