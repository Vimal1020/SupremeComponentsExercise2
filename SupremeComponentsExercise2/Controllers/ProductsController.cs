using Microsoft.AspNetCore.Mvc;
using SupremeComponentsExercise2.DTOs;
using SupremeComponentsExercise2.Helpers;
using SupremeComponentsExercise2.Interfaces;
using SupremeComponentsExercise2.Models;

namespace SupremeComponentsExercise2.Controllers
{
    /// <summary>
    /// API controller for performing CRUD operations on products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">Injected service for product operations.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves a page of products, optionally filtered by the given criteria.
        /// </summary>
        /// <param name="filters">Filter parameters for category, price range, or search term.</param>
        /// <param name="paging">Pagination parameters including page number and page size.</param>
        /// <returns>
        /// A <see cref="PagedResult{Product}"/> containing the requested page of products.
        /// </returns>
        [HttpGet]
        public ActionResult<PagedResult<Product>> SearchProducts(
            [FromQuery] SearchProductParameters filters,
            [FromQuery] PaginationParameters paging)
        {
            var result = _productService.GetProducts(filters, paging);
            Response.AddPaginationHeaders(result);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a single product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>
        /// A <see cref="Product"/> if found; otherwise, a 404 Not Found response.
        /// </returns>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(string id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID '{id}' not found." });
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product with the specified data.
        /// </summary>
        /// <param name="productDto">Data transfer object containing product details.</param>
        /// <returns>
        /// A 201 Created response with the newly created <see cref="Product"/>.
        /// </returns>
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] ProductDTO productDto)
        {
            var created = _productService.CreateProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = created.ProductId }, created);
        }

        /// <summary>
        /// Updates an existing product identified by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="productDto">Data transfer object containing updated product details.</param>
        /// <returns>
        /// The updated <see cref="Product"/> if successful; otherwise, a 404 Not Found response.
        /// </returns>
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(string id, [FromBody] ProductDTO productDto)
        {
            var updated = _productService.UpdateProduct(id, productDto);
            if (updated == null)
            {
                return NotFound(new { message = $"Product with ID '{id}' not found." });
            }

            return Ok(updated);
        }

        /// <summary>
        /// Deletes the product with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>
        /// A 204 No Content response if deletion is successful; otherwise, 404 Not Found.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            var removed = _productService.DeleteProduct(id);
            if (!removed)
            {
                return NotFound(new { message = $"Product with ID '{id}' not found." });
            }

            return NoContent();
        }
    }
}
