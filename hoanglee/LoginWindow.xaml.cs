using hoanglee.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace hoanglee
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;
        
        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
            
            // Set focus to email textbox
            Loaded += (s, e) => txtEmail.Focus();
        }
        
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}