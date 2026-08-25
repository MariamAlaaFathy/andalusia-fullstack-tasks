using AutoMapper;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Enums;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using CinemaTicketBookingProject.Services.Interfaces;

namespace CinemaTicketBookingProject.Services
{
    public class BookingService : IBookingService
    {
        private IBookingRepository _bookingRepository;
        private IShowTimeRepository _showTimeRepository;
        private ICustomerRepository _customerRepository;
        private IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IShowTimeRepository showTimeRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _showTimeRepository = showTimeRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookingDTO>> GetBookings(BookingFilterParams paginationParams)
        {
            var auditoriums = await _bookingRepository.GetBookings(paginationParams);
            return new PagedResult<BookingDTO>
            {
                Data = _mapper.Map<List<BookingDTO>>(auditoriums.Data),
                Page = auditoriums.Page,
                PageSize = auditoriums.PageSize,
                TotalCount = auditoriums.TotalCount
            };
        }

        public async Task<BookingDTO> GetBookingById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid booking ID.");
            }
            var booking = await _bookingRepository.GetBookingById(id);
            var bookingDTO = _mapper.Map<BookingDTO>(booking);
            return bookingDTO;
        }

        public async Task<List<BookingDTO>> GetBookingsByShowTime(int showTimeId)
        {
            if (showTimeId <= 0)
            {
                throw new InvalidId("You have provided an invalid showtime ID.");
            }
            var booking = await _bookingRepository.GetBookingsByShowTime(showTimeId);
            var bookingDTO = _mapper.Map<List<BookingDTO>>(booking);
            return bookingDTO;
        }

        public async Task<List<BookingDTO>> GetBookingsByCustomer(int customerId)
        {
            if (customerId <= 0)
            {
                throw new InvalidId("You have provided an invalid customer ID.");
            }
            var booking = await _bookingRepository.GetBookingsByShowTime(customerId);
            var bookingDTO = _mapper.Map<List<BookingDTO>>(booking);
            return bookingDTO;
        }

        public async Task<BookingDTO> CreateBooking(CreateBookingRequest booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            var mappedBooking = _mapper.Map<Booking>(booking);
            mappedBooking.ShowTime = await _showTimeRepository.GetShowTimeById(mappedBooking.ShowTimeId);
            mappedBooking.Customer = await _customerRepository.GetCustomerById(mappedBooking.CustomerId);
            var created = await _bookingRepository.CreateBooking(mappedBooking);
            var bookingDTO = _mapper.Map<BookingDTO>(created);
            return bookingDTO;
        }

        public async Task<BookingDTO> ConfirmBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                throw new InvalidId("You have provided an invalid booking ID.");
            }
            var booking = await _bookingRepository.GetBookingById(bookingId);

            if (booking.Status == BookingStatus.Cancelled)
            {
                throw new InvalidBookingException("Cannot confirm a cancelled booking.");
            }

            booking.Status = BookingStatus.Confirmed;
            booking.UpdatedAt = DateTime.Now;

            var updated = await _bookingRepository.UpdateBooking(booking);
            var bookingDTO = _mapper.Map<BookingDTO>(updated);
            return bookingDTO;
        }

        public async Task<BookingDTO> CancelBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                throw new InvalidId("You have provided an invalid booking ID.");
            }
            var booking = await _bookingRepository.GetBookingById(bookingId);

            if (booking.Status == BookingStatus.Cancelled)
            {
                throw new InvalidBookingException("This booking is already cancelled.");
            }

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.Now;

            var updated = await _bookingRepository.UpdateBooking(booking);
            var bookingDTO = _mapper.Map<BookingDTO>(updated);
            return bookingDTO;
        }
        
    }
}
