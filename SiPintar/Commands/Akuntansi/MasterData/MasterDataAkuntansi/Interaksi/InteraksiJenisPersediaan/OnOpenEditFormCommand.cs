using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(InteraksiJenisPersediaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.IsLoading)
                return;

            if (_viewModel.SelectedData?.IdInPersediaan == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Koreksi Data Interaksi Persediaan",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            _viewModel.Parent.IsAdd = false;

            _viewModel.InJenisPersediaanForm = new();
            _viewModel.SelectedJenis = _viewModel.Parent.JenisBarangList.FirstOrDefault(x => x.IdJenisBarang! == _viewModel.SelectedData.IdJenis!)!;
            _viewModel.SelectedKeperluan = _viewModel.Parent.KeperluanList.FirstOrDefault(x => x.IdKeperluan! == _viewModel.SelectedData.IdKeperluan!)!;
            _viewModel.SelectedDebet = _viewModel.Parent.PerkiraanDebet.FirstOrDefault(x => x.IdPerkiraan3! == _viewModel.SelectedData.IdDebet!)!;
            _viewModel.SelectedKredit = _viewModel.Parent.PerkiraanKredit.FirstOrDefault(x => x.IdPerkiraan3! == _viewModel.SelectedData.IdKredit!)!;
            _viewModel.IsAktivaChecked = _viewModel.SelectedData.Aktiva != 0;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);

            await Task.FromResult(_isTest);
        }
        private object GetInstance() => new DialogFormView(_viewModel);

    }
}
