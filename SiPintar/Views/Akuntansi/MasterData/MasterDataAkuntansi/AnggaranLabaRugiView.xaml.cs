using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for AnggaranLabaRugiView.xaml
    /// </summary>
    public partial class AnggaranLabaRugiView : UserControl
    {
        public AnggaranLabaRugiView()
        {
            InitializeComponent();
            IsLoadingUraian.Visibility = Visibility.Collapsed;
        }

        private void DataUaraian_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsLoadingUraian.Visibility = Visibility.Visible;
            IsEmptyUraian.Visibility = Visibility.Visible;

            if (DataContext is AnggaranLabaRugiViewModel viewModel)
            {
                NamaJenisLbl.Text = $"Jenis: {viewModel.SelectedDataJenis.Value}";
                SaldoAnggaranClickAble();

                viewModel.DataUraianList = new ObservableCollection<AnggaranLabaRugiDto>(viewModel.DataList.Where(x => x.Uraian == viewModel.SelectedDataJenis.Value));
                if (viewModel.DataUraianList?.Count > 0)
                {
                    IsEmptyUraian.Visibility = Visibility.Collapsed;
                }
            }

            IsLoadingUraian.Visibility = Visibility.Collapsed;
        }

        private void SaldoAnggaranClickAble()
        {
            if (DataContext is AnggaranLabaRugiViewModel viewModel)
            {
                var isEnabled = true;
                if (!viewModel.IsKonsolidasi)
                {
                    isEnabled = viewModel.SelectedDataJenis.Value != null && viewModel.SelectedWilayah.NamaWilayah != "KANTOR PUSAT";
                }

                CtxMenuSaldoAnggaran.IsEnabled = isEnabled;
                BtnSaldoAnggaran.IsEnabled = isEnabled;
            }
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(464);
        }

        private void RefreshCommand()
        {
            DataJenis.UnselectAllCells();
            IsEmptyUraian.Visibility = Visibility.Visible;

            if (DataContext is AnggaranLabaRugiViewModel viewModel)
            {
                _ = ((AsyncCommandBase)viewModel.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is AnggaranLabaRugiViewModel vm && vm.SelectedDataJenis.Key is null)
                e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AnggaranLabaRugiViewModel viewModel && viewModel.SelectedDataUraian != null)
            {
                viewModel.OnOpenEditFormCommand.Execute(null);
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                false,
                false,
                "AkuntansiRootDialog",
                $"Anggaran Laba Rugi",
                $"Data uraian masih belum dipilih !",
                "warning",
                "Ok",
                false,
                "Akuntansi");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCommand();
        }
    }
}
