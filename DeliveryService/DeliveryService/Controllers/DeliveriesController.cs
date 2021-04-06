using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Cors;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        public DeliveriesController(IDbCrud dbCrud)
        {
            dbOp = dbCrud; //временно
        }

        [HttpGet]
        public IEnumerable<DeliveryModel> GetAll()
        {
            return dbOp.GetAllDeliveries();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDelivery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var status = dbOp.GetDelivery(id);
            return Ok(status);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliveryModel delivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.CreateDelivery(delivery);
            return CreatedAtAction("GetOrder", new { id = delivery.ID }, delivery);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeliveryModel delivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.UpdateDelivery(delivery);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.DeleteDelivery(id);
            return NoContent();
        }

    }
}
