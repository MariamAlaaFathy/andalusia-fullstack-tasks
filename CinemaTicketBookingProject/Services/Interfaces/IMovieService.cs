using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<PagedResult<MovieDTOV1>> GetMoviesV1(MovieFilterParams paginationParams);
        public Task<PagedResult<MovieDTOV2>> GetMoviesV2(MovieFilterParams paginationParams);
        public Task<MovieDTOV1> GetMovieByIdV1(int id);
        public Task<MovieDTOV2> GetMovieByIdV2(int id);
        public Task<MovieDTOV1> GetMovieByNameV1(string name);
        public Task<MovieDTOV2> GetMovieByNameV2(string name);
        public Task<MovieDTOV2> CreateMovie(CreateMovieRequest movie);
        public Task<MovieDTOV2> UpdateMovie(int id, UpdateMovieRequest movie);
        public Task DeleteMovie(int id);
    }
}
