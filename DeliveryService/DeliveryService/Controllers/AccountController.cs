using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using BLL.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ASPNetCoreApp.Controllers
{
    [EnableCors("SUPolicy")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IDbCrud dbOp;
        private readonly ILogger logger;

        public AccountController(IAccountService accountService, IDbCrud dbCrud, ILogger<AccountController> logger)
        {
            this.accountService = accountService;
            dbOp = dbCrud;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                logger.LogInformation("User is trying to register");
                string msg;
                List<string> err;

                (msg, err) = await accountService.Register(model);


                if (err == null)
                {

                    var msgres = new
                    {
                        message = msg
                    };
                    logger.LogInformation("Successful customer registration:" + msg);
                    return Ok(msgres);
                }
                else
                {
                    var errorMsg = new
                    {
                        message = msg,
                        error = err.ToArray()
                    };
                    logger.LogWarning("Customer registration was finished with errors");
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
                logger.LogWarning("Customer registration attempt failed: BadRequest");
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/RegisterCourier")]
        [Authorize(Roles = "admin")]
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
                    logger.LogInformation("Successful courier registration:" + msg);
                    return Ok(msgres);
                }
                else
                {
                    var errorMsg = new
                    {
                        message = msg,
                        error = err.ToArray()
                    };
                    logger.LogWarning("Courier registration was finished with errors");
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
                logger.LogWarning("Courier registration attempt failed: BadRequest");
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
                    logger.LogInformation("Successful login:" + msg);
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
                    logger.LogWarning("Login failed: incorrect login or/and password");
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
                logger.LogWarning("Login failed: BadRequest");
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
            logger.LogInformation("LogOut: " + msg.message);
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
            logger.LogInformation("LogOut: " + msg.message);
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
