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
    [Produces("application/json")]
    public class OneClickController : ControllerBase
    {
        private readonly IDbCrud dbOp;

        public OneClickController(IDbCrud dbCrud)
        {
            dbOp = dbCrud;
        }

        [Route("api/OneClick/Add")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Add([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrderModel o = dbOp.GetOrder(id);
            if (o != null)
            {
                if (o.Status_ID_FK == 1)
                {
                    o.Status_ID_FK = 3;
                    await Task.Run(() => dbOp.UpdateOrder(o));
                }
            }
            return NoContent();
        }
        [Route("api/OneClick/Recieved")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Recieved([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrderModel o = dbOp.GetOrder(id);
            if (o != null)
            {
                if (o.Status_ID_FK != 1)
                {
                    o.Status_ID_FK = 2;
                    await Task.Run(() => dbOp.UpdateOrder(o));
                }
            }

            return NoContent();
        }
        [Route("api/OneClick/Delivered")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Delivered([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrderModel o = dbOp.GetOrder(id);
            if (o != null)
            {
                if (o.Status_ID_FK == 3)
                {
                    o.Status_ID_FK = 4;
                    await Task.Run(() => dbOp.UpdateOrder(o));
                }

            }
            return NoContent();
        }

    }
}
