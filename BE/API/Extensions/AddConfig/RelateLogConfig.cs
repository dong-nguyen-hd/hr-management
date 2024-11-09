using System.Reflection;
using System.Collections;
using API.Controllers.Middlewares;
using Business.Domain.Services;
using Business.Extensions;
using Business.Resources.Attributes;
using Business.Resources.SystemData;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

namespace API.Extensions.AddConfig;

public static class RelateLogConfig
{
    public static Serilog.ILogger LogWithContext(this string context) =>
        Log.ForContext("SourceContext", context);

    public static void AddLog(this IServiceCollection services, IConfiguration configuration)
    {
        #region Config log-type

        var logCfg = new LoggerConfiguration();

        logCfg
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithProperty("ApplicationName", SystemInformation.ApplicationName);

        if (SystemGlobal.IsDebug) // Enable log query EF Core for dev env
            logCfg.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information);

        if (SerilogConfig.EnableConsoleLog)
            logCfg.WriteTo.Console();
        else
        {
            // Log only important information
            logCfg.WriteTo.Logger(lc =>
                lc.Filter.ByIncludingOnly(Matching.WithProperty<string>("SourceContext", p => p == "Microsoft.Hosting.Lifetime" ||
                                                                                              p == CronJobService.JobContext ||
                                                                                              p == ErrorHandlerMiddleware.ErrorHandlerMiddlewareContext))
                    .WriteTo.Console());
        }

        if (SerilogConfig.EnableFileLog)
            logCfg.WriteTo.File(new JsonFormatter(), SerilogConfig.PathFileLog ?? "./logs/.json", rollingInterval: RollingInterval.Day);

        if (SerilogConfig.EnableElasticsearchLog)
        {
            logCfg.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(SerilogConfig.ElasticsearchUri))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                ModifyConnectionSettings = (settings) =>
                {
                    settings.ServerCertificateValidationCallback((o, certificate, arg3, arg4) => true);
                    settings.BasicAuthentication(SerilogConfig.ElasticsearchUsername, SerilogConfig.ElasticsearchUsername);
                    return settings;
                },
                IndexFormat = SerilogConfig.ElasticsearchIndexFormat
            });
        }

        Log.Logger = logCfg.CreateLogger();

        #endregion
    }

    #region My Redaction

    private const string _relateObjectContext = "MyRedactionContext";

    public static string? MaskSensitiveData(this object? source)
    {
        try
        {
            if (source is null)
                return string.Empty;

            if (!IsValidType(source))
                return source.MySerialize();

            return MaskInner(source).MySerialize();
        }
        catch (Exception ex)
        {
            _relateObjectContext.LogWithContext().Error(ex.Message, ex);

            return string.Empty;
        }
    }

    private static bool IsValidType(object source)
    {
        var @namespace = source.GetType().Namespace;
        if (@namespace is null || @namespace.StartsWith("System") || source.GetType().IsEnum)
            return false;

        return true;
    }

    private static object? MaskInner(object? obj)
    {
        var visited = new Dictionary<object, object>();

        return Work(obj);

        object? Work(object? inst)
        {
            if (inst is null)
                return null;

            if (visited.TryGetValue(inst, out var prev))
                return prev;

            if (inst is IEnumerable)
            {
                if (inst is IList castList)
                {
                    if (castList is null || castList.Count == 0)
                        return inst;

                    var castListCount = castList.Count;
                    for (int i = 0; i < castListCount; i++)
                    {
                        if (castList[i] is null)
                            continue;

                        var itemType = castList[i].GetType();

                        if (itemType.IsValueType || itemType == typeof(string))
                            return inst;

                        castList[i] = Work(castList[i]);
                    }

                    return castList;
                }

                return inst;
            }

            var type = inst.GetType();
            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
                return inst;

            var result = visited[inst] = Activator.CreateInstance(type);

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetSetMethod() == null)
                    continue;

                var value = property.GetValue(inst);

                if (property.GetCustomAttribute<SensitiveDataAttribute>() != null)
                {
                    if (property.PropertyType == typeof(string))
                        property.SetValue(result, SystemGlobal.Masked);
                    else if (property.PropertyType.IsValueType)
                        property.SetValue(result, 0);
                    else if (property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                        property.SetValue(result, null);
                }
                else if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    property.SetValue(result, value);
                else
                    property.SetValue(result, Work(value));
            }

            return result;
        }
    }

    #endregion
}