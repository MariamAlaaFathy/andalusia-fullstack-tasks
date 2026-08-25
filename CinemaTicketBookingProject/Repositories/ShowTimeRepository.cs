using CinemaTicketBookingProject.Data;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingProject.Repositories
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly AppDbContext _dbcontext;
        private IQueryable<ShowTime> showTimesQuery;

        public ShowTimeRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            showTimesQuery = _dbcontext.ShowTimes
                .Include(b => b.Movie)
                .Include(b => b.Auditorium);
        }

        public async Task<PagedResult<ShowTime>> GetShowTimes(PaginationParams paginationParams)
        {
            var query = showTimesQuery;

            var totalCount = await query.CountAsync();

            IEnumerable<ShowTime> filteredAuditorium = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();

            return new PagedResult<ShowTime>
            {
                Data = filteredAuditorium,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ShowTime> GetShowTimeById(int id)
        {
            if (await _dbcontext.ShowTimes.FindAsync(id) == null)
            {
                throw new ShowTimeNotFoundException("The requested showtime could not be found.");
            }
            return await showTimesQuery.Where(s => s.Id == id).SingleAsync();
        }

        public async Task<List<ShowTime>> GetShowTimesByAuditorium(int auditoriumId, DateTime? date)
        {
            if (await _dbcontext.Auditoriums.FindAsync(auditoriumId) == null)
            {
                throw new AuditoriumNotFoundException("The requested auditorium could not be found.");
            }
            if (await _dbcontext.ShowTimes.AnyAsync(b => b.AuditoriumId != auditoriumId))
            {
                throw new ShowTimeNotFoundException("The requested showtime could not be found.");
            }
            return await showTimesQuery.Where(b => b.AuditoriumId == auditoriumId).ToListAsync();
        }
        public async Task<ShowTime> CreateShowTime(ShowTime showTime)
        {
            _dbcontext.ShowTimes.Add(showTime);
            await _dbcontext.SaveChangesAsync();
            return await GetShowTimeById(showTime.Id);
        }

        public async Task<ShowTime> UpdateShowTime(int id, ShowTime showTime)
        {
            await _dbcontext.SaveChangesAsync();
            return await GetShowTimeById(id);
        }

        public async Task DeleteShowTime(int id)
        {
            if (await _dbcontext.Bookings.AnyAsync(b => b.ShowTimeId == id))
            {
                throw new EntityShouldNotBeDeletedException("This showtime cannot be deleted due to having scheduled bookings.");
            }
            _dbcontext.ShowTimes.Remove(await GetShowTimeById(id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
