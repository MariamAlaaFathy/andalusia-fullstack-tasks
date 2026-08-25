namespace CinemaTicketBookingProject.Model
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime Show_Time { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Foreign Keys
        public int MovieId { get; set; }
        public int AuditoriumId { get; set; }

        // Navigation Property
        public Movie Movie { get; set; }
        public Auditorium Auditorium { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
