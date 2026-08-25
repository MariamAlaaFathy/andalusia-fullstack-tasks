namespace CinemaTicketBookingProject.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool AvailableInCinema { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
    }
}
