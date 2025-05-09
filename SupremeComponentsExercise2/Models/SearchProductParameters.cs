namespace SupremeComponentsExercise2.Models
{
    public class SearchProductParameters
    {
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SearchTerm { get; set; }
    }
}
