namespace TaskSeven.Model
{
    public class TaskFilterParams : PaginationParams
    {
        public string? Search { get; set; }
        public bool? IsCompleted { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; } = "asc";
    }
}
