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
    public class SuppliersController : ControllerBase
    {
        NorthwindContext northwindContext;

        public SuppliersController(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await northwindContext.Suppliers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await northwindContext.Suppliers.FindAsync(id);

            if (supplier == null) return NotFound();

            return supplier;
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier([FromBody] Supplier newSupplier)
        {
            northwindContext.Suppliers.Add(newSupplier);
            await northwindContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new { id = newSupplier.SupplierId }, newSupplier);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutSupplier(int id, [FromBody] Supplier editedSupplier)
        {
            if (id == editedSupplier.SupplierId) return BadRequest();

            northwindContext.Entry(editedSupplier).State = EntityState.Modified;

            try
            {
                await northwindContext.SaveChangesAsync();
            }
            catch(DBConcurrencyException)
            {
                var isSupplierExist = northwindContext.Suppliers.Any(a => a.SupplierId == id);

                if (!isSupplierExist) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            var supplier = await northwindContext.Suppliers.FindAsync(id);

            northwindContext.Suppliers.Remove(supplier);

            await northwindContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
