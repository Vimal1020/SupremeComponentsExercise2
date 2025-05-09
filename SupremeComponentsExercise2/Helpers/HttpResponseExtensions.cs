using SupremeComponentsExercise2.Models;

namespace SupremeComponentsExercise2.Helpers
{
    public static class HttpResponseExtensions
    {
        private const string TotalCount = "X-Pagination-TotalCount";
        private const string PageSize = "X-Pagination-PageSize";
        private const string CurrentPage = "X-Pagination-CurrentPage";
        private const string TotalPages = "X-Pagination-TotalPages";

        /// <summary>
        /// Adds pagination metadata to the HTTP response headers
        /// </summary>
        public static void AddPaginationHeaders<T>(this HttpResponse response, PagedResult<T> result)
        {
            response.Headers[TotalCount] = result.TotalCount.ToString();
            response.Headers[PageSize] = result.PageSize.ToString();
            response.Headers[CurrentPage] = result.PageNumber.ToString();
            response.Headers[TotalPages] = result.TotalPages.ToString();
        }
    }
}