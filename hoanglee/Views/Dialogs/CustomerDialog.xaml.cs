using DAL.Models;
using hoanglee.ViewModels.Dialogs;
using System.Windows;
using System.Windows.Controls;

namespace hoanglee.Views.Dialogs
{
    public partial class CustomerDialog : Window
    {
        private readonly CustomerDialogViewModel _viewModel;
        
        public Customer Customer => _viewModel.Customer;
        
        public CustomerDialog(Customer customer = null)
        {
            InitializeComponent();
            
            _viewModel = new CustomerDialogViewModel(customer);
            _viewModel.RequestClose += (sender, args) => 
            {
                DialogResult = args;
                Close();
            };
            
            DataContext = _viewModel;
        }
        
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerDialogViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}