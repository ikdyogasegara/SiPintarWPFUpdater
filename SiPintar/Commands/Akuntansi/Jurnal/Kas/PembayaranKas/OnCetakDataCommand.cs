using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.Jurnal.Kas.PembayaranKas
{
    public class OnCetakDataCommand : AsyncCommandBase
    {
        private readonly PembayaranKasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCetakDataCommand(PembayaranKasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;
            await CetakDataAsync(parameter.ToString()!);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task CetakDataAsync(string pilihanCetak)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });
                var bodyCetak = new Dictionary<string, dynamic>
                {
                    { "IdUser", 1 },
                    { "tglAwal", _viewModel.PeriodeAwal },
                    { "tglAkhir", _viewModel.PeriodeAkhir }
                };

                if (pilihanCetak == "detail")
                {
                    ((OnCetakCommand)_viewModel.OnCetakJbkDetailCommand).IsCetak = true;
                    ((OnCetakCommand)_viewModel.OnCetakJbkDetailCommand).IsPreview = true;
                    ((OnCetakCommand)_viewModel.OnCetakJbkDetailCommand).Method = CetakApiMethod.GET;
                    ((OnCetakCommand)_viewModel.OnCetakJbkDetailCommand).TemplateName = "JurnalBayarKas(JBK)Detail";
                    await ((AsyncCommandBase)_viewModel.OnCetakJbkDetailCommand).ExecuteAsync(bodyCetak);
                    ((OnCetakCommand)_viewModel.OnCetakJbkDetailCommand).Method = CetakApiMethod.GET;
                }
                else
                {
                    ((OnCetakCommand)_viewModel.OnCetakJbkRekapCommand).IsCetak = true;
                    ((OnCetakCommand)_viewModel.OnCetakJbkRekapCommand).IsPreview = true;
                    ((OnCetakCommand)_viewModel.OnCetakJbkRekapCommand).Method = CetakApiMethod.GET;
                    ((OnCetakCommand)_viewModel.OnCetakJbkRekapCommand).TemplateName = "JurnalBayarKas(JBK)Rekap";
                    await ((AsyncCommandBase)_viewModel.OnCetakJbkRekapCommand).ExecuteAsync(bodyCetak);
                    ((OnCetakCommand)_viewModel.OnCetakJbkRekapCommand).Method = CetakApiMethod.GET;
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
