using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        // Get customer by email
        public Customer GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(c => c.EmailAddress.Equals(email));
        }

        // Get customer with bookings
        public Customer GetCustomerWithBookings(int customerId)
        {
            return _dbSet
                .Include(c => c.BookingReservations)
                .FirstOrDefault(c => c.CustomerId == customerId);
        }

        // Authenticate customer
        public Customer Authenticate(string email, string password)
        {
            return _dbSet.FirstOrDefault(c => c.EmailAddress.Equals(email) && c.Password.Equals(password));
        }
        
        // Get active customers
        public IEnumerable<Customer> GetActiveCustomers()
        {
            return _dbSet.Where(c => c.CustomerStatus == 1).ToList();
        }
    }
}