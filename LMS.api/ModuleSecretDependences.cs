namespace LMS.Api
{
    public static class ModuleSecretDependences
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mailingSection = configuration.GetSection("Mailing");
            //services.Configure<MailSettings>(mailingSection);
            return services;
        }
    }
}
