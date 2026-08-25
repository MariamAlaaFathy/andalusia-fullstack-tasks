using CinemaTicketBookingProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBookingProject.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public DateTime Show_Time { get; set; }
        public string MovieName { get; set; }
        public string RoomNumber { get; set; }
    }

    public class CreateBookingRequest
    {
        [Required(ErrorMessage = "BookingDate is required")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "ShowTimeId is required")]
        public int ShowTimeId { get; set; }

        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
    }
}
