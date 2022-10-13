using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnSaveDetailFormCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly bool _isTest;

        public OnSaveDetailFormCommand(SKPegawaiTetapViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsEditDetail)
            {
                _viewModel.FormDetailList.Add((MutasiStatusTetapDetailDto)_viewModel.FormDetailData.Clone());
            }
            else
            {
                var item = _viewModel.FormDetailList.FirstOrDefault(s => s.IdPegawai == _viewModel.SelectedDetailData.IdPegawai);
                var index = _viewModel.FormDetailList.IndexOf(item);

                var formDetail = (MutasiStatusTetapDetailDto)_viewModel.FormDetailList[index].Clone();

                formDetail.IdGolonganPegawai = _viewModel.FormDetailData.IdGolonganPegawai;
                formDetail.Mkg_Thn = _viewModel.FormDetailData.Mkg_Thn;
                formDetail.Mkg_Bln = _viewModel.FormDetailData.Mkg_Bln;

                _viewModel.FormDetailList[index] = (MutasiStatusTetapDetailDto)formDetail.Clone();
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
