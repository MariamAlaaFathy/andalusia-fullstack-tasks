using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingProject.Controllers
{
    /// <summary>
    /// Manages ticket bookings for guest customers.
    /// </summary>
    [ApiController]
    [Route("api/bookings")]
    [Produces("application/json")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Gets a paginated, filterable list of bookings.
        /// </summary>
        /// <param name="paginationParams">Filter by customerId, customerName, showTimeId, and/or status, plus page/pageSize.</param>
        /// <response code="200">The paginated list of bookings.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<BookingDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<BookingDTO>>> GetBookings([FromQuery] BookingFilterParams paginationParams)
        {
            return Ok(await _bookingService.GetBookings(paginationParams));
        }

        /// <summary>
        /// Gets a single booking by id.
        /// </summary>
        /// <param name="id">The booking id.</param>
        /// <response code="200">The requested booking.</response>
        /// <response code="404">No booking exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int id)
        {
            return Ok(await _bookingService.GetBookingById(id));
        }

        /// <summary>
        /// Gets all bookings for a specific showtime, including customer info.
        /// </summary>
        /// <param name="showTimeid">The showtime id.</param>
        /// <response code="200">The list of bookings for the showtime.</response>
        /// <response code="404">No showtime exists with the given id.</response>
        [HttpGet]
        [Route("showtime={id}")]
        [ProducesResponseType(typeof(List<BookingDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BookingDTO>>> GetBookingsByShowTime(int showTimeid)
        {
            return Ok(await _bookingService.GetBookingsByShowTime(showTimeid));
        }

        /// <summary>
        /// Gets all bookings for a specific customer, including movie/showtime/auditorium details.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <response code="200">The list of bookings for the customer.</response>
        /// <response code="404">No customer exists with the given id.</response>
        [HttpGet]
        [Route("customer={id}")]
        [ProducesResponseType(typeof(List<BookingDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDTO>> GetBookingsByCustomer(int customerId)
        {
            return Ok(await _bookingService.GetBookingsByCustomer(customerId));
        }

        /// <summary>
        /// Creates a new booking for a guest customer. Booking status always starts as Pending.
        /// </summary>
        /// <param name="booking">The showtime to book and the guest's name/email.</param>
        /// <response code="201">The booking was created. The response includes a Location header pointing to it.</response>
        /// <response code="400">The request body failed validation, or the showtime has already passed.</response>
        /// <response code="404">No showtime exists with the given id.</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookingDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDTO>> CreateBooking([FromBody] CreateBookingRequest booking)
        {
            var createdBooking = await _bookingService.CreateBooking(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.Id }, createdBooking);
        }

        /// <summary>
        /// Confirms a pending booking.
        /// </summary>
        /// <param name="id">The booking id.</param>
        /// <response code="200">The confirmed booking.</response>
        /// <response code="400">The booking is already cancelled and cannot be confirmed.</response>
        /// <response code="404">No booking exists with the given id.</response>
        [HttpPut]
        [Route("{id}/confirm")]
        [ProducesResponseType(typeof(BookingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDTO>> ConfirmBooking(int id)
        {
            return Ok(await _bookingService.ConfirmBooking(id));
        }

        /// <summary>
        /// Cancels a booking.
        /// </summary>
        /// <param name="id">The booking id.</param>
        /// <response code="200">The cancelled booking.</response>
        /// <response code="400">The booking is already cancelled.</response>
        /// <response code="404">No booking exists with the given id.</response>
        [HttpPut]
        [Route("{id}/cancel")]
        [ProducesResponseType(typeof(BookingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDTO>> CancelBooking(int id)
        {
            return Ok(await _bookingService.CancelBooking(id));
        }
    }
}
