﻿using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPelanggan
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly DataPelangganViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(DataPelangganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                _viewModel.OnLoadCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
