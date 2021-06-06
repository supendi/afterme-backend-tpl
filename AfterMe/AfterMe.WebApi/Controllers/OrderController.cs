using AfterMe.Core.Domains.Orders;
using AfterMe.Core.Domains.Orders.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AfterMe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderService orderService;
        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            Order createdOrder = await orderService.CreateOrder(order);
            return Ok(createdOrder);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(string orderId)
        {
            Order order = await orderService.GetOrder(orderId);
            return Ok(order);
        }
    }
}
