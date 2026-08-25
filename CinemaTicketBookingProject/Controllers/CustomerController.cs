using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingProject.Controllers
{
    /// <summary>
    /// Manages guest customers.
    /// </summary>
    [ApiController]
    [Route("api/customers")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Gets a paginated list of customers.
        /// </summary>
        /// <param name="paginationParams">Page and page size (pageSize capped at 100).</param>
        /// <response code="200">The paginated list of customers.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<CustomerDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<CustomerDTO>>> GetCustomers([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _customerService.GetCustomers(paginationParams));
        }

        /// <summary>
        /// Gets a single customer by id.
        /// </summary>
        /// <param name="id">The customer id.</param>
        /// <response code="200">The requested customer.</response>
        /// <response code="404">No customer exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            return Ok(await _customerService.GetCustomerById(id));
        }

        /// <summary>
        /// Gets a single customer by email address.
        /// </summary>
        /// <param name="email">The customer's email address.</param>
        /// <response code="200">The requested customer.</response>
        /// <response code="404">No customer exists with the given email.</response>
        [HttpGet]
        [Route("email={email}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> GetCustomerByEmail(string email)
        {
            return Ok(await _customerService.GetCustomerByEmail(email));
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer's name and email.</param>
        /// <response code="201">The customer was created. The response includes a Location header pointing to it.</response>
        /// <response code="400">The request body failed validation.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerRequest customer)
        {
            var createdCustomer = await _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);
        }

        /// <summary>
        /// Replaces an existing customer's details.
        /// </summary>
        /// <param name="id">The customer id.</param>
        /// <param name="customer">The full replacement name and email.</param>
        /// <response code="200">The updated customer.</response>
        /// <response code="400">The request body failed validation.</response>
        /// <response code="404">No customer exists with the given id.</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest customer)
        {
            return Ok(await _customerService.UpdateCustomer(id, customer));
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">The customer id.</param>
        /// <response code="204">The customer was deleted.</response>
        /// <response code="404">No customer exists with the given id.</response>
        /// <response code="409">The customer has existing bookings and cannot be deleted.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
