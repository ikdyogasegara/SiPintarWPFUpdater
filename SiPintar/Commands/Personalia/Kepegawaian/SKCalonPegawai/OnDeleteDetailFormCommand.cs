using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnDeleteDetailFormCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnDeleteDetailFormCommand(SKCalonPegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormDetailList.Remove(_viewModel.FormDetailList.FirstOrDefault(d => d.IdPegawai == _viewModel.SelectedDetailData.IdPegawai));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
