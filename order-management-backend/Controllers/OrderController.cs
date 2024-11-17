using Microsoft.AspNetCore.Mvc;
using order_management_backend.Models;

namespace order_management_backend.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        public static List<Order> Orders = new List<Order>(); // static list as database

        [HttpPost("api/orders")]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            order.Id = Orders.Count + 1;
            Orders.Add(order);
            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        [HttpGet("api/orders")]
        public IActionResult GetOrders([FromQuery] string productName)
        {
            var filteredOrders = string.IsNullOrWhiteSpace(productName)
                ? Orders
                : Orders.Where(o => o.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(filteredOrders);
        }

    }
}
