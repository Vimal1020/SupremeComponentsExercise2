using SupremeComponentsExercise2.Models;
using System.Net;
using System.Text.Json;

namespace SupremeComponentsExercise2.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions globally and return consistent error responses.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">Logger for recording errors.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware, catching any exceptions thrown by downstream components.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware/component
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Unhandled exception occurred");

                // Handle the exception and write a response
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Formats and writes an error response based on exception type.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The caught exception.</param>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Always return JSON
            context.Response.ContentType = "application/json";

            // Prepare error response model
            var response = new ApiErrorResponse();

            // Map exception types to HTTP status codes
            switch (exception)
            {
                case ArgumentNullException _:
                case ArgumentException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = context.Response.StatusCode;
                    response.Message = exception.Message;
                    break;

                case KeyNotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.StatusCode = context.Response.StatusCode;
                    response.Message = exception.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = context.Response.StatusCode;
                    response.Message = "An unexpected error occurred.";
                    break;
            }

            // Serialize and write the JSON response
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
