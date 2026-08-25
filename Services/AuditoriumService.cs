using AutoMapper;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using CinemaTicketBookingProject.Services.Interfaces;

namespace CinemaTicketBookingProject.Services
{
    public class AuditoriumService : IAuditoriumService
    {
        private IAuditoriumRepository _auditoriumRepository;
        private IMapper _mapper;

        public AuditoriumService(IAuditoriumRepository auditoriumRepository, IMapper mapper)
        {
            _auditoriumRepository = auditoriumRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AuditoriumDTO>> GetAuditoriums(PaginationParams paginationParams)
        {
            var auditoriums = await _auditoriumRepository.GetAuditoriums(paginationParams);
            return new PagedResult<AuditoriumDTO>
            {
                Data = _mapper.Map<List<AuditoriumDTO>>(auditoriums.Data),
                Page = auditoriums.Page,
                PageSize = auditoriums.PageSize,
                TotalCount = auditoriums.TotalCount
            };
        }

        public async Task<AuditoriumDTO> GetAuditoriumById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid auditorium ID.");
            }
            var auditorium = await _auditoriumRepository.GetAuditoriumById(id);
            var auditoriumDTO = _mapper.Map<AuditoriumDTO>(auditorium);
            return auditoriumDTO;
        }

        public async Task<AuditoriumDTO> CreateAuditorium(CreateAuditoriumRequest auditorium)
        {
            if (auditorium == null)
            {
                throw new ArgumentNullException(nameof(auditorium));
            }
            var mappedAuditorium = _mapper.Map<Auditorium>(auditorium);
            var created = await _auditoriumRepository.CreateAuditorium(mappedAuditorium);
            var auditoriumDTO = _mapper.Map<AuditoriumDTO>(created);
            return auditoriumDTO;
        }

        public async Task<AuditoriumDTO> UpdateAuditorium(int id, UpdateAuditoriumRequest auditorium)
        {
            var existingAuditorium = await GetAuditoriumById(id);
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid auditorium ID.");
            }
            else if (existingAuditorium == null)
            {
                throw new ArgumentNullException(nameof(existingAuditorium));
            }

            var mappedAuditorium = _mapper.Map<Auditorium>(existingAuditorium);
            mappedAuditorium.UpdatedAt = DateTime.Now;
            var updatedAuditorium = await _auditoriumRepository.UpdateAuditorium(id, mappedAuditorium);
            var auditoriumDTO = _mapper.Map<AuditoriumDTO>(updatedAuditorium);
            return auditoriumDTO;
        }

        public async Task DeleteAuditorium(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid auditorium ID.");
            }
            else if (await _auditoriumRepository.GetAuditoriumById(id) == null)
            {
                throw new AuditoriumNotFoundException("The requested auditorium could not be found.");
            }
            await _auditoriumRepository.DeleteAuditorium(id);
        }
    }
}
