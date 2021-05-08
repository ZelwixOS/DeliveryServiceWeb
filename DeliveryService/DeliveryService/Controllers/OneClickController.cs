using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [ApiController]
    public class OneClickController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IDbCrud dbOp;
        private readonly IAccountService accountService;

        public OneClickController(IDbCrud dbCrud, IAccountService accountService, ILogger<OrdersController> logger)
        {
            dbOp = dbCrud;
            this.accountService = accountService;
            this.logger = logger;
        }

        [Authorize(Roles = "courier")]
        [Route("api/Add/{id}")]
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Add: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.UpdateOrderStatus(id, 3, role, usr));
            string answer = ResultHelper.Result(res, usr, role, "Add Order", logger);

            return Ok(answer);
        }

        [Authorize(Roles = "customer")]
        [Route("api/Recieved/{id}")]
        [HttpPost]
        public async Task<IActionResult> Recieved(int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Recieved: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.UpdateOrderStatus(id, 2, role, usr));
            string answer = ResultHelper.Result(res, usr, role, "Order Received", logger);

            return Ok(answer);
        }

        [Authorize(Roles = "courier")]
        [Route("api/Delivered/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delivered(int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Delivered: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.UpdateOrderStatus(id, 4, role, usr));
            string answer = ResultHelper.Result(res, usr, role, "Order Delivered", logger);
            return Ok(answer);
        }

        [Authorize(Roles = "customer")]
        [Route("api/Confirmed/{id}")]
        [HttpPost]
        public async Task<IActionResult> Confirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Confirmed: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.UpdateOrderStatus(id, 1, role, usr));
            string answer = ResultHelper.Result(res, usr, role, "Order Confirmed", logger);
            return Ok(answer);
        }

        [Authorize(Roles = "customer")]
        [Route("api/Updating/{id}")]
        [HttpPost]
        public async Task<IActionResult> Updating(int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Updating: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.UpdateOrderStatus(id, 5, role, usr));
            string answer = ResultHelper.Result(res, usr, role, "Order Updated", logger);
            return Ok(answer);
        }


    }
}
