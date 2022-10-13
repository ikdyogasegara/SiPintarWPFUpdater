using System.Windows.Controls;
using SiPintar.ViewModels;

namespace SiPintar.Views.Personalia
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
            ((PersonaliaViewModel)dataContext).OnBackToMainMenuCommand.Execute("personalia");


            //try
            //{
            //    var dataContext = ((Button)sender).DataContext;

            //    ((PersonaliaViewModel)dataContext).OnOpenLogoutDialogCommand.Execute(null);
            //}
            //catch (Exception err)
            //{
            //    Debug.WriteLine(err);
            //}
        }
    }
}
