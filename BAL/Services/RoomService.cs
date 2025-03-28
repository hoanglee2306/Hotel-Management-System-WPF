using BAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomRepository _roomRepository;
        private readonly RoomTypeRepository _roomTypeRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;

        public RoomService(
            RoomRepository roomRepository,
            RoomTypeRepository roomTypeRepository,
            BookingDetailRepository bookingDetailRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _bookingDetailRepository = bookingDetailRepository;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepository.GetAll().OrderBy(r => r.RoomNumber);
        }

        public IEnumerable<Room> GetActiveRooms()
        {
            return _roomRepository.GetActiveRooms();
        }

        public Room GetRoomById(int id)
        {
            return _roomRepository.GetRoomWithType(id);
        }

        public void AddRoom(Room room)
        {
            // Validate room type exists
            var roomType = _roomTypeRepository.GetById(room.RoomTypeId);
            if (roomType == null)
                throw new Exception("Room type not found");
            
            // Set default status to active
            room.RoomStatus = 1;
            
            _roomRepository.Add(room);
            _roomRepository.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            try
            {
                // Validate room type exists
                var roomType = _roomTypeRepository.GetById(room.RoomTypeId);
                if (roomType == null)
                    throw new Exception("Room type not found");
                
                // Get existing room from database
                var existingRoom = _roomRepository.GetById(room.RoomId);
                if (existingRoom == null)
                    throw new Exception("Room not found");
                
                // Update properties on the existing entity
                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.RoomDescription = room.RoomDescription;
                existingRoom.RoomMaxCapacity = room.RoomMaxCapacity;
                existingRoom.RoomStatus = room.RoomStatus;
                existingRoom.RoomPricePerDate = room.RoomPricePerDate;
                existingRoom.RoomTypeId = room.RoomTypeId;
                
                // Save changes
                _roomRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating room: {ex.Message}", ex);
            }
        }

        public bool DeleteRoom(int id)
        {
            var room = _roomRepository.GetById(id);
            if (room == null)
                return false;
            
            // Check if room is used in any booking details
            var bookingDetails = _bookingDetailRepository.Find(bd => bd.RoomId == id);
            
            if (bookingDetails.Any())
            {
                // Just deactivate the room instead of deleting
                room.RoomStatus = 0; // Deactivate
                _roomRepository.Update(room);
                _roomRepository.SaveChanges();
                return true;
            }
            
            // Otherwise, delete the room
            _roomRepository.Remove(room);
            _roomRepository.SaveChanges();
            return true;
        }

        public void UpdateRoomStatus(int roomId, int status)
        {
            var room = _roomRepository.GetById(roomId);
            if (room == null)
                throw new Exception("Room not found");
            
            room.RoomStatus = status;
            _roomRepository.Update(room);
            _roomRepository.SaveChanges();
        }

        public IEnumerable<Room> SearchRooms(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllRooms();
                
            searchTerm = searchTerm.ToLower();
            
            return _roomRepository.GetAll()
                .Where(r => 
                    r.RoomNumber.ToLower().Contains(searchTerm) ||
                    (r.RoomDescription != null && r.RoomDescription.ToLower().Contains(searchTerm)) ||
                    r.RoomType.RoomTypeName.ToLower().Contains(searchTerm))
                .OrderBy(r => r.RoomNumber)
                .ToList();
        }

        public IEnumerable<Room> GetAvailableRooms(DateOnly startDate, DateOnly endDate)
        {
            // Convert DateOnly to DateTime for the repository method
            DateTime startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDateTime = endDate.ToDateTime(TimeOnly.MinValue);
            
            return _roomRepository.GetAvailableRooms(startDateTime, endDateTime);
        }

        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            return _roomTypeRepository.GetAllRoomTypes();
        }
    }
}