using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.Materai;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Materai
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly MateraiViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenAddFormCommand(MateraiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;

            _viewModel.IsLoadingForm = true;

            _viewModel.MateraiForm = new MasterMeteraiDto();

            _viewModel.Year = DateTime.Now.Year;
            _viewModel.Month = DateTime.Now.Month;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }


}
