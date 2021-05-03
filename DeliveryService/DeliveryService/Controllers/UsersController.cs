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
using Microsoft.AspNetCore.Authorization;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        private readonly IAccountService serv;

        public UsersController(IDbCrud dbCrud, IAccountService services)
        {
            dbOp =  dbCrud;
            serv = services;
        }

        [HttpGet]
        public UsersByRole GetAll()
        {
            return dbOp.GetUsersByRole(serv);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
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
        public async Task<IActionResult> Delete([FromRoute] string id)
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
