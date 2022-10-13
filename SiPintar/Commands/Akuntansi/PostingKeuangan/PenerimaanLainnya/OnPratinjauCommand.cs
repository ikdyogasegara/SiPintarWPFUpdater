using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnPratinjauCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnPratinjauCommand(PenerimaanLainnyaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            _viewModel.IsLoadingForm = true;
            DialogHelper.ShowLoading(_isTest, "AkuntansiRootDialog");
            await PratinjauProcessAsync();
            _viewModel.IsLoadingForm = false;
        }

        private async Task PratinjauProcessAsync()
        {

            var bodyCetak = new Dictionary<string, dynamic>
            {
                { "IdUser", 1 },
                { "Data1", 1 },
                { "Data2", 1 },
                { "IncludeData", true }
            };

            ((OnCetakCommand)_viewModel.OnCetakCommand).IsCetak = true;
            ((OnCetakCommand)_viewModel.OnCetakCommand).IsPreview = true;
            ((OnCetakCommand)_viewModel.OnCetakCommand).Method = CetakApiMethod.POST;
            ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = "DaftarPenerimaanLainnya";
            await ((AsyncCommandBase)_viewModel.OnCetakCommand).ExecuteAsync(bodyCetak);
            ((OnCetakCommand)_viewModel.OnCetakCommand).Method = CetakApiMethod.GET;

        }
    }
}
