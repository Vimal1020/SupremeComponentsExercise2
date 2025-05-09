namespace SupremeComponentsExercise2.Models
{
    /// <summary>
    /// Model for consistent API error responses
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// HTTP status code of the error
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Additional error details (stack trace in development)
        /// </summary>
        public string Details { get; set; }
    }
}
