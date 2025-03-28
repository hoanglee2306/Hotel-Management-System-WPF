using BAL;
using DAL.Models;
using hoanglee.Commands;
using hoanglee.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class CustomerBookingsViewModel : ViewModelBase
    {
        private ObservableCollection<BookingReservation> _bookings;
        private BookingReservation _selectedBooking;
        private ObservableCollection<BookingDetail> _bookingDetails;
        private bool _isViewingDetails;

        public ObservableCollection<BookingReservation> Bookings
        {
            get => _bookings;
            set => SetProperty(ref _bookings, value);
        }

        public BookingReservation SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                if (SetProperty(ref _selectedBooking, value))
                {
                    // Load booking details when a booking is selected
                    if (_selectedBooking != null)
                    {
                        LoadBookingDetails(_selectedBooking.BookingReservationId);
                    }
                    else
                    {
                        BookingDetails = new ObservableCollection<BookingDetail>();
                    }
                }
            }
        }

        public ObservableCollection<BookingDetail> BookingDetails
        {
            get => _bookingDetails;
            set => SetProperty(ref _bookingDetails, value);
        }

        public bool IsViewingDetails
        {
            get => _isViewingDetails;
            set => SetProperty(ref _isViewingDetails, value);
        }

        public ICommand ViewDetailsCommand { get; }
        public ICommand BackToListCommand { get; }

        public CustomerBookingsViewModel()
        {
            ViewDetailsCommand = new RelayCommand(ExecuteViewDetails, CanExecuteViewDetails);
            BackToListCommand = new RelayCommand(ExecuteBackToList);

            BookingDetails = new ObservableCollection<BookingDetail>();
            LoadBookings();
        }

        private void LoadBookings()
        {
            try
            {
                var bookingService = ServiceLocator.Instance.BookingService;
                var customerId = CurrentUserStore.Instance.CurrentUser?.CustomerId ?? 0;
                
                if (customerId > 0)
                {
                    var bookingList = bookingService.GetCustomerBookings(customerId).ToList();
                    Bookings = new ObservableCollection<BookingReservation>(bookingList);
                }
                else
                {
                    Bookings = new ObservableCollection<BookingReservation>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadBookingDetails(int bookingId)
        {
            try
            {
                var bookingService = ServiceLocator.Instance.BookingService;
                var details = bookingService.GetBookingDetailsByReservationId(bookingId).ToList();
                BookingDetails = new ObservableCollection<BookingDetail>(details);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteViewDetails(object parameter)
        {
            if (SelectedBooking != null)
            {
                IsViewingDetails = true;
            }
        }

        private bool CanExecuteViewDetails(object parameter)
        {
            return SelectedBooking != null;
        }

        private void ExecuteBackToList(object parameter)
        {
            IsViewingDetails = false;
        }
    }
}