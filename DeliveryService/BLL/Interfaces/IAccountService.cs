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
        public Task<(string, List<string>)> Register(RegisterViewModel model);
        public Task<(string, List<string>)> RegisterCourier(RegisterViewModel model);
        public Task<(string, List<string>)> Login(LoginViewModel model);
        public Task<string> LogOut();
        public Task<string> LogisAuthenticatedOff(HttpContext httpCont);
        public Task<IList<User>> GetByRole(string role);
        public Task<User> GetCurrentUserAsync(HttpContext httpCont);
        public Task<IList<string>> GetRole(HttpContext httpCont);

    }
}
