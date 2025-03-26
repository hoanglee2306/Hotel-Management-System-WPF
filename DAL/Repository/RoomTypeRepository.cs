using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class RoomTypeRepository : GenericRepository<RoomType>
    {
        public RoomTypeRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        // Get room type with all rooms
        public RoomType GetRoomTypeWithRooms(int roomTypeId)
        {
            return _dbSet
                .Include(rt => rt.Rooms)
                .FirstOrDefault(rt => rt.RoomTypeId == roomTypeId);
        }

        // Get all room types
        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            return _dbSet
                .OrderBy(rt => rt.RoomTypeName)
                .ToList();
        }

        // Get room types with room count
        public IEnumerable<object> GetRoomTypesWithRoomCount()
        {
            return _dbSet
                .Select(rt => new
                {
                    RoomType = rt,
                    RoomCount = rt.Rooms.Count()
                })
                .OrderBy(x => x.RoomType.RoomTypeName)
                .ToList();
        }
    }
}