using BAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void AddCustomer(Customer customer)
        {
            // Set default status to active
            customer.CustomerStatus = 1;
            
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();
        }

        public bool DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetCustomerWithBookings(id);
            
            if (customer == null)
                return false;

            // If customer has bookings, just deactivate instead of deleting
            if (customer.BookingReservations.Any())
            {
                customer.CustomerStatus = 0; // Deactivate
                _customerRepository.Update(customer);
                _customerRepository.SaveChanges();
                return true;
            }
            
            // Otherwise, delete the customer
            _customerRepository.Remove(customer);
            _customerRepository.SaveChanges();
            return true;
        }

        public IEnumerable<Customer> SearchCustomers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllCustomers();

            searchTerm = searchTerm.ToLower();
            
            return _customerRepository.GetAll()
                .Where(c => 
                    c.CustomerFullName.ToLower().Contains(searchTerm) ||
                    c.EmailAddress.ToLower().Contains(searchTerm) ||
                    (c.Telephone != null && c.Telephone.Contains(searchTerm)))
                .ToList();
        }

        public void UpdateCustomerProfile(Customer customer)
        {
            // Get a reference to the existing entity
            var existingCustomer = _customerRepository.GetById(customer.CustomerId);
            if (existingCustomer == null)
                throw new Exception("Customer not found");

            // Update properties manually
            existingCustomer.CustomerFullName = customer.CustomerFullName;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.Telephone = customer.Telephone;
            existingCustomer.CustomerBirthday = customer.CustomerBirthday;
            existingCustomer.CustomerStatus = customer.CustomerStatus;
            existingCustomer.CustomerType = customer.CustomerType;

            // Only update password if provided
            if (!string.IsNullOrEmpty(customer.Password))
            {
                existingCustomer.Password = customer.Password;
            }

            // No need to call Update() since we're modifying a tracked entity
            _customerRepository.SaveChanges();
        }
    }
}