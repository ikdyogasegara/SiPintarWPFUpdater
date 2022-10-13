using System;
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

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(InteraksiJenisPersediaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.Parent.IsLoading = true;

            _viewModel.TableColumnConfiguration(_isTest);

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Value },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            if (_viewModel.Parent.IsJenisChecked && _viewModel.Parent.FilterJenis?.IdJenisBarang != null)
                param.Add("IdJenis", _viewModel.Parent.FilterJenis.IdJenisBarang!);

            if (_viewModel.Parent.IsKeperluanChecked && _viewModel.Parent.FilterKeperluan?.IdKeperluan != null)
                param.Add("IdKeperluan", _viewModel.Parent.FilterKeperluan.IdKeperluan!);

            if (_viewModel.Parent.IsDebetChecked && _viewModel.Parent.FilterDebet?.IdPerkiraan3 != null)
                param.Add("IdDebet", _viewModel.Parent.FilterDebet.IdPerkiraan3!);

            if (_viewModel.Parent.IsKreditChecked && _viewModel.Parent.FilterKredit?.IdPerkiraan3 != null)
                param.Add("IdKredit", _viewModel.Parent.FilterKredit.IdPerkiraan3!);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-inpersediaan", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<MasterInPersediaanDto>>()!;
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
            _ = _viewModel.DataList.Count > 100 ? _viewModel.IsOverLimit : !_viewModel.IsOverLimit;

            _viewModel.Parent.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
