using BLL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class UserRoles : IUserRoles
    {
        public async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            // Создание ролей администратора и пользователя
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new
                IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("courier") == null)
            {
                await roleManager.CreateAsync(new
                IdentityRole("courier"));
            }
            if (await roleManager.FindByNameAsync("customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("customer"));
            }
            // Создание Администратора
            string adminEmail = "admin@mail.com";
            string adminUserName = "Administrator";
            string adminPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminUserName
                };
                IdentityResult result = await
               userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            // Создание Пользователя

            string custEmail = "customer@mail.com";
            string custUserName = "Customer";
            string custPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(custEmail) == null)
            {
                User cust = new User
                {
                    Email = custEmail,
                    UserName = custUserName

                };
                IdentityResult result = await
               userManager.CreateAsync(cust, custPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(cust, "customer");
                }
            }

            // Создание Курьера

            string courEmail = "courier@mail.com";
            string courUserName = "Courier";
            string courPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(courEmail) == null)
            {
                User cour = new User
                {
                    Email = courEmail,
                    UserName = courUserName

                };
                IdentityResult result = await
               userManager.CreateAsync(cour, courPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(cour, "courier");
                }
            }

        }

    }
}
