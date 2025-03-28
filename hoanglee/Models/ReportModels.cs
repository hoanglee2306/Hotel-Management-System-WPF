using System;
using System.Collections.Generic;

namespace hoanglee.Models
{
    public class DailyRevenue
    {
        public DateOnly Date { get; set; }
        public decimal Revenue { get; set; }
        public int BookingsCount { get; set; }
    }

    public class RevenueReport
    {
        public decimal TotalRevenue { get; set; }
        public int TotalBookings { get; set; }
        public decimal AverageBookingValue { get; set; }
        public List<DailyRevenue> DailyRevenues { get; set; } = new List<DailyRevenue>();
    }

    public class RoomOccupancyReport
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int TotalBookingDays { get; set; }
        public decimal OccupancyPercentage { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}