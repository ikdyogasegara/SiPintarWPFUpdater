using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Views.Loket.Tagihan.TagihanKolektifAir
{
    public partial class DetailTagihanView : UserControl
    {
        public DetailTagihanView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
                return;

            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            WarningMessage.Text = "Jumlah Bayar Kurang";
            WarningMessage.Visibility = Visibility.Visible;
        }

        private void Bayar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                WarningMessage.Text = string.Empty;

                var tanggal = vm.ParentPage.TanggalTransaksi.Date;
                var newTanggal = new DateTime(tanggal.Year, tanggal.Month, tanggal.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTimeKind.Local);

                if (tanggal.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    newTanggal = new DateTime(tanggal.Year, tanggal.Month, tanggal.Day, 0, 0, 0, DateTimeKind.Local);
                }

                vm.ParentPage.WaktuTransaksi = newTanggal;

                _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, true, "LoketRootDialog", $"Konfirmasi Pembayaran ?",
                $"Tanggal Transaksi : {vm.ParentPage.WaktuTransaksi.ToString("dd MMMM yyyy HH:mm:ss")}",
                "warning", vm.OnSubmitBayarCommand, "Bayar", "Batal", false, false, "loket");
            }
        }

        private void Bayar_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var viewModel = (DetailTagihanViewModel)DataContext;

            if (viewModel == null || viewModel.RincianDetail == null || viewModel.RincianDetail.TotalTagihan <= 0)
                return;

            var current = ((TextBox)sender).Text;

            if (!string.IsNullOrEmpty(current) && Convert.ToDecimal(current.Replace(".", "").Replace(",", ".")) >= viewModel.RincianDetail.TotalTagihan)
            {
                BayarBtn.IsEnabled = true;
                WarningMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                BayarBtn.IsEnabled = false;
                WarningMessage.Visibility = Visibility.Visible;
            }
        }

        private void BtnUangPas_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                vm.JumlahBayar = vm.RincianDetail.TotalTagihan;
                if (!string.IsNullOrEmpty(BayarText.Text) && Convert.ToDecimal(BayarText.Text.Replace(".", "").Replace(",", ".")) >= vm.RincianDetail.TotalTagihan)
                {
                    BayarBtn.IsEnabled = true;
                    WarningMessage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BayarBtn.IsEnabled = false;
                    WarningMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void TagihanCheckBoxAll_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                vm.CheckUpdate();
            }
        }

        private void CheckTagihan_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel { SelectedTagihan: { } } vm)
            {
                var selectedData = vm.SelectedTagihan;

                if (selectedData.IsSelected) // unCheck
                {
                    selectedData.IsUpdating = true;
                    selectedData.IsSelected = false;
                    vm.SetRincian();
                    selectedData.IsUpdating = false;

                    // var kodePeriodeSelectedMax = vm.TagihanRekeningAir.Where(c => c.IsSelected).Max(c => c.KodePeriode);
                    //
                    // if (selectedData.KodePeriode == kodePeriodeSelectedMax)
                    // {
                    //     selectedData.IsUpdating = true;
                    //     selectedData.IsSelected = false;
                    //     vm.SetRincian();
                    //     selectedData.IsUpdating = false;
                    // }
                    // else
                    // {
                    //     _ = DialogHelper.ShowDialogGlobalViewAsync(
                    //         false,
                    //         true,
                    //         "LoketRootDialog",
                    //         "Un-Centang Tagihan",
                    //         $"Mohon lakukan un-centang sesuai urutan periode rekening. Periode yang dapat di un-centang adalah '{kodePeriodeSelectedMax}'",
                    //         "warning",
                    //         moduleName: "loket");
                    // }
                }
                else // check
                {
                    selectedData.IsUpdating = true;
                    selectedData.IsSelected = true;
                    vm.SetRincian();
                    selectedData.IsUpdating = false;

                    // var kodePeriodeSelectedMax = vm.TagihanRekeningAir.Where(c => c.IsSelected).Max(c => c.KodePeriode) ?? 1;
                    // var list = vm.TagihanRekeningAir.Where(c => c.KodePeriode > kodePeriodeSelectedMax);
                    //
                    // if (list.Any())
                    // {
                    //     var kodePeriodeValid = list.OrderBy(c => c.KodePeriode).Take(1).Select(c => c.KodePeriode);
                    //
                    //     if (selectedData.KodePeriode == kodePeriodeValid.FirstOrDefault())
                    //     {
                    //         selectedData.IsUpdating = true;
                    //         selectedData.IsSelected = true;
                    //         vm.SetRincian();
                    //         selectedData.IsUpdating = false;
                    //     }
                    //     else
                    //     {
                    //         _ = DialogHelper.ShowDialogGlobalViewAsync(
                    //             false,
                    //             true,
                    //             "LoketRootDialog",
                    //             "Check Tagihan",
                    //             $"Mohon lakukan centang sesuai urutan periode rekening. Periode yang dapat di centang adalah '{kodePeriodeValid.FirstOrDefault()}'",
                    //             "warning",
                    //             moduleName: "loket");
                    //     }
                    // }
                }
            }
        }

        private void OpenFotoMeter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                var bulan = vm.SelectedTagihan.KodePeriode.ToString().Right(2);
                var tahun = vm.SelectedTagihan.KodePeriode.ToString().Mid(2, 2);
                var _fotoStanUri = new Uri(Path.Combine(AppSetting.LokasiFotoMeter, bulan + tahun, $"{vm.SelectedTagihan.NoSamb}.jpg"), UriKind.Absolute);
                if (CheckWebsite(_fotoStanUri) == true)
                {
                    _ = DialogHost.Show(new FotoMeterView(bulan, tahun, vm.SelectedTagihan.NoSamb), "LoketRootDialog");
                }
                else
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(false, true,
                               "LoketRootDialog",
                               "Tidak Ada Foto",
                               "Foto pembacaan tidak ditemukan !",
                               "warning",
                               "Ok",
                               false,
                               "loket");
                }
            }
        }

        private static bool CheckWebsite(Uri url)
        {
            var webRequest = WebRequest.Create(url.OriginalString);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch //If exception thrown then couldn't get response from address
            {
                return false;
            }
            return true;
        }

        private void DesignKwitansiClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                ((OnCetakCommand)vm.OnCetakCommand).IsCetak = false;
                ((OnCetakCommand)vm.OnCetakCommand).TemplateName = "KwitansiTagihan";
                vm.OnCetakCommand.Execute(null);
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
            _ = (DetailTagihanViewModel)DataGridTagihan.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridTagihan);

            var fileName = $"Tagihan Rekening Air";
            var titlePage = $"Data Tagihan Rekening Air";

            switch (fileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, "LoketRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(sanitizedData, fileName, "LoketRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(sanitizedData, fileName, "LoketRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(sanitizedData, fileName, "LoketRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(sanitizedData, fileName, "LoketRootDialog");
                    break;
                default:
                    break;
            }
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<TagihanGlobalWpf>;
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ContractResolver = new IgnorePropertiesResolver(
                new[]
                {
                    "ParameterWpf",
                    "Parameter",
                    "NonAirReguler",
                    "SPK",
                    "BeritaAcara",
                    "Pelaksana",
                    "RAB"
                })
            });

            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

            string[] columnsToBeDeleted = { "IdPdam" };

            var list = new List<string>(columnsToBeDeleted.ToList());

            foreach (var i in data.Columns)
            {
                if (AppExtensionsHelpers.Right(i.ToString(), 3) == "Wpf")
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

        private void DataGridTagihan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                vm.ShowSideInfo = false;

                if (vm.SelectedTagihan != null)
                {
                    vm.ShowSideInfo = true;

                    if (vm.SelectedTagihan.JenisTagihan == "Air")
                    {
                        JenisNonAir.Visibility = Visibility.Collapsed;
                        Golongan.Visibility = Visibility.Visible;
                        Diameter.Visibility = Visibility.Visible;
                        NamaStatus.Visibility = Visibility.Visible;
                        StanLalu.Visibility = Visibility.Visible;
                        StanAngkat.Visibility = Visibility.Visible;
                        StanKini.Visibility = Visibility.Visible;
                        Pakai.Visibility = Visibility.Visible;
                        Kelainan.Visibility = Visibility.Visible;
                        BiayaPemakaian.Visibility = Visibility.Visible;
                        Administrasi.Visibility = Visibility.Visible;
                        Pemeliharaan.Visibility = Visibility.Visible;
                        Retribusi.Visibility = Visibility.Visible;
                        Pelayanan.Visibility = Visibility.Visible;
                        AirLimbah.Visibility = Visibility.Visible;
                        DendaPakai0.Visibility = Visibility.Visible;
                        Ppn.Visibility = Visibility.Visible;
                        Meterai.Visibility = Visibility.Visible;
                        RekAir.Visibility = Visibility.Visible;
                        Denda.Visibility = Visibility.Visible;
                        Keterangan.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        JenisNonAir.Visibility = Visibility.Visible;
                        Golongan.Visibility = Visibility.Collapsed;
                        Diameter.Visibility = Visibility.Collapsed;
                        NamaStatus.Visibility = Visibility.Collapsed;
                        StanLalu.Visibility = Visibility.Collapsed;
                        StanAngkat.Visibility = Visibility.Collapsed;
                        StanKini.Visibility = Visibility.Collapsed;
                        Pakai.Visibility = Visibility.Collapsed;
                        Kelainan.Visibility = Visibility.Collapsed;
                        BiayaPemakaian.Visibility = Visibility.Collapsed;
                        Administrasi.Visibility = Visibility.Collapsed;
                        Pemeliharaan.Visibility = Visibility.Collapsed;
                        Retribusi.Visibility = Visibility.Collapsed;
                        Pelayanan.Visibility = Visibility.Collapsed;
                        AirLimbah.Visibility = Visibility.Collapsed;
                        DendaPakai0.Visibility = Visibility.Collapsed;
                        Ppn.Visibility = Visibility.Collapsed;
                        Meterai.Visibility = Visibility.Collapsed;
                        RekAir.Visibility = Visibility.Collapsed;
                        Denda.Visibility = Visibility.Collapsed;
                        Keterangan.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                if (vm.SelectedTagihan is null)
                {
                    e.Handled = true;
                }

                BtnLihatBuktiPembacaan.Visibility = Visibility.Collapsed;
                BtnHapusDenda.Visibility = Visibility.Collapsed;

                if (vm.SelectedTagihan != null && vm.SelectedTagihan.JenisTagihan == "Air")
                {
                    BtnLihatBuktiPembacaan.Visibility = Visibility.Visible;
                    BtnHapusDenda.Visibility = vm.SelectedTagihan.DendaWpf > 0 ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void HapusDenda_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                if (vm.SelectedTagihan != null && vm.SelectedTagihan.JenisTagihan == "Air")
                {
                    var selectedData = vm.SelectedTagihan;
                    selectedData.DendaWpf = 0;
                    selectedData.TotalWpf = selectedData.Rekair + 0;
                    vm.SetRincian();
                }
            }
        }

        private void RiwayatTransaksi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailTagihanViewModel vm)
            {
                vm.ParentPage.PageSebelumRiwayat = "DetailTagihan";
                vm.Parent.UpdatePage("RiwayatTransaksi");
            }
        }
    }
}
