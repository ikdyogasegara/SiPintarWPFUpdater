using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Pengaturan;

namespace SiPintar.Commands.Akuntansi.Pengaturan.DaftarPosting
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly DaftarPostingViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(DaftarPostingViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _ = _restApi;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
