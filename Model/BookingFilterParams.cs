using CinemaTicketBookingProject.Enums;
using System.Net.NetworkInformation;

namespace CinemaTicketBookingProject.Model
{
    public class BookingFilterParams : PaginationParams
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? ShowTimeId { get; set; }
        public BookingStatus? Status { get; set; }
    }
}
