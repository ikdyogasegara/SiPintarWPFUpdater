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
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.PeriodeList = null;
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                {"pageSize", _viewModel.LimitData.Key},
                {"currentPage", _viewModel.CurrentPage},
                {"showDetail", true}
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);

            _viewModel.PeriodeList = new ObservableCollection<MasterPeriodeWpf>();

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                {
                    _viewModel.PeriodeList = result.Data.ToObject<ObservableCollection<MasterPeriodeWpf>>();
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                    _viewModel.IsLoading = false;

                    if (_viewModel.PeriodeList != null && _viewModel.PeriodeList.Count > 0)
                    {
                        _viewModel.SelectedData = _viewModel.PeriodeList[0];
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "billing");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = !_viewModel.PeriodeList.Any();

            _viewModel.IsLoading = false;

            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_periode_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Bulan = data["show_table_column"]["bulan"] == "1",
                PelangganAir = data["show_table_column"]["pelanggan_air"] == "1",
                PelangganLimbah = data["show_table_column"]["pelanggan_limbah"] == "1",
                PelangganL2T2 = data["show_table_column"]["pelanggan_l2t2"] == "1",
                RekeningAir = data["show_table_column"]["rekening_air"] == "1",
                RekeningLimbah = data["show_table_column"]["rekening_limbah"] == "1",
                RekeningL2T2 = data["show_table_column"]["rekening_l2t2"] == "1",
                Status = data["show_table_column"]["status"] == "1",
                JumlahPakaiAir = data["show_table_column"]["jumlahpakaiair"] == "1",
                JumlahKelainan = data["show_table_column"]["jumlahkelainan"] == "1",
                JumlahTaksir = data["show_table_column"]["jumlahtaksir"] == "1",
                PelangganAirPublish = data["show_table_column"]["pelangganairpublish"] == "1",
                PelangganLimbahPublish = data["show_table_column"]["pelangganlimbahpublish"] == "1",
                PelangganLlttPublish = data["show_table_column"]["pelangganllttpublish"] == "1"
            };
        }
    }
}
