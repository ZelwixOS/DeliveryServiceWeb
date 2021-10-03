using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserRoles, UserRoles>();

            return services;
        }
    }
}
