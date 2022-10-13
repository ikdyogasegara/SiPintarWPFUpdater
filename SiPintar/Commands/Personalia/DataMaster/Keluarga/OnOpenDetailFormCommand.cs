using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Keluarga;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnOpenDetailFormCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailFormCommand(KeluargaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            if (_viewModel.SelectedData != null)
            {
                _viewModel.SelectedKeluargaList = new ObservableCollection<MasterKeluargaDto>(_viewModel.KeluargaList.Where(k => k.IdPegawai == _viewModel.SelectedData.IdPegawai));
                _viewModel.SelectedKeluargaListForm = new ObservableCollection<MasterKeluargaDto>(_viewModel.SelectedKeluargaList.ToList());
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogKeluargaView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Data Jabatan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
