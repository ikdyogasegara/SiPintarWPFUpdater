using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnDeletePesertaBiayaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnDeletePesertaBiayaFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormPesertaBiayaList.Remove(_viewModel.FormPesertaBiayaList.FirstOrDefault(d => d == _viewModel.SelectedDataPesertaBiaya));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
