using Microsoft.AspNetCore.Mvc;
using SupremeComponentsExercise2.DTOs;
using SupremeComponentsExercise2.Helpers;
using SupremeComponentsExercise2.Interfaces;
using SupremeComponentsExercise2.Models;

namespace SupremeComponentsExercise2.Controllers
{
    /// <summary>
    /// API controller for product operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the ProductsController class
        /// </summary>
        /// <param name="productService">The product service</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Search products with optional filtering (provided in request body) and pagination (query string).
        /// If no filters provided, returns all products.
        /// </summary>
        [HttpGet]
        public ActionResult<PagedResult<Product>> SearchProducts([FromQuery] SearchProductParameters filters,
            [FromQuery] PaginationParameters paging)
        {
            var result = _productService.GetProducts(filters, paging);
            Response.AddPaginationHeaders(result);
            return Ok(result);
        }


        /// <summary>
        /// Get a product by ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>The product if found</returns>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(string id)
        {
            var product = _productService.GetProductById(id);
            return Ok(product);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productDto">The product data</param>
        /// <returns>The created product</returns>
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productService.CreateProduct(productDto);

            return CreatedAtAction("GetProductById", new { id = product.ProductId }, product);
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <param name="productDto">The updated product data</param>
        /// <returns>The updated product</returns>
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(string id, [FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = _productService.UpdateProduct(id, productDto);
            return Ok(product);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
