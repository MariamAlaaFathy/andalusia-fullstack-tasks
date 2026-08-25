using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Repositories.Interfaces
{
    public interface IAuditoriumRepository
    {
        public Task<PagedResult<Auditorium>> GetAuditoriums(PaginationParams paginationParams);
        public Task<Auditorium> GetAuditoriumById(int id);
        public Task<Auditorium> CreateAuditorium(Auditorium auditorium);
        public Task<Auditorium> UpdateAuditorium(int id, Auditorium auditorium);
        public Task DeleteAuditorium(int id);
    }
}
