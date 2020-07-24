using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMSWebMini.Data;
using OMSWebMini.Model;

namespace OMSWebMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        NorthwindContext northwindContext;

        public CustomersController(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await northwindContext.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await northwindContext.Customers.FindAsync(id);

            if (customer == null) return NotFound();

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult> PostCustomer([FromBody] Customer newCustomer)
        {
            northwindContext.Customers.Add(newCustomer);
            await northwindContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(string id, [FromBody] Customer editedCustomer)
        {
            northwindContext.Entry(editedCustomer).State = EntityState.Modified;

            try
            {
                await northwindContext.SaveChangesAsync();
            }
            catch(DBConcurrencyException)
            {
                bool isCustomerExist = northwindContext.Customers.Any(a => a.CustomerId == id);

                if (!isCustomerExist) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DelteCustomer(string id)
        {
            var customer = await northwindContext.Customers.FindAsync(id);

            if (customer == null) return NotFound();

            northwindContext.Customers.Remove(customer);
            await northwindContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
