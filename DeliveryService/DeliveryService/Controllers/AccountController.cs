using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using BLL.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace ASPNetCoreApp.Controllers
{
    [EnableCors("SUPolicy")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string msg;
                List<string> err;

                (msg, err) = await accountService.Register(model);


                if (err == null)
                {
                    var msgres = new
                    {
                        message = msg

                    };
                    return Ok(msgres);
                }
                else
                {
                    var errorMsg = new
                    {
                        message = msg,
                        error = err.ToArray()
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные.",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToArray()
                };
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string msg;
                List<string> err;

                (msg, err) = await accountService.Login(model);

                if (err == null)
                {
                    var response = new
                    {
                        message = msg
                    };
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и/или пароль");

                    var errorMsg = new
                    {
                        message = msg,
                        error = err.ToArray()
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage)).ToArray()
                };
                return Ok(errorMsg);
            }
        }


        [HttpPost]
        [Route("api/Account/LogOut")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            var msg = new
            {
                message = await accountService.LogOut()
            };
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/Account/isAuthenticated")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogisAuthenticatedOff()
        {         
            var msg = new
            {
                message = await accountService.LogisAuthenticatedOff(HttpContext)
            };
            return Ok(msg);
        }

    }
}
