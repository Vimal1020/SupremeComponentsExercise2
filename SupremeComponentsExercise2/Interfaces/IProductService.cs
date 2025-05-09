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
        /// Gets a paged list of products with optional filtering
        /// </summary>
        /// <param name="filters">The search filters</param>
        /// <param name="paging">The pagination parameters</param>
        /// <returns>A paged result of products</returns>
        PagedResult<Product> GetProducts(SearchProductParameters filters, PaginationParameters paging);

        /// <summary>
        /// Gets a product by ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>The product if found, null otherwise</returns>
        Product GetProductById(string id);

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productDto">The product data</param>
        /// <returns>The created product</returns>
        Product CreateProduct(ProductDTO productDto);

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <param name="productDto">The updated product data</param>
        /// <returns>The updated product if found, null otherwise</returns>
        Product UpdateProduct(string id, ProductDTO productDto);

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>True if the product was deleted, false if not found</returns>
        bool DeleteProduct(string id);
    }
}
