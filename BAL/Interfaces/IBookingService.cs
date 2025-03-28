using DAL.Models;

namespace BAL.Interfaces
{
    public interface IBookingService
    {
        // Booking Reservation Management
        IEnumerable<BookingReservation> GetAllBookings();
        IEnumerable<BookingReservation> GetActiveBookings();
        BookingReservation GetBookingById(int id);
        BookingReservation GetBookingWithDetails(int id);
        void AddBooking(BookingReservation booking, IEnumerable<BookingDetail> details);
        void UpdateBooking(BookingReservation booking, IEnumerable<BookingDetail> details);
        bool DeleteBooking(int id);
        IEnumerable<BookingReservation> SearchBookings(string searchTerm);
        
        // Customer specific booking operations
        IEnumerable<BookingReservation> GetCustomerBookings(int customerId);
        
        // Booking Details Management
        IEnumerable<BookingDetail> GetBookingDetailsByReservationId(int reservationId);
        void AddBookingDetail(BookingDetail detail);
        void UpdateBookingDetail(BookingDetail detail);
        void DeleteBookingDetail(int id);
        
        // Calculate booking prices
        decimal CalculateRoomPrice(int roomId, DateOnly startDate, DateOnly endDate);
        decimal CalculateTotalBookingPrice(IEnumerable<BookingDetail> details);
    }
}