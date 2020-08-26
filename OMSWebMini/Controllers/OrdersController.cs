using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class OrdersController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrdersController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        //You may have problem on this endpoint
        //this link may help you: https://stackoverflow.com/questions/59199593/net-core-3-0-possible-object-cycle-was-detected-which-is-not-supported
        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var detailedOrder = await _context.Orders.Where(o => o.OrderId == id).Include(o => o.Order_Details).FirstOrDefaultAsync();

            order.Order_Details = detailedOrder.Order_Details;

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody]Order order)
        {
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var orderDetailsToDelete = (await _context.Orders.Include(o => o.Order_Details).Where(o => o.OrderId == id).FirstOrDefaultAsync()).Order_Details;

                    _context.OrderDetails.RemoveRange(orderDetailsToDelete);
                    await _context.SaveChangesAsync();

                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return Ok();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();

                    throw new Exception(e.Message);
                }
            }
        }

        [Route("[action]")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderObject>>> GetOrdersInfo(int id)
        {
            var orders = _context.Orders.Include(order => order.Employee).Include(order => order.Customer).Where(order => order.OrderId == id);

            var orderObject = await orders.Select(o => new OrderObject
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate.Value,
                EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
                CustomerName = o.Customer.CompanyName
            }).ToListAsync();

            return orderObject;
        }

        [Route("[action]")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderDetailObject>>> GetOrderDetailsInfo(int id)
        {
            var orderDetails = _context.OrderDetails.Include(orderDetail => orderDetail.Product).Where(orderDetail => orderDetail.OrderId == id);

            var orderDetailObjects = await orderDetails.Select(o => new OrderDetailObject()
            {
                OrderId = o.OrderId,
                ProductName = o.Product.ProductName,
                UnitPrice = o.UnitPrice,
                Quantity = o.Quantity,
                Discount = o.Discount * 100,
                SubTotal = (decimal)((float)o.UnitPrice * o.Quantity * (1 - o.Discount))
            }).ToListAsync();

            return orderDetailObjects;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }

    #region Screen objects
    public class OrderObject
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderDetailObject
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal SubTotal { get; set; }
    }
    #endregion

}
