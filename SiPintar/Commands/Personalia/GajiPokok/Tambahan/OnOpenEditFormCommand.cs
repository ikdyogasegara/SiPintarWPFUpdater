using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;
using SiPintar.Views.Personalia.GajiPokok.Tambahan;

namespace SiPintar.Commands.Personalia.GajiPokok.Tambahan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TambahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TambahanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (!_viewModel.IsPilih)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "PersonaliaRootDialog",
                    "Warning",
                    "Periode dan Kode Gaji harus dipilih",
                    "warning",
                    "OK",
                    false,
                    "personalia");
                return;
            }

            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                _viewModel.FormFlagPersen = _viewModel.SelectedData.FlagPersen;
                _viewModel.FormNominal = _viewModel.SelectedData.Nominal;
                _viewModel.SelectedDataPegawaiForm = new MasterPegawaiDto
                {
                    IdPegawai = _viewModel.SelectedData.IdPegawai,
                    NoPegawai = _viewModel.SelectedData.NoPegawai,
                    NamaPegawai = _viewModel.SelectedData.NamaPegawai,
                };
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Gaji Tambahan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
