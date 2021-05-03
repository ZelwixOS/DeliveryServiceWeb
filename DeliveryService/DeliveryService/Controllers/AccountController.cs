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
        private readonly IDbCrud dbOp;

        public AccountController(IAccountService accountService, IDbCrud dbCrud)
        {
            this.accountService = accountService;
            dbOp = dbCrud;
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
        [Route("api/Account/RegisterCourier")]
        public async Task<IActionResult> RegisterCourier([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string msg;
                List<string> err;

                (msg, err) = await accountService.RegisterCourier(model);


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
        public async Task<IActionResult> LogisAuthenticatedOff()
        {         
            var msg = new
            {
                message = await accountService.LogisAuthenticatedOff(HttpContext)
            };
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/Account/Role")]
        public async Task<IActionResult> Role()
        {
            (string msg, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            return Ok(msg);
        }


    }
}
