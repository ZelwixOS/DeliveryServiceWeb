using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [ApiController]
    public class OneClickController : ControllerBase
    {
        private readonly IDbCrud dbOp;
        private readonly IAccountService accountService;

        public OneClickController(IDbCrud dbCrud, IAccountService accountService)
        {
            dbOp = dbCrud;
            this.accountService = accountService;
        }

        [Route("api/Add/{id}")]
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 3, accountService, HttpContext));

            return NoContent();
        }

        [Route("api/Recieved/{id}")]
        [HttpPost]
        public async Task<IActionResult> Recieved(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 2, accountService, HttpContext));

            return NoContent();
        }
        [Route("api/Delivered/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delivered(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 4, accountService, HttpContext));
            return NoContent();
        }

        [Route("api/Confirmed/{id}")]
        [HttpPost]
        public async Task<IActionResult> Confirmed(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 1, accountService, HttpContext));
            return NoContent();
        }

        [Route("api/Updating/{id}")]
        [HttpPost]
        public async Task<IActionResult> Updating(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => dbOp.UpdateOrderStatus(id, 5, accountService, HttpContext));
            return NoContent();
        }

    }
}
