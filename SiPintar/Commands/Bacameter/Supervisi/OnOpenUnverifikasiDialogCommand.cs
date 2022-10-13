using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenUnverifikasiDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenUnverifikasiDialogCommand(SupervisiViewModel viewModel, bool isTest = false)
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
                if (_viewModel.SelectedData == null || _viewModel.SelectedData.IsUnverifikasi == true)
                    return;

                _viewModel.KoreksiForm = new RekeningAirDto()
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
            if (!_isTest) _ = DialogHost.Show(new SetVerifikasiView(_viewModel, false, IgnoreCheckbox), "BacameterRootDialog");
        }

        [ExcludeFromCodeCoverage]
        public void OpenConfirmationDialog(bool IgnoreCheckbox)
        {
            if (!_isTest) _ = DialogHost.Show(new KonfirmasiPasswordView(_viewModel, false, IgnoreCheckbox), "BacameterRootDialog");
        }
    }
}
