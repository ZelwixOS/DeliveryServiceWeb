using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
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

        [HttpGet]
        public AllOrdersModel GetAll()
        {
            return dbOp.GetAllOrders(accountService, HttpContext);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = await Task.Run(() => dbOp.GetOrder(id));
            return Ok(order);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.CreateOrder(order, accountService, HttpContext));
            return CreatedAtAction("GetOrder", new { id = order.ID }, order);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.ID = id;
            await Task.Run(() => dbOp.UpdateOrder(order));
            
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.DeleteOrder(id));

            return NoContent();
        }

    }
}

