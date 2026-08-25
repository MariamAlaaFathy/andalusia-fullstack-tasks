using CinemaTicketBookingProject.Data;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingProject.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbcontext;
        private IQueryable<Booking> bookingsQuery;

        public BookingRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            bookingsQuery = _dbcontext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.ShowTime).ThenInclude(s => s!.Movie)
                .Include(b => b.ShowTime).ThenInclude(s => s!.Auditorium);
        }

        public async Task<PagedResult<Booking>> GetBookings(BookingFilterParams paginationParams)
        {
            var query = bookingsQuery;

            var totalCount = await query.CountAsync();

            if (paginationParams.CustomerId.HasValue)
            {
                query = query.Where(b => b.CustomerId == paginationParams.CustomerId);
            }
            if (!string.IsNullOrEmpty(paginationParams.CustomerName)) 
            {
                query = query.Where(b => b.Customer.Name == paginationParams.CustomerName);
            }
            if (paginationParams.ShowTimeId.HasValue)
            {
                query = query.Where(b => b.ShowTimeId == paginationParams.ShowTimeId);
            }
            if (paginationParams.Status.HasValue)
            {
                query = query.Where(b => b.Status == paginationParams.Status);
            }

            IEnumerable<Booking> filteredBookings = await query
                .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();
            return new PagedResult<Booking>
            {
                Data = filteredBookings,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Booking> GetBookingById(int id)
        {
            if (await _dbcontext.Bookings.FindAsync(id) == null)
            {
                throw new BookingNotFoundException("The requested movie could not be found.");
            }
            return await bookingsQuery.Where(b => b.Id == id).SingleAsync();
        }

        public async Task<List<Booking>> GetBookingsByShowTime(int showTimeId)
        {
            if (await _dbcontext.ShowTimes.FindAsync(showTimeId) == null)
            {
                throw new ShowTimeNotFoundException("The requested showtime could not be found.");
            }
            if (await _dbcontext.Bookings.AnyAsync(b => b.ShowTimeId != showTimeId))
            {
                throw new BookingNotFoundException("The requested booking could not be found.");
            }
            return await bookingsQuery.Where(b => b.ShowTimeId == showTimeId).ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByCustomer(int customerId)
        {
            if (await _dbcontext.Customers.FindAsync(customerId) == null)
            {
                throw new CustomerNotFoundException("The requested customer could not be found.");
            }
            if (await _dbcontext.Bookings.AnyAsync(b => b.CustomerId != customerId))
            {
                throw new BookingNotFoundException("The requested booking could not be found.");
            }
            return await bookingsQuery.Where(b => b.CustomerId == customerId).ToListAsync();
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            _dbcontext.Bookings.Add(booking);
            await _dbcontext.SaveChangesAsync();
            return await GetBookingById(booking.Id);
        }

        public async Task<Booking> UpdateBooking(Booking booking)
        {
            await _dbcontext.SaveChangesAsync();
            return (await GetBookingById(booking.Id))!;
        }
    }
}
