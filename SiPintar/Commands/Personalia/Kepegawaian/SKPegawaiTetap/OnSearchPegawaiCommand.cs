using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnSearchPegawaiCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSearchPegawaiCommand(SKPegawaiTetapViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                { "IdStatusPegawai", 2 },
                { "NamaPegawai", _viewModel.FilterFormNamaPegawai }
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PegawaiList = Result.Data.ToObject<ObservableCollection<PegawaiTetapWpf>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

    }
}
