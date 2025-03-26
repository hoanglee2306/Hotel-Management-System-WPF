using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BAL.Services
{
    public class BookingService
    {
        private readonly BookingReservationRepository _bookingReservationRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly RoomRepository _roomRepository;

        public BookingService(
            BookingReservationRepository bookingReservationRepository,
            BookingDetailRepository bookingDetailRepository,
            RoomRepository roomRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _roomRepository = roomRepository;
        }
        
        public IEnumerable<BookingReservation> GetAllBookings()
        {
            return _bookingReservationRepository.GetAll()
                .OrderByDescending(b => b.BookingDate);
        }
        
        public BookingReservation GetBookingById(int id)
        {
            return _bookingReservationRepository.GetBookingWithDetails(id);
        }
        
        public IEnumerable<BookingReservation> GetBookingsByCustomer(int customerId)
        {
            return _bookingReservationRepository.GetBookingsByCustomer(customerId);
        }
        
        public IEnumerable<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _bookingReservationRepository.GetBookingsByDateRange(startDate, endDate);
        }
        
        public BookingReservation CreateBooking(BookingReservation booking, List<int> roomIds)
        {
            // Validate booking data
            if (booking.CustomerId <= 0)
            {
                throw new ArgumentException("Customer is required.");
            }
            
            if (booking.CheckinDate >= booking.CheckoutDate)
            {
                throw new ArgumentException("Check-out date must be after check-in date.");
            }
            
            if (roomIds == null || !roomIds.Any())
            {
                throw new ArgumentException("At least one room must be selected.");
            }
            
            // Set default values
            booking.BookingDate = DateTime.Now;
            booking.CreatedDate = DateTime.Now;
            booking.BookingStatus = 1; // Active
            
            // Calculate total price
            decimal totalPrice = 0;
            
            // Create booking
            _bookingReservationRepository.Add(booking);
            _bookingReservationRepository.SaveChanges();
            
            // Create booking details for each room
            foreach (var roomId in roomIds)
            {
                var room = _roomRepository.GetById(roomId);
                if (room == null || room.RoomStatus != 1)
                {
                    throw new ArgumentException($"Room with ID {roomId} is not available.");
                }
                
                var bookingDetail = new BookingDetail
                {
                    BookingReservationId = booking.BookingReservationId,
                    RoomId = roomId,
                    RoomPrice = room.RoomPricePerDate,
                    ActualPrice = room.RoomPricePerDate // You might apply discounts or other calculations here
                };
                
                _bookingDetailRepository.Add(bookingDetail);
                totalPrice += bookingDetail.ActualPrice;
            }
            
            // Update total price
            booking.TotalPrice = totalPrice;
            _bookingReservationRepository.Update(booking);
            _bookingReservationRepository.SaveChanges();
            
            return booking;
        }
        
        public void UpdateBooking(BookingReservation booking)
        {
            // Validate booking data
            if (booking.CustomerId <= 0)
            {
                throw new ArgumentException("Customer is required.");
            }
            
            if (booking.CheckinDate >= booking.CheckoutDate)
            {
                throw new ArgumentException("Check-out date must be after check-in date.");
            }
            
            // Update booking
            _bookingReservationRepository.Update(booking);
            _bookingReservationRepository.SaveChanges();
        }
        
        public bool CancelBooking(int bookingId)
        {
            var booking = _bookingReservationRepository.GetById(bookingId);
            if (booking == null)
                return false;
                
            // Mark as cancelled
            booking.BookingStatus = 0;
            _bookingReservationRepository.Update(booking);
            _bookingReservationRepository.SaveChanges();
            return true;
        }
        
        public bool DeleteBooking(int bookingId)
        {
            var booking = _bookingReservationRepository.GetBookingWithDetails(bookingId);
            if (booking == null)
                return false;
            
            // Delete booking details first
            foreach (var detail in booking.BookingDetails.ToList())
            {
                _bookingDetailRepository.Remove(detail);
            }
            
            // Then delete the booking
            _bookingReservationRepository.Remove(booking);
            _bookingReservationRepository.SaveChanges();
            return true;
        }
        
        // Booking Detail methods
        public BookingDetail GetBookingDetailById(int id)
        {
            return _bookingDetailRepository.GetDetailWithRoomAndReservation(id);
        }
        
        public IEnumerable<BookingDetail> GetBookingDetailsByReservation(int bookingReservationId)
        {
            return _bookingDetailRepository.GetDetailsByReservation(bookingReservationId);
        }
        
        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            // Validate booking detail data
            if (bookingDetail.BookingReservationId <= 0)
            {
                throw new ArgumentException("Booking reservation is required.");
            }
            
            if (bookingDetail.RoomId <= 0)
            {
                throw new ArgumentException("Room is required.");
            }
            
            // Get room to set price
            var room = _roomRepository.GetById(bookingDetail.RoomId);
            if (room == null)
            {
                throw new ArgumentException("Room not found.");
            }
            
            bookingDetail.RoomPrice = room.RoomPricePerDate;
            bookingDetail.ActualPrice = room.RoomPricePerDate; // You might apply discounts or other calculations here
            
            // Add booking detail
            _bookingDetailRepository.Add(bookingDetail);
            _bookingDetailRepository.SaveChanges();
            
            // Update total price in booking reservation
            var booking = _bookingReservationRepository.GetById(bookingDetail.BookingReservationId);
            if (booking != null)
            {
                booking.TotalPrice = _bookingDetailRepository.CalculateTotalPrice(booking.BookingReservationId);
                _bookingReservationRepository.Update(booking);
                _bookingReservationRepository.SaveChanges();
            }
        }
        
        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            // Update booking detail
            _bookingDetailRepository.Update(bookingDetail);
            _bookingDetailRepository.SaveChanges();
            
            // Update total price in booking reservation
            var booking = _bookingReservationRepository.GetById(bookingDetail.BookingReservationId);
            if (booking != null)
            {
                booking.TotalPrice = _bookingDetailRepository.CalculateTotalPrice(booking.BookingReservationId);
                _bookingReservationRepository.Update(booking);
                _bookingReservationRepository.SaveChanges();
            }
        }
        
        public bool RemoveBookingDetail(int bookingDetailId)
        {
            var bookingDetail = _bookingDetailRepository.GetById(bookingDetailId);
            if (bookingDetail == null)
                return false;
                
            int bookingReservationId = bookingDetail.BookingReservationId;
            
            // Remove booking detail
            _bookingDetailRepository.Remove(bookingDetail);
            _bookingDetailRepository.SaveChanges();
            
            // Update total price in booking reservation
            var booking = _bookingReservationRepository.GetById(bookingReservationId);
            if (booking != null)
            {
                booking.TotalPrice = _bookingDetailRepository.CalculateTotalPrice(bookingReservationId);
                _bookingReservationRepository.Update(booking);
                _bookingReservationRepository.SaveChanges();
            }
            
            return true;
        }
    }
}