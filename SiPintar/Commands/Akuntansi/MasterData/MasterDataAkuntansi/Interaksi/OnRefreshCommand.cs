using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly InteraksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnRefreshCommand(InteraksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PageViewModel == null)
                return;

            switch (_viewModel.PageViewModel)
            {
                case InteraksiLayananViewModel interaksiLayanan:
                    await (interaksiLayanan.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiJenisPersediaanViewModel interaksiJenisPersediaan:
                    await (interaksiJenisPersediaan.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiPenyusutanViewModel interaksiPenyusutan:
                    await (interaksiPenyusutan.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                default:
                    break;
            }

            await Task.FromResult(_isTest);
        }
    }
}
