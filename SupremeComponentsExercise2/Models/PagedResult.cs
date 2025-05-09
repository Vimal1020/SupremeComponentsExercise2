using Microsoft.AspNetCore.Mvc;

namespace SupremeComponentsExercise2.Models
{
    /// <summary>
    /// Represents a paginated result set
    /// </summary>
    public class PagedResult<T>
    {
        /// <summary>
        /// The items for the current page
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Total number of matching items
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}