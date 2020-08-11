using System;
using System.Collections.Generic;
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
    public class OrderDetailsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrderDetailsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetailsByOrder(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(orderDetail => orderDetail.OrderId == orderId).ToListAsync();

            if (orderDetails == null)
            {
                return NotFound();
            }

            return orderDetails;
        }

        // PUT: api/OrderDetails/5/4
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> PutOrderDetail([FromRoute] int orderId, [FromRoute] int productId, OrderDetail orderDetail)
        {
            if (orderId != orderDetail.OrderId && productId != orderDetail.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(orderId, productId))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OrderId, orderDetail.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderId }, orderDetail);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> PostOrderDetails([FromBody] IEnumerable<OrderDetail> orderDetails)
        {
            _context.OrderDetails.AddRange(orderDetails);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailsExists(orderDetails))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetailsByOrder(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(orderDetail => orderDetail.OrderId == orderId).ToListAsync();

            if (orderDetails == null)
            {
                return NotFound();
            }

            _context.OrderDetails.RemoveRange(orderDetails);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<ActionResult> DeleteOrderDetail(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(orderDetail => orderDetail.OrderId == orderId && orderDetail.ProductId == productId);

            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool OrderDetailExists(int orderId, int productId)
        {
            return _context.OrderDetails.Any(e => e.OrderId == orderId & e.ProductId == productId);
        }

        private bool OrderDetailsExists(IEnumerable<OrderDetail> orderDetails)
        {
            return _context.OrderDetails.Any(e => orderDetails.Any(orderDetail => orderDetail.OrderId == e.OrderId && orderDetail.ProductId == e.ProductId));
        }
    }
}
