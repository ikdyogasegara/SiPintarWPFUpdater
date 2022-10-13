using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Billing.Atribut.Tarif.Golongan;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Golongan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(GolonganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.GolonganForm = (MasterGolonganDto)_viewModel.SelectedData.Clone();
            _viewModel.Year = (_viewModel.SelectedData.PeriodeMulaiBerlaku ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) / 100;
            _viewModel.Month = (_viewModel.SelectedData.PeriodeMulaiBerlaku ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) % 100;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                if (_viewModel.IsFromDetail)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
            }
        }
    }
}
