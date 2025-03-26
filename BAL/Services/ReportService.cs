using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace BAL.Services
{
    public class ReportService
    {
        private readonly BookingReservationRepository _bookingReservationRepository;
        private readonly RoomRepository _roomRepository;
        private readonly FUMiniHotelManagementContext _context;

        public ReportService(
            BookingReservationRepository bookingReservationRepository,
            RoomRepository roomRepository,
            FUMiniHotelManagementContext context)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _roomRepository = roomRepository;
            _context = context;
        }
        
        public class BookingStatistic
        {
            public DateTime Date { get; set; }
            public int TotalBookings { get; set; }
            public decimal TotalRevenue { get; set; }
            public int TotalRoomsBooked { get; set; }
        }
        
        public IEnumerable<BookingStatistic> GetBookingStatisticsByPeriod(DateTime startDate, DateTime endDate)
        {
            // Convert to DateOnly for comparison with model
            DateOnly startDateOnly = DateOnly.FromDateTime(startDate);
            DateOnly endDateOnly = DateOnly.FromDateTime(endDate);
            
            // Get all bookings in the date range
            var bookings = _bookingReservationRepository.GetBookingsByDateRange(startDate, endDate);
            
            // Group by booking date and calculate statistics
            var statistics = bookings
                .GroupBy(b => b.BookingDate.Date)
                .Select(g => new BookingStatistic
                {
                    Date = g.Key,
                    TotalBookings = g.Count(),
                    TotalRevenue = g.Sum(b => b.TotalPrice),
                    TotalRoomsBooked = g.Sum(b => b.BookingDetails.Count)
                })
                .OrderByDescending(s => s.Date)
                .ToList();
                
            return statistics;
        }
        
        public class RoomTypeStatistic
        {
            public string RoomTypeName { get; set; }
            public int TotalBookings { get; set; }
            public decimal TotalRevenue { get; set; }
            public decimal OccupancyRate { get; set; } // Percentage of days the room type was occupied
        }
        
        public IEnumerable<RoomTypeStatistic> GetRoomTypeStatisticsByPeriod(DateTime startDate, DateTime endDate)
        {
            // Convert to DateOnly for comparison with model
            DateOnly startDateOnly = DateOnly.FromDateTime(startDate);
            DateOnly endDateOnly = DateOnly.FromDateTime(endDate);
            
            // Get all bookings in the date range with details
            var bookings = _bookingReservationRepository.GetBookingsByDateRange(startDate, endDate);
            
            // Get all room types with rooms
            var roomTypes = _context.RoomTypes
                .Include(rt => rt.Rooms)
                .ToList();
                
            // Calculate total days in period
            int totalDays = (endDate - startDate).Days + 1;
            
            // Calculate statistics for each room type
            var statistics = roomTypes.Select(rt => 
            {
                // Get all booking details for this room type
                var bookingDetails = bookings
                    .SelectMany(b => b.BookingDetails)
                    .Where(bd => bd.Room.RoomTypeId == rt.RoomTypeId)
                    .ToList();
                    
                // Calculate total bookings and revenue
                int totalBookings = bookingDetails.Count;
                decimal totalRevenue = bookingDetails.Sum(bd => bd.ActualPrice);
                
                // Calculate occupancy rate
                int totalRoomsOfType = rt.Rooms.Count;
                if (totalRoomsOfType == 0)
                    return new RoomTypeStatistic
                    {
                        RoomTypeName = rt.RoomTypeName,
                        TotalBookings = 0,
                        TotalRevenue = 0,
                        OccupancyRate = 0
                    };
                    
                // Each room could be booked for multiple days
                int totalPossibleRoomDays = totalRoomsOfType * totalDays;
                int totalBookedRoomDays = 0;
                
                foreach (var booking in bookings)
                {
                    // Count days booked for each room of this type
                    var roomsOfTypeBooked = booking.BookingDetails
                        .Where(bd => bd.Room.RoomTypeId == rt.RoomTypeId)
                        .Select(bd => bd.RoomId)
                        .Distinct()
                        .Count();
                        
                    // Calculate days between check-in and check-out
                    int daysBooked = (booking.CheckoutDate.DayNumber - booking.CheckinDate.DayNumber);
                    
                    totalBookedRoomDays += roomsOfTypeBooked * daysBooked;
                }
                
                decimal occupancyRate = totalPossibleRoomDays > 0 
                    ? (decimal)totalBookedRoomDays / totalPossibleRoomDays * 100 
                    : 0;
                
                return new RoomTypeStatistic
                {
                    RoomTypeName = rt.RoomTypeName,
                    TotalBookings = totalBookings,
                    TotalRevenue = totalRevenue,
                    OccupancyRate = Math.Round(occupancyRate, 2)
                };
            })
            .OrderByDescending(s => s.TotalRevenue)
            .ToList();
            
            return statistics;
        }
    }
}