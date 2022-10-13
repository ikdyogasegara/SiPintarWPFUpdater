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
using SiPintar.ViewModels.Akuntansi;


namespace SiPintar.Views.Akuntansi
{
    /// <summary>
    /// Interaction logic for MasterDataView.xaml
    /// </summary>
    public partial class MasterDataView : UserControl
    {
        public MasterDataView()
        {
            InitializeComponent();
        }
        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (MasterDataViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
