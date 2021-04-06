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
    public class StatusController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        public StatusController(IDbCrud dbCrud)
        {
            dbOp = dbCrud; 
        }

        [HttpGet]
        public IEnumerable<StatusModel> GetAll()
        {
            return dbOp.GetAllStatuses();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var status = dbOp.GetStatus(id);
            return Ok(status);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusModel status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.CreateStatus(status);
            return CreatedAtAction("GetOrder", new { id = status.ID }, status);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StatusModel status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.UpdateStatus(status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.DeleteStatus(id);
            return NoContent();
        }

    }
}
