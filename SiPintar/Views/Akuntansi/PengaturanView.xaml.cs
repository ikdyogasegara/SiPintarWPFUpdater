using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Views.Akuntansi
{
    /// <summary>
    /// Interaction logic for PengaturanView.xaml
    /// </summary>
    public partial class PengaturanView : UserControl
    {
        private PengaturanViewModel _viewModel;
        public PengaturanView()
        {
            InitializeComponent();
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (PengaturanViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }

        private void Navigation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = (PengaturanViewModel)MenuList.DataContext;
            var nav = (Button)sender;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Tag.ToString());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel = (PengaturanViewModel)MenuList.DataContext;
            var nav = (RadioButton)sender;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Tag.ToString());
        }
    }
}
