using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketplaceDemo.Data;
using MarketplaceDemo.Dtos;

namespace MarketplaceDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrderItemsApiController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("by-order/{orderId:int}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            var items = await _db.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                })
                .ToListAsync();

            if (!items.Any()) return NotFound($"Item not found for Order {orderId}.");
            return Ok(items);
        }
    }
}
