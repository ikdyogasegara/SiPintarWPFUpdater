using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Akuntansi.MasterData;

namespace SiPintar.Views.Akuntansi.MasterData
{
    /// <summary>
    /// Interaction logic for MasterDataKeuanganView.xaml
    /// </summary>
    public partial class MasterDataKeuanganView : UserControl
    {
        public MasterDataKeuanganView()
        {
            InitializeComponent();
        }
        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (MasterDataKeuanganViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }
    }
}
