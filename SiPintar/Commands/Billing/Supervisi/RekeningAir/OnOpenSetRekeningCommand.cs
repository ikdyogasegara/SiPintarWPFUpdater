using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenSetRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenSetRekeningCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesSetFlag == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.SelectedData.FlagPublishWpf == true)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Set Rekening",
                    $"Tidak bisa koreksi data yang sudah dipublish",
                    "warning",
                    moduleName: "billing");
                return;
            }

            _viewModel.TanpaDenda = Convert.ToInt32(parameter) == 2;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "BillingRootDialog", GetInstance);
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new SetRekeningNormalView(_viewModel);
    }
}
