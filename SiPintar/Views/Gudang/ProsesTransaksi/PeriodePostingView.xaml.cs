using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi
{
    /// <summary>
    /// Interaction logic for PeriodePostingView.xaml
    /// </summary>
    public partial class PeriodePostingView : UserControl
    {
        public PeriodePostingView()
        {
            InitializeComponent();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PeriodePostingViewModel model)
                _ = Task.Run(() => ((AsyncCommandBase)model.OnRefreshCommand).ExecuteAsync(null));
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PeriodePostingViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage--;
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null));
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PeriodePostingViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage++;
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null));
            }
        }

        private void BukaPeriode(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PeriodePostingViewModel;
            if (Vm != null && Vm.SelectedData != null && !Vm.SelectedData.Status.Value)
            {
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnOpenBukaPeriodePostingCommand).ExecuteAsync(null));
            }
        }

        private void TutupPeriode(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PeriodePostingViewModel;
            if (Vm != null && Vm.SelectedData != null && Vm.SelectedData.Status.Value)
            {
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnOpenTutupPeriodePostingCommand).ExecuteAsync(null));
            }
        }

        private void OpenAddForm(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as PeriodePostingViewModel;
            if (Vm != null)
            {
                _ = Task.Run(() => ((AsyncCommandBase)Vm.OnOpenAddFormCommand).ExecuteAsync(null));
            }
        }
    }
}
