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
    public class UsersController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        public UsersController(IDbCrud dbCrud)
        {
            dbOp =  dbCrud;
        }

        [HttpGet]
        public IEnumerable<UserModel> GetAll()
        {
            return dbOp.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await Task.Run(() => dbOp.GetUser(id)); 
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.CreateUser(user));
            
            return CreatedAtAction("GetOrder", new { id = user.ID }, user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateUser(user));
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.DeleteUser(id));
            
            return NoContent();
        }

    }
}
