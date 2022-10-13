using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan3ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KelompokKodePerkiraan3ViewModel viewModel, bool isTest)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                "", true, true, 20);

            _viewModel.Parent.IsAdd = true;
            _viewModel.Parent.IsLoadingForm = true;

            _viewModel.FormPerkiraan = new ParamMasterPerkiraan3Dto();
            _viewModel.Parent.SelectedAkunEtap = new MasterAkunEtapDto();
            _viewModel.Parent.SelectedJenisVoucher = new MasterJenisVoucherDto();
            _viewModel.Parent.SelectedPenyusutanAktiva = new MasterPenyusutanAktivaDto();
            _viewModel.Parent.SelectedHarianKas = new MasterHarianKasDto();
            _viewModel.Parent.SelectedPerputaranUang = new LaporanPerputaranUangMasterDto();
            _viewModel.Parent.SelectedEkuitasMaster = new EkuitasMasterDto();
            _viewModel.Parent.SelectedLabaRugiMaster = new AnggaranLabaRugiMasterDto();
            _viewModel.Parent.SelectedDataPerkiraan1 = new MasterPerkiraan1Dto();
            _viewModel.Parent.SelectedDataPerkiraan2 = new MasterPerkiraan2Dto();
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstancePerkiraan);
            _viewModel.Parent.IsLoadingForm = false;

        }

        private object GetInstancePerkiraan() => new DialogFormView(_viewModel);

    }
}
