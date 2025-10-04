using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketplaceDemo.Data;
using MarketplaceDemo.Models;
using MarketplaceDemo.Dtos;

namespace MarketplaceDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrdersApiController(AppDbContext db) { _db = db; }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            if (dto == null || dto.Items == null || !dto.Items.Any())
                return BadRequest("Order must contain at least one item.");

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                // Load all products involved
                var productIds = dto.Items.Select(i => i.ProductId).Distinct().ToList();
                var products = await _db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id);

                // Validate products and stock
                foreach (var it in dto.Items)
                {
                    if (!products.ContainsKey(it.ProductId))
                        return BadRequest($"Product {it.ProductId} not found.");

                    var prod = products[it.ProductId];
                    if (prod.Stock < it.Quantity)
                        return BadRequest($"Not enough stock for product {prod.Name} (id={prod.Id}).");
                }

                // Create order
                var order = new Order { CreatedAt = DateTime.UtcNow, Total = 0m };
                _db.Orders.Add(order);
                await _db.SaveChangesAsync(); // get order.Id

                decimal orderTotal = 0m;
                var orderItems = new List<OrderItem>();
                foreach (var it in dto.Items)
                {
                    var prod = products[it.ProductId];
                    prod.Stock -= it.Quantity;
                    _db.Products.Update(prod);

                    var item = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = prod.Id,
                        Quantity = it.Quantity,
                        UnitPrice = prod.Price
                    };
                    orderItems.Add(item);
                    orderTotal += prod.Price * it.Quantity;
                }

                _db.OrderItems.AddRange(orderItems);

                order.Total = orderTotal;
                _db.Orders.Update(order);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                // Load order with items for response
                var createdOrder = await _db.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                // Map to DTO
                var dtoResult = new OrderDto
                {
                    Id = createdOrder.Id,
                    CreatedAt = createdOrder.CreatedAt,
                    Total = createdOrder.Total,
                    Items = createdOrder.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        UnitPrice = oi.UnitPrice,
                        Quantity = oi.Quantity
                    }).ToList()
                };

                return CreatedAtAction(nameof(GetOrder), new { id = dtoResult.Id }, dtoResult);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return StatusCode(500, "Error creating order: " + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            // Map to DTO
            var dto = new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Total = order.Total,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                }).ToList()
            };

            return Ok(dto);
        }
    }
}
