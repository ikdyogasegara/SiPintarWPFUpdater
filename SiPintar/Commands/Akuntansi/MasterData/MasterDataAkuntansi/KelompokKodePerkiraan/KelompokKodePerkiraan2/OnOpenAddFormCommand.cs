using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KelompokKodePerkiraan2ViewModel viewModel, bool isTest)
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

            _viewModel.FormPerkiraan = new ParamMasterPerkiraan2Dto();
            _viewModel.Parent.SelectedDataPerkiraan1 = new MasterPerkiraan1Dto();
            _viewModel.Parent.SelectedNeracaMaster = new NeracaMasterDto();
            _viewModel.Parent.SelectedArusKasMaster = new ArusKasTidakLangsungMasterDto();
            _viewModel.Parent.IsHeaderChecked = null;
            _viewModel.Parent.IsSubKodeChecked = null;
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstancePerkiraan);
            _viewModel.Parent.IsLoadingForm = false;
        }
        private object GetInstancePerkiraan() => new DialogFormView(_viewModel);
    }
}
