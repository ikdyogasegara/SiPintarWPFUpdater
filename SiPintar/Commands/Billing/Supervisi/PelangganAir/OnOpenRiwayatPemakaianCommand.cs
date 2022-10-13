using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Billing.Supervisi.PelangganAir;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnOpenRiwayatPemakaianCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenRiwayatPemakaianCommand(PelangganAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var minYear = DateTime.Now.Year - 10;
            _viewModel.ListYearPemakaian = new List<int>(Enumerable.Range(minYear, 10 + 1).Reverse());
            _viewModel.YearPemakaian = _viewModel.ListYearPemakaian.First();
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "BillingRootDialog",
                GetDialog);
            await Task.FromResult(_isTest);
        }

        private object GetDialog() => new RiwayatPemakaianView(_viewModel);
    }
}
