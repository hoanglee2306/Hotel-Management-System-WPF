using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repository
{
    public class UserRepository : GenericRepository<Customer>
    {
        public UserRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        // Authenticate user
        public Customer Authenticate(string email, string password)
        {
            return _dbSet.FirstOrDefault(u => u.EmailAddress.Equals(email) && u.Password.Equals(password));
        }

        // Get user by email
        public Customer GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u => u.EmailAddress.Equals(email));
        }

        // Check if email exists
        public bool EmailExists(string email)
        {
            return _dbSet.Any(u => u.EmailAddress.Equals(email));
        }

        // Get users with bookings
        public IEnumerable<Customer> GetUsersWithBookings()
        {
            return _dbSet
                .Include(u => u.BookingReservations)
                .Where(u => u.BookingReservations.Any())
                .ToList();
        }
    }
}
