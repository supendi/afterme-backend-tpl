using AfterMe.Core.Domains.Orders;
using AfterMe.Core.Domains.Orders.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AfterMe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductService productService;
        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            Product createdProduct = await productService.CreateProduct(product);
            return Ok(createdProduct);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            Product product = await productService.GetProduct(productId);
            return Ok(product);
        }
    }
}
