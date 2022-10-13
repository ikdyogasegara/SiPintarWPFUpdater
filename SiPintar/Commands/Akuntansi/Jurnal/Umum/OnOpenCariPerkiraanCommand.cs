using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views.Akuntansi.Jurnal.Umum;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnOpenCariPerkiraanCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenCariPerkiraanCommand(UmumViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new SearchDialogView(_viewModel);
    }
}
