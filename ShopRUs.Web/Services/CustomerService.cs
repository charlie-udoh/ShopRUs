using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Web.Interfaces;
using ShopRUs.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            var customers = new List<CustomerDTO>();
            var records = await _customerRepository.GetAll();
            foreach (var rec in records)
            {
                customers.Add(new CustomerDTO
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Address = rec.Address,
                    Email = rec.Email,
                    PhoneNumber = rec.PhoneNumber,
                    Role = rec.Role,
                    DateRegistered = rec.DateRegistered
                });
            }
            return customers;
        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            var customer = new CustomerDTO();
            var record = await _customerRepository.GetById(id);
            if (record == null)
                return null;
            customer.Id = record.Id;
            customer.Name = record.Name;
            customer.Address = record.Address;
            customer.Email = record.Email;
            customer.PhoneNumber = record.PhoneNumber;
            customer.Role = record.Role;
            customer.DateRegistered = record.DateRegistered;
            return customer;
        }

        public async Task<CustomerDTO> GetCustomerByName(string name)
        {
            var customer = new CustomerDTO();
            var record = await _customerRepository.GetByName(name);
            if (record == null)
                return null;
            customer.Id = record.Id;
            customer.Name = record.Name;
            customer.Address = record.Address;
            customer.Email = record.Email;
            customer.PhoneNumber = record.PhoneNumber;
            customer.Role = record.Role;
            customer.DateRegistered = record.DateRegistered;
            return customer;
        }

        public async Task<CustomerDTO> CreateCustomer(CustomerDTO customer)
        {
            var newCustomer = new Customer
            {
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Role = customer.Role,
                DateRegistered = DateTime.Now
            };
            await _unitOfWork.CustomerRepository.Insert(newCustomer);            
            _unitOfWork.Commit();
            customer.Id = newCustomer.Id;
            return customer;
        }
    }
}
