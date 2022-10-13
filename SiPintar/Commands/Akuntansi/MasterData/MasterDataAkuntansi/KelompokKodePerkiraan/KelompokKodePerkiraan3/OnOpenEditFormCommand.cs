using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan3ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KelompokKodePerkiraan3ViewModel viewModel, bool isTest = false)
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
            _viewModel.FormPerkiraan = new ParamMasterPerkiraan3Dto()
            {
                IdPdam = _viewModel.SelectedData.IdPdam,
                IdPerkiraan3 = _viewModel.SelectedData.IdPerkiraan3,
                KodePerkiraan3 = _viewModel.SelectedData.KodePerkiraan3.Substring(_viewModel.SelectedData.KodePerkiraan3.Length - 8, 2),
                NamaPerkiraan3 = _viewModel.SelectedData.NamaPerkiraan3,

            };

            _viewModel.Parent.SelectedDataPerkiraan2 = MasterListGlobal.MasterPerkiraan2.FirstOrDefault(x => x.IdPerkiraan2 == _viewModel.SelectedData.IdPerkiraan2);
            _viewModel.Parent.SelectedDataPerkiraan1 = MasterListGlobal.MasterPerkiraan1.FirstOrDefault(x => x.IdPerkiraan1 == _viewModel.Parent.SelectedDataPerkiraan2.IdPerkiraan1);
            _viewModel.Parent.SelectedAkunEtap = MasterListGlobal.MasterAkunEtap.FirstOrDefault(x => x.IdAkunEtap == _viewModel.SelectedData.IdAkunEtap);
            _viewModel.Parent.SelectedPenyusutanAktiva = MasterListGlobal.MasterPenyusutanAktiva.FirstOrDefault(x => x.IdGolAktiva == _viewModel.SelectedData.IdGolAktiva);
            _viewModel.Parent.SelectedJenisVoucher = MasterListGlobal.MasterJenisVoucher.FirstOrDefault(x => x.IdJenisVoucher == _viewModel.SelectedData.IdJenisVoucher);
            _viewModel.Parent.SelectedHarianKas = MasterListGlobal.MasterHarianKas.FirstOrDefault(x => x.IdLhkMaster == _viewModel.SelectedData.IdLHKMaster);
            _viewModel.Parent.SelectedPerputaranUang = MasterListGlobal.PerputaranUangMaster.FirstOrDefault(x => x.IdPerputaranUangMaster == _viewModel.SelectedData.IdLPUMaster);
            _viewModel.Parent.SelectedEkuitasMaster = MasterListGlobal.EkuitasMaster.FirstOrDefault(x => x.IdEkuitasMaster == _viewModel.SelectedData.IdEkuitasMaster);
            _viewModel.Parent.SelectedLabaRugiMaster = MasterListGlobal.AnggaranLabaRugiMaster.FirstOrDefault(x => x.IdGroupLabaRugi == _viewModel.SelectedData.IdLabaRugiMaster);

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstancePerkiraan3);

            _viewModel.Parent.IsLoadingForm = false;
        }
        private object GetInstancePerkiraan3() => new DialogFormView(_viewModel);

    }
}
