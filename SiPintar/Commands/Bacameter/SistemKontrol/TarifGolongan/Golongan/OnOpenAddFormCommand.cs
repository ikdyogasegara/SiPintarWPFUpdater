using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Golongan;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Golongan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenAddFormCommand(GolonganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.GolonganForm = new MasterGolonganDto
            {
                T1Tetap = false,
                T2Tetap = false,
                T3Tetap = false,
                T4Tetap = false,
                T5Tetap = false,
                TrfDendaBerdasarkanPersen = false,
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
