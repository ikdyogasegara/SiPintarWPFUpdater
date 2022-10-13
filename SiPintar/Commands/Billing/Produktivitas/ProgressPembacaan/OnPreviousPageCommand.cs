﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Produktivitas;

namespace SiPintar.Commands.Billing.Produktivitas.ProgressPembacaan
{
    [ExcludeFromCodeCoverage]
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly ProgressPembacaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnPreviousPageCommand(ProgressPembacaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;

                _viewModel.OnLoadCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
