using AutoMapper;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using CinemaTicketBookingProject.Services.Interfaces;

namespace CinemaTicketBookingProject.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _movieRepository;
        private IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<MovieDTOV1>> GetMoviesV1(MovieFilterParams paginationParams)
        {
            var movies = await _movieRepository.GetMovies(paginationParams);
            return new PagedResult<MovieDTOV1>
            {
                Data = _mapper.Map<List<MovieDTOV1>>(movies.Data),
                Page = movies.Page,
                PageSize = movies.PageSize,
                TotalCount = movies.TotalCount
            };
        }

        public async Task<PagedResult<MovieDTOV2>> GetMoviesV2(MovieFilterParams paginationParams)
        {
            var movies = await _movieRepository.GetMovies(paginationParams);
            return new PagedResult<MovieDTOV2>
            {
                Data = _mapper.Map<List<MovieDTOV2>>(movies.Data),
                Page = movies.Page,
                PageSize = movies.PageSize,
                TotalCount = movies.TotalCount
            };
        }

        public async Task<MovieDTOV1> GetMovieByIdV1(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid movie ID.");
            }
            var movie = await _movieRepository.GetMovieById(id);
            var movieDTO = _mapper.Map<MovieDTOV1>(movie);
            return movieDTO;
        }

        public async Task<MovieDTOV2> GetMovieByIdV2(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid movie ID.");
            }
            var movie = await _movieRepository.GetMovieById(id);
            var movieDTO = _mapper.Map<MovieDTOV2>(movie);
            return movieDTO;
        }

        public async Task<MovieDTOV1> GetMovieByNameV1(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var movie = await _movieRepository.GetMovieByName(name);
            var movieDTO = _mapper.Map<MovieDTOV1>(movie);
            return movieDTO;
        }

        public async Task<MovieDTOV2> GetMovieByNameV2(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var movie = await _movieRepository.GetMovieByName(name);
            var movieDTO = _mapper.Map<MovieDTOV2>(movie);
            return movieDTO;
        }

        public async Task<MovieDTOV2> CreateMovie(CreateMovieRequest movie)
        {
            var mappedMovie = _mapper.Map<Movie>(movie);
            var created = await _movieRepository.CreateMovie(mappedMovie);
            var movieDTO = _mapper.Map<MovieDTOV2>(created);
            return movieDTO;
        }

        public async Task<MovieDTOV2> UpdateMovie(int id, UpdateMovieRequest movie)
        {
            if (id < 0)
            {
                throw new InvalidId("You have provided an invalid movie ID.");
            }
            else if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            var mappedMovie = _mapper.Map<Movie>(movie);
            var existingMovie = await _movieRepository.UpdateMovie(id, mappedMovie);
            var movieDTO = _mapper.Map<MovieDTOV2>(existingMovie);
            return movieDTO;
        }

        public async Task DeleteMovie(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid movie ID.");
            }
            else if (await _movieRepository.GetMovieById(id) == null)
            {
                throw new MovieNotFoundException("The requested movie could not be found.");
            }
            await _movieRepository.DeleteMovie(id);
        }
    }
}
