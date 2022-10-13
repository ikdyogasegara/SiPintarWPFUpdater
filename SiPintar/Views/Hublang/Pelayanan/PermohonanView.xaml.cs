using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan
{
    public partial class PermohonanView : UserControl
    {
        public PermohonanView()
        {
            InitializeComponent();
        }

        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                switch (vm.FiturName)
                {
                    case "SPK":
                        _ = ((AsyncCommandBase)vm.OnOpenAddSpkFormCommand).ExecuteAsync(null);
                        break;
                    case "RAB":
                        _ = ((AsyncCommandBase)vm.OnOpenAddRabFormCommand).ExecuteAsync(null);
                        break;
                    case "Berita Acara":
                        _ = ((AsyncCommandBase)vm.OnOpenAddBaFormCommand).ExecuteAsync(null);
                        break;
                    case "Usulan":
                        _ = ((AsyncCommandBase)vm.OnOpenAddUsulanFormCommand).ExecuteAsync(null);
                        break;
                    default:
                        _ = ((AsyncCommandBase)vm.OnOpenAddFormCommand).ExecuteAsync(null);
                        break;
                }
            }
        }

        private void Koreksi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                switch (vm.FiturName)
                {
                    case "SPK":
                        _ = ((AsyncCommandBase)vm.OnOpenEditSpkFormCommand).ExecuteAsync("edit");
                        break;
                    case "RAB":
                        _ = ((AsyncCommandBase)vm.OnOpenEditRabFormCommand).ExecuteAsync("edit");
                        break;
                    case "Berita Acara":
                        _ = ((AsyncCommandBase)vm.OnOpenEditBaFormCommand).ExecuteAsync("edit");
                        break;
                    case "Usulan":
                        _ = ((AsyncCommandBase)vm.OnOpenEditUsulanFormCommand).ExecuteAsync("edit");
                        break;
                    default:
                        _ = ((AsyncCommandBase)vm.OnOpenEditFormCommand).ExecuteAsync("edit");
                        break;
                }
            }
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                switch (vm.FiturName)
                {
                    case "SPK":
                        _ = ((AsyncCommandBase)vm.OnOpenEditSpkFormCommand).ExecuteAsync("detail");
                        break;
                    case "RAB":
                        _ = ((AsyncCommandBase)vm.OnOpenEditRabFormCommand).ExecuteAsync("detail");
                        break;
                    case "Berita Acara":
                        _ = ((AsyncCommandBase)vm.OnOpenEditBaFormCommand).ExecuteAsync("detail");
                        break;
                    case "Usulan":
                        _ = ((AsyncCommandBase)vm.OnOpenEditUsulanFormCommand).ExecuteAsync("detail");
                        break;
                    default:
                        _ = ((AsyncCommandBase)vm.OnOpenEditFormCommand).ExecuteAsync("detail");
                        break;
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel { SelectedTipePermohonanComboBox: { } } model)
            {
                if (model.SelectedTipePermohonanComboBox.NamaTipePermohonan != null)
                {
                    model.CurrentPage = 1;
                    _ = ((AsyncCommandBase)model.OnRefreshCommand).ExecuteAsync(null);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel viewModel)
            {
                if (viewModel.FiturName == "SPK")
                {
                    if (viewModel.SelectedData != null)
                    {
                        if (viewModel.SelectedData.StatusPermohonanText == "Selesai")
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "PerencanaanRootDialog",
                                "Hapus Tidak diijinkan",
                                $"{viewModel.FiturName} sudah selesai. Hapus {viewModel.FiturName} tidak diijinkan",
                                "warning",
                                "Batal",
                                false,
                                "perencanaan");
                        }
                        else if (viewModel.SelectedData.NonAirReguler != null && viewModel.SelectedData.NonAirReguler.StatusTransaksi == true)
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "PerencanaanRootDialog",
                                "Hapus Tidak diijinkan",
                                "Tidak dapat dihapus karena tagihan sudah dilunasi !",
                                "warning",
                                "Batal",
                                false,
                                "perencanaan");
                        }
                        else
                            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "PerencanaanRootDialog", $"Hapus Data {viewModel.FiturName} {viewModel.SelectedData.NamaTipePermohonan}?",
                                $"Data {viewModel.FiturName} {viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                                "warning", viewModel.OnSubmitDeleteSpkFormCommand, "Hapus", "Batal", false, false, "perencanaan");

                    }
                    else
                        _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                            "PerencanaanRootDialog",
                            $"Hapus Data {viewModel.FiturName}",
                            "Silahkan pilih data",
                            "warning",
                            "Ok",
                            false,
                            "perencanaan");
                }
                else if (viewModel.FiturName == "RAB")
                {
                    if (viewModel.SelectedData != null)
                    {
                        if (viewModel.SelectedData.StatusPermohonanText == "Selesai")
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "PerencanaanRootDialog",
                                "Hapus Tidak diijinkan",
                                $"{viewModel.FiturName} sudah selesai. Hapus {viewModel.FiturName} tidak diijinkan",
                                "warning",
                                "Batal",
                                false,
                                "perencanaan");
                        }
                        else if (viewModel.SelectedData.RAB.NonAirRab != null && viewModel.SelectedData.RAB.NonAirRab.StatusTransaksi == true)
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "PerencanaanRootDialog",
                                "Hapus Tidak diijinkan",
                                "Tidak dapat dihapus karena tagihan sudah dilunasi !",
                                "warning",
                                "Batal",
                                false,
                                "perencanaan");
                        }
                        else
                            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "PerencanaanRootDialog", $"Hapus Data {viewModel.FiturName} {viewModel.SelectedData.NamaTipePermohonan}?",
                                $"Data {viewModel.FiturName} {viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                                "warning", viewModel.OnSubmitDeleteRabFormCommand, "Hapus", "Batal", false, false, "perencanaan");

                    }
                    else
                        _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                            "PerencanaanRootDialog",
                            $"Hapus Data {viewModel.FiturName}",
                            "Silahkan pilih data",
                            "warning",
                            "Ok",
                            false,
                            "perencanaan");
                }
                else if (viewModel.FiturName == "Berita Acara")
                {
                    if (viewModel.SelectedData != null)
                    {
                        if (viewModel.SelectedData.StatusPermohonanText == "Selesai")
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "DistribusiRootDialog",
                                "Hapus Tidak diijinkan",
                                $"{viewModel.FiturName} sudah selesai. Hapus {viewModel.FiturName} tidak diijinkan",
                                "warning",
                                "Batal",
                                false,
                                "distribusi");
                        }
                        else
                            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "DistribusiRootDialog", $"Hapus Data {viewModel.FiturName} {viewModel.SelectedData.NamaTipePermohonan}?",
                                $"Data {viewModel.FiturName} {viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                                "warning", viewModel.OnSubmitDeleteBaFormCommand, "Hapus", "Batal", false, false, "distribusi");

                    }
                    else
                        _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                            "DistribusiRootDialog",
                            $"Hapus Data {viewModel.FiturName}",
                            "Silahkan pilih data",
                            "warning",
                            "Ok",
                            false,
                            "distribusi");
                }
                else if (viewModel.FiturName == "Usulan")
                {
                    if (viewModel.SelectedData != null)
                    {
                        if (viewModel.SelectedData.StatusPermohonanText == "Selesai")
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "DistribusiRootDialog",
                                "Hapus Tidak diijinkan",
                                $"{viewModel.FiturName} sudah selesai. Hapus {viewModel.FiturName} tidak diijinkan",
                                "warning",
                                "Batal",
                                false,
                                "distribusi");
                        }
                        else
                            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "DistribusiRootDialog", $"Hapus Data {viewModel.FiturName} {viewModel.SelectedData.NamaTipePermohonan}?",
                                $"Data {viewModel.FiturName} {viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                                "warning", viewModel.OnSubmitDeleteUsulanFormCommand, "Hapus", "Batal", false, false, "distribusi");

                    }
                    else
                        _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                            "DistribusiRootDialog",
                            $"Hapus Data {viewModel.FiturName}",
                            "Silahkan pilih data",
                            "warning",
                            "Ok",
                            false,
                            "distribusi");
                }
                else
                {
                    if (viewModel.SelectedData != null)
                    {
                        if (viewModel.SelectedData.StatusPermohonanText == "Selesai")
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "HublangRootDialog",
                                "Hapus Tidak diijinkan",
                                $"{viewModel.FiturName} sudah selesai. Hapus {viewModel.FiturName} tidak diijinkan",
                                "warning",
                                "Batal",
                                false,
                                "hublang");
                        }
                        else if (viewModel.SelectedData.NonAirReguler != null && viewModel.SelectedData.NonAirReguler.StatusTransaksi == true)
                        {
                            _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                                "HublangRootDialog",
                                "Hapus Tidak diijinkan",
                                "Tidak dapat dihapus karena tagihan sudah dilunasi !",
                                "warning",
                                "Batal",
                                false,
                                "hublang");
                        }
                        else
                            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(false, false, "HublangRootDialog", $"Hapus Data {viewModel.FiturName} {viewModel.SelectedData.NamaTipePermohonan}?",
                                $"Data {viewModel.FiturName} {viewModel.SelectedData.NomorPermohonan} yang akan dihapus tidak dapat dibatalkan.",
                                "warning", viewModel.OnSubmitDeleteFormCommand, "Hapus", "Batal", false, false, "hublang");

                    }
                    else
                        _ = DialogHelper.ShowDialogGlobalViewAsync(false, false,
                            "HublangRootDialog",
                            $"Hapus Data {viewModel.FiturName}",
                            "Silahkan pilih data",
                            "warning",
                            "Ok",
                            false,
                            "hublang");
                }
            }
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(210);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void TipePermohonan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh_Click(null, null);
        }

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is PermohonanViewModel model)
            {
                if (model.SelectedData is null)
                {
                    e.Handled = true;
                }

                BtnDesign.Header = $"Design Template {model.FiturName}";
                BtnCetak.Header = $"Cetak {model.FiturName}";
                BtnHapusSpk.Header = $"Hapus {model.FiturName}";
                BtnKoreksiSpk.Header = $"Koreksi {model.FiturName}";
                BtnDetailSpk.Header = $"Detail {model.FiturName}";

                if (model.FiturName == "SPK")
                {
                    BtnBatalkanSpk.Visibility = Visibility.Visible;

                    if (model.SelectedData != null && (model.SelectedData.StatusPermohonanWpf == "SPK Dibatalkan" ||
                                                       model.SelectedData.StatusPermohonanWpf == "Menunggu Verifikasi" ||
                                                       model.SelectedData.StatusPermohonanWpf == "Menunggu Pelunasan RAB" ||
                                                       model.SelectedData.StatusPermohonanWpf == "Menunggu BPPI" ||
                                                       model.SelectedData.StatusPermohonanWpf == "Menunggu SPKP" ||
                                                       model.SelectedData.StatusPermohonanWpf == "Selesai"))
                    {
                        BtnBatalkanSpk.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    BtnBatalkanSpk.Visibility = Visibility.Collapsed;
                }

                if (model.FiturName == "RAB")
                {
                    BtnBppi.Visibility = Visibility.Visible;
                    BtnDesignBppi.Visibility = Visibility.Visible;
                    BtnCetakBppi.Visibility = Visibility.Visible;

                    BtnSpkp.Visibility = Visibility.Visible;
                    BtnDesignSpkp.Visibility = Visibility.Visible;
                    BtnCetakSpkp.Visibility = Visibility.Visible;
                }
                else
                {
                    BtnBppi.Visibility = Visibility.Collapsed;
                    BtnDesignBppi.Visibility = Visibility.Collapsed;
                    BtnCetakBppi.Visibility = Visibility.Collapsed;

                    BtnSpkp.Visibility = Visibility.Collapsed;
                    BtnDesignSpkp.Visibility = Visibility.Collapsed;
                    BtnCetakSpkp.Visibility = Visibility.Collapsed;
                }
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
            var viewModel = (PermohonanViewModel)DataGridPermohonan.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridPermohonan);

            var fileName = $"{viewModel.FiturName} {viewModel.SelectedJenisPelanggan}";
            var titlePage = $"Data {viewModel.FiturName} {viewModel.SelectedJenisPelanggan}";

            switch (fileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, viewModel.Identfire);
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(sanitizedData, fileName, viewModel.Identfire);
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(sanitizedData, fileName, viewModel.Identfire);
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(sanitizedData, fileName, viewModel.Identfire);
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(sanitizedData, fileName, viewModel.Identfire);
                    break;
                default:
                    break;
            }
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<PermohonanWpf>;
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
                    "RAB",
                    "ParameterWpf",
                    "PelaksanaWpf",
                    "RabWpf",
                    "BeritaAcaraWpf"
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

        private void DesignTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan }
                };

                var fitur = "";
                switch (vm.FiturName)
                {
                    case "Permohonan":
                        fitur = "Permohonan";
                        break;
                    case "Pengaduan":
                        fitur = "Permohonan";
                        break;
                    case "SPK":
                        fitur = "SPK";
                        break;
                    case "RAB":
                        fitur = "RAB";
                        break;
                    case "Berita Acara":
                        fitur = "BA";
                        break;
                    case "Usulan":
                        fitur = "Usulan";
                        break;
                    case "Berita Acara View Only":
                        fitur = "BA View Only";
                        break;
                }

                if (fitur == "SPK")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpk).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpk).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirSpk.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpk).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpk).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahSpk.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpk).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpk).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttSpk.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpk).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpk).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganSpk.Execute(param);
                            break;
                    }
                }
                else if (fitur == "RAB")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirRab).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirRab).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirRab.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahRab).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahRab).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahRab.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttRab).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttRab).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttRab.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganRab).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganRab).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganRab.Execute(param);
                            break;
                    }
                }
                else if (fitur == "BA")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcara).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcara).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirBeritaAcara.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcara).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcara).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahBeritaAcara.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcara).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcara).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttBeritaAcara.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcara).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcara).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganBeritaAcara.Execute(param);
                            break;
                    }
                }
                else if (fitur == "BA View Only")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcaraViewOnly).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcaraViewOnly).TemplateName = $"BAPelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirBeritaAcaraViewOnly.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcaraViewOnly).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcaraViewOnly).TemplateName = $"BAPelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahBeritaAcaraViewOnly.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcaraViewOnly).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcaraViewOnly).TemplateName = $"BAPermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttBeritaAcaraViewOnly.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcaraViewOnly).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcaraViewOnly).TemplateName = $"BANonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganBeritaAcaraViewOnly.Execute(param);
                            break;
                    }
                }
                else if (fitur == "Usulan")
                {
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirUsulan).IsCetak = false;
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirUsulan).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                    vm.OnCetakCommandPelangganAirUsulan.Execute(param);
                }
                else
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAir).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAir).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAir.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbah).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbah).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbah.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLltt).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLltt).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLltt.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelanggan).IsCetak = false;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelanggan).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelanggan.Execute(param);
                            break;
                    }
                }
            }
        }

        private void Cetak_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdPermohonan", vm.SelectedData.IdPermohonan }
                };

                var fitur = "";
                switch (vm.FiturName)
                {
                    case "Permohonan":
                        fitur = "Permohonan";
                        break;
                    case "Pengaduan":
                        fitur = "Permohonan";
                        break;
                    case "SPK":
                        fitur = "SPK";
                        break;
                    case "RAB":
                        fitur = "RAB";
                        break;
                    case "Berita Acara":
                        fitur = "BA";
                        break;
                    case "Usulan":
                        fitur = "Usulan";
                        break;
                    case "Berita Acara View Only":
                        fitur = "BA View Only";
                        break;
                }

                if (fitur == "SPK")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpk).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpk).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirSpk.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpk).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpk).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahSpk.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpk).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpk).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttSpk.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpk).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpk).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganSpk.Execute(param);
                            break;
                    }
                }
                else if (fitur == "RAB")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirRab).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirRab).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirRab.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahRab).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahRab).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahRab.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttRab).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttRab).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttRab.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganRab).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganRab).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganRab.Execute(param);
                            break;
                    }
                }
                else if (fitur == "BA")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcara).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcara).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirBeritaAcara.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcara).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcara).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahBeritaAcara.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcara).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcara).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttBeritaAcara.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcara).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcara).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganBeritaAcara.Execute(param);
                            break;
                    }
                }
                else if (fitur == "BA View Only")
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcaraViewOnly).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBeritaAcaraViewOnly).TemplateName = $"BAPelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirBeritaAcaraViewOnly.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcaraViewOnly).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBeritaAcaraViewOnly).TemplateName = $"BAPelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahBeritaAcaraViewOnly.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcaraViewOnly).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBeritaAcaraViewOnly).TemplateName = $"BAPermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttBeritaAcaraViewOnly.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcaraViewOnly).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBeritaAcaraViewOnly).TemplateName = $"BANonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganBeritaAcaraViewOnly.Execute(param);
                            break;
                    }
                }
                else if (fitur == "Usulan")
                {
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirUsulan).IsCetak = true;
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirUsulan).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                    vm.OnCetakCommandPelangganAirUsulan.Execute(param);
                }
                else
                {
                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAir).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAir).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAir.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbah).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbah).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbah.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLltt).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLltt).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLltt.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelanggan).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelanggan).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelanggan.Execute(param);
                            break;
                    }
                }
            }
        }


        private void DesignTemplateBppi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan },
                    { "IdPermohonan", vm.SelectedData.IdPermohonan }
                };

                var fitur = "BPPI";

                switch (vm.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)vm.OnCetakCommandPelangganAirBppi).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandPelangganAirBppi).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandPelangganAirBppi.Execute(param);
                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBppi).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBppi).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandPelangganLimbahBppi.Execute(param);
                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBppi).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBppi).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandPelangganLlttBppi.Execute(param);
                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)vm.OnCetakCommandNonPelangganBppi).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandNonPelangganBppi).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandNonPelangganBppi.Execute(param);
                        break;
                }
            }
        }

        private void CetakBppi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                if (string.IsNullOrWhiteSpace(vm.SelectedData.RAB.NomorBppi))
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "PerencanaanRootDialog",
                    "BPPI Belum dibuat",
                    $"Harap buat BPPI terlebih dahulu !",
                    "warning",
                    moduleName: "perencanaan");
                }
                else
                {
                    var param = new Dictionary<string, dynamic>()
                    {
                        { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan },
                        { "IdPermohonan", vm.SelectedData.IdPermohonan }
                    };

                    var fitur = "BPPI";

                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBppi).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirBppi).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirBppi.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBppi).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahBppi).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahBppi.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBppi).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttBppi).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttBppi.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBppi).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganBppi).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganBppi.Execute(param);
                            break;
                    }
                }
            }
        }


        private void DesignTemplateSpkp_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                var param = new Dictionary<string, dynamic>()
                {
                    { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan },
                    { "IdPermohonan", vm.SelectedData.IdPermohonan }
                };

                var fitur = "SPKP";

                switch (vm.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpkp).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpkp).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandPelangganAirSpkp.Execute(param);
                        break;
                    case "Pelanggan Limbah":
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpkp).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpkp).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandPelangganLimbahSpkp.Execute(param);
                        break;
                    case "Pelanggan L2T2":
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpkp).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpkp).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandPelangganLlttSpkp.Execute(param);
                        break;
                    case "Non Pelanggan":
                        ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpkp).IsCetak = false;
                        ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpkp).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                        vm.OnCetakCommandNonPelangganSpkp.Execute(param);
                        break;
                }
            }
        }

        private void CetakSpkp_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanViewModel vm)
            {
                if (string.IsNullOrWhiteSpace(vm.SelectedData.RAB.NomorSpkp))
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "PerencanaanRootDialog",
                    "SPKP-SPPB Belum dibuat",
                    $"Harap buat SPKP-SPPB terlebih dahulu !",
                    "warning",
                    moduleName: "perencanaan");
                }
                else
                {
                    var param = new Dictionary<string, dynamic>()
                    {
                        { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan },
                        { "IdPermohonan", vm.SelectedData.IdPermohonan }
                    };

                    var fitur = "SPKP";

                    switch (vm.SelectedJenisPelanggan)
                    {
                        case "Pelanggan Air":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpkp).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganAirSpkp).TemplateName = $"{fitur}PelangganAir_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganAirSpkp.Execute(param);
                            break;
                        case "Pelanggan Limbah":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpkp).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLimbahSpkp).TemplateName = $"{fitur}PelangganLimbah_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLimbahSpkp.Execute(param);
                            break;
                        case "Pelanggan L2T2":
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpkp).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandPelangganLlttSpkp).TemplateName = $"{fitur}PermohonanPelangganLltt_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandPelangganLlttSpkp.Execute(param);
                            break;
                        case "Non Pelanggan":
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpkp).IsCetak = true;
                            ((OnCetakCommand)vm.OnCetakCommandNonPelangganSpkp).TemplateName = $"{fitur}NonPelanggan_{vm.SelectedData.KodeTipePermohonan}";
                            vm.OnCetakCommandNonPelangganSpkp.Execute(param);
                            break;
                    }
                }
            }
        }
    }
}
