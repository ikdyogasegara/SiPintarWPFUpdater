using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(InteraksiPenyusutanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.Parent.IsLoading = true;

            TableColumnConfiguration();

            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" , _viewModel.LimitData.Value },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            //add filter
            if (_viewModel.Parent.IsAkunAktivaChecked && _viewModel.Parent.FilterAkunAktiva?.IdPerkiraan2 != null)
                param.Add("IdAkunAktiva", _viewModel.Parent.FilterAkunAktiva.IdPerkiraan2!);

            if (_viewModel.Parent.IsAkunAkmPenyusutanChecked && _viewModel.Parent.FilterAkunAkmPenyusutan?.IdPerkiraan3 != null)
                param.Add("IdAkunAkmPeny", _viewModel.Parent.FilterAkunAkmPenyusutan.IdPerkiraan3!);

            if (_viewModel.Parent.IsAkunBiayaChecked && _viewModel.Parent.FilterAkunBiaya?.IdPerkiraan3 != null)
                param.Add("IdAkunBiaya", _viewModel.Parent.FilterAkunBiaya.IdPerkiraan3!);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-inpenyusutan", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<MasterInPenyusutanDto>>()!;
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
            _viewModel.IsOverLimit = _viewModel.DataList.Count > 100;

            _viewModel.Parent.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_penyusutan_config.ini";

            if (!File.Exists(configIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                AkunAktiva = data["show_table_column"]["AkunAktiva"] == "1",
                NamaAktiva = data["show_table_column"]["NamaAktiva"] == "1",
                AkunPenyusutan = data["show_table_column"]["AkunPenyusutan"] == "1",
                AkumulasiPenyusutan = data["show_table_column"]["AkumulasiPenyusutan"] == "1"
            };

        }

    }
}
