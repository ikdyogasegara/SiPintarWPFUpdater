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
    public partial class SearchDialog : UserControl
    {
        public SearchDialog(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            PreviewKeyUp += new KeyEventHandler(SearchDialog_PreviewKeyUp);

            Nama.KeyUp += Cari_KeyUp;
            NoSamb.KeyUp += Cari_KeyUp;
            Alamat.KeyUp += Cari_KeyUp;
            NoRekening.KeyUp += Cari_KeyUp;

            Nama.Clear();
            Alamat.Clear();
            NoSamb.Clear();
            NoRekening.Clear();
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
                if (vm.ListSearchPelangganAir != null && vm.ListSearchPelangganAir.Any(c => c.IsSelected))
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

        private void Cari_KeyUp(object sender, KeyEventArgs e)
        {
            CheckButtonCariPelanggan();
        }

        private void CheckButtonCariPelanggan()
        {
            if (Nama.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (NoSamb.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (Alamat.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (NoRekening.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            CariPelangganButton.IsEnabled = false;
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CurrentPage = 1;
                SearchData();
            }
        }

        private void SearchData()
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                if (string.IsNullOrEmpty(NoSamb.Text) && string.IsNullOrEmpty(Nama.Text) && string.IsNullOrEmpty(Alamat.Text) & string.IsNullOrEmpty(NoRekening.Text) && vm.KolektifForm == null)
                {
                    return;
                }

                var param = new Dictionary<string, dynamic>()
                {
                    { "PageSize", vm.LimitData.Key },
                    { "CurrentPage", vm.CurrentPage }
                };

                if (!string.IsNullOrEmpty(NoSamb.Text))
                    param.Add("NoSamb", NoSamb.Text);
                if (!string.IsNullOrEmpty(Nama.Text))
                    param.Add("Nama", Nama.Text);
                if (!string.IsNullOrEmpty(Alamat.Text))
                    param.Add("Alamat", Alamat.Text);
                if (!string.IsNullOrEmpty(NoRekening.Text))
                    param.Add("NoRekening", NoRekening.Text);
                if (vm.KolektifForm != null && vm.KolektifForm.IdKolektif != null)
                    param.Add("IdKolektif", vm.KolektifForm.IdKolektif);

                _ = ((AsyncCommandBase)vm.OnSearchPelangganAirCommand).ExecuteAsync(param);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CurrentPage = 1;
                SearchData();
            }
        }

        private void Bersihkan_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                NoSamb.Clear();
                Nama.Clear();
                Alamat.Clear();
                NoRekening.Clear();
                vm.KolektifForm = null;
                vm.ListSearchPelangganAir = null;
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
                if (vm.IsLoading || vm.ListSearchPelangganAir == null || vm.ListSearchPelangganAir.Count == 0)
                {
                    return;
                }

                vm.ListSelectedPelangganAir ??= new ObservableCollection<MasterPelangganAirWpf>();

                var selectedPelangganAir = new ObservableCollection<MasterPelangganAirWpf>(vm.ListSelectedPelangganAir);
                if (vm.ListSearchPelangganAir != null)
                {
                    foreach (var item in vm.ListSearchPelangganAir)
                    {
                        if (item.IsSelected == true && selectedPelangganAir.FirstOrDefault(i => i.IdPelangganAir == item.IdPelangganAir) == null)
                            selectedPelangganAir.Add(item);
                    }
                    vm.ListSelectedPelangganAir = selectedPelangganAir;
                }

                vm.IsEmptyAir = selectedPelangganAir.Count == 0;
                vm.IsEmpty = selectedPelangganAir.Count == 0;

                vm.ListSelectedNonAir = null;
                vm.IsEmptyNonAir = true;

                vm.CheckDataUpdate();
                DialogHelper.CloseDialog(false, true, "LoketRootDialog");
            }
        }

        private void LangsungCekTagihan_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                if (vm.IsLoading || vm.ListSearchPelangganAir == null || vm.ListSearchPelangganAir.Count == 0)
                {
                    return;
                }

                vm.ListSelectedPelangganAir ??= new ObservableCollection<MasterPelangganAirWpf>();

                var selectedPelangganAir = new ObservableCollection<MasterPelangganAirWpf>(vm.ListSelectedPelangganAir);
                if (vm.ListSearchPelangganAir != null)
                {
                    foreach (var item in vm.ListSearchPelangganAir)
                    {
                        if (item.IsSelected == true && selectedPelangganAir.FirstOrDefault(i => i.IdPelangganAir == item.IdPelangganAir) == null)
                            selectedPelangganAir.Add(item);
                    }
                    vm.ListSelectedPelangganAir = selectedPelangganAir;
                }

                vm.IsEmptyAir = selectedPelangganAir.Count == 0;
                vm.IsEmpty = selectedPelangganAir.Count == 0;

                vm.ListSelectedNonAir = null;
                vm.IsEmptyNonAir = true;

                vm.CheckDataUpdate();
                DialogHelper.CloseDialog(false, true, "LoketRootDialog");
                _ = ((AsyncCommandBase)vm.OnCekTagihanCommand).ExecuteAsync(null);
            }
        }

        private void Kolektif_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                if (vm.KolektifForm != null)
                {
                    CariPelangganButton.IsEnabled = true;
                }
            }
        }
    }
}
