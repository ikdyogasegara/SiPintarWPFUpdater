using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnSavePesertaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnSavePesertaFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsEditPeserta)
            {
                _viewModel.FormPesertaData.Biaya = _viewModel.FormPesertaBiayaList.ToList();
                _viewModel.FormPesertaList.Add((SppdPesertaWpf)_viewModel.FormPesertaData.Clone());
            }
            else
            {
                var item = _viewModel.FormPesertaList.FirstOrDefault(s => s.IdPegawai == _viewModel.SelectedDataPeserta.IdPegawai);
                var index = _viewModel.FormPesertaList.IndexOf(item);

                var formPeserta = (SppdPesertaWpf)_viewModel.FormPesertaList[index].Clone();

                formPeserta.Biaya = _viewModel.FormPesertaBiayaList.ToList();

                _viewModel.FormPesertaList[index] = (SppdPesertaWpf)formPeserta.Clone();
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
