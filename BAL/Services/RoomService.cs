using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BAL.Services
{
    public class RoomService
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
            return _roomRepository.GetAll()
                .OrderBy(r => r.RoomNumber);
        }
        
        public IEnumerable<Room> GetActiveRooms()
        {
            return _roomRepository.GetActiveRooms();
        }
        
        public Room GetRoomById(int id)
        {
            return _roomRepository.GetRoomWithType(id);
        }
        
        public IEnumerable<Room> GetRoomsByType(int roomTypeId)
        {
            return _roomRepository.GetRoomsByType(roomTypeId);
        }
        
        public IEnumerable<Room> SearchRooms(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return _roomRepository.GetAll();
                
            return _roomRepository.Find(r => 
                r.RoomNumber.Contains(searchTerm) || 
                r.RoomDescription.Contains(searchTerm));
        }
        
        public IEnumerable<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            return _roomRepository.GetAvailableRooms(checkInDate, checkOutDate);
        }
        
        public void CreateRoom(Room room)
        {
            // Validate room data
            if (string.IsNullOrEmpty(room.RoomNumber))
            {
                throw new ArgumentException("Room number is required.");
            }
            
            // Check if room number already exists
            var existingRoom = _roomRepository.Find(r => r.RoomNumber == room.RoomNumber).FirstOrDefault();
            if (existingRoom != null)
            {
                throw new ArgumentException("Room number already exists.");
            }
            
            // Set default values
            room.RoomStatus = 1; // Active
            
            // Add room to repository
            _roomRepository.Add(room);
            _roomRepository.SaveChanges();
        }
        
        public void UpdateRoom(Room room)
        {
            // Validate room data
            if (string.IsNullOrEmpty(room.RoomNumber))
            {
                throw new ArgumentException("Room number is required.");
            }
            
            // Check if room number already exists for another room
            var existingRoom = _roomRepository.Find(r => r.RoomNumber == room.RoomNumber).FirstOrDefault();
            if (existingRoom != null && existingRoom.RoomId != room.RoomId)
            {
                throw new ArgumentException("Room number already exists for another room.");
            }
            
            // Update room
            _roomRepository.Update(room);
            _roomRepository.SaveChanges();
        }
        
        public bool DeleteRoom(int roomId)
        {
            var room = _roomRepository.GetById(roomId);
            if (room == null)
                return false;
                
            // Check if room has any booking details
            var bookingDetails = _bookingDetailRepository.Find(bd => bd.RoomId == roomId);
            if (bookingDetails.Any())
            {
                // Don't delete, just mark as inactive
                room.RoomStatus = 0;
                _roomRepository.Update(room);
                _roomRepository.SaveChanges();
                return true;
            }
            
            // If no booking details, can safely delete
            _roomRepository.Remove(room);
            _roomRepository.SaveChanges();
            return true;
        }
        
        // Room Type methods
        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            return _roomTypeRepository.GetAllRoomTypes();
        }
        
        public RoomType GetRoomTypeById(int id)
        {
            return _roomTypeRepository.GetById(id);
        }
        
        public RoomType GetRoomTypeWithRooms(int id)
        {
            return _roomTypeRepository.GetRoomTypeWithRooms(id);
        }
        
        public void CreateRoomType(RoomType roomType)
        {
            // Validate room type data
            if (string.IsNullOrEmpty(roomType.RoomTypeName))
            {
                throw new ArgumentException("Room type name is required.");
            }
            
            // Add room type to repository
            _roomTypeRepository.Add(roomType);
            _roomTypeRepository.SaveChanges();
        }
        
        public void UpdateRoomType(RoomType roomType)
        {
            // Validate room type data
            if (string.IsNullOrEmpty(roomType.RoomTypeName))
            {
                throw new ArgumentException("Room type name is required.");
            }
            
            // Update room type
            _roomTypeRepository.Update(roomType);
            _roomTypeRepository.SaveChanges();
        }
        
        public bool DeleteRoomType(int roomTypeId)
        {
            var roomType = _roomTypeRepository.GetRoomTypeWithRooms(roomTypeId);
            if (roomType == null)
                return false;
                
            // Check if room type has any rooms
            if (roomType.Rooms.Any())
            {
                throw new InvalidOperationException("Cannot delete room type with associated rooms.");
            }
            
            // If no rooms, can safely delete
            _roomTypeRepository.Remove(roomType);
            _roomTypeRepository.SaveChanges();
            return true;
        }
    }
}