using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Views.Hublang
{
    public partial class VerifikasiView : UserControl
    {
        public VerifikasiView()
        {
            InitializeComponent();
            ShowFilter_Click(null, null);
            PreviewKeyUp += new KeyEventHandler(ShortCutGrid);
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
            FilterWrapper.Width = new GridLength(240);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            if (DataContext is VerifikasiViewModel vm)
            {
                if (current == "PelangganAir")
                {
                    vm.IsPelangganAir = true;
                    vm.IsPelangganLimbah = false;
                    vm.IsPelangganLltt = false;
                    vm.IsNonPelanggan = false;
                    PelangganAir.IsChecked = true;

                    vm.OnResetFilterCommand.Execute(null);
                    _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
                }
                else if (current == "PelangganLimbah")
                {
                    vm.IsPelangganAir = false;
                    vm.IsPelangganLimbah = true;
                    vm.IsPelangganLltt = false;
                    vm.IsNonPelanggan = false;
                    PelangganLimbah.IsChecked = true;

                    vm.OnResetFilterCommand.Execute(null);
                    _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
                }
                else if (current == "PelangganLltt")
                {
                    vm.IsPelangganAir = false;
                    vm.IsPelangganLimbah = false;
                    vm.IsPelangganLltt = true;
                    vm.IsNonPelanggan = false;
                    PelangganLltt.IsChecked = true;

                    vm.OnResetFilterCommand.Execute(null);
                    _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
                }
                else if (current == "NonPelanggan")
                {
                    vm.IsPelangganAir = false;
                    vm.IsPelangganLimbah = false;
                    vm.IsPelangganLltt = false;
                    vm.IsNonPelanggan = true;
                    NonPelanggan.IsChecked = true;

                    vm.OnResetFilterCommand.Execute(null);
                    _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
                }
            }
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PelangganAir.Visibility = Visibility.Collapsed;
            PelangganLimbah.Visibility = Visibility.Collapsed;
            PelangganLltt.Visibility = Visibility.Collapsed;
            NonPelanggan.Visibility = Visibility.Collapsed;

            if (DataContext is VerifikasiViewModel model)
            {
                if (model.SelectedJenis != null)
                {
                    if (model.SelectedJenis.JumlahPelangganAir > 0)
                    {
                        PelangganAir.Visibility = Visibility.Visible;
                    }

                    if (model.SelectedJenis.JumlahPelangganLimbah > 0)
                    {
                        PelangganLimbah.Visibility = Visibility.Visible;
                    }

                    if (model.SelectedJenis.JumlahPelangganLltt > 0)
                    {
                        PelangganLltt.Visibility = Visibility.Visible;
                    }

                    if (model.SelectedJenis.JumlahNonPelanggan > 0)
                    {
                        NonPelanggan.Visibility = Visibility.Visible;
                    }

                    SetRadioButtonChecked();

                    model.LabelPelangganAir = $"{model.JenisPelanggan} ({model.SelectedJenis.JumlahPelangganAirWpf})";
                    model.LabelPelangganLimbah = $"{model.JenisPelanggan} ({model.SelectedJenis.JumlahPelangganLimbahWpf})";
                    model.LabelPelangganLltt = $"{model.JenisPelanggan} ({model.SelectedJenis.JumlahPelangganLlttWpf})";
                    model.LabelNonPelanggan = $"{model.JenisPelanggan} ({model.SelectedJenis.JumlahNonPelangganWpf})";

                    model.OnResetFilterCommand.Execute(null);
                    _ = ((AsyncCommandBase)model.OnRefreshCommand).ExecuteAsync(null);
                }
            }
        }

        private void SetRadioButtonChecked()
        {
            if (DataContext is VerifikasiViewModel model)
            {
                if (PelangganAir.Visibility == Visibility.Visible)
                {
                    PelangganAir.IsChecked = true;
                    model.IsPelangganAir = true;
                    model.IsPelangganLimbah = false;
                    model.IsPelangganLltt = false;
                    model.IsNonPelanggan = false;
                    return;
                }

                if (PelangganLimbah.Visibility == Visibility.Visible)
                {
                    PelangganLimbah.IsChecked = true;
                    model.IsPelangganAir = false;
                    model.IsPelangganLimbah = true;
                    model.IsPelangganLltt = false;
                    model.IsNonPelanggan = false;
                    return;
                }

                if (PelangganLltt.Visibility == Visibility.Visible)
                {
                    PelangganLltt.IsChecked = true;
                    model.IsPelangganAir = false;
                    model.IsPelangganLimbah = false;
                    model.IsPelangganLltt = true;
                    model.IsNonPelanggan = false;
                    return;
                }

                if (NonPelanggan.Visibility == Visibility.Visible)
                {
                    NonPelanggan.IsChecked = true;
                    model.IsPelangganAir = false;
                    model.IsPelangganLimbah = false;
                    model.IsPelangganLltt = false;
                    model.IsNonPelanggan = true;
                    return;
                }
            }
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is VerifikasiViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is VerifikasiViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void Verifikasi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VerifikasiViewModel vm)
            {
                _ = ((AsyncCommandBase)vm.OnOpenVerifikasiConfirmationCommand).ExecuteAsync(null);
            }
        }

        private void ShortCutGrid(object sender, KeyEventArgs e)
        {
            if (DataContext is VerifikasiViewModel vm)
            {
                if (vm.SelectedData == null) return;
                switch (e.Key)
                {
                    case Key.F2:
                        _ = ((AsyncCommandBase)vm.OnOpenVerifikasiConfirmationCommand).ExecuteAsync(null);
                        break;
                    default:
                        return;

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
            var viewModel = (VerifikasiViewModel)DataGridPermohonan.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridPermohonan);

            var fileName = $"Draft Verifikasi {viewModel.SelectedJenis.NamaTipePermohonan} {viewModel.JenisPelanggan}";
            var titlePage = $"Data Draft Verifikasi {viewModel.SelectedJenis.NamaTipePermohonan} {viewModel.JenisPelanggan}";

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
    }
}
