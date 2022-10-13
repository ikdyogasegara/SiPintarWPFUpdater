
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.SupervisiGaji;

namespace SiPintar.Commands.Personalia.SupervisiGaji.SupervisiGajiThr
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SupervisiGajiThrViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(SupervisiGajiThrViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _ = _viewModel;
            _ = _restApi;

            await Task.FromResult(_isTest);
        }
    }
}
