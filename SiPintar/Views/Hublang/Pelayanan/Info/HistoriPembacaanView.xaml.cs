using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Views.Hublang.Pelayanan.Info
{
    public partial class HistoriPembacaanView : UserControl
    {
        public HistoriPembacaanView()
        {
            InitializeComponent();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("ExportMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (HistoriPembacaanViewModel)DataGridHistoriPembacaan.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridHistoriPembacaan },
                { "FileType", fileType }
            };

            _ = ((AsyncCommandBase)viewModel.OnExportCommand).ExecuteAsync(param);
        }

        private void OpenFotoMeter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is HistoriPembacaanViewModel vm)
            {
                var bulan = vm.SelectedData.KodePeriode.ToString().Right(2);
                var tahun = vm.SelectedData.KodePeriode.ToString().Mid(2, 2);
                var _fotoStanUri = new Uri(Path.Combine(AppSetting.LokasiFotoMeter, bulan + tahun, $"{vm.SelectedData.NoSamb}.jpg"), UriKind.Absolute);
                if (CheckWebsite(_fotoStanUri) == true)
                {
                    _ = DialogHost.Show(new FotoMeterView(bulan, tahun, vm.SelectedData.NoSamb), "HublangRootDialog");
                }
                else
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(false, true,
                              "HublangRootDialog",
                              "Tidak Ada Foto",
                              "Foto pembacaan tidak ditemukan !",
                              "warning",
                              "Ok",
                              false,
                              "hublang");
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
    }
}
