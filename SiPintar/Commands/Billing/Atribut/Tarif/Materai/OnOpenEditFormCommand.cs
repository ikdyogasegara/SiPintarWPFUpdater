using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.Materai;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Materai
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly MateraiViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(MateraiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.IsLoading || parameter == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.SelectedData = (MasterMeteraiDto)parameter;

            _viewModel.Year = (_viewModel.SelectedData.KodePeriodeMulaiBerlaku ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) / 100;
            _viewModel.Month = (_viewModel.SelectedData.KodePeriodeMulaiBerlaku ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) % 100;
            _viewModel.MateraiForm = _viewModel.SelectedData;

            _viewModel.IsEdit = true;
            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _viewModel.IsLoadingForm = false;
            _viewModel.IsEditFromDetail = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                if (_viewModel.IsEditFromDetail)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
            }
        }
    }
}
