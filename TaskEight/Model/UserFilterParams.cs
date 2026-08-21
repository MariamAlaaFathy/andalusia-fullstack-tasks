namespace TaskEight.Model
{
    public class UserFilterParams : PaginationParams
    {
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; } = "asc";
    }
}
