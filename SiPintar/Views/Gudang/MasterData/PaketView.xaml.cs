using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData
{
    /// <summary>
    /// Interaction logic for PaketView.xaml
    /// </summary>
    public partial class PaketView : UserControl
    {
        public PaketView()
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
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                Vm.OnRefreshCommand.Execute(null);
            }
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage--;
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage++;
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void ButtonOpenAddForm_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnOpenAddFormCommand).ExecuteAsync(null);
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void ButtonOpenEditForm_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                int? IdPaketBarang = ((Button)sender).Tag as int?;
                if (IdPaketBarang.HasValue)
                {
                    _ = ((AsyncCommandBase)Vm.OnOpenEditFormCommand).ExecuteAsync(IdPaketBarang.Value);
                }
            }
        }

        private void ButtonOpenDeleteForm_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                int? IdPaketBarang = ((Button)sender).Tag as int?;
                if (IdPaketBarang.HasValue)
                {
                    _ = ((AsyncCommandBase)Vm.OnOpenDeleteFormCommand).ExecuteAsync(IdPaketBarang.Value);
                }
            }
        }

        private void BtnTerapkanFilter_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PaketViewModel;
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnResetFilterCommand).ExecuteAsync(null);
            }
        }
    }
}
