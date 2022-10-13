using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PersentasePenyusutanAktivaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData is null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Koreksi Data Persentase Penyusutan Aktiva",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");
                return;
            }

            _viewModel.IsAdd = false;
            _viewModel.FormData = _viewModel.SelectedData;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
