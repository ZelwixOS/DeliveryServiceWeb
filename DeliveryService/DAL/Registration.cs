using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;



namespace DAL
{
    public static class Registration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IdbOperations), typeof(dbReposSQL));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DSdb>();
            services.AddScoped(typeof(DbContext), typeof(DSdb));
            services
                .AddDbContext<DSdb>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
            

            return services;
        }
    }
}
