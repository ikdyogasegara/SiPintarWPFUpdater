using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(InteraksiPenyusutanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.IsLoading)
                return;

            if (_viewModel.SelectedData?.IdInPenyusutan == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Koreksi Data Interaksi Penyusutan",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            _viewModel.Parent.IsAdd = false;

            _viewModel.SelectedAkunAktiva = _viewModel.Parent.AkunAktivaList.FirstOrDefault(x => x.IdPerkiraan2 == _viewModel.SelectedData.IdAkunAktiva);
            _viewModel.SelectedAkunAkumPenyusutan = _viewModel.Parent.AkunAkumPenyusutanList.FirstOrDefault(x => x.IdPerkiraan3 == _viewModel.SelectedData.IdAkunAkmPeny);
            _viewModel.SelectedAkunBiaya = _viewModel.Parent.AkunBiayaList.FirstOrDefault(x => x.IdPerkiraan3 == _viewModel.SelectedData.IdAkunBiaya);

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);

            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
