using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Distribusi.Atribut;

namespace SiPintar.Views.Distribusi.Atribut
{
    /// <summary>
    /// Interaction logic for KelainanGantiMeterView.xaml
    /// </summary>
    public partial class KelainanGantiMeterView : UserControl
    {
        public KelainanGantiMeterView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (KelainanGantiMeterViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }
    }
}
