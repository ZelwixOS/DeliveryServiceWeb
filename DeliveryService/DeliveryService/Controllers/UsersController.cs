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
using Microsoft.Extensions.Logging;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        private readonly IAccountService serv;
        private readonly ILogger logger;

        public UsersController(IDbCrud dbCrud, IAccountService services, ILogger<UsersController> logger)
        {
            dbOp =  dbCrud;
            serv = services;
            this.logger = logger;
        }

        [HttpGet]
        public UsersByRole GetAll()
        {
            logger.LogInformation("All users were requested");
            return dbOp.GetUsersByRole(serv);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Get User: Bad Request");
                return BadRequest(ModelState);
            }
            logger.LogInformation("User "+ id+" was requested");
            var user = await Task.Run(() => dbOp.GetUser(id)); 
            if (user!=null)
                logger.LogInformation("User " + id + " was returned");
            else
                logger.LogWarning("User " + id + " was not found");
            return Ok(user);
        }


    }
}
