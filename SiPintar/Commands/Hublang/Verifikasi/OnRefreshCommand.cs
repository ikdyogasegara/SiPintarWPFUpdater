using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang;
using SiPintar.Views;

namespace SiPintar.Commands.Hublang.Verifikasi
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly VerifikasiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(VerifikasiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedJenis == null || _viewModel.SelectedJenis.JumlahWpf == 0)
            {
                _viewModel.IsShowListData = false;
                return;
            }

            _viewModel.IsLoading = true;
            _viewModel.IsShowListData = true;

            var param = new Dictionary<string, dynamic>()
            {
                {"IdTipePermohonan", _viewModel.SelectedJenis.IdTipePermohonan },
                {"StatusPermohonanText", "Menunggu Verifikasi" },
                {"IncludeRabDetail", false },
                {"pageSize", _viewModel.LimitData.Key },
                {"currentPage", _viewModel.CurrentPage },
            };

            //add filter
            if (_viewModel.IsNoSambunganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNoSambungan) && _viewModel.IsPelangganAir)
                param.Add("NoSamb", _viewModel.FilterNoSambungan);
            if (_viewModel.IsNoSambunganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNoSambungan) && _viewModel.IsPelangganLimbah)
                param.Add("NomorLimbah", _viewModel.FilterNoSambungan);
            if (_viewModel.IsNoSambunganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNoSambungan) && _viewModel.IsPelangganLltt)
                param.Add("NomorLltt", _viewModel.FilterNoSambungan);
            if (_viewModel.IsNomorRegisterChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorRegister))
                param.Add("NomorPermohonan", _viewModel.FilterNomorRegister);
            if (_viewModel.IsNamaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNama))
                param.Add("Nama", _viewModel.FilterNama);
            if (_viewModel.IsAlamatChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterAlamat))
                param.Add("Alamat", _viewModel.FilterAlamat);
            if (_viewModel.IsGolonganChecked && _viewModel.FilterGolongan != null)
                param.Add("IdGolongan", _viewModel.FilterGolongan.IdGolongan);
            if (_viewModel.IsTarifLimbahChecked && _viewModel.FilterTarifLimbah != null)
                param.Add("IdTarifLimbah", _viewModel.FilterTarifLimbah.IdTarifLimbah);
            if (_viewModel.IsTarifLlttChecked && _viewModel.FilterTarifLltt != null)
                param.Add("IdTarifLltt", _viewModel.FilterTarifLltt.IdTarifLltt);
            if (_viewModel.IsRayonChecked && _viewModel.FilterRayon != null)
                param.Add("IdRayon", _viewModel.FilterRayon.IdRayon);
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

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", param);
            _viewModel.DataList = new ObservableCollection<PermohonanWpf>();

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<PermohonanWpf>>();
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;
            _viewModel.IsLoading = false;

            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\verifikasi_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Jenis = data["show_table_column"]["Jenis"] == "1",
                Status = data["show_table_column"]["Status"] == "1",
                NomorRegister = data["show_table_column"]["NomorRegister"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                NoBeritaAcara = data["show_table_column"]["NoBeritaAcara"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                Kecamatan = data["show_table_column"]["Kecamatan"] == "1",
                Cabang = data["show_table_column"]["Cabang"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                Biaya = data["show_table_column"]["Biaya"] == "1",
                UserInput = data["show_table_column"]["UserInput"] == "1",
                UserBeritaAcara = data["show_table_column"]["UserBeritaAcara"] == "1"
            };
        }
    }
}
