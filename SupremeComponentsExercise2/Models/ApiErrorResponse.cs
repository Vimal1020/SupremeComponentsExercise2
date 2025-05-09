using Microsoft.AspNetCore.Mvc;

namespace SupremeComponentsExercise2.Models
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
    }
}
