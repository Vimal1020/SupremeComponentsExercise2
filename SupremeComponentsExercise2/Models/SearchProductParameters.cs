namespace SupremeComponentsExercise2.Models
{
    /// <summary>
    /// Defines the set of optional filtering criteria when searching for products.
    /// </summary>
    public class SearchProductParameters
    {
        /// <summary>
        /// Gets or sets an optional category name to filter products by.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the optional minimum price.  
        /// Only products with Price &gt;= MinPrice will be returned.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Gets or sets the optional maximum price.  
        /// Only products with Price &lt;= MaxPrice will be returned.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Gets or sets an optional search term to match against product names.
        /// The search should be case-insensitive and may match partial names.
        /// </summary>
        public string? SearchTerm { get; set; }
    }
}