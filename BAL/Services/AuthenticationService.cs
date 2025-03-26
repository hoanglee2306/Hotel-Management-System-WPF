using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BAL.Services
{
    public class AuthenticationService
    {
        private readonly CustomerRepository _customerRepository;

        public AuthenticationService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var customer = _customerRepository.GetByEmail(email);
            
            // Check if customer exists, has valid status and password matches
            if (customer != null && customer.CustomerStatus == 1 && customer.Password == password)
                return customer;
                
            return null;
        }
        
        public bool IsAdmin(Customer customer)
        {
            // Assuming CustomerType 1 represents Admin
            return customer != null && customer.CustomerType == 1;
        }
        
        public bool IsCustomer(Customer customer)
        {
            // Assuming CustomerType 2 represents regular Customer
            return customer != null && customer.CustomerType == 2;
        }
    }
}