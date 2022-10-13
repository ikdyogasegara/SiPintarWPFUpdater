using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnSelectPegawaiCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnSelectPegawaiCommand(KeluargaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsCariDariPegawai)
            {
                var dataForm = _viewModel.AnggotaKeluargaForm;

                dataForm.NamaKeluarga = _viewModel.SelectedDataPegawaiForm.NamaPegawai;
                dataForm.IdPekerjaan = _viewModel.PekerjaanList.Where(p => p.NamaPekerjaan.Contains("bumd", StringComparison.OrdinalIgnoreCase)).Select(p => p.IdPekerjaan).FirstOrDefault();
                dataForm.IdJenisKelamin = _viewModel.SelectedDataPegawaiForm.IdJenisKelamin;
                dataForm.NoPenduduk = _viewModel.SelectedDataPegawaiForm.NoPenduduk;
                dataForm.TglLahir = _viewModel.SelectedDataPegawaiForm.TglLahir;
                dataForm.NoBpjsKes = _viewModel.SelectedDataPegawaiForm.NoBpjsKes;
                dataForm.FlagKawin = _viewModel.SelectedDataPegawaiForm.FlagKawin;

                _viewModel.AnggotaKeluargaForm = dataForm;
                DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerInnerDialog");
            }
            else
            {
                _viewModel.SelectedDataPegawai = (MasterPegawaiDto)_viewModel.SelectedDataPegawaiForm.Clone();
                _viewModel.SelectedData = _viewModel.KeluargaList.FirstOrDefault(k => k.IdPegawai == _viewModel.SelectedDataPegawai.IdPegawai);
                _viewModel.SelectedKeluargaList = new ObservableCollection<MasterKeluargaDto>(_viewModel.KeluargaList.Where(k => k.IdPegawai == _viewModel.SelectedDataPegawai.IdPegawai));
                _viewModel.SelectedKeluargaListForm = new ObservableCollection<MasterKeluargaDto>(_viewModel.SelectedKeluargaList.ToList());
                DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");
            }

            await Task.FromResult(_isTest);
        }
    }
}
