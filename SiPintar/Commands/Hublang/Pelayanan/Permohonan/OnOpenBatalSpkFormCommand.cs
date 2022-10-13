using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Permohonan;


namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnOpenBatalSpkFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenBatalSpkFormCommand(PermohonanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
            {
                _viewModel.AlasanBatalList = MasterListGlobal.MasterAlasanBatal;
                _viewModel.SelectedAlasanBatal = null;
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PerencanaanRootDialog", GetInstance);
            }
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new BatalSpkFormView(_viewModel);
    }
}
