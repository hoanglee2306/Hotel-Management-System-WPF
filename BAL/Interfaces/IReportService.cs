using DAL.Models;
using System;
using System.Collections.Generic;

namespace BAL.Interfaces
{
    public interface IReportService
    {
        // Get booking statistics for a date range
        IEnumerable<BookingReservation> GetBookingStatistics(DateOnly startDate, DateOnly endDate);
        
        // Get room occupancy statistics
        IEnumerable<RoomOccupancyReport> GetRoomOccupancyReport(DateOnly startDate, DateOnly endDate);
        
        // Get revenue statistics
        RevenueReport GetRevenueReport(DateOnly startDate, DateOnly endDate);
    }
    
    // DTOs for reporting
    public class RoomOccupancyReport
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int TotalBookingDays { get; set; }
        public int OccupancyPercentage { get; set; }
        public decimal TotalRevenue { get; set; }
    }
    
    public class RevenueReport
    {
        public decimal TotalRevenue { get; set; }
        public int TotalBookings { get; set; }
        public decimal AverageBookingValue { get; set; }
        public List<DailyRevenue> DailyRevenues { get; set; }
    }
    
    public class DailyRevenue
    {
        public DateOnly Date { get; set; }
        public decimal Revenue { get; set; }
        public int BookingsCount { get; set; }
    }
}