using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Views.Perencanaan.Atribut
{
    public partial class PaketView : UserControl
    {
        public PaketView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (PaketViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }
    }
}
