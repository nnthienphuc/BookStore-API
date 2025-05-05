using BookStoreAPI.Services.CustomerSevice.DTOs;
using BookStoreAPI.Services.CustomerSevice.Entities;
using BookStoreAPI.Services.CustomerSevice.Interfaces;
using BookStoreAPI.Services.CustomerSevice.Repositories;

namespace BookStoreAPI.Services.CustomerSevice
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService (ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FamilyName = c.FamilyName,
                GivenName = c.GivenName,
                DateOfBirth = c.DateOfBirth,
                Address = c.Address,
                Phone = c.Phone,
                Gender = c.Gender,
                IsDeleted = c.IsDeleted
            });
        }

        public async Task<CustomerDTO?> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with id '{id}' not found.");

            return new CustomerDTO
            {
                Id = customer.Id,
                FamilyName = customer.FamilyName,
                GivenName = customer.GivenName,
                DateOfBirth = customer.DateOfBirth,
                Address = customer.Address,
                Phone = customer.Phone,
                Gender = customer.Gender,
                IsDeleted = customer.IsDeleted
            };
        }

        public async Task<CustomerDTO?> GetByPhoneAsync(string phone)
        {
            var customer = await _customerRepository.GetByPhoneAsync(phone);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with phone '{phone}' not found.");

            return new CustomerDTO
            {
                Id = customer.Id,
                FamilyName = customer.FamilyName,
                GivenName = customer.GivenName,
                DateOfBirth = customer.DateOfBirth,
                Address = customer.Address,
                Phone = customer.Phone,
                Gender = customer.Gender,
                IsDeleted = customer.IsDeleted
            };
        }

        public async Task<IEnumerable<CustomerDTO>> SearchByKeywordAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Keyword cannot be null or empty");

            var customers = await _customerRepository.SearchByKeywordAsync(keyword);

            return customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FamilyName = c.FamilyName,
                GivenName = c.GivenName,
                DateOfBirth = c.DateOfBirth,
                Address = c.Address,
                Phone = c.Phone,
                Gender = c.Gender,
                IsDeleted = c.IsDeleted
            });
        }

        public async Task<bool> AddAsync(CustomerCreateDTO customerCreateDTO)
        {
            if (string.IsNullOrWhiteSpace(customerCreateDTO.FamilyName))
                throw new ArgumentException("FamilyName cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(customerCreateDTO.GivenName))
                throw new ArgumentException("GivenName cannot be null or empty.");
            if (!IsOver18(customerCreateDTO.DateOfBirth))
                throw new ArgumentException("Customer have to equal or over 18.");
            if (string.IsNullOrWhiteSpace(customerCreateDTO.Address))
                throw new ArgumentException("Address cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(customerCreateDTO.Phone))
                throw new ArgumentException("Phone cannot be null or empty.");

            var existingCustomer = await _customerRepository.GetByPhoneAsync(customerCreateDTO.Phone);
            if (existingCustomer != null)
                throw new InvalidOperationException("A customer with the same phone already exists.");

            var customer = new Customer
            {

            }
        }

        public bool IsOver18(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-age))
                age--;

            return age >= 18;
        }

    }
}
