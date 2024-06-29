using LMS.Data.IGenericRepository_IUOW;
using LMS.Infrastructure.GenericRepository_UOW;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure
{
    public static class ModuleInfrastructureDependences
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
        {
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            return service;
        }
    }
}
