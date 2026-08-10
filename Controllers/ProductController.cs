using FullStackSession6.Model;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullStackSession6.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            else if (_productService.GetProductById(id) == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            else
            {
                return Ok(_productService.GetProductById(id));
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _productService.CreateProduct(product);
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            else if (_productService.GetProductById(id) == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            else
            {
                _productService.UpdateProduct(id, product);
                return Ok(product);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            else if (_productService.GetProductById(id) == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            else
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateProductName(int id, [FromBody] Product product)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }
            else if (_productService.GetProductById(id) == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            else
            {
                _productService.UpdateProductName(id, product.Name);
                return NoContent();
            }
        }
    }
}