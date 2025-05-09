using System.ComponentModel.DataAnnotations;

namespace SupremeComponentsExercise2.DTOs
{
    /// <summary>
    /// Data Transfer Object for Product operations
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Category the product belongs to
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Available quantity of the product
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
