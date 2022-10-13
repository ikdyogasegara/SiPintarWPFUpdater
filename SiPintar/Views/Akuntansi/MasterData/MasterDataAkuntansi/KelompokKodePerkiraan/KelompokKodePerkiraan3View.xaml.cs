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
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan
{
    /// <summary>
    /// Interaction logic for KodeKelompokPerkiraan3View.xaml
    /// </summary>
    public partial class KelompokKodePerkiraan3View : UserControl
    {
        public KelompokKodePerkiraan3View()
        {
            InitializeComponent();
            PreviewKeyUp += new KeyEventHandler(Shortcut);
        }

        private void Shortcut(object sender, KeyEventArgs e)
        {
            var Vm = DataContext as KelompokKodePerkiraan3ViewModel;
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
            var Vm = DataContext as KelompokKodePerkiraan3ViewModel;
            if (Vm.SelectedData is null)
                e.Handled = true;
        }


        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is KelompokKodePerkiraan3ViewModel Vm)
                _ = ((AsyncCommandBase)Vm.OnLoadCommand).ExecuteAsync(null);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as KelompokKodePerkiraan3ViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage--;
                _ = ((AsyncCommandBase)Vm.OnLoadCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            var Vm = DataContext as KelompokKodePerkiraan3ViewModel;
            if (Vm != null)
            {
                Vm.CurrentPage++;
                _ = ((AsyncCommandBase)Vm.OnLoadCommand).ExecuteAsync(null);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is KelompokKodePerkiraan3ViewModel vm)
            {
                vm.CheckDataUpdate();
            }
        }

        private void PilihSemua_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is KelompokKodePerkiraan3ViewModel vm)
            {
                vm.IsAllSelected = true;
                vm.CheckDataUpdate();
            }
        }

        private void KosongkanSemua_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is KelompokKodePerkiraan3ViewModel vm)
            {
                vm.IsAllSelected = false;
                vm.CheckDataUpdate();
            }
        }
    }
}
