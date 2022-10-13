using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan1ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(KelompokKodePerkiraan1ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                param.Add("KodePerkiraan1", _viewModel.Parent.FilterKodePerkiraan);

            if (_viewModel.Parent.IsNamaPerkiraanChecked && _viewModel.Parent.FilterNamaPerkiraan != null)
                param.Add("NamaPerkiraan1", _viewModel.Parent.FilterNamaPerkiraan);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-perkiraan1", param);
            _viewModel.DataList = new ObservableCollection<MasterPerkiraan1Wpf>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var data = result.Data.ToObject<ObservableCollection<MasterPerkiraan1Wpf>>().OrderByDescending(x => x.WaktuUpdate);
                    _viewModel.DataList = new ObservableCollection<MasterPerkiraan1Wpf>(data);
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.Parent.TipePerkiraanList = MasterListGlobal.MasterTipePerkiraan;

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

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_kelompok_kode_perkiraan_1_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);
            _viewModel.TableSetting = new
            {
                KodePerkiraan = data["show_table_column"]["KodePerkiraan"] == "1",
                NamaPerkiraan = data["show_table_column"]["NamaPerkiraan"] == "1",
            };

        }

    }
}
