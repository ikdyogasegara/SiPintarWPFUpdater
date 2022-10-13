using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            TableColumnConfiguration();

            _viewModel.IsLoading = true;
            _viewModel.IsEmpty = true;

            _ = GetPeriodeAsync();
            _ = GetStatusAsync();
            _ = GetKelainanAsync();
            _ = GetRayonAsync();
            _ = GetGolonganAsync();
            _ = GetDiameterAsync();
            _ = GetAdministrasiLainAsync();
            _ = GetPemeliharaanLainAsync();
            _ = GetRetribusiLainAsync();
            _ = GetMeteraiAsync();
            _ = GetKelurahanAsync();
            _ = GetKecamatanAsync();
            await GetPetugasBacaAsync();

            _viewModel.JenisVerifikasiFilter = _viewModel.JenisVerifikasiList[0];

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        public async Task GetPeriodeAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "status", true }
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                }
            }
        }

        public async Task GetKelainanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kelainan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KelainanList = Result.Data.ToObject<ObservableCollection<MasterKelainanDto>>();
                }
            }
        }

        public async Task GetRayonAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-rayon");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }
        }

        public async Task GetGolonganAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-golongan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.GolonganList = Result.Data.ToObject<ObservableCollection<MasterGolonganDto>>();
                }
            }
        }

        public async Task GetDiameterAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-diameter");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DiameterList = Result.Data.ToObject<ObservableCollection<MasterDiameterDto>>();
                }
            }
        }

        public async Task GetAdministrasiLainAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-administrasi-lain");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.AdministrasiLainList = Result.Data.ToObject<ObservableCollection<MasterAdministrasiLainDto>>();
                }
            }
        }

        public async Task GetPemeliharaanLainAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-pemeliharaan-lain");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PemeliharaanLainList = Result.Data.ToObject<ObservableCollection<MasterPemeliharaanLainDto>>();
                }
            }
        }

        public async Task GetRetribusiLainAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-retribusi-lain");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RetribusiLainList = Result.Data.ToObject<ObservableCollection<MasterRetribusiLainDto>>();
                }
            }
        }

        public async Task GetMeteraiAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-meterai");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.MeteraiList = Result.Data.ToObject<ObservableCollection<MasterMeteraiDto>>();
                }
            }
        }

        public async Task GetKelurahanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kelurahan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KelurahanList = Result.Data.ToObject<ObservableCollection<MasterKelurahanDto>>();
                }
            }
        }

        public async Task GetKecamatanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-kecamatan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KecamatanList = Result.Data.ToObject<ObservableCollection<MasterKecamatanDto>>();
                }
            }
        }

        public async Task GetPetugasBacaAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-petugas-baca");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PetugasBacaList = Result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();
                }
            }
        }

        public async Task GetStatusAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-status");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.StatusList = Result.Data.ToObject<ObservableCollection<MasterStatusDto>>();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Bacameter\\supervisi_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                IdPelanggan = data["table_supervisi"]["id_pelanggan"] == "1",
                NamaPelanggan = data["table_supervisi"]["nama_pelanggan"] == "1",
                Status = data["table_supervisi"]["status_aktif"] == "1",
                Verifikasi = data["table_supervisi"]["status_verifikasi"] == "1",
                StanAwal = data["table_supervisi"]["stan_awal"] == "1",
                StanAkhir = data["table_supervisi"]["stan_akhir"] == "1",
                Pakai = data["table_supervisi"]["pakai"] == "1",
                StanAngkat = data["table_supervisi"]["stan_angkat"] == "1",
                BiayaPemakaian = data["table_supervisi"]["biayapemakaian"] == "1",
                Administrasi = data["table_supervisi"]["administrasi"] == "1",
                Pemeliharaan = data["table_supervisi"]["pemeliharaan"] == "1",
                Retribusi = data["table_supervisi"]["retribusi"] == "1",
                Pelayanan = data["table_supervisi"]["pelayanan"] == "1",
                AirLimbah = data["table_supervisi"]["airlimbah"] == "1",
                DendaPakai0 = data["table_supervisi"]["dendapakai0"] == "1",
                AdministrasiLain = data["table_supervisi"]["administrasilain"] == "1",
                PemeliharaanLain = data["table_supervisi"]["pemeliharaanlain"] == "1",
                RetribusiLain = data["table_supervisi"]["retribusilain"] == "1",
                Ppn = data["table_supervisi"]["ppn"] == "1",
                Meterai = data["table_supervisi"]["meterai"] == "1",
                Rekair = data["table_supervisi"]["rekair"] == "1",
                Denda = data["table_supervisi"]["denda"] == "1",
                Total = data["table_supervisi"]["total"] == "1",
                MerekMeter = data["table_supervisi"]["merek_meter"] == "1",
                KodeRayon = data["table_supervisi"]["kode_rayon"] == "1",
                Rayon = data["table_supervisi"]["rayon"] == "1",
                Alamat = data["table_supervisi"]["alamat"] == "1",
                NoSambungan = data["table_supervisi"]["no_sambungan"] == "1",
                KodeGolongan = data["table_supervisi"]["kode_golongan"] == "1",
                Golongan = data["table_supervisi"]["golongan"] == "1",
                Diameter = data["table_supervisi"]["diameter"] == "1",
                Kelainan = data["table_supervisi"]["kelainan"] == "1",
                MetodeBaca = data["table_supervisi"]["metode_baca"] == "1",
                NamaPetugas = data["table_supervisi"]["nama_petugas"] == "1",
                WaktuVerifikasi = data["table_supervisi"]["waktu_verifikasi"] == "1",
                WaktuBaca = data["table_supervisi"]["waktu_baca"] == "1",
                Lampiran = data["table_supervisi"]["lampiran"] == "1",
                WideRowHeight = data["table_supervisi"]["wide_row_height"] == "1",
                NormalRowHeight = data["table_supervisi"]["normal_row_height"] == "1",
                NarrowRowHeight = data["table_supervisi"]["narrow_row_height"] == "1"
            };

            _viewModel.FilterSetting = new
            {
                JenisData = data["filter_supervisi"]["jenis_data"] == "1",
                RentangWaktu = data["filter_supervisi"]["rentang_waktu"] == "1",
                TanggalBaca = data["filter_supervisi"]["tanggal_baca"] == "1",
                TelatBaca = data["filter_supervisi"]["telat_baca"] == "1",
                Kelainan = data["filter_supervisi"]["kelainan"] == "1",
                JumlahPakai = data["filter_supervisi"]["jumlah_pakai"] == "1",
                StanAwal = data["filter_supervisi"]["stan_awal"] == "1",
                StanAkhir = data["filter_supervisi"]["stan_akhir"] == "1",
                JenisPipa = data["filter_supervisi"]["jenis_pipa"] == "1",
                HitungAirLimbah = data["filter_supervisi"]["hitung_air_limbah"] == "1",
                SegelRumah = data["filter_supervisi"]["segel_rumah"] == "1",
                KodeRayon = data["filter_supervisi"]["kode_rayon"] == "1",
                Rayon = data["filter_supervisi"]["rayon"] == "1",
                KodeGolongan = data["filter_supervisi"]["kode_golongan"] == "1",
                Golongan = data["filter_supervisi"]["golongan"] == "1",
                KodeKelurahan = data["filter_supervisi"]["kode_kelurahan"] == "1",
                Kelurahan = data["filter_supervisi"]["kelurahan"] == "1",
                KodeKecamatan = data["filter_supervisi"]["kode_kecamatan"] == "1",
                Kecamatan = data["filter_supervisi"]["kecamatan"] == "1",
                Alamat = data["filter_supervisi"]["alamat"] == "1",
                LuasRumah = data["filter_supervisi"]["luas_rumah"] == "1",
                Keakuratan = data["filter_supervisi"]["keakuratan"] == "1",
                PetugasBaca = data["filter_supervisi"]["petugas_baca"] == "1",
                NamaPelanggan = data["filter_supervisi"]["nama_pelanggan"] == "1",
                NoSambungan = data["filter_supervisi"]["no_sambungan"] == "1",
                Lainnya = data["filter_supervisi"]["lainnya"] == "1"
            };
        }
    }
}
