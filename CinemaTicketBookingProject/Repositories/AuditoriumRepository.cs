using CinemaTicketBookingProject.Data;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingProject.Repositories
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly AppDbContext _dbcontext;

        public AuditoriumRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PagedResult<Auditorium>> GetAuditoriums(PaginationParams paginationParams)
        {
            var query = _dbcontext.Auditoriums.AsQueryable();

            var totalCount = await query.CountAsync();

            IEnumerable<Auditorium> filteredAuditorium = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();

            return new PagedResult<Auditorium>
            {
                Data = filteredAuditorium,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };

        }

        public async Task<Auditorium> GetAuditoriumById(int id)
        {
            if (await _dbcontext.Auditoriums.FindAsync(id) == null)
            {
                throw new AuditoriumNotFoundException("The requested auditorium could not be found.");
            }
            return await _dbcontext.Auditoriums.Where(a => a.Id == id).SingleAsync();
        }
        public async Task<Auditorium> CreateAuditorium(Auditorium auditorium)
        {
            _dbcontext.Auditoriums.Add(auditorium);
            await _dbcontext.SaveChangesAsync();
            return await GetAuditoriumById(auditorium.Id);
        }

        public async Task<Auditorium> UpdateAuditorium(int id, Auditorium auditorium)
        {
            await _dbcontext.SaveChangesAsync();
            return await GetAuditoriumById(id);
        }

        public async Task DeleteAuditorium(int id)
        {
            if (await _dbcontext.ShowTimes.AnyAsync(s => s.AuditoriumId == id))
            {
                throw new EntityShouldNotBeDeletedException("This auditorium cannot be deleted due to having scheduled showtimes.");
            }
            _dbcontext.Auditoriums.Remove(await GetAuditoriumById(id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
