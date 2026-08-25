using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Repositories.Interfaces
{
    public interface IShowTimeRepository
    {
        public Task<PagedResult<ShowTime>> GetShowTimes(PaginationParams paginationParams);
        public Task<ShowTime> GetShowTimeById(int id);
        public Task<List<ShowTime>> GetShowTimesByAuditorium(int auditoriumId, DateTime? date);
        public Task<ShowTime> CreateShowTime(ShowTime showTime);
        public Task<ShowTime> UpdateShowTime(int id, ShowTime showTime);
        public Task DeleteShowTime(int id);
    }
}
