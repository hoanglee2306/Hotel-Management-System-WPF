using BAL.Interfaces;
using DAL;
using DAL.Models;
using DAL.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingReservationRepository _bookingRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly RoomRepository _roomRepository;

        public BookingService(
            BookingReservationRepository bookingRepository,
            BookingDetailRepository bookingDetailRepository,
            RoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _roomRepository = roomRepository;
        }

        // Booking Reservation Management
        public IEnumerable<BookingReservation> GetAllBookings()
        {
            return _bookingRepository.GetAll();
        }

        public IEnumerable<BookingReservation> GetActiveBookings()
        {
            return _bookingRepository.Find(b => b.BookingStatus == 1);
        }

        public BookingReservation GetBookingById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public BookingReservation GetBookingWithDetails(int id)
        {
            return _bookingRepository.GetBookingWithDetails(id);
        }

        public void AddBooking(BookingReservation booking, IEnumerable<BookingDetail> details)
        {
            // Calculate total price based on booking details
            decimal totalPrice = CalculateTotalBookingPrice(details);
            booking.TotalPrice = totalPrice;
            booking.CreatedDate = DateTime.Now;
            
            // Add booking reservation
            _bookingRepository.Add(booking);
            _bookingRepository.SaveChanges();
            
            // Add booking details with the new booking ID
            foreach (var detail in details)
            {
                detail.BookingReservationId = booking.BookingReservationId;
                _bookingDetailRepository.Add(detail);
            }
            
            _bookingDetailRepository.SaveChanges();
        }

        public void UpdateBooking(BookingReservation booking, IEnumerable<BookingDetail> details)
        {
            // Get existing booking
            var existingBooking = _bookingRepository.GetBookingWithDetails(booking.BookingReservationId);
            if (existingBooking == null)
                throw new Exception("Booking not found");

            // Remove existing details
            foreach (var detail in existingBooking.BookingDetails.ToList())
            {
                _bookingDetailRepository.Remove(detail);
            }
            
            // Update booking information
            booking.TotalPrice = CalculateTotalBookingPrice(details);
            _bookingRepository.Update(booking);
            
            // Add new details
            foreach (var detail in details)
            {
                detail.BookingReservationId = booking.BookingReservationId;
                _bookingDetailRepository.Add(detail);
            }
            
            _bookingDetailRepository.SaveChanges();
            _bookingRepository.SaveChanges();
        }

        public bool DeleteBooking(int id)
        {
            var booking = _bookingRepository.GetBookingWithDetails(id);
            if (booking == null)
                return false;

            // Remove all booking details first
            foreach (var detail in booking.BookingDetails.ToList())
            {
                _bookingDetailRepository.Remove(detail);
            }
            _bookingDetailRepository.SaveChanges();
            
            // Then remove the booking
            _bookingRepository.Remove(booking);
            _bookingRepository.SaveChanges();
            
            return true;
        }

        public IEnumerable<BookingReservation> SearchBookings(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllBookings();

            searchTerm = searchTerm.ToLower();
            
            return _bookingRepository.GetAll()
                .Where(b => 
                    b.BookingReservationId.ToString().Contains(searchTerm) ||
                    b.Customer.CustomerFullName.ToLower().Contains(searchTerm) ||
                    b.Notes != null && b.Notes.ToLower().Contains(searchTerm))
                .ToList();
        }

        // Customer specific booking operations
        public IEnumerable<BookingReservation> GetCustomerBookings(int customerId)
        {
            return _bookingRepository.GetBookingsByCustomer(customerId);
        }

        // Booking Details Management
        public IEnumerable<BookingDetail> GetBookingDetailsByReservationId(int reservationId)
        {
            return _bookingDetailRepository.GetDetailsByReservation(reservationId);
        }

        public void AddBookingDetail(BookingDetail detail)
        {
            _bookingDetailRepository.Add(detail);
            _bookingDetailRepository.SaveChanges();
            
            // Update total price in the booking reservation
            var booking = _bookingRepository.GetById(detail.BookingReservationId);
            if (booking != null)
            {
                booking.TotalPrice = _bookingDetailRepository.CalculateTotalPrice(booking.BookingReservationId);
                _bookingRepository.Update(booking);
                _bookingRepository.SaveChanges();
            }
        }

        public void UpdateBookingDetail(BookingDetail detail)
        {
            _bookingDetailRepository.Update(detail);
            _bookingDetailRepository.SaveChanges();
            
            // Update total price in the booking reservation
            var booking = _bookingRepository.GetById(detail.BookingReservationId);
            if (booking != null)
            {
                booking.TotalPrice = _bookingDetailRepository.CalculateTotalPrice(booking.BookingReservationId);
                _bookingRepository.Update(booking);
                _bookingRepository.SaveChanges();
            }
        }

        public void DeleteBookingDetail(int id)
        {
            var detail = _bookingDetailRepository.GetById(id);
            if (detail == null)
                return;
                
            int bookingId = detail.BookingReservationId;
            
            _bookingDetailRepository.Remove(detail);
            _bookingDetailRepository.SaveChanges();
            
            // Update total price in the booking reservation
            var booking = _bookingRepository.GetById(bookingId);
            if (booking != null)
            {
                booking.TotalPrice = _bookingDetailRepository.CalculateTotalPrice(bookingId);
                _bookingRepository.Update(booking);
                _bookingRepository.SaveChanges();
            }
        }

        // Calculate booking prices
        public decimal CalculateRoomPrice(int roomId, DateOnly startDate, DateOnly endDate)
        {
            var room = _roomRepository.GetById(roomId);
            if (room == null)
                throw new Exception("Room not found");
                
            int numberOfDays = endDate.DayNumber - startDate.DayNumber;
            if (numberOfDays <= 0)
                numberOfDays = 1; // Minimum 1 day
                
            return room.RoomPricePerDate * numberOfDays;
        }

        public decimal CalculateTotalBookingPrice(IEnumerable<BookingDetail> details)
        {
            return details.Sum(d => d.ActualPrice);
        }
    }
}