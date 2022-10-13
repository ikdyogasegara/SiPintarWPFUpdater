using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FilterKategori = "";
            _viewModel.FilterKodeTarif = "";
            _viewModel.FilterNamaTarif = "";
            _viewModel.IsKategoriChecked = false;
            _viewModel.IsKodeTarifChecked = false;
            _viewModel.IsNamaTarifChecked = false;
            await Task.FromResult(_isTest);
        }
    }
}
