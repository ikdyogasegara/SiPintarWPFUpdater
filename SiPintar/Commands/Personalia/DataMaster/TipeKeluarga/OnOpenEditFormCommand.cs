using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.TipeKeluarga;

namespace SiPintar.Commands.Personalia.DataMaster.TipeKeluarga
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TipeKeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TipeKeluargaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                _viewModel.FormKodeTipeKeluarga = _viewModel.SelectedData.KodeTipeKeluarga;
                _viewModel.FormFlagKawin = _viewModel.SelectedData.FlagKawin;
                _viewModel.FormUraian = _viewModel.SelectedData.Uraian;
                _viewModel.FormFlagPersenPangan = _viewModel.SelectedData.FlagPersenPangan;
                _viewModel.FormNominalPangan = _viewModel.SelectedData.NominalPangan;
                _viewModel.FormFlagPersenKel = _viewModel.SelectedData.FlagPersenKel;
                _viewModel.FormNominalKel = _viewModel.SelectedData.NominalKel;
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Data Tipe Keluarga",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
