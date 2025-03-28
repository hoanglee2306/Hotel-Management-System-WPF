using BAL;
using DAL.Models;
using hoanglee.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class BookingManagementViewModel : ViewModelBase
    {
        private ObservableCollection<BookingReservation> _bookings;
        private BookingReservation _selectedBooking;
        private string _searchText;
        private bool _isDialogOpen;
        private BookingReservation _dialogBooking;
        private ObservableCollection<BookingDetail> _bookingDetails;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<Room> _availableRooms;
        private DateOnly _startDate;
        private DateOnly _endDate;

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
                    // Update commands that depend on selection
                    ((RelayCommand)EditBookingCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteBookingCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)ViewDetailsCommand).RaiseCanExecuteChanged();
                    
                    // Load booking details if a booking is selected
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

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    SearchBookings();
                }
            }
        }

        public bool IsDialogOpen
        {
            get => _isDialogOpen;
            set => SetProperty(ref _isDialogOpen, value);
        }

        public BookingReservation DialogBooking
        {
            get => _dialogBooking;
            set => SetProperty(ref _dialogBooking, value);
        }

        public ObservableCollection<BookingDetail> BookingDetails
        {
            get => _bookingDetails;
            set => SetProperty(ref _bookingDetails, value);
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public ObservableCollection<Room> AvailableRooms
        {
            get => _availableRooms;
            set => SetProperty(ref _availableRooms, value);
        }

        public DateOnly StartDate
        {
            get => _startDate;
            set
            {
                if (SetProperty(ref _startDate, value))
                {
                    LoadAvailableRooms();
                    
                    // Cập nhật ngày check-in trong DialogBooking
                if (DialogBooking != null)
                {
                        DialogBooking.CheckinDate = value;
                        UpdateBookingDetailsPrices();
                }
            }
        }
        }

        public DateOnly EndDate
        {
            get => _endDate;
            set
                {
                if (SetProperty(ref _endDate, value))
                {
                    LoadAvailableRooms();
                    
                    // Cập nhật ngày check-out trong DialogBooking
                    if (DialogBooking != null)
                    {
                        DialogBooking.CheckoutDate = value;
                        UpdateBookingDetailsPrices();
        }
    }
}
        }

        public ICommand AddBookingCommand { get; }
        public ICommand EditBookingCommand { get; }
        public ICommand DeleteBookingCommand { get; }
        public ICommand ViewDetailsCommand { get; }
        public ICommand SaveBookingCommand { get; }
        public ICommand CancelDialogCommand { get; }
        public ICommand AddRoomToBookingCommand { get; }
        public ICommand RemoveRoomFromBookingCommand { get; }

        public BookingManagementViewModel()
        {
            AddBookingCommand = new RelayCommand(ExecuteAddBooking);
            EditBookingCommand = new RelayCommand(ExecuteEditBooking, CanExecuteBookingAction);
            DeleteBookingCommand = new RelayCommand(ExecuteDeleteBooking, CanExecuteBookingAction);
            ViewDetailsCommand = new RelayCommand(ExecuteViewDetails, CanExecuteBookingAction);
            SaveBookingCommand = new RelayCommand(ExecuteSaveBooking, CanExecuteSaveBooking);
            CancelDialogCommand = new RelayCommand(ExecuteCancelDialog);
            AddRoomToBookingCommand = new RelayCommand(ExecuteAddRoomToBooking);
            RemoveRoomFromBookingCommand = new RelayCommand(ExecuteRemoveRoomFromBooking);

            // Initialize with default dates
            StartDate = DateOnly.FromDateTime(DateTime.Today);
            EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            
            BookingDetails = new ObservableCollection<BookingDetail>();
            LoadBookings();
            LoadCustomers();
        }

        // Thêm phương thức mới để cập nhật giá của tất cả các booking details
        private void UpdateBookingDetailsPrices()
        {
            if (BookingDetails == null || DialogBooking == null)
                return;

            int numberOfDays = DialogBooking.CheckoutDate.DayNumber - DialogBooking.CheckinDate.DayNumber;
            
            // Kiểm tra nếu số ngày không hợp lệ
            if (numberOfDays <= 0)
                return;

            // Tạo một DialogBooking mới để đảm bảo binding được cập nhật
            var updatedBooking = new BookingReservation
            {
                BookingReservationId = DialogBooking.BookingReservationId,
                CustomerId = DialogBooking.CustomerId,
                BookingDate = DialogBooking.BookingDate,
                CheckinDate = DialogBooking.CheckinDate,
                CheckoutDate = DialogBooking.CheckoutDate,
                BookingStatus = DialogBooking.BookingStatus,
                Notes = DialogBooking.Notes,
                CreatedDate = DialogBooking.CreatedDate,
                Customer = DialogBooking.Customer
            };

            // Cập nhật lại giá cho từng booking detail
            if (BookingDetails.Count > 0)
            {
                foreach (var detail in BookingDetails)
        {
                    // Cập nhật ngày bắt đầu và kết thúc
                    detail.StartDate = DialogBooking.CheckinDate;
                    detail.EndDate = DialogBooking.CheckoutDate;
                    
                    // Tính lại giá thực tế dựa trên số ngày mới
                    detail.ActualPrice = detail.RoomPrice * numberOfDays;
                }

                // Cập nhật tổng giá của booking
                updatedBooking.TotalPrice = BookingDetails.Sum(bd => bd.ActualPrice);
            }
            else
            {
                updatedBooking.TotalPrice = 0;
            }

            // Gán lại DialogBooking để kích hoạt binding update
            DialogBooking = updatedBooking;
            }
        private void LoadBookings()
        {
            try
            {
                var bookingService = ServiceLocator.Instance.BookingService;
                var bookingList = bookingService.GetAllBookings().ToList();
                Bookings = new ObservableCollection<BookingReservation>(bookingList);
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

        private void LoadCustomers()
        {
            try
            {
                var customerService = ServiceLocator.Instance.CustomerService;
                var customerList = customerService.GetAllCustomers().ToList();
                Customers = new ObservableCollection<Customer>(customerList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAvailableRooms()
        {
            if (StartDate >= EndDate)
            {
                MessageBox.Show("End date must be after start date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var roomService = ServiceLocator.Instance.RoomService;
                var rooms = roomService.GetAvailableRooms(StartDate, EndDate).ToList();
                AvailableRooms = new ObservableCollection<Room>(rooms);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading available rooms: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchBookings()
        {
            try
        {
                var bookingService = ServiceLocator.Instance.BookingService;
                var bookingList = bookingService.SearchBookings(SearchText).ToList();
                Bookings = new ObservableCollection<BookingReservation>(bookingList);
        }
            catch (Exception ex)
        {
                MessageBox.Show($"Error searching bookings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteAddBooking(object parameter)
            {
            DialogBooking = new BookingReservation
                {
                BookingDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CheckinDate = DateOnly.FromDateTime(DateTime.Today),
                CheckoutDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                BookingStatus = 1, // Active by default
                TotalPrice = 0
                };

            BookingDetails = new ObservableCollection<BookingDetail>();
            StartDate = DialogBooking.CheckinDate;
            EndDate = DialogBooking.CheckoutDate;
            LoadAvailableRooms();
            
            IsDialogOpen = true;
        }

        private void ExecuteEditBooking(object parameter)
                {
            if (SelectedBooking != null)
            {
                // Create a clone of the selected booking to avoid modifying the original until save
                DialogBooking = new BookingReservation
                {
                    BookingReservationId = SelectedBooking.BookingReservationId,
                    CustomerId = SelectedBooking.CustomerId,
                    BookingDate = SelectedBooking.BookingDate,
                    CheckinDate = SelectedBooking.CheckinDate,
                    CheckoutDate = SelectedBooking.CheckoutDate,
                    TotalPrice = SelectedBooking.TotalPrice,
                    BookingStatus = SelectedBooking.BookingStatus,
                    Notes = SelectedBooking.Notes,
                    CreatedDate = SelectedBooking.CreatedDate,
                    Customer = SelectedBooking.Customer
                };
                
                // Load booking details
                LoadBookingDetails(SelectedBooking.BookingReservationId);
                
                // Set date range for available rooms
                StartDate = DialogBooking.CheckinDate;
                EndDate = DialogBooking.CheckoutDate;
                LoadAvailableRooms();
                
                IsDialogOpen = true;
        }
        }

        private void ExecuteDeleteBooking(object parameter)
        {
            if (SelectedBooking != null)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this booking? This action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var bookingService = ServiceLocator.Instance.BookingService;
                        bool success = bookingService.DeleteBooking(SelectedBooking.BookingReservationId);

                        if (success)
                        {
                            // Reload bookings to reflect changes
                            LoadBookings();
                            MessageBox.Show("Booking deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
}
                }
            }
        }

        private void ExecuteViewDetails(object parameter)
        {
            if (SelectedBooking != null)
            {
                // Load booking details
                LoadBookingDetails(SelectedBooking.BookingReservationId);
                
                // Show details in a read-only dialog or separate view
                // This could be implemented with a different dialog or view
            }
        }

        private bool CanExecuteBookingAction(object parameter)
        {
            return SelectedBooking != null;
        }

        private void ExecuteSaveBooking(object parameter)
        {
            if (DialogBooking == null) return;

            // Validate booking data
            if (DialogBooking.CustomerId <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DialogBooking.CheckinDate >= DialogBooking.CheckoutDate)
            {
                MessageBox.Show("Checkout date must be after check-in date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (BookingDetails.Count == 0)
            {
                MessageBox.Show("Please add at least one room to the booking.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var bookingService = ServiceLocator.Instance.BookingService;

                // Check if this is an add or update
                if (DialogBooking.BookingReservationId == 0)
                {
                    // Add new booking
                    bookingService.AddBooking(DialogBooking, BookingDetails);
                    MessageBox.Show("Booking added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Update existing booking
                    bookingService.UpdateBooking(DialogBooking, BookingDetails);
                    MessageBox.Show("Booking updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Reload bookings and close dialog
                LoadBookings();
                IsDialogOpen = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteSaveBooking(object parameter)
        {
            return DialogBooking != null &&
                   DialogBooking.CustomerId > 0 &&
                   DialogBooking.CheckinDate < DialogBooking.CheckoutDate &&
                   BookingDetails.Count > 0;
        }

        private void ExecuteCancelDialog(object parameter)
        {
            IsDialogOpen = false;
        }

        private void ExecuteAddRoomToBooking(object parameter)
        {
            if (parameter is Room selectedRoom && DialogBooking != null)
            {
                // Calculate price for the room
                decimal roomPrice = selectedRoom.RoomPricePerDate;
                int numberOfDays = DialogBooking.CheckoutDate.DayNumber - DialogBooking.CheckinDate.DayNumber;
                decimal actualPrice = roomPrice * numberOfDays;

                // Create new booking detail
                var bookingDetail = new BookingDetail
                {
                    RoomId = selectedRoom.RoomId,
                    Room = selectedRoom,
                    RoomPrice = roomPrice,
                    ActualPrice = actualPrice,
                    StartDate = DialogBooking.CheckinDate,
                    EndDate = DialogBooking.CheckoutDate
                };

                // Add to collection
                BookingDetails.Add(bookingDetail);

                // Tạo một DialogBooking mới để đảm bảo binding được cập nhật
                var updatedBooking = new BookingReservation
                {
                    BookingReservationId = DialogBooking.BookingReservationId,
                    CustomerId = DialogBooking.CustomerId,
                    BookingDate = DialogBooking.BookingDate,
                    CheckinDate = DialogBooking.CheckinDate,
                    CheckoutDate = DialogBooking.CheckoutDate,
                    BookingStatus = DialogBooking.BookingStatus,
                    Notes = DialogBooking.Notes,
                    CreatedDate = DialogBooking.CreatedDate,
                    Customer = DialogBooking.Customer,
                    TotalPrice = BookingDetails.Sum(bd => bd.ActualPrice)
                };

                // Gán lại DialogBooking để kích hoạt binding update
                DialogBooking = updatedBooking;
            }
        }

        private void ExecuteRemoveRoomFromBooking(object parameter)
        {
            if (parameter is BookingDetail bookingDetail && DialogBooking != null)
            {
                // Remove from collection
                BookingDetails.Remove(bookingDetail);

                // Tạo một DialogBooking mới để đảm bảo binding được cập nhật
                var updatedBooking = new BookingReservation
                {
                    BookingReservationId = DialogBooking.BookingReservationId,
                    CustomerId = DialogBooking.CustomerId,
                    BookingDate = DialogBooking.BookingDate,
                    CheckinDate = DialogBooking.CheckinDate,
                    CheckoutDate = DialogBooking.CheckoutDate,
                    BookingStatus = DialogBooking.BookingStatus,
                    Notes = DialogBooking.Notes,
                    CreatedDate = DialogBooking.CreatedDate,
                    Customer = DialogBooking.Customer,
                    TotalPrice = BookingDetails.Sum(bd => bd.ActualPrice)
                };

                // Gán lại DialogBooking để kích hoạt binding update
                DialogBooking = updatedBooking;
            }
        }
    }
}
