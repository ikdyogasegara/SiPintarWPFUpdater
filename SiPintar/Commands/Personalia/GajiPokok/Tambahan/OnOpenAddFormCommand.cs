using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;
using SiPintar.Views.Personalia.GajiPokok.Tambahan;

namespace SiPintar.Commands.Personalia.GajiPokok.Tambahan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly TambahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(TambahanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            _viewModel.IsEdit = false;
            _viewModel.FormFlagPersen = false;
            _viewModel.FormNominal = 0;
            _viewModel.SelectedDataPegawaiForm = new MasterPegawaiDto();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
