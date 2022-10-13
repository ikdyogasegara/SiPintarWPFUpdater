using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePendaftaran
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TipePendaftaranViewModel _viewModel;

        public OnResetFilterCommand(TipePendaftaranViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNamaTipePendaftaranSambunganChecked = false;
            _viewModel.FilterNamaTipePendaftaranSambungan = null;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
