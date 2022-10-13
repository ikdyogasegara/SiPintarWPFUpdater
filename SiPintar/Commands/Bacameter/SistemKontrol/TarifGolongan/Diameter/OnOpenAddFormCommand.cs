using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Diameter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Diameter
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenAddFormCommand(DiameterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;

            _viewModel.DiameterForm = new MasterDiameterDto
            {
                Status = true,
            };
            _viewModel.Year = DateTime.Now.Year;
            _viewModel.Month = DateTime.Now.Month;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "BacameterRootDialog");
        }
    }

}
