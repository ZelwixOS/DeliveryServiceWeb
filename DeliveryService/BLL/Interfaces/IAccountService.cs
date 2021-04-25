using BLL.Models;
using DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task<(string, List<string>)> Register(RegisterViewModel model);
        Task<(string, List<string>)> Login(LoginViewModel model);
        Task<string> LogOut();
        Task<string> LogisAuthenticatedOff(HttpContext httpCont);

        public Task<User> GetCurrentUserAsync(HttpContext httpCont);
        public Task<IList<string>> GetRole(HttpContext httpCont);

    }
}
