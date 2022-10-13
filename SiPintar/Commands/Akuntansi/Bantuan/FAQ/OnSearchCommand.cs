﻿using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.Bantuan;

namespace SiPintar.Commands.Akuntansi.Bantuan.FAQ
{
    public class OnSearchCommand : AsyncCommandBase
    {
        private readonly FaqViewModel _viewModel;

        public OnSearchCommand(FaqViewModel ViewModel)
        {
            _viewModel = ViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!string.IsNullOrEmpty(_viewModel.SearchText) && _viewModel.SearchText.Length < 3)
                return;

            await Task.FromResult(true);
        }
    }
}
