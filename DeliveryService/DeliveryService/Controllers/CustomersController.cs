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
    public class CustomersController : ControllerBase
    {
        private readonly IDbCrud dbOp;
        public CustomersController(IDbCrud dbCrud)
        {
            dbOp = dbCrud;
        }

        [HttpGet]
        public IEnumerable<CustomerModel> GetAll()
        {
            return dbOp.GetAllCustomers();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var custumer = dbOp.GetClient(id);
            return Ok(custumer);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.CreateCustomer(customer);
            return CreatedAtAction("GetOrder", new { id = customer.ID }, customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.UpdateCustomer(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbOp.DeleteCustomer(id);
            return NoContent();
        }
    }
}
