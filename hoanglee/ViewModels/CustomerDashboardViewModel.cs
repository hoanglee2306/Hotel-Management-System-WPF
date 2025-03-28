using hoanglee.Commands;
using hoanglee.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class CustomerDashboardViewModel : ViewModelBase
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
        private CustomerProfileViewModel _profileViewModel;
        private CustomerBookingsViewModel _bookingsViewModel;
        
        public CustomerDashboardViewModel()
        {
            NavigateCommand = new RelayCommand(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);
            
            // Initialize with profile view
            ExecuteNavigate("Profile");
        }
        
        private void ExecuteNavigate(object parameter)
        {
            string destination = parameter as string;
            
            switch (destination)
            {
                case "Profile":
                    if (_profileViewModel == null)
                        _profileViewModel = new CustomerProfileViewModel();
                    
                    CurrentView = new Views.CustomerProfileView { DataContext = _profileViewModel };
                    CurrentViewTitle = "My Profile";
                    break;
                    
                case "Bookings":
                    if (_bookingsViewModel == null)
                        _bookingsViewModel = new CustomerBookingsViewModel();
                    
                    CurrentView = new Views.CustomerBookingsView { DataContext = _bookingsViewModel };
                    CurrentViewTitle = "My Bookings";
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
                if (window is Views.CustomerDashboard)
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