using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Views.Loket
{
    public partial class BantuanView : UserControl
    {
        public BantuanView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (BantuanViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }
    }
}
