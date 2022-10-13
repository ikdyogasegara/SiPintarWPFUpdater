using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenVerifikasiDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenVerifikasiDialogCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.KonfirmasiPassword = null;
            bool IgnoreCheckbox = parameter == null;

            if (!IgnoreCheckbox && _viewModel.RekeningList != null && _viewModel.RekeningList.FirstOrDefault(i => i.IsSelected == true) == null)
                IgnoreCheckbox = true;

            if (IgnoreCheckbox)
            {
                if (_viewModel.SelectedData == null || _viewModel.SelectedData.IsVerifikasi == true)
                    return;

                _viewModel.KoreksiForm = new RekeningAirWpf()
                {
                    IdPelangganAir = _viewModel.SelectedData.IdPelangganAir
                };

                OpenDialog(IgnoreCheckbox);
            }
            else
            {
                OpenConfirmationDialog(IgnoreCheckbox);
            }

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog(bool IgnoreCheckbox)
        {
            if (!_isTest) _ = DialogHost.Show(new SetVerifikasiView(_viewModel, true, IgnoreCheckbox), "BacameterRootDialog");
        }

        [ExcludeFromCodeCoverage]
        public void OpenConfirmationDialog(bool IgnoreCheckbox)
        {
            if (!_isTest) _ = DialogHost.Show(new KonfirmasiPasswordView(_viewModel, true, IgnoreCheckbox), "BacameterRootDialog");
        }
    }
}
