using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnNextFormPageCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextFormPageCommand(PegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FormCurrentPage < 3)
                _viewModel.FormCurrentPage += 1;
            else
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaInnerDialog",
                    _viewModel.IsEdit ? "Konfirmasi Koreksi Data Pegawai" : "Konfirmasi Tambah Data Pegawai Baru",
                    _viewModel.IsEdit ? $"Anda akan mengoreksi data pegawai {_viewModel.FormData.NamaPegawai} ?" : $"Anda akan menambahkan data pegawai baru {_viewModel.FormData.NamaPegawai} ?",
                    "confirmation", _viewModel.OnSubmitFormCommand, "Proses", "Batal",
                    false, false, "Personalia");

            await Task.FromResult(true);
        }


    }
}
