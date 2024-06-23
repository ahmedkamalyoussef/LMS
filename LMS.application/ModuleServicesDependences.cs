using Microsoft.Extensions.DependencyInjection;

namespace LMS.Application
{
    public static class ModuleServicesDependences
    {
        public static IServiceCollection AddReposetoriesServices(this IServiceCollection service)
        {
            return service;
        }
    }
}
