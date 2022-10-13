using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.Commands.Akuntansi.Penyusutan.CetakTabelPenyusutan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly CetakTabelPenyusutanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(CetakTabelPenyusutanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            _ = _restApi;

            await Task.FromResult(_isTest);
        }
    }
}
