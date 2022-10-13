using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public partial class Step1AddFormView : UserControl
    {
        public Step1AddFormView()
        {
            InitializeComponent();
            NamaPelanggan.KeyUp += Cari_KeyUp;
            NomorSambungan.KeyUp += Cari_KeyUp;
            Alamat.KeyUp += Cari_KeyUp;
        }

        private void CheckButtonCariPelanggan()
        {
            if (NamaPelanggan.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (NomorSambungan.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (Alamat.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            CariPelangganButton.IsEnabled = false;
        }

        private void Cari_KeyUp(object sender, KeyEventArgs e)
        {
            CheckButtonCariPelanggan();
            if (e.Key == Key.Enter && CariPelangganButton.IsEnabled)
            {
                CariPelangganButton_Click(null, null);
            }
        }

        private void CariPelangganButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.PermohonanAirList.Clear();
                var param = new Dictionary<string, dynamic>
                {
                    {"NoSamb", NomorSambungan.Text},
                    {"Nama", NamaPelanggan.Text},
                    {"Alamat", Alamat.Text},
                    {"pageSize", viewModel.LimitDataPelanggan.Key},
                    {"currentPage", viewModel.CurrentPagePelanggan},
                    {"StatusPermohonanText", "Menunggu Usulan Koreksi"},
                    {"IncludeRabDetail", false},
                    {"KodeTipePermohonan" , "KREKAIR" }, // perlu kode karena untuk join ke table permohonan koreksi rekening untuk dapat status
                };

                _ = ((AsyncCommandBase)viewModel.OnCariPermohonanCommand).ExecuteAsync(param);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Select_Click();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel)
                CariPelangganButton_Click(null, null);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.CurrentPagePelanggan--;
                CariPelangganButton_Click(null, null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.CurrentPagePelanggan++;
                CariPelangganButton_Click(null, null);
            }
        }

        private void Select_Click(object sender = null, RoutedEventArgs e = null)
        {
            var item = DataGridContent.SelectedItem;
            var viewModel = (UsulanKoreksiViewModel)DataContext;

            viewModel.SelectedPermohonanAir = item as PermohonanWpf;
            viewModel.CurrentStep = 2;

            _ = ((AsyncCommandBase)viewModel.GetPiutangListCommand).ExecuteAsync(null);

        }
    }
}
