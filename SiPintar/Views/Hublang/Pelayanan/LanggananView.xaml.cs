﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan
{
    public partial class LanggananView : UserControl
    {
        public LanggananView()
        {
            InitializeComponent();
            Pelanggan.IsChecked = true;
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            if (DataContext is LanggananViewModel vm)
            {
                if (current == "Pelanggan")
                {
                    vm.IsTabKoreksi = false;
                    _ = ((AsyncCommandBase)vm.OnResetFilterCommand).ExecuteAsync(null);
                }
                else if (current == "DataKoreksi")
                {
                    vm.IsTabKoreksi = true;
                    _ = ((AsyncCommandBase)vm.OnResetFilterCommand).ExecuteAsync(null);
                }
            }
        }

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

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is LanggananViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is LanggananViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LanggananViewModel vm)
            {
                vm.CurrentPage = 1;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var vm = DataContext as LanggananViewModel;
            if (vm.SelectedData is null)
                e.Handled = true;
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
            var vm = DataContext as LanggananViewModel;
            if (!vm.IsTabKoreksi)
            {
                var viewModel = (LanggananViewModel)DataGridPermohonan.DataContext;
                var fileType = ((MenuItem)sender).Tag.ToString();
                var sanitizedData = GetRawData(DataGridPermohonan);

                var fileName = $"{viewModel.SelectedJenisPelanggan}";
                var titlePage = $"Data {viewModel.SelectedJenisPelanggan}";

                switch (fileType)
                {
                    case "html":
                        _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, "HublangRootDialog");
                        break;
                    case "xlsx":
                        _ = ExportHelper.ExportXlsx(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    case "xls":
                        _ = ExportHelper.ExportXls(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    case "xml":
                        _ = ExportHelper.ExportXml(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    case "csv":
                        _ = ExportHelper.ExportCsv(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var viewModel = (LanggananViewModel)DataGridKoreksi.DataContext;
                var fileType = ((MenuItem)sender).Tag.ToString();
                var sanitizedData = GetRawDataKoreksi(DataGridKoreksi);

                var fileName = $"Data Koreksi {viewModel.SelectedJenisPelanggan}";
                var titlePage = $"Data Koreksi {viewModel.SelectedJenisPelanggan}";

                switch (fileType)
                {
                    case "html":
                        _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, "HublangRootDialog");
                        break;
                    case "xlsx":
                        _ = ExportHelper.ExportXlsx(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    case "xls":
                        _ = ExportHelper.ExportXls(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    case "xml":
                        _ = ExportHelper.ExportXml(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    case "csv":
                        _ = ExportHelper.ExportCsv(sanitizedData, fileName, "HublangRootDialog");
                        break;
                    default:
                        break;
                }
            }
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<MasterPelangganGlobalWpf>;
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ContractResolver = new IgnorePropertiesResolver(
                new[]
                {
                    "MasterPelangganAirDetail"
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

        private DataTable GetRawDataKoreksi(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<MasterPelangganGlobalKoreksiWpf>;
            var json = JsonConvert.SerializeObject(item);

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
            // var vm = DataContext as LanggananViewModel;
            // if (vm != null)
            // {
            //     ((OnCetakCommand)vm.OnCetakCommand).IsCetak = false;
            //     ((OnCetakCommand)vm.OnCetakCommand).TemplateName = $"PermohonanNonPelanggan_{vm.SelectedData.NamaTipePermohonan}";
            //     var param = new Dictionary<string, dynamic>()
            //     {
            //         { "IdTipePermohonan", vm.SelectedData.IdTipePermohonan }
            //     };
            //     vm.OnCetakCommand.Execute(param);
            // }
        }


        private void Cetak_Click(object sender, RoutedEventArgs e)
        {
            // var vm = DataContext as LanggananViewModel;
            // if (vm != null)
            // {
            //     ((OnCetakCommand)vm.OnCetakCommand).IsCetak = true;
            //     ((OnCetakCommand)vm.OnCetakCommand).TemplateName = $"PermohonanNonPelanggan_{vm.SelectedData.NamaTipePermohonan}";
            //     var param = new Dictionary<string, dynamic>()
            //     {
            //         { "IdPermohonan", vm.SelectedData.IdPermohonan }
            //     };
            //     vm.OnCetakCommand.Execute(param);
            // }
        }
    }
}
