using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Controllers
{
    [EnableCors("SUPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IDbCrud dbOp;
        private readonly ILogger logger;
        private readonly IAccountService accountService;

        public OrderItemsController(IDbCrud dbCrud, IAccountService accountService, ILogger<OrderItemsController> logger)
        {
            dbOp = dbCrud;
            this.logger = logger;
            this.accountService = accountService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<OrderItemModel> GetAll()
        {
            logger.LogInformation("All Items were requested");
            return dbOp.GetAllOrderItems();
        }

        [HttpGet("order/{ord}")]
        public async Task<IActionResult> GetOrderItems([FromRoute] int ord)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Get Order Items: Bad Request");
                return BadRequest(ModelState);
            }
            logger.LogInformation("Items of order "+ ord +" were requested");
            var order = await Task.Run(()=> dbOp.GetOrderItems(ord));
            logger.LogInformation(order.Count + "items of order " + ord + " were returned");
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Get Item: Bad Request");
                return BadRequest(ModelState);
            }
            logger.LogInformation("Item " + id + " was requested");
            var order = await Task.Run(() => dbOp.GetOrderItem(id));
            if (order != null)
                logger.LogInformation("Item " + order.ID + "of order " + order.Order_ID_FK + " was returned");
            else
                logger.LogWarning("Get Item: item was not found");
            return Ok(order);
        }


        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemModel orderItem)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Create Item: Bad Request");
                return BadRequest(ModelState);
            }

            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int ans = await Task.Run(() => dbOp.CreateOrderItem(orderItem, usr));

            string res = ResultHelper.Result(ans, usr, role, "Create Item", logger);

            return Ok(res);
        }
        [Authorize(Roles = "customer")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderItemModel orderItem)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Update Item: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int ans = await Task.Run(() => dbOp.UpdateOrderItem(orderItem, usr));
            string res = ResultHelper.Result(ans, usr, role, "Update Item", logger);
            return Ok(res);
        }
        [Authorize(Roles = "customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Delete Item: Bad Request");
                return BadRequest(ModelState);
            }
            (string role, UserModel usr) = await Task.Run(() => dbOp.GetRole(accountService, HttpContext));
            int ans = await Task.Run(() => dbOp.DeleteOrderItem(id, usr));
            string res = ResultHelper.Result(ans, usr, role, "Delete Item", logger);
            return Ok(res);
        }

    }
}
