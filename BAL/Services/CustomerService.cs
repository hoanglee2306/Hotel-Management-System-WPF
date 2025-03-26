using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BAL.Services
{
    public class CustomerService
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
        
        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetByEmail(email);
        }
        
        public IEnumerable<Customer> SearchCustomers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return _customerRepository.GetAll();
                
            return _customerRepository.Find(c => 
                c.CustomerFullName.Contains(searchTerm) || 
                c.EmailAddress.Contains(searchTerm) || 
                c.Telephone.Contains(searchTerm));
        }
        
        public void CreateCustomer(Customer customer)
        {
            // Validate customer data
            if (string.IsNullOrEmpty(customer.CustomerFullName) || 
                string.IsNullOrEmpty(customer.EmailAddress) || 
                string.IsNullOrEmpty(customer.Password))
            {
                throw new ArgumentException("Customer name, email, and password are required.");
            }
            
            // Check if email already exists
            var existingCustomer = _customerRepository.GetByEmail(customer.EmailAddress);
            if (existingCustomer != null)
            {
                throw new ArgumentException("Email address is already in use.");
            }
            
            // Set default values
            customer.CustomerStatus = 1; // Active
            
            // Add customer to repository
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
        }
        
        public void UpdateCustomer(Customer customer)
        {
            // Validate customer data
            if (string.IsNullOrEmpty(customer.CustomerFullName) || 
                string.IsNullOrEmpty(customer.EmailAddress))
            {
                throw new ArgumentException("Customer name and email are required.");
            }
            
            // Check if email already exists for another customer
            var existingCustomer = _customerRepository.GetByEmail(customer.EmailAddress);
            if (existingCustomer != null && existingCustomer.CustomerId != customer.CustomerId)
            {
                throw new ArgumentException("Email address is already in use by another customer.");
            }
            
            // Update customer
            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();
        }
        
        public bool DeleteCustomer(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
                return false;
                
            // Check if customer has any bookings
            var customerWithBookings = _customerRepository.GetCustomerWithBookings(customerId);
            if (customerWithBookings != null && customerWithBookings.BookingReservations.Any())
            {
                // Don't delete, just mark as inactive
                customer.CustomerStatus = 0;
                _customerRepository.Update(customer);
                _customerRepository.SaveChanges();
                return true;
            }
            
            // If no bookings, can safely delete
            _customerRepository.Remove(customer);
            _customerRepository.SaveChanges();
            return true;
        }
    }
}