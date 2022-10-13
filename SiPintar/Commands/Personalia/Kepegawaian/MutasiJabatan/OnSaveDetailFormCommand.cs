using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnSaveDetailFormCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly bool _isTest;

        public OnSaveDetailFormCommand(MutasiJabatanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsEditDetail)
            {
                _viewModel.FormDetailData.No = _viewModel.FormDetailList.Count > 0 ? _viewModel.FormDetailList.Max(k => k.No) + 1 : 1;
                _viewModel.FormDetailData.JabatanBaru = _viewModel.JabatanFormList.FirstOrDefault(k => k.IdJabatan == _viewModel.FormDetailData.IdJabatanBaru)?.Jabatan;
                _viewModel.FormDetailData.DepartemenBaru = _viewModel.DepartemenFormList.FirstOrDefault(k => k.IdDepartemen == _viewModel.FormDetailData.IdDepartemenBaru)?.Departemen;
                _viewModel.FormDetailData.AreaKerjaBaru = _viewModel.AreaKerjaFormList.FirstOrDefault(k => k.IdAreaKerja == _viewModel.FormDetailData.IdAreaKerjaBaru)?.AreaKerja;
                _viewModel.FormDetailData.DivisiBaru = _viewModel.DivisiFormList.FirstOrDefault(k => k.IdDivisi == _viewModel.FormDetailData.IdDivisiBaru)?.Divisi;
                _viewModel.FormDetailList.Add((MutasiJabatanDetailWpf)_viewModel.FormDetailData.Clone());
            }
            else
            {
                var item = _viewModel.FormDetailList.FirstOrDefault(s => s.IdPegawai == _viewModel.SelectedDetailData.IdPegawai);
                var index = _viewModel.FormDetailList.IndexOf(item);

                var formDetail = (MutasiJabatanDetailWpf)_viewModel.FormDetailList[index].Clone();

                formDetail.IdJabatanBaru = _viewModel.FormDetailData.IdJabatanBaru;
                formDetail.JabatanBaru = _viewModel.JabatanFormList.FirstOrDefault(k => k.IdJabatan == _viewModel.FormDetailData.IdJabatanBaru)?.Jabatan;
                formDetail.IdDepartemenBaru = _viewModel.FormDetailData.IdDepartemenBaru;
                formDetail.DepartemenBaru = _viewModel.DepartemenFormList.FirstOrDefault(k => k.IdDepartemen == _viewModel.FormDetailData.IdDepartemenBaru)?.Departemen;
                formDetail.IdAreaKerjaBaru = _viewModel.FormDetailData.IdAreaKerjaBaru;
                formDetail.AreaKerjaBaru = _viewModel.AreaKerjaFormList.FirstOrDefault(k => k.IdAreaKerja == _viewModel.FormDetailData.IdAreaKerjaBaru)?.AreaKerja;
                formDetail.IdDivisiBaru = _viewModel.FormDetailData.IdDivisiBaru;
                formDetail.DivisiBaru = _viewModel.DivisiFormList.FirstOrDefault(k => k.IdDivisi == _viewModel.FormDetailData.IdDivisiBaru)?.Divisi;
                formDetail.TugasBaru = _viewModel.FormDetailData.TugasBaru;

                _viewModel.FormDetailList[index] = (MutasiJabatanDetailWpf)formDetail.Clone();
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
