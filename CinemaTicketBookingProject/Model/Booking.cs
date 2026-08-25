using CinemaTicketBookingProject.Enums;

namespace CinemaTicketBookingProject.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        
        // Foreign Keys
        public int CustomerId { get; set; }
        public int ShowTimeId { get; set; }

        // Navigation Property
        public Customer Customer { get; set; }
        public ShowTime ShowTime { get; set; }
    }
}
