using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi
{
    /// <summary>
    /// Interaction logic for InteraksiPenyusutanView.xaml
    /// </summary>
    public partial class InteraksiPenyusutanView : UserControl
    {
        public InteraksiPenyusutanView()
        {
            InitializeComponent();
            PreviewKeyUp += new KeyEventHandler(Shortcut);
        }
        private void Shortcut(object sender, KeyEventArgs e)
        {
            var Vm = DataContext as InteraksiJenisPersediaanViewModel;
            if (e.Key == Key.F2)
                Vm.OnOpenAddFormCommand.Execute(null);
            else if (e.Key == Key.F3 && Vm.SelectedData != null)
                Vm.OnOpenEditFormCommand.Execute(null);
            else if (e.Key == Key.Delete && Vm.SelectedData != null)
                Vm.OnOpenDeleteFormCommand.Execute(null);
            else if (e.Key == Key.F5)
                Vm.OnLoadCommand.Execute(null);
            else
                return;
        }

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var Vm = DataContext as InteraksiPenyusutanViewModel;

            if (Vm.SelectedData is null)
                e.Handled = true;
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is InteraksiPenyusutanViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnLoadCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is InteraksiPenyusutanViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnLoadCommand).ExecuteAsync(null);
            }
        }
        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is InteraksiPenyusutanViewModel Vm)
                _ = ((AsyncCommandBase)Vm.OnLoadCommand).ExecuteAsync(null);
        }
    }
}
