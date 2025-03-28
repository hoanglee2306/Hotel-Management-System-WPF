using hoanglee.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace hoanglee.Views
{
    public partial class CustomerProfileView : UserControl
    {
        public CustomerProfileView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerProfileViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                
                if (passwordBox == CurrentPasswordBox)
                {
                    viewModel.CurrentPassword = passwordBox.Password;
                }
                else if (passwordBox == NewPasswordBox)
                {
                    viewModel.NewPassword = passwordBox.Password;
                }
                else if (passwordBox == ConfirmPasswordBox)
                {
                    viewModel.ConfirmPassword = passwordBox.Password;
                }
            }
        }
    }
}