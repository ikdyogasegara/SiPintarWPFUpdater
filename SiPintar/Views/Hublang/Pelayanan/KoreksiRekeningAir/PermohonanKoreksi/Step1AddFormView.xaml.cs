using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
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
            if (!string.IsNullOrWhiteSpace(NamaPelanggan.Text) || !string.IsNullOrWhiteSpace(Alamat.Text) || !string.IsNullOrWhiteSpace(NomorSambungan.Text))
            {
                if (DataContext is PermohonanKoreksiViewModel viewModel)
                {
                    viewModel.DataPelanggan.Clear();
                    var param = new Dictionary<string, dynamic>
                    {
                        {"NoSamb", NomorSambungan.Text},
                        {"Nama", NamaPelanggan.Text},
                        {"Alamat", Alamat.Text},
                        {"pageSize", viewModel.LimitDataPelanggan.Key},
                        {"currentPage", viewModel.CurrentPagePelanggan}
                    };

                    _ = ((AsyncCommandBase)viewModel.OnCariPelangganCommand).ExecuteAsync(param);
                }
            }
        }

        private void Select_Click(object sender = null, RoutedEventArgs e = null)
        {
            var item = DataGridPelanggan.SelectedItem;
            var viewModel = (PermohonanKoreksiViewModel)DataContext;

            viewModel.SelectedPelangganAir = item as MasterPelangganGlobalWpf;
            viewModel.CurrentStep = 2;
            viewModel.IsFor = "add";

            _ = ((AsyncCommandBase)viewModel.GetPiutangListCommand).ExecuteAsync(null);
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Select_Click();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PermohonanKoreksiViewModel)
                CariPelangganButton_Click(null, null);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanKoreksiViewModel viewModel)
            {
                viewModel.CurrentPagePelanggan--;
                CariPelangganButton_Click(null, null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanKoreksiViewModel viewModel)
            {
                viewModel.CurrentPagePelanggan++;
                CariPelangganButton_Click(null, null);
            }
        }
    }
}
