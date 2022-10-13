using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnSaveDetailFormCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnSaveDetailFormCommand(SKCalonPegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsEditDetail)
            {
                _viewModel.FormDetailData.KodeGolonganPegawai = _viewModel.GolonganFormList.FirstOrDefault(k => k.IdGolonganPegawai == _viewModel.FormDetailData.IdGolonganPegawai)?.KodeGolonganPegawai;
                _viewModel.FormDetailList.Add((MutasiStatusCapegDetailDto)_viewModel.FormDetailData.Clone());
            }
            else
            {
                var item = _viewModel.FormDetailList.FirstOrDefault(s => s.IdPegawai == _viewModel.SelectedDetailData.IdPegawai);
                var index = _viewModel.FormDetailList.IndexOf(item);

                var formDetail = (MutasiStatusCapegDetailDto)_viewModel.FormDetailList[index].Clone();

                formDetail.NoPegawaiBaru = _viewModel.FormDetailData.NoPegawaiBaru;
                formDetail.IdGolonganPegawai = _viewModel.FormDetailData.IdGolonganPegawai;
                formDetail.KodeGolonganPegawai = _viewModel.GolonganFormList.FirstOrDefault(k => k.IdGolonganPegawai == _viewModel.FormDetailData.IdGolonganPegawai)?.KodeGolonganPegawai;
                formDetail.Mkg_Thn = _viewModel.FormDetailData.Mkg_Thn;
                formDetail.Mkg_Bln = _viewModel.FormDetailData.Mkg_Bln;

                _viewModel.FormDetailList[index] = (MutasiStatusCapegDetailDto)formDetail.Clone();
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
