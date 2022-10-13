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
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.Views.Akuntansi.Jurnal
{
    /// <summary>
    /// Interaction logic for RekeningView.xaml
    /// </summary>
    public partial class RekeningView : UserControl
    {
        public RekeningView()
        {
            InitializeComponent();
        }
        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (RekeningViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.Label);
        }

    }
}
