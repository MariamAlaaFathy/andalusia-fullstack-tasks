using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Services.Interfaces
{
    public interface IShowTimeService
    {
        public Task<PagedResult<ShowTimeDTO>> GetShowTimes(PaginationParams paginationParams);
        public Task<ShowTimeDTO> GetShowTimeById(int id);
        public Task<List<ShowTimeDTO>> GetShowTimesByAuditorium(int auditoriumId, DateTime? date);
        public Task<ShowTimeDTO> CreateShowTime(CreateShowTimeRequest showTime);
        public Task<ShowTimeDTO> UpdateShowTime(int id, UpdateShowTimeRequest showTime);
        public Task DeleteShowTime(int id);
    }
}
