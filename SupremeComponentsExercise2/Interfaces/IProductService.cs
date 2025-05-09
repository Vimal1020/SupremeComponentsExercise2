using SupremeComponentsExercise2.DTOs;
using SupremeComponentsExercise2.Models;

namespace SupremeComponentsExercise2.Interfaces
{
    /// <summary>
    /// Service interface for product operations
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get a paginated list of products with optional filtering
        /// </summary>
        /// <param name="parameters">Query parameters for filtering and pagination</param>
        /// <returns>A paginated result of products</returns>
        PagedResult<Product> GetProducts(SearchProductParameters parameters, PaginationParameters paging);

        /// <summary>
        /// Get a product by its ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>The product if found, otherwise null</returns>
        Product GetProductById(string id);

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productDto">Product details</param>
        /// <returns>The created product</returns>
        Product CreateProduct(ProductDTO productDto);

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <param name="productDto">Updated product details</param>
        /// <returns>The updated product if successful, otherwise null</returns>
        Product UpdateProduct(string id, ProductDTO productDto);

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>True if deleted successfully, otherwise false</returns>
        void DeleteProduct(string id);
    }
}
