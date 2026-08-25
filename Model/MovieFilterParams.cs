namespace CinemaTicketBookingProject.Model
{
    public class MovieFilterParams : PaginationParams
    {
        public string? Search { get; set; }
        public string? Genre { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; } = "asc";
    }
}
