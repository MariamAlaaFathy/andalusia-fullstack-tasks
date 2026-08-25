using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<PagedResult<CustomerDTO>> GetCustomers(PaginationParams paginationParams);
        public Task<CustomerDTO> GetCustomerById(int id);
        public Task<CustomerDTO> GetCustomerByEmail(string email);
        public Task<CustomerDTO> CreateCustomer(CreateCustomerRequest customer);
        public Task<CustomerDTO> UpdateCustomer(int id, UpdateCustomerRequest customer);
        public Task DeleteCustomer(int id);
    }
}
