using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePermohonan
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly TipePermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnFilterCommand(TipePermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEmpty = false;

            _viewModel.MasterTipePermohonanList = new ObservableCollection<MasterTipePermohonanDto>();

            _viewModel.IsLoading = true;
            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" ,  _viewModel.Limit},
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            if (_viewModel.IsTipePermohonanChecked && _viewModel.TipePermohonanFilter.IdTipePermohonan.HasValue)
                param.Add("IdTipePermohonan", _viewModel.TipePermohonanFilter.IdTipePermohonan);

            if (_viewModel.IsNamaJenisNonairChecked && _viewModel.TipePermohonanFilter.IdJenisNonAir.HasValue)
                param.Add("IdJenisNonAir", _viewModel.TipePermohonanFilter.IdJenisNonAir);

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tipe-permohonan", param);

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.MasterTipePermohonanList = Result.Data.ToObject<ObservableCollection<MasterTipePermohonanDto>>();
                    _viewModel.TotalRecord = (int)Response.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);


            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            if (_viewModel.MasterTipePermohonanList.Count == 0)
                _viewModel.IsEmpty = true;

            _viewModel.IsLoading = false;
            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

    }
}
