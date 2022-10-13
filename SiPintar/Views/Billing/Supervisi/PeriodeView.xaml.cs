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
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi
{
    public partial class PeriodeView : UserControl
    {
        public PeriodeView()
        {
            InitializeComponent();
        }

        private void ResetZoom_Click(object sender, RoutedEventArgs e)
        {
            KolomX.MinValue = double.NaN;
            KolomX.MaxValue = double.NaN;
            KolomY.MinValue = double.NaN;
            KolomY.MaxValue = double.NaN;
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PeriodeViewModel vm)
            {
                vm.CurrentPage--;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PeriodeViewModel vm)
            {
                vm.CurrentPage++;
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void Tutup_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PeriodeViewModel vm)
            {
                _ = ((AsyncCommandBase)vm.OnOpenChangeStatusConfirmCommand).ExecuteAsync("close");
            }
        }

        private void Buka_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PeriodeViewModel vm)
            {
                _ = ((AsyncCommandBase)vm.OnOpenChangeStatusConfirmCommand).ExecuteAsync("open");
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
            _ = (PeriodeViewModel)DataGridPeriode.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridPeriode);

            var fileName = $"Periode Rekening";
            var titlePage = $"Data Periode Rekening";

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
            var item = Data.ItemsSource as IList<MasterPeriodeWpf>;
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

        private void Grid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is PeriodeViewModel vm)
            {
                if (vm.SelectedData != null)
                {
                    ContextMenuGroup.Visibility = Visibility.Visible;

                    if (vm.AksesBukaTutup)
                    {
                        if (vm.SelectedData.StatusWpf == false)
                        {
                            ButtonBuka.Visibility = Visibility.Visible;
                            ButtonTutup.Visibility = Visibility.Collapsed;
                            ButtonKoreksiTanggalDenda.Visibility = Visibility.Collapsed;
                            ButtonKoreksiTanggalDendaSperator.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ButtonBuka.Visibility = Visibility.Collapsed;
                            ButtonTutup.Visibility = Visibility.Visible;
                            ButtonKoreksiTanggalDenda.Visibility = Visibility.Visible;
                            ButtonKoreksiTanggalDendaSperator.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        ButtonBuka.Visibility = Visibility.Collapsed;
                        ButtonTutup.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    ContextMenuGroup.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
