using System;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(InteraksiLayananViewModel viewModel, bool isTest = false)
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
               "", true, true, 20);
            try
            {
                _viewModel.Parent.IsAdd = false;
                _viewModel.Parent.IsLoadingForm = true;
                _viewModel.SelectedWilayah.IdWilayah = _viewModel.SelectedData.IdWilayah;

                _viewModel.SelectedGolongan.IdGolongan = _viewModel.SelectedData.IdGolongan;
                _viewModel.SelectedPerkiraan3Debet.IdPerkiraan3 = _viewModel.SelectedData.IdPerkiraan3Debet;
                _viewModel.SelectedPerkiraan3Kredit.IdPerkiraan3 = _viewModel.SelectedData.IdPerkiraan3Kredit;
                _viewModel.SelectedPembentukRekAir = _viewModel.Parent.PembentukRekAirList.FirstOrDefault(x => x.Value == _viewModel.SelectedData.Keterangan);
                _viewModel.SelectedPembentukRekNonAir = _viewModel.Parent.PembentukRekNonAirList.FirstOrDefault(x => x.Value == _viewModel.SelectedData.Keterangan);
                _viewModel.SelectedJenisNonAir.IdJenisNonAir = _viewModel.SelectedData.IdJenisNonAir;
                _viewModel.SelectedPerkiraan3NonAir.IdPerkiraan3 = _viewModel.SelectedData.IdPerkiraan3;


            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
                _viewModel.Parent.IsLoadingForm = false;
            }


        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
