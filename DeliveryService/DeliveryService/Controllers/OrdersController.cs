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
    public class OrdersController : ControllerBase
    {
        private readonly IDbCrud dbOp;
        private readonly IAccountService accountService;

        public OrdersController(IDbCrud dbCrud, IAccountService accountService)
        {
            dbOp = dbCrud;
            this.accountService = accountService;
        }

        [Route("api/Orders")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            return Ok(await Task.Run(() => dbOp.GetAllOrders(role, usr)));
        }

        [Route("api/Orders/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = await Task.Run(() => dbOp.GetOrder(id));
            return Ok(order);
        }

 //       [Authorize(Roles = "customer")]
        [Route("api/NewOrder")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.CreateOrder(order, role, usr));
            return CreatedAtAction("GetOrder", new { id = order.ID }, order);
        }

 //       [Authorize(Roles = "customer")]
        [Route("api/PutOrder/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.ID = id;
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.UpdateOrder(order, usr));
            
            return NoContent();
        }
//        [Authorize(Roles = "customer")]
        [Route("api/DeleteOrder/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            await Task.Run(() => dbOp.DeleteOrder(id, usr));

            return Ok();
        }

    }
}

