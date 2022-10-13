using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.RotasiMeter
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(RotasiMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as Dictionary<string, dynamic>;
            _viewModel.DataList = null;
            _viewModel.IsEmpty = true;
            _viewModel.IsLoading = true;

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", param);
            _viewModel.DataList = new ObservableCollection<JadwalGantiMeterWpf>();

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<JadwalGantiMeterWpf>>();
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
            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;
            _viewModel.IsLoading = false;
            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (!_isTest)
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_rotasi_meter_config.ini";
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
}
