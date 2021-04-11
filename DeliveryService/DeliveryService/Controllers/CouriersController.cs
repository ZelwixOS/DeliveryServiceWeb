using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CouriersController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        public CouriersController(IDbCrud dbCrud)
        {
            dbOp = dbCrud; 
        }

        [HttpGet]
        public IEnumerable<CourierModel> GetAll()
        {
            return dbOp.GetAllCouriers();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourier([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var courier = dbOp.GetCourier(id);
            return Ok(courier);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourierModel courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.CreateCourier(courier);
            return CreatedAtAction("GetOrder", new { id = courier.ID }, courier);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CourierModel courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.UpdateCourier(courier);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.DeleteCourier(id);
            return NoContent();
        }

    }
}
