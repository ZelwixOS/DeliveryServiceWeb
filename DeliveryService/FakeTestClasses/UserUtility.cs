using DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FakeTestClasses
{
    public static class UserUtility
    {
        const string password = "Aa123456!";

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static void CreateDefaultUsers(DSdb context)
        {

            context.Roles.Add(new IdentityRole() { Id = "1", Name = "admin", NormalizedName = "ADMIN" });
            context.Roles.Add(new IdentityRole() { Id = "2", Name = "courier", NormalizedName = "COURIER" });
            context.Roles.Add(new IdentityRole() { Id = "3", Name = "customer", NormalizedName = "CUSTOMER" });

            context.User.Add(GetAdmin());
            context.User.Add(GetCustomer());
            context.User.Add(GetCourier());

            context.UserRoles.Add(new IdentityUserRole<string>() { UserId = "1", RoleId = "1" });
            context.UserRoles.Add(new IdentityUserRole<string>() { UserId = "2", RoleId = "2" });
            context.UserRoles.Add(new IdentityUserRole<string>() { UserId = "3", RoleId = "3" });
        }

        public static User GetAdmin()
        {
            return new User() { Id = "1", UserName = "Administrator", Email = "admin@mail.com", PasswordHash = password, FirstName = "Главный", SecondName = "Администратор" };
        }

        public static User GetCustomer()
        {
            return new User() { Id = "3", UserName = "courier", Email = "courier@mail.com", PasswordHash = password, FirstName = "Главный", SecondName = "Курьер" };
        }

        public static User GetCourier()
        {
            return new User() { Id = "2", UserName = "customer", Email = "customer@mail.com", PasswordHash = password, FirstName = "Главный", SecondName = "Клиент" };
        }
    }
}
