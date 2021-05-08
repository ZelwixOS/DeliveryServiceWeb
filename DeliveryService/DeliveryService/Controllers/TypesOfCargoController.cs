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

    public class TypesOfCargoController : ControllerBase
    {

        private readonly IDbCrud dbOp;
        private readonly ILogger logger;
        private readonly IAccountService accountService;

        public TypesOfCargoController(IDbCrud dbCrud, IAccountService accountService, ILogger<TypesOfCargoController> logger)
        {
            dbOp =  dbCrud;
            this.accountService = accountService;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<TypeOfCargoModel> GetAll()
        {
            (string role, UserModel usr) = dbOp.GetRole(accountService, HttpContext);
            if (usr != null)
                logger.LogInformation(usr.UserName + "(" + role + ")" + " requested for all cargo types");
            else
                logger.LogInformation("Guest asked for all types");

            return dbOp.GetAllTypesOfCargo(role);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Get Type: Bad Request");
                return BadRequest(ModelState);
            }
            var typeOfCargo = await Task.Run(() => dbOp.GetTypeOfCargo(id));
     
            return Ok(typeOfCargo);
        }



        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] TypeOfCargoModel typeOfCargo)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Create Type: Bad Request");
                return BadRequest(ModelState);
            }
            int answ = await Task.Run(() => dbOp.CreateCargoType(typeOfCargo));
            string res = ResultHelper.Result(answ, "Create Type of Cargo", logger);
            return Ok(res);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("off/{id}")]
        public async Task<IActionResult> TypeOff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("TypeOff: Bad Request");
                return BadRequest(ModelState);
            }
            int answ = await Task.Run(() => dbOp.TurnCargoType(id, false));
            string res = ResultHelper.Result(answ, "Turn off cargo type" + id, logger);
            return Ok(res);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("on/{id}")]
        public async Task<IActionResult> TypeOn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("TypeOn: Bad Request");
                return BadRequest(ModelState);
            }
            int answ = await Task.Run(() => dbOp.TurnCargoType(id, true));
            string res = ResultHelper.Result(answ, "Turn on cargo type" + id, logger);
            return Ok(res);
        }

    }
}
