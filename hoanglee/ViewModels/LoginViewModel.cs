using BAL;
using hoanglee.Commands;
using hoanglee.Models;
using hoanglee.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        private string _password;
        private string _errorMessage;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    // Clear error message when password changes
                    ErrorMessage = string.Empty;
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand ExitCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            ExitCommand = new RelayCommand(ExecuteExit);
        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            try
            {
                var authService = ServiceLocator.Instance.AuthService;
                
                // Validate login
                if (authService.ValidateLogin(Email, Password))
                {
                    // Get user details and check role
                    var user = authService.GetCustomerByEmail(Email);
                    bool isAdmin = authService.IsAdmin(Email);
                    
                    // Store current user
                    CurrentUserStore.Instance.SetCurrentUser(user, isAdmin);
                    
                    // Open appropriate window based on role
                    if (isAdmin)
                    {
                        var adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                    }
                    else
                    {
                        var customerDashboard = new CustomerDashboard();
                        customerDashboard.Show();
                    }
                    
                    // Close login window
                    CloseLoginWindow();
                }
                else
                {
                    ErrorMessage = "Invalid email or password. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
        }

        private void ExecuteExit(object parameter)
        {
            Application.Current.Shutdown();
        }

        private void CloseLoginWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}