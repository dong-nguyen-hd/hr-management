using API.Controllers.Config.Permission.Handler;
using API.Controllers.Config.Permission.Requirement;
using API.Controllers.Filters;
using API.Domain.Services;
using API.Mapping.Account;
using API.Resources.SystemData;
using API.Services;

namespace API.Extensions.AddConfig;

public static class RelateServices
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        #region Scoped

        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<ICertificateService, CertificateService>();
        services.AddScoped<IWorkHistoryService, WorkHistoryService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryPersonService, CategoryPersonService>();
        services.AddScoped<ITechnologyService, TechnologyService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenManagementService, TokenManagementService>();
        services.AddScoped<ITimesheetService, TimesheetService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IPayService, PayService>();
        services.AddScoped<IImageService, ImageService>(); // ImageCrossPlatformService (use for other OSs except Windows)
        services.AddAutoMapper(typeof(ModelToResourceProfile));

        services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        services.AddAutoMapper(typeof(ResourceToModelProfile));
        //services.AddValidatorsFromAssemblyContaining<CreateValidator>();

        #endregion

        #region Filter

        services.AddMvc(options => { options.Filters.Add(new LoggerActionFilter()); });

        #endregion
    }

    public static void AddPolices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(MyPolicy.Administrator, policy =>
                policy.AddRequirements(new PermissionRequirement([MyPolicy.Administrator])));

            options.AddPolicy(MyPolicy.Editor, policy =>
                policy.AddRequirements(new PermissionRequirement([MyPolicy.Administrator, MyPolicy.Editor])));

            options.AddPolicy(MyPolicy.Viewer, policy =>
                policy.AddRequirements(new PermissionRequirement([MyPolicy.Administrator, MyPolicy.Editor, MyPolicy.Viewer])));
        });
    }
}