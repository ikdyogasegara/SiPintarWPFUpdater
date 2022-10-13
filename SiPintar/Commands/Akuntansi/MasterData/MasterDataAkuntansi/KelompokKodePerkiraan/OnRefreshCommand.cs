using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnRefreshCommand(KelompokKodePerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PageViewModel == null)
                return;

            if (_viewModel.PageViewModel is KelompokKodePerkiraan1ViewModel kelompokKodePerkiraan1)
                kelompokKodePerkiraan1.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is KelompokKodePerkiraan2ViewModel kelompokKodePerkiraan2)
                kelompokKodePerkiraan2.OnLoadCommand.Execute(null);
            else if (_viewModel.PageViewModel is KelompokKodePerkiraan3ViewModel kelompokKodePerkiraan3)
                kelompokKodePerkiraan3.OnLoadCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
