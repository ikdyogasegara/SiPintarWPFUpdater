using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.Pengolahan;

namespace SiPintar.Commands.Gudang.Pengolahan.OpnameBarangBulanan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly OpnameBarangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(OpnameBarangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            _ = Task.Run(() => _viewModel.Data.RefreshPageCommand.Execute(null));
            await Task.FromResult(_isTest);
        }
    }
}
