using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(BaPengembalianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            //kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEmpty = true;
            _viewModel.DataList = null;
            _viewModel.IsLoading = true;

            var Param = new Dictionary<string, dynamic>();
            Param.Add("AdaBeritaAcara", true);
            Param.Add("pageSize", _viewModel.LimitData.Key);
            Param.Add("currentPage", _viewModel.CurrentPage);

            //add filter
            if (_viewModel.IsFlagChecked && _viewModel.FilterFlag != null)
                Param.Add("IdFlag", _viewModel.FilterFlag.IdFlag);
            if (_viewModel.IsCabangChecked && _viewModel.FilterCabang != null)
                Param.Add("IdCabang", _viewModel.FilterCabang.IdCabang);
            if (_viewModel.IsNomorBaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorBa))
                Param.Add("NomorBa", _viewModel.FilterNomorBa);
            if (_viewModel.IsNomorRegisterChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorRegister))
                Param.Add("NomorPermohonan", _viewModel.FilterNomorRegister);
            if (_viewModel.IsNoSambunganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNoSambungan))
                Param.Add("NoSamb", _viewModel.FilterNoSambungan);
            if (_viewModel.IsNamaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNama))
                Param.Add("Nama", _viewModel.FilterNama);
            if (_viewModel.IsAlamatChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterAlamat))
                Param.Add("Alamat", _viewModel.FilterAlamat);
            if (_viewModel.IsWilayahChecked && _viewModel.FilterWilayah != null)
                Param.Add("IdWilayah", _viewModel.FilterWilayah.IdWilayah);
            if (_viewModel.IsTanggalInputChecked && _viewModel.FilterTanggalInputAwal.HasValue)
                Param.Add("TanggalMulaiPermohonan", _viewModel.FilterTanggalInputAwal.Value);
            if (_viewModel.IsTanggalInputChecked && _viewModel.FilterTanggalInputAkhir.HasValue)
                Param.Add("TanggalSampaiDenganPermohonan", _viewModel.FilterTanggalInputAkhir.Value);

            if (_viewModel.IsTanggalBaChecked && _viewModel.FilterTanggalBaAwal.HasValue)
                Param.Add("TanggalMulaiBa", _viewModel.FilterTanggalBaAwal.Value);
            if (_viewModel.IsTanggalBaChecked && _viewModel.FilterTanggalBaAkhir.HasValue)
                Param.Add("TanggalSampaiDenganBa", _viewModel.FilterTanggalBaAkhir.Value);

            if (_viewModel.IsUserInputChecked && _viewModel.FilterUserInput != null)
                Param.Add("IdUser", _viewModel.FilterUserInput.IdUser);
            if (_viewModel.IsUserVerifikasiChecked && _viewModel.FilterUserVerifikasi != null)
                Param.Add("IdUserVerifikasi", _viewModel.FilterUserVerifikasi.IdUser);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-pengembalian", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<RekeningAirPengembalianDto>>();
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);

            _viewModel.DataList ??= new ObservableCollection<RekeningAirPengembalianDto>();
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
            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_berita_acara_pengembalian_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NoBeritaAcara = data["show_table_column"]["NoBeritaAcara"] == "1",
                NomorSambungan = data["show_table_column"]["NomorSambungan"] == "1",
                Bulan = data["show_table_column"]["Bulan"] == "1",
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                RekairLama = data["show_table_column"]["RekairLama"] == "1",
                RekairBaru = data["show_table_column"]["RekairBaru"] == "1",
                Gol = data["show_table_column"]["Gol"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                Rayon = data["show_table_column"]["Rayon"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                Kecamatan = data["show_table_column"]["Kecamatan"] == "1",
                Cabang = data["show_table_column"]["Cabang"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                KondisiMeter = data["show_table_column"]["KondisiMeter"] == "1",
                Keterangan = data["show_table_column"]["Keterangan"] == "1",
                TanggalInput = data["show_table_column"]["TanggalInput"] == "1",
                UserInput = data["show_table_column"]["UserInput"] == "1",
            };
        }
    }
}
