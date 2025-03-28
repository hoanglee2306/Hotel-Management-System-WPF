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
            try
            {
                // Lấy danh sách các phòng đã được đặt trong khoảng thời gian
                var bookedRoomIds = _context.BookingDetails
                    .Where(bd => 
                        bd.BookingReservation.BookingStatus == 1 && // Chỉ xem xét các đặt phòng đang hoạt động
                        (
                            // Kiểm tra xem có sự chồng chéo về thời gian không
                            (bd.StartDate <= DateOnly.FromDateTime(checkOutDate) && 
                             bd.EndDate >= DateOnly.FromDateTime(checkInDate))
                        )
                    )
                    .Select(bd => bd.RoomId)
                    .Distinct()
                    .ToList();

                // Lấy tất cả các phòng đang hoạt động không nằm trong danh sách đã đặt
                var availableRooms = _dbSet
                    .Include(r => r.RoomType)
                    .Where(r => r.RoomStatus == 1)
                    .ToList()
                    .Where(r => !bookedRoomIds.Contains(r.RoomId))
                    .OrderBy(r => r.RoomNumber)
                    .ToList();

                return availableRooms;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting available rooms: {ex.Message}", ex);
            }
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