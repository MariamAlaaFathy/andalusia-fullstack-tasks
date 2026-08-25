using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Services.Interfaces
{
    public interface IBookingService
    {
        public Task<PagedResult<BookingDTO>> GetBookings(BookingFilterParams paginationParams);
        public Task<BookingDTO> GetBookingById(int id);
        public Task<List<BookingDTO>> GetBookingsByShowTime(int showTimeId);
        public Task<List<BookingDTO>> GetBookingsByCustomer(int customerId);
        public Task<BookingDTO> CreateBooking(CreateBookingRequest booking);
        public Task<BookingDTO> ConfirmBooking(int bookingId);
        public Task<BookingDTO> CancelBooking(int bookingId);
    }
}
