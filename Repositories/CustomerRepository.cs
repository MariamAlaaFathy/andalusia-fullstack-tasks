using CinemaTicketBookingProject.Data;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingProject.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbcontext;

        public CustomerRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PagedResult<Customer>> GetCustomers(PaginationParams paginationParams)
        {
            var query = _dbcontext.Customers.AsQueryable();

            var totalCount = await query.CountAsync();

            IEnumerable<Customer> filteredAuditorium = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();

            return new PagedResult<Customer>
            {
                Data = filteredAuditorium,
                Page = paginationParams.Page,
                PageSize = paginationParams.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            if (await _dbcontext.Customers.FindAsync(id) == null)
            {
                throw new CustomerNotFoundException("The requested customer could not be found.");
            }
            return await _dbcontext.Customers.Where(c => c.Id == id).SingleAsync();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            if (!(await _dbcontext.Customers.AnyAsync(c => c.Email == email)))
            {
                throw new CustomerNotFoundException("The requested customer could not be found.");
            }
            return await _dbcontext.Customers.Where(c => c.Email == email).SingleAsync();
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if (await _dbcontext.Customers.AnyAsync(c => c.Email == customer.Email))
            {
                throw new CustomerAlreadyExistsException("A customer already exists with that email.");
            }
            _dbcontext.Customers.Add(customer);
            await _dbcontext.SaveChangesAsync();
            return await GetCustomerById(customer.Id);
        }

        public async Task<Customer> UpdateCustomer(int id, Customer customer)
        {
            await _dbcontext.SaveChangesAsync();
            return await GetCustomerById(id);
        }

        public async Task DeleteCustomer(int id)
        {
            _dbcontext.Customers.Remove(await GetCustomerById(id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
