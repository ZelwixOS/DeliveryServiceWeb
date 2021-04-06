using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IDbCrud dbOp;
        public OrderItemsController(IDbCrud dbCrud)
        {
            dbOp = dbCrud;
        }

        [HttpGet]
        public IEnumerable<OrderItemModel> GetAll()
        {
            return dbOp.GetAllOrderItems();
        }

        [HttpGet("order/{ord}")]
        public async Task<IActionResult> GetOrderItems([FromRoute] int ord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = await Task.Run(()=> dbOp.GetOrderItems(ord));
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = await Task.Run(() => dbOp.GetOrderItem(id));
            return Ok(order);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemModel orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.CreateOrderItem(orderItem));
            
            return CreatedAtAction("GetOrder", new { id = orderItem.ID }, orderItem);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderItemModel orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateOrderItem(orderItem));
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.DeleteOrderItem(id));
            
            return NoContent();
        }
    }
}
