using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using NPOI.XSSF.UserModel;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan3ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(KelompokKodePerkiraan3ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsLoading = true;

            TableColumnConfiguration();
            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" , _viewModel.LimitData.Value },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            //add filter
            if (_viewModel.Parent.IsKodePerkiraanChecked && _viewModel.Parent.FilterKodePerkiraan != null)
                param.Add("KodePerkiraan3", _viewModel.Parent.FilterKodePerkiraan);

            if (_viewModel.Parent.IsNamaPerkiraanChecked && _viewModel.Parent.FilterNamaPerkiraan != null)
                param.Add("NamaPerkiraan3", _viewModel.Parent.FilterNamaPerkiraan);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-perkiraan3", param);
            _viewModel.DataList = new ObservableCollection<MasterPerkiraan3Wpf>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var data = result.Data.ToObject<ObservableCollection<MasterPerkiraan3Wpf>>()!.OrderBy(x => x.KodePerkiraan3);
                    _viewModel.DataList = new ObservableCollection<MasterPerkiraan3Wpf>(data);
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
            _viewModel.Parent.IsEmpty = _viewModel.DataList.Count == 0;
            _ = _viewModel.TotalRecord > 100 ? _viewModel.IsOverLimit : !_viewModel.IsOverLimit;


            _viewModel.Parent.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_kelompok_kode_perkiraan_3_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodePerkiraan = data["show_table_column"]["KodePerkiraan"] == "1",
                NamaPerkiraan = data["show_table_column"]["NamaPerkiraan"] == "1",
                KodeAkunEtap = data["show_table_column"]["KodeAkunEtap"] == "1",
                NamaAkunEtap = data["show_table_column"]["NamaAkunEtap"] == "1",
                KodeGolAktiva = data["show_table_column"]["KodeGolAktiva"] == "1",
                NamaGolAktiva = data["show_table_column"]["NamaGolAktiva"] == "1",
                KodeJenisVoucher = data["show_table_column"]["KodeJenisVoucher"] == "1",
                NamaJenisVoucher = data["show_table_column"]["NamaJenisVoucher"] == "1",
            };
        }
    }
}
