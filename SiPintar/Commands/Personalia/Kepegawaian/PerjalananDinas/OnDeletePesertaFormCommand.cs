using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnDeletePesertaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnDeletePesertaFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormPesertaList.Remove(_viewModel.FormPesertaList.FirstOrDefault(d => d.IdPegawai == _viewModel.SelectedDataPeserta.IdPegawai));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
