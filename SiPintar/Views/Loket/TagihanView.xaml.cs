using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Views.Loket
{
    public partial class TagihanView : UserControl
    {
        public TagihanView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var viewModel = (TagihanViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (viewModel != null && nav != null)
                viewModel.UpdatePage(nav.Label);
        }
    }
}
