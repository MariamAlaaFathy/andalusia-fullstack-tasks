using CinemaTicketBookingProject.Data;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaTicketBookingProject.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _dbcontext;

        public MovieRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<PagedResult<Movie>> GetMovies(MovieFilterParams paginationParams)
        {
            var query = _dbcontext.Movies.AsQueryable();

            var totalCount = await query.CountAsync();

            if(!string.IsNullOrEmpty(paginationParams.Search))
            {
                query = query.Where(m => EF.Functions.Like(m.Name, $"%{paginationParams.Search}%"));
            }
            if(!string.IsNullOrEmpty(paginationParams.Genre))
            {
                query = query.Where(m => EF.Functions.Like(m.Genre, $"%{paginationParams.Genre}"));
            }

            var allowedSort = new Dictionary<string, Expression<Func<Movie, object>>>
            {
                ["name"] = m => m.Name,
                ["releasedate"] = m => m.ReleaseDate
            };
            if(allowedSort.TryGetValue(paginationParams.SortBy ?? "name", out var keySelector))
            {
                query = paginationParams.Order == "desc" ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
            }

            IEnumerable<Movie> filteredMovies = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();

            return new PagedResult<Movie>
            {
                Data = filteredMovies,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Movie> GetMovieById(int id)
        {
            if(await _dbcontext.Movies.FindAsync(id) == null)
            {
                throw new MovieNotFoundException("The requested movie could not be found.");
            }
            return await _dbcontext.Movies.Where(m => m.Id == id).SingleAsync();
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            if(!(await _dbcontext.Movies.AnyAsync(m => m.Name == name)))
            {
                throw new MovieNotFoundException("The requested movie could not be found.");
            }
            return await _dbcontext.Movies.Where(m => m.Name == name).SingleAsync();
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            if (await _dbcontext.Movies.AnyAsync(m => m.Name == movie.Name))
            {
                throw new MovieAlreadyExistsException("A movie with the same name already exists.");
            }
            _dbcontext.Movies.Add(movie);
            await _dbcontext.SaveChangesAsync();
            return await GetMovieById(movie.Id);
        }

        public async Task<Movie> UpdateMovie(int id, Movie movie)
        {
            var existingMovie = await _dbcontext.Movies.FindAsync(id);
            if (existingMovie is null)
            {
                throw new MovieNotFoundException("The requested movie could not be found.");
            }
            else if (await GetMovieByName(movie.Name) != null && (await GetMovieByName(movie.Name)).Id != id)
            {
                throw new MovieAlreadyExistsException("A movie with the same name already exists.");
            }
            existingMovie.Name = movie.Name;
            existingMovie.Genre = movie.Genre;
            existingMovie.AvailableInCinema = movie.AvailableInCinema;
            existingMovie.UpdatedAt = DateTime.Now;
            await _dbcontext.SaveChangesAsync();
            return await GetMovieById(id);
        }

        public async Task DeleteMovie(int id)
        {
            if (await _dbcontext.ShowTimes.AnyAsync(s => s.MovieId == id))
            {
                throw new EntityShouldNotBeDeletedException("This movie cannot be deleted due to having scheduled showtimes.");
            }
            _dbcontext.Movies.Remove(await GetMovieById(id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
