using hoanglee.ViewModels;
using System.Windows;

namespace hoanglee.Views
{
    public partial class AdminDashboard : Window
    {
        private readonly AdminDashboardViewModel _viewModel;
        
        public AdminDashboard()
        {
            InitializeComponent();
            _viewModel = new AdminDashboardViewModel();
            DataContext = _viewModel;
            
            // Handle window closing
            Closing += (s, e) => _viewModel.OnWindowClosing(e);
        }
    }
}