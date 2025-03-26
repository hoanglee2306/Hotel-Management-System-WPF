using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class BookingReservationRepository : GenericRepository<BookingReservation>
    {
        public BookingReservationRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        // Get booking with customer and details
        public BookingReservation GetBookingWithDetails(int bookingId)
        {
            return _dbSet
                .Include(b => b.Customer)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .FirstOrDefault(b => b.BookingReservationId == bookingId);
        }

        // Get bookings by customer
        public IEnumerable<BookingReservation> GetBookingsByCustomer(int customerId)
        {
            return _dbSet
                .Include(b => b.BookingDetails)
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        // Get bookings by date range
        public IEnumerable<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbSet
                .Include(b => b.Customer)
                .Include(b => b.BookingDetails)
                .Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate)
                .OrderBy(b => b.BookingDate)
                .ToList();
        }
        
        // Get recent bookings
        public IEnumerable<BookingReservation> GetRecentBookings(int count)
        {
            return _dbSet
                .Include(b => b.Customer)
                .OrderByDescending(b => b.CreatedDate)
                .Take(count)
                .ToList();
        }
    }
}