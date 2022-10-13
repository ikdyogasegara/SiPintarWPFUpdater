using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Golongan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(GolonganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-golongan-pegawai"));
            _viewModel.GolonganList = new ObservableCollection<MasterGolonganPegawaiDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.GolonganList = Result.Data.ToObject<ObservableCollection<MasterGolonganPegawaiDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.GolonganList.Any();

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadGajiPokokCommand).ExecuteAsync(null));
            _viewModel.SelectedData = _viewModel.GolonganList.FirstOrDefault();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
