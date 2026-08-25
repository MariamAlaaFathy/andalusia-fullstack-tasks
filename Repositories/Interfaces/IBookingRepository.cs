using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        public Task<PagedResult<Booking>> GetBookings(BookingFilterParams paginationParams);
        public Task<Booking> GetBookingById(int id);
        public Task<List<Booking>> GetBookingsByShowTime(int showTimeId);
        public Task<List<Booking>> GetBookingsByCustomer(int customerId);
        public Task<Booking> CreateBooking(Booking booking);
        public Task<Booking> UpdateBooking(Booking booking);

    }
}
