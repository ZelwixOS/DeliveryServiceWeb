using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [ApiController]
    public class OneClickController : ControllerBase
    {
        private readonly IDbCrud dbOp;
        private readonly IAccountService accountService;

        public OneClickController(IDbCrud dbCrud, IAccountService accountService)
        {
            dbOp = dbCrud;
            this.accountService = accountService;
        }

        [Authorize(Roles = "courier")]
        [Route("api/Add/{id}")]
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 3, role, usr));

            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [Route("api/Recieved/{id}")]
        [HttpPost]
        public async Task<IActionResult> Recieved(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 2, role, usr));

            return NoContent();
        }

        [Authorize(Roles = "courier")]
        [Route("api/Delivered/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delivered(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 4, role, usr));
            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [Route("api/Confirmed/{id}")]
        [HttpPost]
        public async Task<IActionResult> Confirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 1, role, usr));
            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [Route("api/Updating/{id}")]
        [HttpPost]
        public async Task<IActionResult> Updating(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 5, role, usr));
            return NoContent();
        }

    }
}
