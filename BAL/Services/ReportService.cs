using BAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BAL.Services
{
    public class ReportService : IReportService
    {
        private readonly BookingReservationRepository _bookingRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly RoomRepository _roomRepository;
        private readonly FUMiniHotelManagementContext _context;

        public ReportService(
            BookingReservationRepository bookingRepository,
            BookingDetailRepository bookingDetailRepository,
            RoomRepository roomRepository,
            FUMiniHotelManagementContext context)
        {
            _bookingRepository = bookingRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _roomRepository = roomRepository;
            _context = context;
        }

        public IEnumerable<BookingReservation> GetBookingStatistics(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                // Debug: Print date range
                Debug.WriteLine($"Searching for bookings between {startDate} and {endDate}");
                
                // Get all bookings first
                var allBookings = _context.BookingReservations
                    .Include(b => b.Customer)
                    .ToList();
                
                // Filter in memory to avoid EF Core translation issues with DateOnly
                var bookings = allBookings
                    .Where(b => !(b.CheckoutDate < startDate || b.CheckinDate > endDate))
                    .ToList();
                
                // Debug: Print found bookings count
                Debug.WriteLine($"Found {bookings.Count} bookings in the date range");
                
                // Sort in descending order by booking date
                return bookings.OrderByDescending(b => b.BookingDate);
            }
            catch (Exception ex)
            {
                // Log the exception
                Debug.WriteLine($"Error in GetBookingStatistics: {ex.Message}");
                // Return empty collection on error
                return new List<BookingReservation>();
            }
        }

        public IEnumerable<RoomOccupancyReport> GetRoomOccupancyReport(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                Debug.WriteLine($"Generating Room Occupancy Report for {startDate} to {endDate}");
                
                // Get all rooms with their room types
                var rooms = _context.Rooms.Include(r => r.RoomType).ToList();
                Debug.WriteLine($"Found {rooms.Count} rooms");
                
                // Calculate total days in the period
                int totalDays = endDate.DayNumber - startDate.DayNumber + 1;
                if (totalDays <= 0) totalDays = 1;
                
                // Get all booking details and their related bookings
                var allBookingDetails = _context.BookingDetails
                    .Include(bd => bd.BookingReservation)
                    .Include(bd => bd.Room)
                    .ThenInclude(r => r.RoomType)
                    .ToList();
                
                Debug.WriteLine($"Found {allBookingDetails.Count} total booking details");
                
                // Filter booking details that overlap with the date range
                var bookingDetails = allBookingDetails
                    .Where(bd => 
                        bd.BookingReservation != null &&
                        !(bd.EndDate < startDate || bd.StartDate > endDate))
                    .ToList();
                
                Debug.WriteLine($"Found {bookingDetails.Count} booking details in the date range");
                
                // Calculate occupancy for each room
                var result = new List<RoomOccupancyReport>();
                foreach (var room in rooms)
                {
                    // Get room type name
                    string roomTypeName = room.RoomType?.RoomTypeName ?? "Unknown";
                    
                    // Get bookings for this room
                    var roomBookings = bookingDetails
                        .Where(bd => bd.RoomId == room.RoomId && 
                               bd.BookingReservation != null && 
                               bd.BookingReservation.BookingStatus == 1)
                        .ToList();
                    
                    Debug.WriteLine($"Room {room.RoomNumber} has {roomBookings.Count} bookings in the period");
                    
                    // Calculate total booking days for this room in the period
                    int totalBookingDays = 0;
                    decimal totalRevenue = 0;
                    
                    foreach (var booking in roomBookings)
                    {
                        // Calculate overlap between booking period and report period
                        DateOnly overlapStart = booking.StartDate > startDate ? booking.StartDate : startDate;
                        DateOnly overlapEnd = booking.EndDate < endDate ? booking.EndDate : endDate;
                        
                        int bookingDays = overlapEnd.DayNumber - overlapStart.DayNumber + 1;
                        if (bookingDays > 0)
                        {
                            totalBookingDays += bookingDays;
                            
                            // Calculate revenue for this booking period
                            decimal dailyRate = booking.RoomPrice; // Use room price per day
                            totalRevenue += dailyRate * bookingDays;
                            
                            Debug.WriteLine($"Booking from {booking.StartDate} to {booking.EndDate} adds {bookingDays} days and ${dailyRate * bookingDays} revenue");
                        }
                    }
                    
                    // Calculate occupancy percentage
                    int occupancyPercentage = (int)Math.Round((double)totalBookingDays / totalDays * 100);
                    result.Add(new RoomOccupancyReport
                    {
                        RoomId = room.RoomId,
                        RoomNumber = room.RoomNumber,
                        RoomType = roomTypeName,
                        TotalBookingDays = totalBookingDays,
                        OccupancyPercentage = occupancyPercentage,
                        TotalRevenue = totalRevenue
                    });
                    
                    Debug.WriteLine($"Room {room.RoomNumber}: {totalBookingDays} days, {occupancyPercentage}% occupancy, ${totalRevenue} revenue");
                }
                
                // Sort by occupancy percentage in descending order
                return result.OrderByDescending(r => r.OccupancyPercentage);
            }
            catch (Exception ex)
            {
                // Log the exception
                Debug.WriteLine($"Error in GetRoomOccupancyReport: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Return empty collection on error
                return new List<RoomOccupancyReport>();
            }
        }

        public RevenueReport GetRevenueReport(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                Debug.WriteLine($"Generating Revenue Report for {startDate} to {endDate}");
                
                // Get all bookings
                var allBookings = _context.BookingReservations.ToList();
                Debug.WriteLine($"Found {allBookings.Count} total bookings");
                
                // Filter bookings that overlap with the date range
                var bookings = allBookings
                    .Where(b => !(b.CheckoutDate < startDate || b.CheckinDate > endDate) && b.BookingStatus == 1)
                    .ToList();
                
                Debug.WriteLine($"Found {bookings.Count} bookings in the date range with status 1");
                
                // Get all booking details with their bookings
                var allBookingDetails = _context.BookingDetails
                    .Include(bd => bd.BookingReservation)
                    .ToList();
                
                // Filter booking details that overlap with the date range
                var bookingDetails = allBookingDetails
                    .Where(bd => bd.BookingReservation != null && 
                                bd.BookingReservation.BookingStatus == 1 &&
                                !(bd.EndDate < startDate || bd.StartDate > endDate))
                    .ToList();
                
                Debug.WriteLine($"Found {bookingDetails.Count} booking details in the date range with status 1");
                
                // Calculate total revenue from booking details
                decimal totalRevenue = 0;
                
                // Calculate daily revenue
                var dailyRevenueDict = new Dictionary<DateOnly, DailyRevenue>();
                
                // Create a dictionary with all dates in the range
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    dailyRevenueDict[date] = new DailyRevenue
                    {
                        Date = date,
                        Revenue = 0,
                        BookingsCount = 0
                    };
                }
                
                // Calculate revenue for each booking detail
                foreach (var detail in bookingDetails)
                {
                    // Calculate overlap between booking period and report period
                    DateOnly overlapStart = detail.StartDate > startDate ? detail.StartDate : startDate;
                    DateOnly overlapEnd = detail.EndDate < endDate ? detail.EndDate : endDate;
                    
                    // Calculate daily rate
                    int bookingDays = detail.EndDate.DayNumber - detail.StartDate.DayNumber + 1;
                    if (bookingDays <= 0) bookingDays = 1;
                    decimal dailyRate = detail.ActualPrice / bookingDays;
                    
                    Debug.WriteLine($"Booking detail ID {detail.BookingDetailId}: ${detail.ActualPrice} total, ${dailyRate} per day for {bookingDays} days");
                    
                    // Add revenue for each day in the booking period
                    for (var date = overlapStart; date <= overlapEnd; date = date.AddDays(1))
                    {
                        if (dailyRevenueDict.ContainsKey(date))
                        {
                            dailyRevenueDict[date].Revenue += dailyRate;
                            
                            // Count each booking only once per day
                            if (date == overlapStart)
                            {
                                dailyRevenueDict[date].BookingsCount++;
                            }
                        }
                    }
                    
                    // Add to total revenue
                    int overlapDays = overlapEnd.DayNumber - overlapStart.DayNumber + 1;
                    totalRevenue += dailyRate * overlapDays;
                }
                
                // Convert dictionary to list
                var dailyRevenues = dailyRevenueDict.Values.OrderBy(d => d.Date).ToList();
                
                // Calculate total bookings (count unique booking IDs)
                int totalBookings = bookings.Count;
                
                // Calculate average booking value
                decimal averageBookingValue = totalBookings > 0 ? totalRevenue / totalBookings : 0;
                
                Debug.WriteLine($"Revenue Report Summary: ${totalRevenue} total, {totalBookings} bookings, ${averageBookingValue} average");
                
                return new RevenueReport
                {
                    TotalRevenue = totalRevenue,
                    TotalBookings = totalBookings,
                    AverageBookingValue = averageBookingValue,
                    DailyRevenues = dailyRevenues
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Debug.WriteLine($"Error in GetRevenueReport: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Return empty report on error
                return new RevenueReport
                {
                    TotalRevenue = 0,
                    TotalBookings = 0,
                    AverageBookingValue = 0,
                    DailyRevenues = new List<DailyRevenue>()
                };
            }
        }
    }
}