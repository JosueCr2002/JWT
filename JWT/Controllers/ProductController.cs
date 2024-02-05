using JWT.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetProducts()
        {
            var products = ProductsConstants.Products;
            return Ok(products);
        }
    }
}
