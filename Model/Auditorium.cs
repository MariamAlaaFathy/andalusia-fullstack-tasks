namespace CinemaTicketBookingProject.Model
{
    public class Auditorium
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<ShowTime> Shows { get; set; } = new List<ShowTime>();
    }
}
