using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Application.Mail;
using LMS.Application.Mapper;
using LMS.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Application
{
    public static class ModuleServicesDependences
    {
        public static IServiceCollection AddReposetoriesServices(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(MappingProfile));
            service.AddTransient<IAuthService, AuthService>();
            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<IUserHelpers, UserHelpers>();
            service.AddTransient<IMailingService, MailingService>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return service;
        }
    }
}
