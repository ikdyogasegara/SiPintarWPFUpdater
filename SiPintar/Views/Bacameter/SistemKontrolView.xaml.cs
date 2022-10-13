using System.Windows;
using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter
{
    public partial class SistemKontrolView : UserControl
    {
        private SistemKontrolViewModel _viewModel;
        public SistemKontrolView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (SistemKontrolViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }

        private void Navigation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = (SistemKontrolViewModel)MenuList.DataContext;
            var nav = (Button)sender;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Tag.ToString());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel = (SistemKontrolViewModel)MenuList.DataContext;
            var nav = (RadioButton)sender;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Tag.ToString());
        }
    }
}

