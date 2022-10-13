using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;
using SiPintar.Views.Distribusi.Distribusi.GantiMeter.KelainanBacameter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter.KelainanBacameter
{
    [ExcludeFromCodeCoverage]
    public class OnOpenSpkSurveiCommand : AsyncCommandBase
    {
        private readonly KelainanBacameterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenSpkSurveiCommand(KelainanBacameterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = GetPetugasAsync();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        public async Task GetPetugasAsync()
        {
            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", null));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                    _viewModel.PetugasListForm = result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>();
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
        }

        private object GetInstance() => new FormSpkSurveiView(_viewModel);
    }
}
