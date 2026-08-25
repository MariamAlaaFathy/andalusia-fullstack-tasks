using AutoMapper;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using CinemaTicketBookingProject.Services.Interfaces;

namespace CinemaTicketBookingProject.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private IShowTimeRepository _showTimeRepository;
        private IMovieRepository _movieRepository;
        private IAuditoriumRepository _auditoriumRepository;
        private IMapper _mapper;

        public ShowTimeService(IShowTimeRepository showTimeRepository, IMovieRepository movieRepository, IAuditoriumRepository auditoriumRepository ,IMapper mapper)
        {
            _showTimeRepository = showTimeRepository;
            _movieRepository = movieRepository;
            _auditoriumRepository = auditoriumRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShowTimeDTO>> GetShowTimes(PaginationParams paginationParams)
        {
            var showTimes = await _showTimeRepository.GetShowTimes(paginationParams);
            return new PagedResult<ShowTimeDTO>
            {
                Data = _mapper.Map<List<ShowTimeDTO>>(showTimes.Data),
                Page = showTimes.Page,
                PageSize = showTimes.PageSize,
                TotalCount = showTimes.TotalCount
            };
        }

        public async Task<ShowTimeDTO> GetShowTimeById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid showtime ID.");
            }
            var showTime = await _showTimeRepository.GetShowTimeById(id);
            var showTimeDTO = _mapper.Map<ShowTimeDTO>(showTime);
            return showTimeDTO;
        }

        public async Task<List<ShowTimeDTO>> GetShowTimesByAuditorium(int auditoriumId, DateTime? date)
        {
            if (auditoriumId <= 0)
            {
                throw new InvalidId("You have provided an invalid auditorium ID.");
            }
            else if (date < DateTime.Now)
            {
                throw new InvalidId("You have provided an invalid date.");
            }
            var showTimes = await _showTimeRepository.GetShowTimesByAuditorium(auditoriumId, date);
            var showTimesDTO = _mapper.Map<List<ShowTimeDTO>>(showTimes);
            return showTimesDTO;
        }

        public async Task<ShowTimeDTO> CreateShowTime(CreateShowTimeRequest showTime)
        {
            if (showTime == null)
            {
                throw new ArgumentNullException(nameof(showTime));
            }
            var mappedShowTime = _mapper.Map<ShowTime>(showTime);
            mappedShowTime.Movie = await _movieRepository.GetMovieById(mappedShowTime.MovieId);
            mappedShowTime.Auditorium = await _auditoriumRepository.GetAuditoriumById(mappedShowTime.AuditoriumId);
            var created = await _showTimeRepository.CreateShowTime(mappedShowTime);
            var showTimesDTO = _mapper.Map<ShowTimeDTO>(created);
            return showTimesDTO;
        }

        public async Task<ShowTimeDTO> UpdateShowTime(int id, UpdateShowTimeRequest showTime)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid showtime ID.");
            }
            else if (showTime == null)
            {
                throw new ArgumentNullException(nameof(showTime));
            }
            var mappedShowTime = _mapper.Map<ShowTime>(showTime);
            mappedShowTime.Movie = await _movieRepository.GetMovieById(mappedShowTime.MovieId);
            mappedShowTime.Auditorium = await _auditoriumRepository.GetAuditoriumById(mappedShowTime.AuditoriumId);
            var created = await _showTimeRepository.UpdateShowTime(id, mappedShowTime);
            var showTimesDTO = _mapper.Map<ShowTimeDTO>(created);
            return showTimesDTO;
        }

        public async Task DeleteShowTime(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid showtime ID.");
            }
            else if (await _showTimeRepository.GetShowTimeById(id) == null)
            {
                throw new ShowTimeNotFoundException("The requested showtime could not be found.");
            }
            await _showTimeRepository.DeleteShowTime(id);
        }
    }
}
