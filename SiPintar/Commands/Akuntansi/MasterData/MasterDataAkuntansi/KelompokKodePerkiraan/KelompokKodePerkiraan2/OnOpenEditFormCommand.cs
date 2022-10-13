﻿using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KelompokKodePerkiraan2ViewModel viewModel, bool isTest = false)
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

            _viewModel.FormPerkiraan = new ParamMasterPerkiraan2Dto()
            {
                IdPdam = _viewModel.SelectedData.IdPdam,
                IdPerkiraan2 = _viewModel.SelectedData.IdPerkiraan2,
                KodePerkiraan2 = _viewModel.SelectedData.KodePerkiraan2.Substring(_viewModel.SelectedData.KodePerkiraan2.Length - 2, 2),
                NamaPerkiraan2 = _viewModel.SelectedData.NamaPerkiraan2,
                IdPerkiraan1 = _viewModel.SelectedData.IdPerkiraan1,
            };

            _viewModel.Parent.SelectedDataPerkiraan1 = _viewModel.Parent.DataPerkiraan1List.FirstOrDefault(x => x.IdPerkiraan1 == _viewModel.SelectedData.IdPerkiraan1);

            _viewModel.Parent.SelectedNeracaMaster = _viewModel.Parent.NeracaMasterList.FirstOrDefault(x => x.IdNeracaMaster == _viewModel.SelectedData.IdNeracaMaster);

            _viewModel.Parent.SelectedArusKasMaster = _viewModel.Parent.ArusKasMasterList.FirstOrDefault(x => x.IdArusKasMaster == _viewModel.SelectedData.IdArusKasMaster);

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstancePerkiraan);
            _viewModel.Parent.IsLoadingForm = false;
        }
        private object GetInstancePerkiraan() => new DialogFormView(_viewModel);

    }
}
