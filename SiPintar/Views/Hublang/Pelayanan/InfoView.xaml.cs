using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan
{
    public partial class InfoView : UserControl
    {
        public InfoView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var viewModel = (InfoViewModel)DataContext;
            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (viewModel != null && nav != null)
                _ = viewModel.UpdatePageAsync(nav.Label);
        }
    }
}
