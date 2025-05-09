using System.ComponentModel.DataAnnotations;

namespace SupremeComponentsExercise2.Models
{
    /// <summary>
    /// Represents a product in the inventory system
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category the product belongs to
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Available quantity of the product
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public decimal Price { get; set; }
    }
}
