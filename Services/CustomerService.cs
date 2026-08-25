using AutoMapper;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Exceptions;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Repositories;
using CinemaTicketBookingProject.Repositories.Interfaces;
using CinemaTicketBookingProject.Services.Interfaces;

namespace CinemaTicketBookingProject.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CustomerDTO>> GetCustomers(PaginationParams paginationParams)
        {
            var customers = await _customerRepository.GetCustomers(paginationParams);
            return new PagedResult<CustomerDTO>
            {
                Data = _mapper.Map<List<CustomerDTO>>(customers.Data),
                Page = customers.Page,
                PageSize = customers.PageSize,
                TotalCount = customers.TotalCount
            };
        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid customer ID.");
            }
            var customer = await _customerRepository.GetCustomerById(id);
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public async Task<CustomerDTO> GetCustomerByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            var customer = await _customerRepository.GetCustomerByEmail(email);
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public async Task<CustomerDTO> CreateCustomer(CreateCustomerRequest customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var mappedCustomer = _mapper.Map<Customer>(customer);
            var created = await _customerRepository.CreateCustomer(mappedCustomer);
            var customerDTO = _mapper.Map<CustomerDTO>(created);
            return customerDTO;
        }

        public async Task<CustomerDTO> UpdateCustomer(int id, UpdateCustomerRequest customer)
        {
            var existingCustomer = await GetCustomerById(id);
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid auditorium ID.");
            }
            else if (existingCustomer == null)
            {
                throw new ArgumentNullException(nameof(existingCustomer));
            }

            var mappedCustomer = _mapper.Map<Customer>(existingCustomer);
            mappedCustomer.UpdatedAt = DateTime.Now;
            var updatedCustomer = await _customerRepository.UpdateCustomer(id, mappedCustomer);
            var customerDTO = _mapper.Map<CustomerDTO>(updatedCustomer);
            return customerDTO;
        }

        public async Task DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                throw new InvalidId("You have provided an invalid customer ID.");
            }
            else if (await _customerRepository.GetCustomerById(id) == null)
            {
                throw new CustomerNotFoundException("The requested customer could not be found.");
            }
            await _customerRepository.DeleteCustomer(id);
        }
    }
}
