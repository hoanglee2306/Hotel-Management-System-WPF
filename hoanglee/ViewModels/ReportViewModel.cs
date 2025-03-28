using BAL;
using BAL.Interfaces;
using DAL.Models;
using hoanglee.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private DateOnly _startDate;
        private DateOnly _endDate;
        private ObservableCollection<BookingReservation> _bookingStatistics;
        private ObservableCollection<RoomOccupancyReport> _roomOccupancyReports;
        private RevenueReport _revenueReport;
        private bool _isLoading;
        private string _reportType = "Bookings"; // Default report type
        private bool _noDataAvailable;
        private string _errorMessage;

        public DateOnly StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateOnly EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public ObservableCollection<BookingReservation> BookingStatistics
        {
            get => _bookingStatistics;
            set => SetProperty(ref _bookingStatistics, value);
        }

        public ObservableCollection<RoomOccupancyReport> RoomOccupancyReports
        {
            get => _roomOccupancyReports;
            set => SetProperty(ref _roomOccupancyReports, value);
        }

        public RevenueReport RevenueReport
        {
            get => _revenueReport;
            set => SetProperty(ref _revenueReport, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool NoDataAvailable
        {
            get => _noDataAvailable;
            set => SetProperty(ref _noDataAvailable, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public string ReportType
        {
            get => _reportType;
            set
            {
                if (SetProperty(ref _reportType, value))
                {
                    // Reload report when type changes
                    GenerateReport();
                }
            }
        }

        public ICommand GenerateReportCommand { get; }

        public ReportViewModel()
        {
            // Initialize with default date range (last 30 days)
            EndDate = DateOnly.FromDateTime(DateTime.Today);
            StartDate = EndDate.AddDays(-30);

            GenerateReportCommand = new RelayCommand(ExecuteGenerateReport, CanExecuteGenerateReport);

            // Initialize collections
            BookingStatistics = new ObservableCollection<BookingReservation>();
            RoomOccupancyReports = new ObservableCollection<RoomOccupancyReport>();
            RevenueReport = new RevenueReport
            {
                TotalRevenue = 0,
                TotalBookings = 0,
                AverageBookingValue = 0,
                DailyRevenues = new List<DailyRevenue>()
            };

            // Generate initial report
            GenerateReport();
        }

        private void ExecuteGenerateReport(object parameter)
        {
            GenerateReport();
        }

        private bool CanExecuteGenerateReport(object parameter)
        {
            return StartDate <= EndDate && !IsLoading;
        }

        private void GenerateReport()
        {
            if (StartDate > EndDate)
            {
                MessageBox.Show("Start date must be before or equal to end date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsLoading = true;
            NoDataAvailable = false;
            ErrorMessage = null;
            try
            {
                var reportService = ServiceLocator.Instance.ReportService;

                switch (ReportType)
                {
                    case "Bookings":
                        GenerateBookingStatistics(reportService);
                        break;
                    case "Occupancy":
                        GenerateOccupancyReport(reportService);
                        break;
                    case "Revenue":
                        GenerateRevenueReport(reportService);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error generating report: {ex.Message}";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
                ((RelayCommand)GenerateReportCommand).RaiseCanExecuteChanged();
            }
        }

        private void GenerateBookingStatistics(IReportService reportService)
        {
            var statistics = reportService.GetBookingStatistics(StartDate, EndDate).ToList();
            BookingStatistics = new ObservableCollection<BookingReservation>(statistics);
            
            // Check if data is available
            NoDataAvailable = BookingStatistics.Count == 0;
            if (NoDataAvailable)
            {
                MessageBox.Show("No booking data available for the selected date range.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GenerateOccupancyReport(IReportService reportService)
        {
            var occupancyData = reportService.GetRoomOccupancyReport(StartDate, EndDate).ToList();
            RoomOccupancyReports = new ObservableCollection<RoomOccupancyReport>(occupancyData);
            
            // Check if data is available
            NoDataAvailable = RoomOccupancyReports.Count == 0 || RoomOccupancyReports.All(r => r.TotalBookingDays == 0);
            if (NoDataAvailable)
            {
                MessageBox.Show("No occupancy data available for the selected date range.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GenerateRevenueReport(IReportService reportService)
        {
            RevenueReport = reportService.GetRevenueReport(StartDate, EndDate);
            
            // Check if data is available
            NoDataAvailable = RevenueReport == null || 
                              RevenueReport.TotalBookings == 0 || 
                              RevenueReport.DailyRevenues == null || 
                              !RevenueReport.DailyRevenues.Any(d => d.Revenue > 0);
            
            if (NoDataAvailable)
            {
                MessageBox.Show("No revenue data available for the selected date range.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
