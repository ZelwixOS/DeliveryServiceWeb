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
    public class TypesOfCargoController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        public TypesOfCargoController(IDbCrud dbCrud)
        {
            dbOp =  dbCrud; 
        }

        [HttpGet]
        public IEnumerable<TypeOfCargoModel> GetAll()
        {
            return dbOp.GetAllTypesOfCargo();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var typeOfCargo = dbOp.GetTypeOfCargo(id);
            return Ok(typeOfCargo);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TypeOfCargoModel typeOfCargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.CreateCargoType(typeOfCargo);
            return CreatedAtAction("GetOrder", new { id = typeOfCargo.ID }, typeOfCargo);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TypeOfCargoModel typeOfCargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.UpdateCargoType(typeOfCargo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.DeleteCargoType(id);
            return NoContent();
        }

    }
}
