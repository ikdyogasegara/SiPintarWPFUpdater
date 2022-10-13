using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir
{
    public partial class UsulanKoreksiView : UserControl
    {
        public UsulanKoreksiView()
        {
            InitializeComponent();
            DataContextChanged += UsulanKoreksiView_DataContextChanged;
        }

        private void UsulanKoreksiView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                PreviewKeyUp += new KeyEventHandler(ShortCutGrid);
            }
        }

        private void ShortCutGrid(object sender, KeyEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                if (vm.SelectedData == null) return;

                switch (e.Key)
                {
                    case Key.F2:

                        if (vm.Parent.IsBilling == true)
                        {
                            if (vm.SelectedData.StatusVerifikasiPusatWpf == 0)
                                _ = ((AsyncCommandBase)vm.OnOpenVerifikasiCommand).ExecuteAsync("pusat");
                        }
                        else
                        {
                            if (vm.SelectedData.StatusVerifikasiLapanganWpf == 0)
                                _ = ((AsyncCommandBase)vm.OnOpenVerifikasiCommand).ExecuteAsync("lapangan");
                        }
                        break;
                    default:
                        return;
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

        private void DataGridContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DataGridContent_Loaded(object sender, RoutedEventArgs e)
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(DataGridContent, UpdatedSource);
            }
        }

        private void UpdatedSource(object sender, EventArgs e)
        {
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                vm.OnRefreshCommand.Execute(null);
            }
        }

        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                vm.OnOpenAddFormCommand.Execute(null);
            }
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                vm.OnOpenDeleteConfirmationCommand.Execute(null);
            }
        }

        private void TableSetting_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                vm.OnOpenSettingTableFormCommand.Execute(null);
            }
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
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
            var viewModel = (UsulanKoreksiViewModel)DataGridContent.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridContent);

            var fileName = $"Usulan Koreksi Rekening Air";
            var titlePage = $"Data Usulan Koreksi Rekening Air";

            switch (fileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, viewModel.Parent.IsBilling ? "BillingRootDialog" : "HublangRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(sanitizedData, fileName, viewModel.Parent.IsBilling ? "BillingRootDialog" : "HublangRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(sanitizedData, fileName, viewModel.Parent.IsBilling ? "BillingRootDialog" : "HublangRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(sanitizedData, fileName, viewModel.Parent.IsBilling ? "BillingRootDialog" : "HublangRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(sanitizedData, fileName, viewModel.Parent.IsBilling ? "BillingRootDialog" : "HublangRootDialog");
                    break;
                default:
                    break;
            }
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<PermohonanKoreksiRekeningWpf>;
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
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                var param = new Dictionary<string, dynamic>() { { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan } };

                if (vm.Parent.IsBilling == true)
                {
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirBilling).IsCetak = false;
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirBilling).TemplateName = $"Usulan_Koreksi_Rekening";
                    vm.OnCetakCommandPelangganAirBilling.Execute(param);
                }
                else
                {
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirHublang).IsCetak = false;
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirHublang).TemplateName = $"Usulan_Koreksi_Rekening";
                    vm.OnCetakCommandPelangganAirHublang.Execute(param);
                }
            }
        }

        private void Cetak_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel vm)
            {
                var param = new Dictionary<string, dynamic>() { { "IdPermohonan", vm.SelectedData.IdPermohonan } };

                if (vm.Parent.IsBilling == true)
                {
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirBilling).IsCetak = true;
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirBilling).TemplateName = $"Usulan_Koreksi_Rekening";
                    vm.OnCetakCommandPelangganAirBilling.Execute(param);
                }
                else
                {
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirHublang).IsCetak = true;
                    ((OnCetakCommand)vm.OnCetakCommandPelangganAirHublang).TemplateName = $"Usulan_Koreksi_Rekening";
                    vm.OnCetakCommandPelangganAirHublang.Execute(param);
                }
            }
        }

        private void TipePermohonan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel model)
            {
                if (model.FilterStatusPermohonan != null)
                {
                    _ = ((AsyncCommandBase)model.OnRefreshCommand).ExecuteAsync(null);
                }
            }
        }
    }
}
