using hoanglee.ViewModels;
using System.Windows;

namespace hoanglee.Views
{
    public partial class CustomerDashboard : Window
    {
        private readonly CustomerDashboardViewModel _viewModel;
        
        public CustomerDashboard()
        {
            InitializeComponent();
            _viewModel = new CustomerDashboardViewModel();
            DataContext = _viewModel;
            
            // Handle window closing
            Closing += (s, e) => _viewModel.OnWindowClosing(e);
        }
    }
}