using System.Windows.Controls;
using SiPintar.ViewModels;

namespace SiPintar.Views.Loket
{
    /// <summary>
    /// Interaction logic for AppHeaderView.xaml
    /// </summary>
    public partial class AppHeaderView : UserControl
    {
        public AppHeaderView()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            ((LoketViewModel)dataContext).OnBackToMainMenuCommand.Execute("loket");
        }
    }
}
