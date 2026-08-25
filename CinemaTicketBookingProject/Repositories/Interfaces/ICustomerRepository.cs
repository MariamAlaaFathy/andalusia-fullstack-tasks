using CinemaTicketBookingProject.Model;

namespace CinemaTicketBookingProject.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<PagedResult<Customer>> GetCustomers(PaginationParams paginationParams);
        public Task<Customer> GetCustomerById(int id);
        public Task<Customer> GetCustomerByEmail(string email);
        public Task<Customer> CreateCustomer(Customer customer);
        public Task<Customer> UpdateCustomer(int id, Customer customer);
        public Task DeleteCustomer(int id);
    }
}
