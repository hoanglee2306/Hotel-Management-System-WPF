using hoanglee.Commands;
using hoanglee.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class AdminDashboardViewModel : ViewModelBase
    {
        private UserControl _currentView;
        private string _currentViewTitle;
        
        public UserControl CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }
        
        public string CurrentViewTitle
        {
            get => _currentViewTitle;
            set => SetProperty(ref _currentViewTitle, value);
        }
        
        public string CurrentUserInitials => GetUserInitials();
        
        public DAL.Models.Customer CurrentUser => CurrentUserStore.Instance.CurrentUser;
        
        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }
        
        // View models for different sections
        private CustomerManagementViewModel _customerViewModel;
        private RoomManagementViewModel _roomViewModel;
        private BookingManagementViewModel _bookingViewModel;
        private ReportViewModel _reportViewModel;
        
        public AdminDashboardViewModel()
        {
            NavigateCommand = new RelayCommand(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);
            
            // Initialize with customer management view
            ExecuteNavigate("Customers");
        }
        
        private void ExecuteNavigate(object parameter)
        {
            string destination = parameter as string;
            
            switch (destination)
            {
                case "Customers":
                    if (_customerViewModel == null)
                        _customerViewModel = new CustomerManagementViewModel();
                    
                    CurrentView = new Views.CustomerManagementView { DataContext = _customerViewModel };
                    CurrentViewTitle = "Customer Management";
                    break;
                    
                case "Rooms":
                    if (_roomViewModel == null)
                        _roomViewModel = new RoomManagementViewModel();
                    
                    CurrentView = new Views.RoomManagementView { DataContext = _roomViewModel };
                    CurrentViewTitle = "Room Management";
                    break;
                    
                case "Bookings":
                    if (_bookingViewModel == null)
                        _bookingViewModel = new BookingManagementViewModel();
                    
                    CurrentView = new Views.BookingManagementView { DataContext = _bookingViewModel };
                    CurrentViewTitle = "Booking Management";
                    break;
                    
                case "Reports":
                    if (_reportViewModel == null)
                        _reportViewModel = new ReportViewModel();
                    
                    CurrentView = new Views.ReportView { DataContext = _reportViewModel };
                    CurrentViewTitle = "Reports & Statistics";
                    break;
            }
        }
        
        private void ExecuteLogout(object parameter)
        {
            // Confirm logout
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                
            if (result == MessageBoxResult.Yes)
            {
                // Clear current user
                CurrentUserStore.Instance.Logout();
                
                // Open login window
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                
                // Close current window
                CloseCurrentWindow();
            }
        }
        
        private string GetUserInitials()
        {
            if (CurrentUser == null || string.IsNullOrWhiteSpace(CurrentUser.CustomerFullName))
                return "?";
                
            var nameParts = CurrentUser.CustomerFullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length == 0)
                return "?";
                
            if (nameParts.Length == 1)
                return nameParts[0].Substring(0, 1).ToUpper();
                
            return (nameParts[0].Substring(0, 1) + nameParts[nameParts.Length - 1].Substring(0, 1)).ToUpper();
        }
        
        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.AdminDashboard)
                {
                    window.Close();
                    break;
                }
            }
        }
        
        public void OnWindowClosing(CancelEventArgs e)
        {
            // If this is the last window, open the login window
            if (Application.Current.Windows.Count == 1)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}