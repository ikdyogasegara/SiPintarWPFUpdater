using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData
{
    /// <summary>
    /// Interaction logic for DiameterView.xaml
    /// </summary>
    public partial class DiameterView : UserControl
    {
        public DiameterView()
        {
            InitializeComponent();
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(240);
            FilterSection.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                Vm.OnRefreshCommand.Execute(null);
            }
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage--;
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage++;
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void ButtonOpenAddForm_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnOpenAddFormCommand).ExecuteAsync(null);
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void ButtonOpenEditForm_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null && Vm.SelectedData != null)
            {
                _ = ((AsyncCommandBase)Vm.OnOpenEditFormCommand).ExecuteAsync(null);
            }
        }

        private void ButtonOpenDeleteForm_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null && Vm.SelectedData != null)
            {
                _ = ((AsyncCommandBase)Vm.OnOpenDeleteFormCommand).ExecuteAsync(null);
            }
        }

        private void BtnTerapkanFilter_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as DiameterViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnResetFilterCommand).ExecuteAsync(null);
            }
        }
    }
}
