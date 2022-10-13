using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan.PostingTransKas;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PostingTransKas.DataPenerimaan
{
    public class OnCancelCommand : AsyncCommandBase
    {
        private readonly DataPenerimaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnCancelCommand(DataPenerimaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.UpdatePage("DataTransKas");

            await Task.FromResult(_isTest);
        }
    }
}
