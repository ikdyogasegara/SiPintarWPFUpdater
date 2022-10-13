using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Views.Loket.Tagihan.TagihanKolektifAir
{
    public partial class SearchDialogNonAir : UserControl
    {
        public SearchDialogNonAir(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            PreviewKeyUp += new KeyEventHandler(SearchDialog_PreviewKeyUp);

            Nama.Clear();
            Alamat.Clear();
            NomorNonAir.Clear();
        }

        private void SearchDialog_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

            if (e.Key == Key.Enter)
            {
                var ischecked = IsChecked();
                if (ischecked == false)
                {
                    if (DataContext is CariTagihanViewModel vm)
                    {
                        if (CariPelangganButton.IsEnabled == true)
                        {
                            vm.CurrentPage = 1;
                            SearchData();
                        }
                    }
                }
                else
                {
                    Tambah_Click(null, null);
                }
            }
        }

        private bool IsChecked()
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                if (vm.ListSearchNonAir != null && vm.ListSearchNonAir.Any(c => c.IsSelected))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private void SearchData()
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "PageSize", vm.LimitData.Key },
                    { "CurrentPage", vm.CurrentPage }
                };

                if (vm.FilterStatusPelunasan != null && vm.FilterStatusPelunasan == "Belum Lunas")
                    param.Add("StatusTransaksi", false);
                if (vm.FilterStatusPelunasan != null && vm.FilterStatusPelunasan == "Sudah Lunas")
                    param.Add("StatusTransaksi", true);
                if (!string.IsNullOrEmpty(NomorNonAir.Text))
                    param.Add("NomorNonAir", NomorNonAir.Text);
                if (!string.IsNullOrEmpty(Nama.Text))
                    param.Add("Nama", Nama.Text);
                if (!string.IsNullOrEmpty(Alamat.Text))
                    param.Add("Alamat", Alamat.Text);
                if (vm.JenisNonAirForm != null && vm.JenisNonAirForm.IdJenisNonAir != null)
                    param.Add("IdJenisNonAir", vm.JenisNonAirForm.IdJenisNonAir);

                _ = ((AsyncCommandBase)vm.OnSearchNonAirCommand).ExecuteAsync(param);
            }
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CurrentPage = 1;
                SearchData();
            }
        }

        private void Search_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CurrentPage = 1;
                SearchData();
            }
        }

        private void Bersihkan_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                NomorNonAir.Clear();
                Nama.Clear();
                Alamat.Clear();
                vm.JenisNonAirForm = null;
                vm.ListSearchNonAir = null;
                CariPelangganButton.IsEnabled = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CheckDataUpdate();
            }
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CurrentPage--;
                SearchData();
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CurrentPage++;
                SearchData();
            }
        }

        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                if (vm.IsLoading || vm.ListSearchNonAir == null || vm.ListSearchNonAir.Count == 0)
                {
                    return;
                }

                vm.ListSelectedNonAir ??= new ObservableCollection<RekeningNonAirWpf>();

                var selectedNonAir = new ObservableCollection<RekeningNonAirWpf>(vm.ListSelectedNonAir);
                if (vm.ListSearchNonAir != null)
                {
                    foreach (var item in vm.ListSearchNonAir)
                    {
                        if (item.IsSelected == true && selectedNonAir.FirstOrDefault(i => i.IdNonAir == item.IdNonAir) == null)
                            selectedNonAir.Add(item);
                    }
                    vm.ListSelectedNonAir = selectedNonAir;
                }

                vm.IsEmptyNonAir = selectedNonAir.Count == 0;
                vm.IsEmpty = selectedNonAir.Count == 0;

                vm.ListSelectedPelangganAir = null;
                vm.IsEmptyAir = true;

                vm.CheckDataUpdate();
                DialogHelper.CloseDialog(false, true, "LoketRootDialog");
            }
        }

        private void LangsungCekTagihan_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                if (vm.IsLoading || vm.ListSearchNonAir == null || vm.ListSearchNonAir.Count == 0)
                {
                    return;
                }

                vm.ListSelectedNonAir ??= new ObservableCollection<RekeningNonAirWpf>();

                var selectedNonAir = new ObservableCollection<RekeningNonAirWpf>(vm.ListSelectedNonAir);
                if (vm.ListSearchNonAir != null)
                {
                    foreach (var item in vm.ListSearchNonAir)
                    {
                        if (item.IsSelected == true && selectedNonAir.FirstOrDefault(i => i.IdNonAir == item.IdNonAir) == null)
                            selectedNonAir.Add(item);
                    }
                    vm.ListSelectedNonAir = selectedNonAir;
                }

                vm.IsEmptyNonAir = selectedNonAir.Count == 0;
                vm.IsEmpty = selectedNonAir.Count == 0;

                vm.ListSelectedPelangganAir = null;
                vm.IsEmptyAir = true;

                vm.CheckDataUpdate();
                DialogHelper.CloseDialog(false, true, "LoketRootDialog");
                _ = ((AsyncCommandBase)vm.OnCekTagihanCommand).ExecuteAsync(null);
            }
        }
    }
}
