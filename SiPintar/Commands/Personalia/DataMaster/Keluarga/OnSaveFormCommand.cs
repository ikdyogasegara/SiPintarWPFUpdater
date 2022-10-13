using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnSaveFormCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnSaveFormCommand(KeluargaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.AnggotaKeluargaForm.JenisKelamin = _viewModel.JenisKelaminList.FirstOrDefault(j => j.IdJenisKelamin == _viewModel.AnggotaKeluargaForm.IdJenisKelamin).JenisKelamin;
            _viewModel.AnggotaKeluargaForm.NamaPekerjaan = _viewModel.PekerjaanList.FirstOrDefault(j => j.IdPekerjaan == _viewModel.AnggotaKeluargaForm.IdPekerjaan).NamaPekerjaan;

            if (!_viewModel.IsEdit)
            {
                _viewModel.SelectedKeluargaListForm.Add(_viewModel.AnggotaKeluargaForm);
            }
            else
            {
                var item = _viewModel.SelectedKeluargaListForm.FirstOrDefault(s => s.IdKeluarga == _viewModel.AnggotaKeluargaForm.IdKeluarga);
                var index = _viewModel.SelectedKeluargaListForm.IndexOf(item);
                _viewModel.SelectedKeluargaListForm[index] = (MasterKeluargaDto)_viewModel.AnggotaKeluargaForm.Clone();
            }

            _viewModel.AnggotaKeluargaForm = new MasterKeluargaDto();
            _viewModel.IsCariDariPegawai = false;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
