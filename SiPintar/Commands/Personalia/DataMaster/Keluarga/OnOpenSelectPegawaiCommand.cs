using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Keluarga;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnOpenSelectPegawaiCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenSelectPegawaiCommand(KeluargaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize", 1000000 }
            };
            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PegawaiList = Result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            _viewModel.IsLoading = false;

            if (_viewModel.IsCariDariPegawai)
            {
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "PersonaliaInnerInnerDialog", () => new DialogPegawaiView(_viewModel));
            }
            else
            {
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogPegawaiView(_viewModel));
            }

            await Task.FromResult(_isTest);
        }
    }
}
