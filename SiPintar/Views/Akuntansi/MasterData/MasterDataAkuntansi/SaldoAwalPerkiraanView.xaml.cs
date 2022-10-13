using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for SaldoAwalPerkiraanView.xaml
    /// </summary>
    public partial class SaldoAwalPerkiraanView : UserControl
    {

        public SaldoAwalPerkiraanView()
        {
            InitializeComponent();
            HideFilter_Click(null, null);
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(230);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var _viewModel = DataContext as SaldoAwalPerkiraanViewModel;
            if (_viewModel.SelectedData is null)
                e.Handled = true;
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is SaldoAwalPerkiraanViewModel _viewModel)
                _ = ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
        }

        private void ComboTahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is SaldoAwalPerkiraanViewModel _viewModel)
            {
                _ = ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
            }
        }
    }
}
