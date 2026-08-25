using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Services.Interfaces
{
    public interface IAuditoriumService
    {
        public Task<PagedResult<AuditoriumDTO>> GetAuditoriums(PaginationParams paginationParams);
        public Task<AuditoriumDTO> GetAuditoriumById(int id);
        public Task<AuditoriumDTO> CreateAuditorium(CreateAuditoriumRequest auditorium);
        public Task<AuditoriumDTO> UpdateAuditorium(int id, UpdateAuditoriumRequest auditorium);
        public Task DeleteAuditorium(int id);
    }
}
