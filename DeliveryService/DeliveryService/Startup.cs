using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BLL;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DeliveryService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterBllServices(Configuration);
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();

            services.AddSwaggerGen((options) =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DS API", Version = "v1" });
            });


            services.AddCors(options =>
            {
                options.AddPolicy(name: "SUPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                                .WithMethods("PUT", "DELETE", "GET", "POST")
                                .AllowAnyHeader()
                                .AllowCredentials();
                        
                    });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "SimpleWebApp";
                options.LoginPath = "/";
                options.AccessDeniedPath = "/";
                options.LogoutPath = "/";
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IUserRoles userRoles)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DS API V1");
            });

            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            userRoles.CreateUserRoles(services).Wait();
        }
    }
}
