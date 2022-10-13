using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan1ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KelompokKodePerkiraan1ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null) { return; }

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                null, true, true, 20);

            _viewModel.Parent.IsAdd = false;
            _viewModel.Parent.IsLoadingForm = true;

            _viewModel.FormPerkiraan = new ParamMasterPerkiraan1Dto()
            {
                IdPdam = _viewModel.SelectedData.IdPdam,
                IdPerkiraan1 = _viewModel.SelectedData.IdPerkiraan1,
                KodePerkiraan1 = _viewModel.SelectedData.KodePerkiraan1,
                NamaPerkiraan1 = _viewModel.SelectedData.NamaPerkiraan1,
            };

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstancePerkiraan);

            _viewModel.Parent.IsLoadingForm = false;
        }

        private object GetInstancePerkiraan() => new DialogFormView(_viewModel);

    }
}
