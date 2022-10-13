using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Billing.Supervisi
{
    public partial class RekeningAirView : UserControl
    {
        private SupervisiView _parent { get; set; }
        public void SetParent(SupervisiView parent) => _parent = parent;

        public RekeningAirView()
        {
            InitializeComponent();
            ShowFilter_Click();
            DataContextChanged += RekeningAirView_DataContextChanged;
            ColumnVerifikasi.Visibility = AppSetting.LockVerifikasiBacameter == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RekeningAirView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                vm.UpdateDataGridEvent += UpdateDataGrid;
                PreviewKeyUp += new KeyEventHandler(ShortCutGrid);
            }
        }

        private void UpdateDataGrid()
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                var saveList = vm.RekeningAirList;
                var saveId = vm.SelectedData?.IdPelangganAir;
                vm.SelectedData = null;
                vm.RekeningAirList = null;
                vm.RekeningAirList = saveList;
                vm.SelectedData = vm.RekeningAirList.FirstOrDefault(x => x.IdPelangganAir == saveId);
                if (vm.SelectedData != null)
                {
                    TabelRekeningair.ScrollIntoView(vm.SelectedData);
                }
            }
        }

        private void ShortCutGrid(object sender, KeyEventArgs e)
        {
            var ctx = DataContext as RekeningAirViewModel;

            if (ctx.SelectedData == null) return;
            switch (e.Key)
            {
                case Key.F2:
                    if (ctx.SelectedDataPeriode.Status == true && ctx.SelectedData.FlagPublishWpf == false)
                        _ = ((AsyncCommandBase)ctx.OnUpdatePublishCommand).ExecuteAsync(null);
                    break;
                case Key.F4:
                    if (ctx.SelectedDataPeriode.Status == true && ctx.SelectedData.FlagPublishWpf == true)
                        _ = ((AsyncCommandBase)ctx.OnUpdatePublishCommand).ExecuteAsync(null);
                    break;
                case Key.F5:
                    _ = ((AsyncCommandBase)ctx.OnOpenRiwayatPiutangCommand).ExecuteAsync(null);
                    break;
                default:
                    return;
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            // Method intentionally left empty.
        }

        #region == Show/Hide Filter Section

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(210);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        #endregion

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            CheckStatus.IsChecked = false;
            ComboStatus.SelectedIndex = -1;

            CheckKolektif.IsChecked = false;
            ComboKolektif.SelectedIndex = -1;

            CheckFlag.IsChecked = false;
            ComboFlag.SelectedIndex = -1;

            CheckTglBayar.IsChecked = false;

            CheckRekair.IsChecked = false;
            TxtRekeningAirAwal.Text = null;
            TxtRekeningAirAkhir.Text = null;

            CheckPakai.IsChecked = false;
            TxtPakaiAwal.Text = null;
            TxtPakaiAkhir.Text = null;

            CheckKasir.IsChecked = false;
            ComboKasir.SelectedIndex = -1;

            CheckLoket.IsChecked = false;
            ComboLoket.SelectedIndex = -1;

            CheckNama.IsChecked = false;
            txtNama.Text = null;

            CheckNosamb.IsChecked = false;
            TxtNosamb.Text = null;

            CheckNorek.IsChecked = false;
            TxtNorek.Text = null;

            CheckGolongan.IsChecked = false;
            ComboGolongan.SelectedIndex = -1;

            CheckAlamat.IsChecked = false;
            TxtAlamat.Text = null;

            ComboWilayah.IsEnabled = false;
            ComboWilayah.SelectedIndex = -1;

            CheckKelurahan.IsChecked = false;
            ComboKelurahan.SelectedIndex = -1;

            CheckRayon.IsChecked = false;
            ComboRayon.SelectedIndex = -1;

            CheckBlok.IsChecked = false;
            ComboBlok.SelectedIndex = -1;

            CheckCabang.IsChecked = false;
            ComboCabang.SelectedIndex = -1;

            CheckMerekWM.IsChecked = false;
            ComboMerekWM.SelectedIndex = -1;

            CheckSeriWM.IsChecked = false;
            TxtSeriWM.Text = null;

            CheckDiameter.IsChecked = false;
            ComboDiameter.SelectedIndex = -1;

            CheckKondisiWM.IsChecked = false;
            ComboKondisiWM.SelectedIndex = -1;

            CheckKelainan.IsChecked = false;
            ComboKelainan.SelectedIndex = -1;

            CheckPetugasBaca.IsChecked = false;
            ComboPetugasBaca.SelectedIndex = -1;
        }

        private void Hitung_Ulang_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as RekeningAirViewModel;
            ctx.IsHitungUlangSemua = false;
            _ = Task.Run(() => ((AsyncCommandBase)ctx.OnCalculationCommand).ExecuteAsync(null));
        }

        private void Hitung_Ulang_Semua_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as RekeningAirViewModel;
            ctx.IsHitungUlangSemua = true;
            _ = Task.Run(() => ((AsyncCommandBase)ctx.OnCalculationCommand).ExecuteAsync(null));
        }

        private void PerbaruiSemuaRekening_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as RekeningAirViewModel;
            ctx.IsPerbaruiSemua = true;
            _ = Task.Run(() => ((AsyncCommandBase)ctx.OnPerbaruiDataRekeningCommand).ExecuteAsync(null));
        }

        private void PerbaruiRekening_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as RekeningAirViewModel;
            ctx.IsPerbaruiSemua = false;
        }

        private void ComboPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel { SelectedDataPeriode: { } } vm)
            {
                LabelJudul.Text = "Periode Rekening";
                if (vm.SelectedDataPeriode.Status == false)
                {
                    LabelJudul.Text = "Periode Rekening (Tutup)";
                }
                vm.RekeningAirList = new ObservableCollection<RekeningAirWpf>();
                vm.RekeningAirDetailList = null;

                //_ = ((AsyncCommandBase)vm.OnFilterCommand).ExecuteAsync(null);
            }
        }

        private void Taksasi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Taksasi Pemakaian Otomatis",
                    $@"Apakah Anda yakin ingin melakukan flag taksasi milik '{vm.SelectedData.Nama}' pada periode '{vm.SelectedData.NamaPeriode}'?",
                    "warning",
                    vm.OnSubmitTaksasiCommand,
                    "Ok",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        private void MeterTerbalik_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Meter Terbalik",
                    $@"Apakah Anda yakin ingin melakukan flag meter terbalik milik '{vm.SelectedData.Nama}' pada periode '{vm.SelectedData.NamaPeriode}'?",
                    "warning",
                    vm.OnSubmitMeterTerbalikCommand,
                    "Ok",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        private void StanKembaliMuda_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Set Stan Kembali Muda",
                    $@"Apakah Anda yakin ingin melakukan set stan kembali muda milik '{vm.SelectedData.Nama}' pada periode '{vm.SelectedData.NamaPeriode}'?",
                    "warning",
                    vm.OnSubmitStanKembaliMudaCommand,
                    "Ok",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        private void SetBelumDibaca_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Set Belum Dibaca",
                    $@"Apakah Anda yakin ingin melakukan set belum dibaca milik '{vm.SelectedData.Nama}' pada periode '{vm.SelectedData.NamaPeriode}'?",
                    "warning",
                    vm.OnSubmitSetBelumBacaCommand,
                    "Ok",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        private void PermintaanBacaUlang_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Permintaan Baca Ulang",
                    $@"Apakah Anda yakin ingin melakukan permintaan baca ulang milik '{vm.SelectedData.Nama}' pada periode '{vm.SelectedData.NamaPeriode}'?",
                    "warning",
                    vm.OnSubmitPermintaanBacaUlangRequestCommand,
                    "Ok",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        private void BatalkanPermintaanBacaUlang_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    $"Batalkan Permintaan Baca Ulang",
                    $@"Apakah Anda yakin ingin melakukan pembatalan permintaan baca ulang milik '{vm.SelectedData.Nama}' pada periode '{vm.SelectedData.NamaPeriode}'?",
                    "warning",
                    vm.OnSubmitPermintaanBacaUlangBatalCommand,
                    "Ok",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
            }
        }

        #region Export Function

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("ExportMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            _ = (RekeningAirViewModel)TabelRekeningair.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(TabelRekeningair);

            var fileName = $"Rekening Air";
            var titlePage = $"Data Rekening Air";

            switch (fileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, "BillingRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(sanitizedData, fileName, "BillingRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(sanitizedData, fileName, "BillingRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(sanitizedData, fileName, "BillingRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(sanitizedData, fileName, "BillingRootDialog");
                    break;
                default:
                    break;
            }
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<RekeningAirWpf>;
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ContractResolver = new IgnorePropertiesResolver(
                    new[] { "ParameterWpf", "Parameter", "NonAirReguler", "SPK", "BeritaAcara", "Pelaksana", "RAB" })
            });

            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

            string[] columnsToBeDeleted = { "IdPdam" };

            var list = new List<string>(columnsToBeDeleted.ToList());

            foreach (var i in data.Columns)
            {
                if (i.ToString().Right(3) == "Wpf")
                {
                    list.Add(i.ToString());
                }
            }

            columnsToBeDeleted = list.ToArray();

            foreach (var colName in columnsToBeDeleted)
            {
                if (data.Columns.Contains(colName))
                {
                    data.Columns.Remove(colName);
                }
            }

            return data;
        }

        public class IgnorePropertiesResolver : DefaultContractResolver
        {
            private readonly HashSet<string> _ignoreProps;

            public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
            {
                _ignoreProps = new HashSet<string>(propNamesToIgnore);
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                if (_ignoreProps.Contains(property.PropertyName))
                {
                    property.ShouldSerialize = _ => false;
                }

                return property;
            }
        }

        #endregion

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnFilterCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnFilterCommand).ExecuteAsync(null);
            }
        }

        private void TabelRekeningair_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                if (vm.SelectedData != null)
                {
                    if (vm.SelectedData.FlagPublishWpf == true || vm.SelectedDataPeriode.Status == false)
                    {
                        MenuItem_Koreksi.Header = "Lihat Bukti Baca";
                    }
                    else
                    {
                        MenuItem_Koreksi.Header = "Koreksi";
                    }

                    #region Perbarui Data

                    if (vm.Parent.IsBilling == true)
                    {
                        MenuItem_PerbaruiDataPelanggan.Visibility = Visibility.Visible;
                        MenuItem_PerbaruiDataPelanggan.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;
                    }
                    else
                    {
                        MenuItem_PerbaruiDataPelanggan.Visibility = Visibility.Collapsed;
                    }

                    #endregion


                    //#region Koreksi
                    //MenuItem_Koreksi.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;
                    //#endregion


                    #region Flag Koreksi

                    MenuItem_FlagKoreksi.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;

                    #endregion

                    #region Hitung Ulang

                    MenuItem_HitungUlang.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;

                    #endregion

                    #region Hapus

                    MenuItem_Hapus.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;

                    #endregion

                    #region Verifikasi Un Verifikasi

                    if (vm.Parent.IsBacameter == true)
                    {
                        MenuItem_Verifikasi.Visibility = Visibility.Visible;
                        MenuItem_UnVerifikasi.Visibility = Visibility.Visible;

                        if (vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagVerifikasiWpf == true)
                        {
                            MenuItem_Verifikasi.IsEnabled = false;
                            MenuItem_UnVerifikasi.IsEnabled = true;
                        }
                        else if (vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagVerifikasiWpf == false)
                        {
                            MenuItem_Verifikasi.IsEnabled = true;
                            MenuItem_UnVerifikasi.IsEnabled = false;
                        }
                        else
                        {
                            MenuItem_Verifikasi.IsEnabled = false;
                            MenuItem_UnVerifikasi.IsEnabled = false;
                        }
                    }
                    else
                    {
                        MenuItem_Verifikasi.Visibility = Visibility.Collapsed;
                        MenuItem_UnVerifikasi.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                    #region Publish Un Publish

                    if (vm.Parent.IsBilling == true)
                    {
                        MenuItem_Publish.Visibility = Visibility.Visible;
                        MenuItem_UnPublish.Visibility = Visibility.Visible;

                        if (vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf == true)
                        {
                            MenuItem_Publish.IsEnabled = false;
                            MenuItem_UnPublish.IsEnabled = true;
                        }
                        else if (vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf == false)
                        {
                            MenuItem_Publish.IsEnabled = true;
                            MenuItem_UnPublish.IsEnabled = false;
                        }
                        else
                        {
                            MenuItem_Publish.IsEnabled = false;
                            MenuItem_UnPublish.IsEnabled = false;
                        }
                    }
                    else
                    {
                        MenuItem_Publish.Visibility = Visibility.Collapsed;
                        MenuItem_UnPublish.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                    #region UploadUlang

                    if (vm.Parent.IsBilling == true)
                    {
                        MenuItem_UploadUlang.Visibility = Visibility.Visible;

                        if (vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf == true && vm.SelectedData.FlagUploadWpf == false)
                        {
                            MenuItem_UploadUlang.IsEnabled = true;
                        }
                        else
                        {
                            MenuItem_UploadUlang.IsEnabled = false;
                        }
                    }
                    else
                    {
                        MenuItem_UploadUlang.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                    #region Set Rekening Normal

                    MenuItem_SetRekeningNormal.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;

                    #endregion

                    #region Set Rekening Tanpa Denda

                    MenuItem_SetRekeningTanpDenda.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;

                    #endregion

                    #region Set Rekening Tanpa Denda

                    MenuItem_SetStanKembaliMuda.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagPublishWpf != true;

                    #endregion

                    #region Set Belum Dibaca

                    if (vm.Parent.IsBacameter == true)
                    {
                        MenuItem_SetBelumDiBaca.Visibility = Visibility.Visible;
                        MenuItem_SetBelumDiBaca.IsEnabled = vm.SelectedDataPeriode.Status == true && vm.SelectedData.FlagBaca == true;
                    }
                    else
                    {
                        MenuItem_SetBelumDiBaca.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                    #region Request Baca Ulang

                    if (vm.SelectedDataPeriode.Status == true && vm.Parent.IsBacameter == true)
                    {
                        MenuItem_PermintaanBacaUlang.Visibility = Visibility.Visible;
                        MenuItem_BatalkanPermintaanBacaUlang.Visibility = Visibility.Visible;

                        if (vm.SelectedData.FlagRequestBacaUlang == 1 || vm.SelectedData.FlagRequestBacaUlang == 2 || vm.SelectedData.FlagRequestBacaUlang == 2)
                        {
                            MenuItem_PermintaanBacaUlang.IsEnabled = false;
                            MenuItem_BatalkanPermintaanBacaUlang.IsEnabled = true;
                        }
                        else
                        {
                            MenuItem_PermintaanBacaUlang.IsEnabled = true;
                            MenuItem_BatalkanPermintaanBacaUlang.IsEnabled = false;
                        }
                    }
                    else
                    {
                        MenuItem_PermintaanBacaUlang.Visibility = Visibility.Collapsed;
                        MenuItem_BatalkanPermintaanBacaUlang.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                    #region Lihat Hasil Baca Ulang

                    if (vm.SelectedDataPeriode.Status == true && vm.Parent.IsBacameter == true)
                    {
                        MenuItem_LihatHasilPermintaanBacaUlang.Visibility = Visibility.Visible;
                        MenuItem_LihatHasilPermintaanBacaUlang.IsEnabled = vm.SelectedData.FlagRequestBacaUlang == 2;
                    }
                    else
                    {
                        MenuItem_LihatHasilPermintaanBacaUlang.Visibility = Visibility.Collapsed;
                    }

                    #endregion

                    ContextMenuGroup.Visibility = Visibility.Visible;
                }
                else
                {
                    ContextMenuGroup.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TabelRekeningair_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RekeningAirViewModel vm)
            {
                if (vm.SelectedData != null)
                {
                    PanelNama.Visibility = Visibility.Visible;
                    PanelAlamat.Visibility = Visibility.Visible;
                }
                else
                {
                    PanelNama.Visibility = Visibility.Collapsed;
                    PanelAlamat.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
