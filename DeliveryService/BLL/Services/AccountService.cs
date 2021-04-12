using BLL.Interfaces;
using BLL.Models;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(string, List<string>)> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return ("Выполнен вход пользователем: " + model.UserName, null);
            }
            else
            {
                List<string> err = new List<string>();
                err.Add("Неправильный логин и/или пароль");

                return ("Вход не выполнен.", err);
            }


        }
        public async Task<string> LogOut()
        {
            await _signInManager.SignOutAsync();
            var msg = "Выполнен выход.";

            return msg;
        }

        public async Task<(string, List<string>)> Register(RegisterViewModel model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
            // Добавление нового пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "customer");

                await _signInManager.SignInAsync(user, false);

                var msg = "Добавлен новый пользователь: " + user.UserName;

                return (msg, null);
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
                return ("Ошибка. Пользователь не был добавлен.", errorList);
            }
        }

        Task<User> GetCurrentUserAsync(HttpContext httpCont) => _userManager.GetUserAsync(httpCont.User);


        public async Task<string> LogisAuthenticatedOff(HttpContext httpCont)
        {
       

            User usr = await GetCurrentUserAsync(httpCont);

            var message = usr == null ? "" : usr.UserName;

            return message;
        }

   

    }
}
