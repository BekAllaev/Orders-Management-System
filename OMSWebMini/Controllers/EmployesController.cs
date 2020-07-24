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
    public class EmployesController : ControllerBase
    {
        NorthwindContext northwindContext;

        public EmployesController(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        //GET api/Employes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(bool showReports = false, bool showPhoto = false)
        {
            var employes = await northwindContext.Employees.ToListAsync();

            if (showPhoto & showReports)
                return employes;
            else if (!showPhoto & !showReports)
                await Task.Run(() =>
                {
                    employes.ForEach(a =>
                    {
                        a.Photo = null;
                        a.ReportsToNavigation = null;
                        a.InverseReportsToNavigation = null;
                    });
                });
            else if (!showReports)
                await Task.Run(() => { employes.ForEach(a => a.ReportsToNavigation = null); });
            else
                await Task.Run(() => { employes.ForEach(a => a.Photo = null); });

            return employes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await northwindContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody]Employee newEmployee)
        {
            northwindContext.Employees.Add(newEmployee);
            await northwindContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmployeeId }, newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id,[FromBody]Employee editedEmployee)
        {
            if (id != editedEmployee.EmployeeId) return BadRequest();

            northwindContext.Entry(editedEmployee).State = EntityState.Modified;

            try
            {
                await northwindContext.SaveChangesAsync();
            }
            catch(DBConcurrencyException)
            {
                if (!EmployeeExists(id)) return NotFound();
                else throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await northwindContext.Employees.FindAsync(id);

            if (employee == null) return NotFound();

            northwindContext.Employees.Remove(employee);
            await northwindContext.SaveChangesAsync();

            return Ok();
        }


        private bool EmployeeExists(int id)
        {
            return northwindContext.Employees.Any(e => e.EmployeeId== id);
        }
    }
}
