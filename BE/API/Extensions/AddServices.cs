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

    
}