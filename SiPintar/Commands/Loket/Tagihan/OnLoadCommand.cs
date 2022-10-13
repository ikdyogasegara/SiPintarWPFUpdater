using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Tagihan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TagihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(TagihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFullPage = false;
            _viewModel.IsLoadingMainPage = true;

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterUser",
                "MasterLoket",
                "MasterAlasanBatal",
                "MasterRayon",
                "MasterKolektif",
                "MasterPeriode",
                "MasterJenisNonAir"
            });

            _viewModel.UpdatePage("Kolektif Air");
            _viewModel.IsLoadingMainPage = false;
            await Task.FromResult(_isTest);
        }
    }
}
