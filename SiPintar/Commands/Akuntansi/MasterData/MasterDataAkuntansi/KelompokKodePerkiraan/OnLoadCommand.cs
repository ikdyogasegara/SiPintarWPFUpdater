using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(KelompokKodePerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SelectedPage = parameter as string ?? "";

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                "", true, true, 20);

            _viewModel.UpdatePage(_viewModel.SelectedPage);
            switch (_viewModel.SelectedPage)
            {
                case "Kelompok Kode Perkiraan(XX,YY)":
                    {
                        await UpdateListData.ProcessAsync(_isTest, new List<string>()
                        {
                            "MasterPerkiraan1",
                            "NeracaMaster",
                            "ArusKasMaster",
                        });
                        _viewModel.DataPerkiraan1List = MasterListGlobal.MasterPerkiraan1;
                        _viewModel.NeracaMasterList = MasterListGlobal.NeracaMaster;
                        _viewModel.ArusKasMasterList = MasterListGlobal.ArusKasMaster;
                    }
                    break;
                case "Kelompok Kode Perkiraan(XX,YY,ZZ)":
                    {
                        await UpdateListData.ProcessAsync(_isTest, new List<string>()
                        {
                            "MasterPerkiraan1",
                            "MasterPerkiraan2",
                            "MasterAkunEtap",
                            "MasterPenyusutanAktiva",
                            "MasterHarianKas",
                            "AnggaranLabaRugiMaster",
                            "PerputaranUangMaster",
                            "MasterJenisVoucher",
                            "NeracaMaster",
                            "ArusKasMaster",
                            "EkuitasMaster",
                        });

                        _viewModel.DataPerkiraan1List = MasterListGlobal.MasterPerkiraan1;
                        _viewModel.DataPerkiraan2List = MasterListGlobal.MasterPerkiraan2;
                        _viewModel.DataAkunEtapList = MasterListGlobal.MasterAkunEtap;
                        _viewModel.PenyusutanAktivaList = MasterListGlobal.MasterPenyusutanAktiva;

                        _viewModel.NeracaMasterList = MasterListGlobal.NeracaMaster;
                        _viewModel.ArusKasMasterList = MasterListGlobal.ArusKasMaster;
                        _viewModel.EkuitasMasterList = MasterListGlobal.EkuitasMaster;
                        _viewModel.JenisVoucherList = MasterListGlobal.MasterJenisVoucher;
                        _viewModel.HarianKasList = MasterListGlobal.MasterHarianKas;
                        _viewModel.PerputaranUangList = MasterListGlobal.PerputaranUangMaster;
                        _viewModel.LabaRugiMasterList = MasterListGlobal.AnggaranLabaRugiMaster;
                    }
                    break;
            }


            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
            await Task.FromResult(_isTest);
        }
    }
}
