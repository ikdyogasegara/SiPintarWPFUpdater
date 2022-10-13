using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnDeleteDetailFormCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly bool _isTest;

        public OnDeleteDetailFormCommand(MutasiJabatanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormDetailList.Remove(_viewModel.FormDetailList.FirstOrDefault(d => d.IdPegawai == _viewModel.SelectedDetailData.IdPegawai));
            for (int i = 0; i < _viewModel.FormDetailList.Count; i++)
            {
                var formDetail = (MutasiJabatanDetailWpf)_viewModel.FormDetailList[i].Clone();
                formDetail.No = i + 1;
                _viewModel.FormDetailList[i] = formDetail;
            }
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
