using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan1ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KelompokKodePerkiraan1ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                null, true, true, 20);

            _viewModel.Parent.IsAdd = true;
            _viewModel.Parent.IsLoadingForm = true;

            _viewModel.FormPerkiraan = new ParamMasterPerkiraan1Dto();
            _viewModel.Parent.SelectedTipePerkiraan = new MasterTipePerkiraanDto();
            _viewModel.Parent.IsHeaderChecked = null;
            _viewModel.Parent.IsSubKodeChecked = null;
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstancePerkiraan);
            _viewModel.Parent.IsLoadingForm = false;
        }
        private object GetInstancePerkiraan() => new DialogFormView(_viewModel);

    }
}
