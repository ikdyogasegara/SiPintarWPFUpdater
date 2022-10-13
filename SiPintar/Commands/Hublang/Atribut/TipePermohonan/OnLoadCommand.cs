using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePermohonan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TipePermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;


        public OnLoadCommand(TipePermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            #region Get Jenis Nonair
            await GetJenisNonAirListAsync();
            #endregion

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

            _viewModel.MasterTipePermohonanList = new ObservableCollection<MasterTipePermohonanDto>();

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


            _viewModel.IsEmpty = !_viewModel.MasterTipePermohonanList.Any();

            _viewModel.TipePermohonanList = _viewModel.MasterTipePermohonanList;
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        public async Task GetJenisNonAirListAsync()
        {

            var ResponseJenis = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-non-air");

            _viewModel.MasterJenisNonAirList = new ObservableCollection<MasterJenisNonAirDto>();

            if (!ResponseJenis.IsError)
            {
                var Result = ResponseJenis.Data;

                if (Result.Status)
                {
                    _viewModel.MasterJenisNonAirList = Result.Data.ToObject<ObservableCollection<MasterJenisNonAirDto>>();
                    _viewModel.TotalRecord = (int)ResponseJenis.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }

            }
        }
    }
}
