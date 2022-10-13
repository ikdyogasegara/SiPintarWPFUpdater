using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePermohonan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TipePermohonanViewModel _viewModel;

        public OnResetFilterCommand(TipePermohonanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTipePermohonanChecked = false;
            _viewModel.IsNamaJenisNonairChecked = false;

            _viewModel.TipePermohonanFilter = new MasterTipePermohonanDto();
            _viewModel.OnFilterCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
