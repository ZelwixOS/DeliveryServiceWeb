using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IDbCrud dbOp;
        private readonly IAccountService accountService;

        public OrdersController(IDbCrud dbCrud, IAccountService accountService, ILogger<OrdersController> logger)
        {
            dbOp = dbCrud;
            this.accountService = accountService;
            this.logger = logger;
        }

        [Route("api/Orders")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            if (usr != null)
                logger.LogInformation("Orders were requested by "+ usr.UserName);
            else
                logger.LogWarning("Orders were requested by unauthorized user");
            return Ok(await Task.Run(() => dbOp.GetAllOrders(role, usr)));
        }

        [Route("api/Orders/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Get order answered: Bad Request");
                return BadRequest(ModelState);
            }
            logger.LogInformation("User requested order "+ id);
            var order = await Task.Run(() => dbOp.GetOrder(id));

            if (order != null)
                logger.LogInformation("Order " + id + "was found and sent");
            else
                logger.LogWarning("Order "+ id +"wasn't found");
            return Ok(order);
        }

        [Authorize(Roles = "customer")]
        [Route("api/NewOrder")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Create order answered: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.CreateOrder(order, role, usr));
            string answer = ResultHelper.Result(res, usr, role, "Create Order", logger);
            return Ok(answer);
        }

        [Authorize(Roles = "customer")]
        [Route("api/PutOrder/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Update order answered: Bad Request");
                return BadRequest(ModelState);
            }
            order.ID = id;
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.UpdateOrder(order, usr));
            string answer = ResultHelper.Result(res, usr, role, "Update Order", logger);
            return Ok(answer);
        }

        [Authorize(Roles = "customer")]
        [Route("api/DeleteOrder/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Delete order answered: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int res = await Task.Run(() => dbOp.DeleteOrder(id, usr));
            string answer = ResultHelper.Result(res, usr, role, "Delete Order", logger);
            return Ok(answer);
        }

    }
}

