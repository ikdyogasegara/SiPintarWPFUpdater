using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.TipeKeluarga;

namespace SiPintar.Commands.Personalia.DataMaster.TipeKeluarga
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly TipeKeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(TipeKeluargaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.FormKodeTipeKeluarga = null;
            _viewModel.FormFlagKawin = false;
            _viewModel.FormUraian = null;
            _viewModel.FormFlagPersenPangan = false;
            _viewModel.FormNominalPangan = null;
            _viewModel.FormFlagPersenKel = false;
            _viewModel.FormNominalKel = null;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
