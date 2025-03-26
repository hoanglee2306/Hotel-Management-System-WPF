using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class BookingDetailRepository : GenericRepository<BookingDetail>
    {
        public BookingDetailRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        // Get booking detail with room and reservation
        public BookingDetail GetDetailWithRoomAndReservation(int bookingDetailId)
        {
            return _dbSet
                .Include(bd => bd.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(bd => bd.BookingReservation)
                    .ThenInclude(br => br.Customer)
                .FirstOrDefault(bd => bd.BookingDetailId == bookingDetailId);
        }

        // Get booking details by reservation
        public IEnumerable<BookingDetail> GetDetailsByReservation(int bookingReservationId)
        {
            return _dbSet
                .Include(bd => bd.Room)
                .Where(bd => bd.BookingReservationId == bookingReservationId)
                .ToList();
        }

        // Get booking history for a room
        public IEnumerable<BookingDetail> GetBookingHistoryForRoom(int roomId)
        {
            return _dbSet
                .Include(bd => bd.BookingReservation)
                    .ThenInclude(br => br.Customer)
                .Where(bd => bd.RoomId == roomId)
                .OrderByDescending(bd => bd.BookingReservation.BookingDate)
                .ToList();
        }
        
        // Calculate total price for a booking
        public decimal CalculateTotalPrice(int bookingReservationId)
        {
            return _dbSet
                .Where(bd => bd.BookingReservationId == bookingReservationId)
                .Sum(bd => bd.ActualPrice);
        }
    }
}
