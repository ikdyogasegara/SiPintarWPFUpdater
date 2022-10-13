using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Views.Loket.Tagihan.TagihanKolektifAir
{
    public partial class ListTagihanView : UserControl
    {
        public ListTagihanView()
        {
            InitializeComponent();

            CheckButton();
        }

        private void CheckButton()
        {
            var _viewModel = (CariTagihanViewModel)DataContext;

            if (_viewModel == null)
                return;

            // check go to detail page...
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                vm.CheckDataUpdate();
            }
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                    false,
                    true,
                    "LoketRootDialog",
                    "Hapus Data",
                    "Data yang dipilih akan terhapus. Apakah ingin melanjutkan ?",
                    "confirmation",
                    vm.OnConfirmRemoveFromListCommand,
                    moduleName: "loket");
            }
        }

        private void CekTagihan_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                _ = ((AsyncCommandBase)vm.OnCekTagihanCommand).ExecuteAsync(null);
            }
        }

        private void CekRiwayat_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CariTagihanViewModel vm)
            {
                #region setData
                if (vm.ListSelectedPelangganAir != null && vm.ListSelectedPelangganAir.Count > 0)
                {
                    var selectedPelangganAir = new ObservableCollection<MasterPelangganAirWpf>();
                    if (vm.ListSelectedPelangganAir != null)
                    {
                        foreach (var item in vm.ListSelectedPelangganAir)
                        {
                            if (item.IsSelected == true && selectedPelangganAir.FirstOrDefault(i => i.IdPelangganAir == item.IdPelangganAir) == null)
                                selectedPelangganAir.Add(item);
                        }
                    }

                    vm.Parent.ListSelectedPelangganAir = selectedPelangganAir;
                }

                if (vm.ListSelectedNonAir != null && vm.ListSelectedNonAir.Count > 0)
                {
                    var selectedNonAir = new ObservableCollection<RekeningNonAirWpf>();
                    if (vm.ListSelectedNonAir != null)
                    {
                        foreach (var item in vm.ListSelectedNonAir)
                        {
                            if (item.IsSelected == true && selectedNonAir.FirstOrDefault(i => i.IdNonAir == item.IdNonAir) == null)
                                selectedNonAir.Add(item);
                        }
                    }

                    vm.Parent.ListSelectedNonAir = selectedNonAir;
                }
                #endregion

                vm.ParentPage.PageSebelumRiwayat = "ListTagihan";

                if (vm.Parent.ListSelectedPelangganAir != null && vm.Parent.ListSelectedPelangganAir.Count > 0)
                {
                    vm.Parent.UpdatePage("RiwayatTransaksi");
                }

                if (vm.Parent.ListSelectedNonAir != null && vm.Parent.ListSelectedNonAir.Count > 0)
                {
                    vm.Parent.UpdatePage("RiwayatTransaksi");
                }
            }
        }
    }
}
