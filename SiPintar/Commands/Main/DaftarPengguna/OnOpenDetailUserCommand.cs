using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;
using SiPintar.Views;
using SiPintar.Views.Main.DaftarPengguna;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnOpenDetailUserCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenDetailUserCommand(DaftarPenggunaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            DialogHelper.ShowLoading(_isTest, "MainRootDialog");

            _viewModel.DetailData = null;
            _viewModel.DetailPermission = null;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            string ErrorMessage = null;

            var param = new Dictionary<string, dynamic>
            {
                { "IdUser", _viewModel.SelectedData.IdUser },
                { "IncludeAkses", true }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-user", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterUserDto>>();
                    if (data.Count > 0)
                    {
                        _viewModel.DetailData = data[0];
                        SetModulePermission();
                    }
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            _viewModel.IsLoadingForm = false;

            ShowSnackbar(ErrorMessage);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void SetModulePermission()
        {
            var Permission = new List<string>();
            foreach (var item in _viewModel.DetailData.Akses)
            {
                // per menu
                if (!Permission.Contains($"{item.NamaModule}.{item.NamaAccess}") && item.Value == true)
                    Permission.Add($"{item.NamaModule}.{item.NamaAccess}");
            }

            _viewModel.DetailPermission = new
            {
                BillingPeriode = Permission.Contains("Billing.Periode"),
                BillingRekeningAir = Permission.Contains("Billing.Rekening_Air"),
                BillingRekeningLimbah = Permission.Contains("Billing.Rekening_Limbah"),
                BillingRekeningL2T2 = Permission.Contains("Billing.Rekening_L2T2"),
                BillingPelangganAir = Permission.Contains("Billing.Pelanggan_Air"),
                BillingPelangganLimbah = Permission.Contains("Billing.Pelanggan_Limbah"),
                BillingPelangganL2T2 = Permission.Contains("Billing.Pelanggan_L2T2"),
                BillingKoreksiRekeningAir = Permission.Contains("Billing.Koreksi_Rekening_Air"),
                BillingPenghapusanRekening = Permission.Contains("Billing.Penghapusan_Rekening"),
                BillingUploadDownload = Permission.Contains("Billing.Upload_Download"),
                BillingPosting = Permission.Contains("Billing.Posting"),
                BillingTarif = Permission.Contains("Billing.Atribut_Tarif"),
                BillingWilayahAdministrasi = Permission.Contains("Billing.Atribut_Wilayah_Administrasi"),
                BillingLoket = Permission.Contains("Billing.Atribut_Loket"),
                BillingMeteran = Permission.Contains("Billing.Atribut_Meteran"),
                BillingSumberAir = Permission.Contains("Billing.Atribut_Sumber_Air"),
                BillingKolektif = Permission.Contains("Billing.Atribut_Kolektif"),
                BillingKepemilikan = Permission.Contains("Billing.Atribut_Kepemilikan"),
                BillingStatus = Permission.Contains("Billing.Atribut_Status"),
                BillingFlag = Permission.Contains("Billing.Atribut_Flag"),
                LoketTagihanSR = Permission.Contains("Loket.Tagihan_Pelanggan_SR"),
                LoketTagihanNonAirLainnya = Permission.Contains("Loket.Tagihan_Non_Air_Lainnya"),
                LoketTagihanKolektifAir = Permission.Contains("Loket.Tagihan_Kolektif_Air"),
                LoketTagihanKolektifNonAir = Permission.Contains("Loket.Tagihan_Kolektif_Non_Air"),
                LoketAngsuran = Permission.Contains("Loket.Angsuran"),
                LoketTunggakan = Permission.Contains("Loket.Tunggakan"),
                LoketSetoran = Permission.Contains("Loket.Setoran"),
                LoketTutupLoket = Permission.Contains("Loket.Tutup_Loket"),
                BacameterSupervisi = Permission.Contains("Bacameter.Supervisi"),
                BacameterProduktivitas = Permission.Contains("Bacameter.Produktivitas"),
                BacameterPemetaanPelanggan = Permission.Contains("Bacameter.Pemetaan_Pelanggan"),
                BacameterRuteBaca = Permission.Contains("Bacameter.Rute_Baca_Meter"),
                BacameterWilayahAdministrasi = Permission.Contains("Bacameter.Atribut_Wilayah_Administrasi"),
                BacameterTarifGolongan = Permission.Contains("Bacameter.Atribut_Tarif_Golongan"),
                BacameterPetugasBaca = Permission.Contains("Bacameter.Atribut_Petugas_Baca"),
                BacameterDaftarKelainan = Permission.Contains("Bacameter.Atribut_Daftar_Kelainan"),
                BacameterDataPelanggan = Permission.Contains("Bacameter.Atribut_Data_Pelanggan"),
                BacameterDistribusiPelanggan = Permission.Contains("Bacameter.Laporan_Distribusi_Pelanggan"),
                BacameterJadwalRuteBaca = Permission.Contains("Bacameter.Laporan_Jadwal_Rute_Baca"),
                BacameterPeriode = Permission.Contains("Bacameter.Periode"),
                BacameterPengaturanUmum = Permission.Contains("Bacameter.Pengaturan_Umum"),
                HublangPermohonan = Permission.Contains("Hublang.Permohonan"),
                HublangInfo = Permission.Contains("Hublang.Info"),
                HublangDataPelanggan = Permission.Contains("Hublang.Atribut_Data_Pelanggan"),
                HublangTipePermohonan = Permission.Contains("Hublang.Atribut_Tipe_Permohonan"),
                HublangJenisNonAir = Permission.Contains("Hublang.Atribut_Jenis_Non_Air"),
                HublangTarifAirTangki = Permission.Contains("Hublang.Atribut_Tarif_Air_Tangki"),
                HublangTipePendaftaran = Permission.Contains("Hublang.Atribut_Tipe_Pendaftaran"),
                HublangPekerjaan = Permission.Contains("Hublang.Atribut_Pekerjaan"),
                HublangJenisBangunan = Permission.Contains("Hublang.Atribut_Jenis_Bangunan"),
                HublangPeruntukan = Permission.Contains("Hublang.Atribut_Peruntukan"),
                PerencanaanUsulan = Permission.Contains("Perencanaan.Usulan"),
                PerencanaanSPK = Permission.Contains("Perencanaan.SPK"),
                PerencanaanRAB = Permission.Contains("Perencanaan.RAB"),
                PerencanaanMaterial = Permission.Contains("Perencanaan.Atribut_Material"),
                PerencanaanOngkos = Permission.Contains("Perencanaan.Atribut_Ongkos"),
                PerencanaanPaket = Permission.Contains("Perencanaan.Atribut_Paket"),
                PerencanaanRumusVolume = Permission.Contains("Perencanaan.Atribut_Rumus_Volume"),
                DistribusiBA = Permission.Contains("Distribusi.BA"),
                DistribusiGantiMeter = Permission.Contains("Distribusi.Ganti_Meter"),
            };
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { ShowInformation(ErrorMessage); });
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowInformation(string ErrorMessage)
        {
            var mainView = (MainView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(ErrorMessage, "danger");
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                _ = DialogHost.Show(new DetailUserView(_viewModel), "MainRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close("MainRootDialog");
        }
    }
}
