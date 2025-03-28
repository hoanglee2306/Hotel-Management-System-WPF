using DAL.Models;
using hoanglee.Commands;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace hoanglee.ViewModels.Dialogs
{
    public class CustomerDialogViewModel : ViewModelBase
    {
        private Customer _customer;
        private string _password;
        private DateTime? _birthdayDate;
        private int _statusIndex;
        private int _userTypeIndex;
        private bool _isNewCustomer;
        
        // Validation error messages
        private string _nameError;
        private string _emailError;
        private string _passwordError;
        private string _phoneError;

        public event EventHandler<bool> RequestClose;

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    ValidatePassword();
                }
            }
        }

        public DateTime? BirthdayDate
        {
            get => _birthdayDate;
            set
            {
                if (SetProperty(ref _birthdayDate, value) && value.HasValue)
                {
                    Customer.CustomerBirthday = DateOnly.FromDateTime(value.Value);
                }
            }
        }

        public int StatusIndex
        {
            get => _statusIndex;
            set
            {
                if (SetProperty(ref _statusIndex, value))
                {
                    Customer.CustomerStatus = value;
                }
            }
        }

        public int UserTypeIndex
        {
            get => _userTypeIndex;
            set
            {
                if (SetProperty(ref _userTypeIndex, value))
                {
                    Customer.CustomerType = value;
                }
            }
        }

        public bool IsNewCustomer
        {
            get => _isNewCustomer;
            set => SetProperty(ref _isNewCustomer, value);
        }

        public string NameError
        {
            get => _nameError;
            set => SetProperty(ref _nameError, value);
        }

        public string EmailError
        {
            get => _emailError;
            set => SetProperty(ref _emailError, value);
        }

        public string PasswordError
        {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }

        public string PhoneError
        {
            get => _phoneError;
            set => SetProperty(ref _phoneError, value);
        }

        public string DialogTitle => IsNewCustomer ? "Add New Customer" : "Edit Customer";

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CustomerDialogViewModel(Customer customer = null)
        {
            IsNewCustomer = customer == null;
            
            // Initialize customer object
            if (customer != null)
            {
                // Edit existing customer
                Customer = new Customer
                {
                    CustomerId = customer.CustomerId,
                    CustomerFullName = customer.CustomerFullName,
                    EmailAddress = customer.EmailAddress,
                    Password = customer.Password,
                    Telephone = customer.Telephone,
                    CustomerBirthday = customer.CustomerBirthday,
                    CustomerStatus = customer.CustomerStatus,
                    CustomerType = customer.CustomerType
                };
                
                // Set UI properties
                if (customer.CustomerBirthday != null)
                {
                    // Convert DateOnly to DateTime
                    BirthdayDate = new DateTime(
                        //customer.CustomerBirthday.Year,
                        //customer.CustomerBirthday.Month,
                        //customer.CustomerBirthday.Day
                    );
                }
                StatusIndex = customer.CustomerStatus;
                UserTypeIndex = customer.CustomerType;
            }
            else
            {
                // Create new customer
                Customer = new Customer
                {
                    CustomerStatus = 1, // Active by default
                    CustomerType = 0,   // Customer by default
                    CustomerBirthday = DateOnly.FromDateTime(DateTime.Today)
                };
                
                // Set UI properties
                BirthdayDate = DateTime.Today;
                StatusIndex = 0; // Active
                UserTypeIndex = 0; // Customer
            }
            
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteSave(object parameter)
        {
            // Validate all fields
            if (!ValidateAll())
            {
                return;
            }
            
            // For new customers, set the password
            if (IsNewCustomer && !string.IsNullOrEmpty(Password))
            {
                Customer.Password = Password;
            }
            
            // Close the dialog with success result
            RequestClose?.Invoke(this, true);
        }

        private bool CanExecuteSave(object parameter)
        {
            // Basic validation for required fields
            return !string.IsNullOrWhiteSpace(Customer.CustomerFullName) &&
                   !string.IsNullOrWhiteSpace(Customer.EmailAddress) &&
                   (IsNewCustomer ? !string.IsNullOrWhiteSpace(Password) : true);
        }

        private void ExecuteCancel(object parameter)
        {
            // Close the dialog with cancel result
            RequestClose?.Invoke(this, false);
        }

        private bool ValidateAll()
        {
            bool isValid = true;
            
            // Validate name
            if (string.IsNullOrWhiteSpace(Customer.CustomerFullName))
            {
                NameError = "Full name is required";
                isValid = false;
            }
            else
            {
                NameError = null;
            }
            
            // Validate email
            if (string.IsNullOrWhiteSpace(Customer.EmailAddress))
            {
                EmailError = "Email address is required";
                isValid = false;
            }
            else if (!IsValidEmail(Customer.EmailAddress))
            {
                EmailError = "Please enter a valid email address";
                isValid = false;
            }
            else
            {
                EmailError = null;
            }
            
            // Validate password for new customers
            if (IsNewCustomer)
            {
                if (!ValidatePassword())
                {
                    isValid = false;
                }
            }
            
            // Validate phone number if provided
            if (!string.IsNullOrWhiteSpace(Customer.Telephone) && !IsValidPhoneNumber(Customer.Telephone))
            {
                PhoneError = "Please enter a valid phone number";
                isValid = false;
            }
            else
            {
                PhoneError = null;
            }
            
            return isValid;
        }

        private bool ValidatePassword()
        {
            if (IsNewCustomer)
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    PasswordError = "Password is required";
                    return false;
                }
                else if (Password.Length < 6)
                {
                    PasswordError = "Password must be at least 6 characters";
                    return false;
                }
                else
                {
                    PasswordError = null;
                    return true;
                }
            }
            
            return true;
        }

        private bool IsValidEmail(string email)
        {
            // Simple email validation using regex
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            // Simple phone validation - allows various formats
            string pattern = @"^[\d\s\+\-\(\)]{10,15}$";
            return Regex.IsMatch(phone, pattern);
        }
    }
}