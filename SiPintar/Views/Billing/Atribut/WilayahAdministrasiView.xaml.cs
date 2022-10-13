using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Views.Billing.Atribut
{
    public partial class WilayahAdministrasiView : UserControl
    {
        public WilayahAdministrasiView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var viewModel = (WilayahAdministrasiViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (viewModel != null && nav != null)
                viewModel.UpdatePage(nav.Label);
        }
    }
}
