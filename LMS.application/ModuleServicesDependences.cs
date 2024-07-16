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
        public static IServiceCollection AddReposetoriesServices(this IServiceCollection services)
        {
            services.AddScoped<CloudinaryService>();
            services.AddLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerSevrice, AnswerService>();
            services.AddTransient<IEvaluationService, EvaluationService>();
            services.AddTransient<IUserHelpers, UserHelpers>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ILectureService, LectureService>();
            services.AddScoped<IMailingService, MailingService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
