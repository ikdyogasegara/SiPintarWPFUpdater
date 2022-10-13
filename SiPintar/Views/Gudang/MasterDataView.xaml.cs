using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Gudang;

namespace SiPintar.Views.Gudang
{
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
