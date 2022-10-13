using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly bool _isTest;
        private readonly PendaftaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;

        public OnRefreshCommand(PendaftaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedTipePermohonanComboBox == null)
            {
                return;
            }

            _viewModel.DataList = null;
            _viewModel.IsEmpty = true;
            _viewModel.IsLoading = true;

            SetBiaya();

            var param = new Dictionary<string, dynamic>();

            if (_viewModel.SelectedTipePermohonanComboBox != null && _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan.HasValue)
                param.Add("IdTipePermohonan", _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan);

            param.Add("pageSize", _viewModel.LimitData.Key);
            param.Add("currentPage", _viewModel.CurrentPage);

            if (_viewModel.IsStatusPermohonanChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterStatusPermohonan))
                param.Add("StatusPermohonanText", _viewModel.FilterStatusPermohonan);
            if (_viewModel.IsNomorRegisterChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorRegister))
                param.Add("NomorPermohonan", _viewModel.FilterNomorRegister);
            if (_viewModel.IsNamaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNama))
                param.Add("Nama", _viewModel.FilterNama);
            if (_viewModel.IsAlamatChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterAlamat))
                param.Add("Alamat", _viewModel.FilterAlamat);
            if (_viewModel.IsRayonChecked && _viewModel.FilterRayon != null)
                param.Add("Idrayon", _viewModel.FilterRayon.IdRayon);
            if (_viewModel.IsWilayahChecked && _viewModel.FilterWilayah != null)
                param.Add("IdWilayah", _viewModel.FilterWilayah.IdWilayah);
            if (_viewModel.IsKelurahanChecked && _viewModel.FilterKelurahan != null)
                param.Add("IdKelurahan", _viewModel.FilterKelurahan.IdKelurahan);
            if (_viewModel.IsCabangChecked && _viewModel.FilterCabang != null)
                param.Add("IdCabang", _viewModel.FilterCabang.IdCabang);
            if (_viewModel.IsTanggalInputChecked && _viewModel.FilterTanggalInputAwal.HasValue)
                param.Add("TanggalMulaiPermohonan", _viewModel.FilterTanggalInputAwal.Value);
            if (_viewModel.IsTanggalInputChecked && _viewModel.FilterTanggalInputAkhir.HasValue)
                param.Add("TanggalSampaiDenganPermohonan", _viewModel.FilterTanggalInputAkhir.Value);
            if (_viewModel.IsUserInputChecked && _viewModel.FilterUserInput != null)
                param.Add("IdUserPermohonan", _viewModel.FilterUserInput.IdUser);
            if (_viewModel.IsUserBeritaAcaraChecked && _viewModel.FilterUserBeritaAcara != null)
                param.Add("IdUserBeritaAcara", _viewModel.FilterUserBeritaAcara.IdUser);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan", param);
            _viewModel.DataList = new ObservableCollection<PermohonanNonPelangganWpf>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<PermohonanNonPelangganWpf>>();
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = !_viewModel.DataList.Any();
            _viewModel.IsLoading = false;
            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void SetBiaya()
        {
            if (!_isTest)
            {
                var data = _viewModel.SelectedTipePermohonanComboBox;

                _viewModel.JenisNonAir = new MasterJenisNonAirDto()
                {
                    IdJenisNonAir = data.IdJenisNonAir,
                    KodeJenisNonAir = data.KodeJenisNonAir,
                    NamaJenisNonAir = data.NamaJenisNonAir,
                    PersentasePpn = data.PersentasePpn,
                    DetailBiaya = data.DetailBiaya
                };
            }
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            var configIni =
                $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_pendaftaran_sambungan_baru_config.ini";

            if (!File.Exists(configIni))
            {
                return;
            }

            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Flag = data["show_table_column"]["flag"] == "1",
                NomorPendaftaran = data["show_table_column"]["nomor_pendaftaran"] == "1",
                Tanggal = data["show_table_column"]["tanggal"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                NoHandphone = data["show_table_column"]["no_handphone"] == "1",
                NoSambDepan = data["show_table_column"]["no_samb_depan"] == "1",
                NoSambBelakang = data["show_table_column"]["no_samb_belakang"] == "1",
                NoSambKiri = data["show_table_column"]["no_samb_kiri"] == "1",
                NoSambKanan = data["show_table_column"]["no_samb_kanan"] == "1",
                Cabang = data["show_table_column"]["cabang"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Penghuni = data["show_table_column"]["penghuni"] == "1",
                JenisBangunan = data["show_table_column"]["jenis_bangunan"] == "1",
                Pekerjaan = data["show_table_column"]["pekerjaan"] == "1",
                Biaya = data["show_table_column"]["biaya"] == "1",
                Tipe = data["show_table_column"]["tipe"] == "1",
                User = data["show_table_column"]["user"] == "1",
                NomorRAB = data["show_table_column"]["nomor_RAB"] == "1",
                TanggalRAB = data["show_table_column"]["tanggal_RAB"] == "1",
                NomorSPPRAB = data["show_table_column"]["nomor_SPPRAB"] == "1",
                TanggalSPPRAB = data["show_table_column"]["tanggal_SPPRAB"] == "1",
                NomorBA = data["show_table_column"]["nomor_BA"] == "1",
                NomorSPKO = data["show_table_column"]["nomor_SPKO"] == "1",
                NomorSPKP = data["show_table_column"]["nomor_SPKP"] == "1",
                NomorSPA = data["show_table_column"]["nomor_SPA"] == "1"
            };
        }
    }
}
