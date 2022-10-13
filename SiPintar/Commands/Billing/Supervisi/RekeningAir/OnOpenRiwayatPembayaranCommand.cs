using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenRiwayatPembayaranCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenRiwayatPembayaranCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var minYear = DateTime.Now.Year - 10;
            _viewModel.ListYearPembayaran = new List<int>(Enumerable.Range(minYear, 10 + 1).Reverse());
            _viewModel.YearPembayaran = DateTime.Now.Year;
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "BillingRootDialog",
                GetDialog);
            await Task.FromResult(_isTest);
        }

        private object GetDialog() => new RiwayatPembayaranView(_viewModel);
    }
}
