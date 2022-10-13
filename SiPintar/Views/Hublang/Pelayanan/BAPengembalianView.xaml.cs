using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan
{
    /// <summary>
    /// Interaction logic for BAPengembalianView.xaml
    /// </summary>
    public partial class BAPengembalianView : UserControl
    {
        public BAPengembalianView()
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
            FilterWrapper.Width = new GridLength(240);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is BaPengembalianViewModel model)
                _ = Task.Run(() => ((AsyncCommandBase)model.OnRefreshCommand).ExecuteAsync(null));
        }
        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as BaPengembalianViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage--;
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null));
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as BaPengembalianViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage++;
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null));
            }
        }

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var Vm = DataContext as BaPengembalianViewModel;
            if (Vm.SelectedData is null)
                e.Handled = true;
        }
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (BaPengembalianViewModel)DataGridBeritaAcara.DataContext;
            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridBeritaAcara }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportExcelCommand).ExecuteAsync(param);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

