using SupremeComponentsExercise2.DTOs;
using SupremeComponentsExercise2.Interfaces;
using SupremeComponentsExercise2.Models;
using System.Collections.Concurrent;

namespace SupremeComponentsExercise2.Services
{
    /// <summary>
    /// Implementation of IProductService that uses in-memory storage
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly ConcurrentDictionary<string, Product> _products;

        /// <summary>
        /// Initializes a new instance of the ProductService class
        /// </summary>
        public ProductService()
        {
            // Initialize with some sample data
            _products = new ConcurrentDictionary<string, Product>();
            SeedData();
        }

        /// <inheritdoc/>
        public PagedResult<Product> GetProducts(SearchProductParameters filters, PaginationParameters paging)
        {
            // Ensure filters is not null when no body is provided
            filters ??= new SearchProductParameters();

            var query = _products.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filters.Category))
                query = query.Where(p => p.Category.Equals(filters.Category, StringComparison.OrdinalIgnoreCase));

            if (filters.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filters.MinPrice.Value);

            if (filters.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filters.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
                query = query.Where(p => p.Name.Contains(filters.SearchTerm, StringComparison.OrdinalIgnoreCase));

            var totalCount = query.Count();

            var items = query
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToList();

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
        }

        /// <inheritdoc/>
        public Product GetProductById(string id)
        {
            if (!_products.TryGetValue(id, out var product))
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            return product;
        }

        /// <inheritdoc/>
        public Product CreateProduct(ProductDTO productDto)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid().ToString(),
                Name = productDto.Name,
                Category = productDto.Category,
                Quantity = productDto.Quantity,
                Price = productDto.Price
            };

            _products.TryAdd(product.ProductId, product);
            return product;
        }

        /// <inheritdoc/>
        public Product UpdateProduct(string id, ProductDTO productDto)
        {
            if (!_products.TryGetValue(id, out var existingProduct))
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            var updatedProduct = new Product
            {
                ProductId = id,
                Name = productDto.Name,
                Category = productDto.Category,
                Quantity = productDto.Quantity,
                Price = productDto.Price
            };
            _products[id] = updatedProduct;
            return updatedProduct;
        }

        /// <inheritdoc/>
        public void DeleteProduct(string id)
        {
            if (!_products.TryRemove(id, out _))
                throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        /// <summary>
        /// Adds initial sample data to the in-memory store
        /// </summary>
        private void SeedData()
        {
            var sampleProducts = new List<Product>
            {
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = "Laptop",
                    Category = "Electronics",
                    Quantity = 25,
                    Price = 999.99m
                },
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = "Smartphone",
                    Category = "Electronics",
                    Quantity = 50,
                    Price = 699.99m
                },
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = "Desk Chair",
                    Category = "Furniture",
                    Quantity = 15,
                    Price = 249.99m
                },
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = "Coffee Maker",
                    Category = "Appliances",
                    Quantity = 30,
                    Price = 79.99m
                },
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = "Book Shelf",
                    Category = "Furniture",
                    Quantity = 10,
                    Price = 189.99m
                }
            };

            foreach (var product in sampleProducts)
            {
                _products.TryAdd(product.ProductId, product);
            }
        }
    }
}
