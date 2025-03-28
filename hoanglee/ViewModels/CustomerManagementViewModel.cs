using BAL;
using DAL.Models;
using hoanglee.Commands;
using hoanglee.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hoanglee.ViewModels
{
    public class CustomerManagementViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _customers;
        private IEnumerable<Customer> _filteredCustomers;
        private Customer _selectedCustomer;
        private string _searchText;
        private int _currentPage = 1;
        private int _itemsPerPage = 10;
        private int _totalPages;
        
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }
        
        public IEnumerable<Customer> FilteredCustomers
        {
            get => _filteredCustomers;
            set => SetProperty(ref _filteredCustomers, value);
        }
        
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }
        
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterCustomers();
                }
            }
        }
        
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (SetProperty(ref _currentPage, value))
                {
                    UpdateFilteredCustomers();
                }
            }
        }
        
        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, value);
        }
        
        public int ActiveCustomersCount => Customers?.Count(c => c.CustomerStatus == 1) ?? 0;
        public int InactiveCustomersCount => Customers?.Count(c => c.CustomerStatus == 0) ?? 0;
        
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        
        public CustomerManagementViewModel()
        {
            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete);
            NextPageCommand = new RelayCommand(ExecuteNextPage, CanExecuteNextPage);
            PreviousPageCommand = new RelayCommand(ExecutePreviousPage, CanExecutePreviousPage);
            
            LoadCustomers();
        }
        
        private void LoadCustomers()
        {
            try
            {
                var customerService = ServiceLocator.Instance.CustomerService;
                var customerList = customerService.GetAllCustomers().ToList();
                Customers = new ObservableCollection<Customer>(customerList);
                
                FilterCustomers();
                OnPropertyChanged(nameof(ActiveCustomersCount));
                OnPropertyChanged(nameof(InactiveCustomersCount));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void FilterCustomers()
        {
            IEnumerable<Customer> filtered = Customers;
            
            // Apply search filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string searchLower = SearchText.ToLower();
                filtered = filtered.Where(c => 
                    c.CustomerFullName.ToLower().Contains(searchLower) ||
                    c.EmailAddress.ToLower().Contains(searchLower) ||
                    (c.Telephone != null && c.Telephone.ToLower().Contains(searchLower)));
            }
            
            // Calculate total pages
            TotalPages = (int)Math.Ceiling(filtered.Count() / (double)_itemsPerPage);
            
            // Ensure current page is valid
            if (CurrentPage > TotalPages && TotalPages > 0)
            {
                CurrentPage = TotalPages;
            }
            else if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            
            // Apply pagination
            UpdateFilteredCustomers(filtered);
            
            // Update commands
            (NextPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (PreviousPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
        
        private void UpdateFilteredCustomers(IEnumerable<Customer> source = null)
        {
            source = source ?? Customers;
            
            if (source == null)
            {
                FilteredCustomers = new List<Customer>();
                return;
            }
            
            // Apply search filter if not already applied
            if (source == Customers && !string.IsNullOrWhiteSpace(SearchText))
            {
                string searchLower = SearchText.ToLower();
                source = source.Where(c => 
                    c.CustomerFullName.ToLower().Contains(searchLower) ||
                    c.EmailAddress.ToLower().Contains(searchLower) ||
                    (c.Telephone != null && c.Telephone.ToLower().Contains(searchLower)));
            }
            
            // Apply pagination
            FilteredCustomers = source
                .Skip((CurrentPage - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();
        }
        
        private void ExecuteAdd(object parameter)
        {
            var dialog = new CustomerDialog();
            
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var customerService = ServiceLocator.Instance.CustomerService;
                    customerService.AddCustomer(dialog.Customer);
                    
                    // Refresh the list
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void ExecuteEdit(object parameter)
        {
            var customer = parameter as Customer;
            if (customer == null) return;
            
            var dialog = new CustomerDialog(customer);
            
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var customerService = ServiceLocator.Instance.CustomerService;
                    customerService.UpdateCustomer(dialog.Customer);
                    
                    // Refresh the list
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void ExecuteDelete(object parameter)
        {
            var customer = parameter as Customer;
            if (customer == null) return;
            
            var result = MessageBox.Show(
                $"Are you sure you want to delete customer '{customer.CustomerFullName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var customerService = ServiceLocator.Instance.CustomerService;
                    bool success = customerService.DeleteCustomer(customer.CustomerId);
                    
                    if (success)
                    {
                        // Refresh the list
                        LoadCustomers();
                    }
                    else
                    {
                        MessageBox.Show("Could not delete customer. It may be referenced by other records.", 
                            "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void ExecuteNextPage(object parameter)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
            }
        }
        
        private bool CanExecuteNextPage(object parameter)
        {
            return CurrentPage < TotalPages;
        }
        
        private void ExecutePreviousPage(object parameter)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }
        
        private bool CanExecutePreviousPage(object parameter)
        {
            return CurrentPage > 1;
        }
    }
}