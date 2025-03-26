using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        // Get room with room type
        public Room GetRoomWithType(int roomId)
        {
            return _dbSet
                .Include(r => r.RoomType)
                .FirstOrDefault(r => r.RoomId == roomId);
        }

        // Get available rooms for a date range
        public IEnumerable<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            // Convert DateTime to DateOnly for comparison with model
            DateOnly checkIn = DateOnly.FromDateTime(checkInDate);
            DateOnly checkOut = DateOnly.FromDateTime(checkOutDate);
            
            var bookedRoomIds = _context.BookingDetails
                .Where(bd => bd.BookingReservation.BookingStatus == 1 && // Only consider active bookings
                      ((bd.BookingReservation.CheckinDate <= checkOut && 
                        bd.BookingReservation.CheckoutDate >= checkIn)))
                .Select(bd => bd.RoomId)
                .Distinct()
                .ToList();

            return _dbSet
                .Include(r => r.RoomType)
                .Where(r => !bookedRoomIds.Contains(r.RoomId) && r.RoomStatus == 1)
                .OrderBy(r => r.RoomNumber)
                .ToList();
        }

        // Get rooms by type
        public IEnumerable<Room> GetRoomsByType(int roomTypeId)
        {
            return _dbSet
                .Where(r => r.RoomTypeId == roomTypeId)
                .OrderBy(r => r.RoomNumber)
                .ToList();
        }

        // Get active rooms
        public IEnumerable<Room> GetActiveRooms()
        {
            return _dbSet
                .Include(r => r.RoomType)
                .Where(r => r.RoomStatus == 1)
                .OrderBy(r => r.RoomNumber)
                .ToList();
        }
    }
}