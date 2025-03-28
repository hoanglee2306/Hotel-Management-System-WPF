using BAL;
using DAL.Models;
using hoanglee.Commands;
using hoanglee.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class CustomerProfileViewModel : ViewModelBase
    {
        private Customer _customer;
        private string _currentPassword;
        private string _newPassword;
        private string _confirmPassword;
        private bool _isEditMode;

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        public string CurrentPassword
        {
            get => _currentPassword;
            set => SetProperty(ref _currentPassword, value);
        }

        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public CustomerProfileViewModel()
        {
            EditCommand = new RelayCommand(ExecuteEdit);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
            ChangePasswordCommand = new RelayCommand(ExecuteChangePassword, CanExecuteChangePassword);

            // Load customer data
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                // Get current customer from the store
                var currentUser = CurrentUserStore.Instance.CurrentUser;
                if (currentUser != null)
                {
                    // Create a clone of the customer to avoid modifying the original until save
                    Customer = new Customer
                    {
                        CustomerId = currentUser.CustomerId,
                        CustomerFullName = currentUser.CustomerFullName,
                        EmailAddress = currentUser.EmailAddress,
                        Telephone = currentUser.Telephone,
                        CustomerBirthday = currentUser.CustomerBirthday,
                        CustomerStatus = currentUser.CustomerStatus,
                        CustomerType = currentUser.CustomerType,
                        Password = currentUser.Password
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteEdit(object parameter)
        {
            IsEditMode = true;
        }

        private void ExecuteSave(object parameter)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(Customer.CustomerFullName))
                {
                    MessageBox.Show("Please enter your full name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update customer profile
                var customerService = ServiceLocator.Instance.CustomerService;
                customerService.UpdateCustomerProfile(Customer);

                // Update the current user in the store
                var currentUser = CurrentUserStore.Instance.CurrentUser;
                currentUser.CustomerFullName = Customer.CustomerFullName;
                currentUser.Telephone = Customer.Telephone;
                currentUser.CustomerBirthday = Customer.CustomerBirthday;

                // Exit edit mode
                IsEditMode = false;

                MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteSave(object parameter)
        {
            return IsEditMode && Customer != null && !string.IsNullOrWhiteSpace(Customer.CustomerFullName);
        }

        private void ExecuteCancel(object parameter)
        {
            // Reload original data and exit edit mode
            LoadCustomerData();
            IsEditMode = false;
        }

        private void ExecuteChangePassword(object parameter)
        {
            try
            {
                // Validate current password
                var authService = ServiceLocator.Instance.AuthService;
                if (!authService.ValidateLogin(Customer.EmailAddress, CurrentPassword))
                {
                    MessageBox.Show("Current password is incorrect.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate new password
                if (string.IsNullOrWhiteSpace(NewPassword) || NewPassword.Length < 6)
                {
                    MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate password confirmation
                if (NewPassword != ConfirmPassword)
                {
                    MessageBox.Show("New password and confirmation do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update password
                Customer.Password = NewPassword;
                var customerService = ServiceLocator.Instance.CustomerService;
                customerService.UpdateCustomerProfile(Customer);

                // Update the current user in the store
                var currentUser = CurrentUserStore.Instance.CurrentUser;
                currentUser.Password = NewPassword;

                // Clear password fields
                CurrentPassword = string.Empty;
                NewPassword = string.Empty;
                ConfirmPassword = string.Empty;

                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteChangePassword(object parameter)
        {
            return !string.IsNullOrWhiteSpace(CurrentPassword) &&
                   !string.IsNullOrWhiteSpace(NewPassword) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   NewPassword == ConfirmPassword;
        }
    }
}