using System;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SaldoAwalViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(SaldoAwalViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await UpdateListData.ProcessAsync(_isTest, new()
            {
                "MasterBank"
            });

            _viewModel.BankList = MasterListGlobal.MasterBank;
            _viewModel.FilterTglRekonsiliasi = DateTime.Now;
            _viewModel.Data = null;

            await Task.FromResult(_isTest);
        }
    }

}
