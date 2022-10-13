using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenLampiranDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenLampiranDialogCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.KoreksiForm = new RekeningAirDto()
            {
                IdPelangganAir = _viewModel.SelectedData.IdPelangganAir,
                Lampiran = _viewModel.SelectedData.Lampiran
            };

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new LampiranView(_viewModel), "BacameterRootDialog");
        }
    }
}
