using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Reopsitory;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> ProductList()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }
            var createdProduct = await _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null || id != product.Id)
            {
                return BadRequest("Invalid product data");
            }

            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            var updatedProduct = await _productRepository.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            await _productRepository.DeleteProduct(id);

            // Return the updated product list
            return CreatedAtAction(nameof(ProductList), null, await _productRepository.GetAllProducts());
        }
    }
}
