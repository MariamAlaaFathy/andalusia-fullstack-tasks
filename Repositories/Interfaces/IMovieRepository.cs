using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        public Task<PagedResult<Movie>> GetMovies(MovieFilterParams paginationParams);
        public Task<Movie> GetMovieById(int id);
        public Task<Movie> GetMovieByName(string name);
        public Task<Movie> CreateMovie(Movie movie);
        public Task<Movie> UpdateMovie(int id, Movie movie);
        public Task DeleteMovie(int id);
    }
}
