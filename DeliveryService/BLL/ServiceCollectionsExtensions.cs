using BLL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BLL
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection RegisterBllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepositories(configuration);
            services.AddScoped<IDbCrud, dbOperations>();


            return services;
        }
    }
}
