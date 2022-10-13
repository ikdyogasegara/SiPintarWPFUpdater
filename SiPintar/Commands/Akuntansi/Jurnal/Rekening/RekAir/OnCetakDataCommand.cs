using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekAir
{
    public class OnCetakDataCommand : AsyncCommandBase
    {
        private readonly RekAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCetakDataCommand(RekAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

            var bodyCetak = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.SelectedPeriode.IdPeriode ?? 0 },
            };

            if (pilihanCetak == "detail")
            {
                ((OnCetakCommand)_viewModel.OnCetakJraDetailCommand).IsCetak = true;
                ((OnCetakCommand)_viewModel.OnCetakJraDetailCommand).IsPreview = true;
                ((OnCetakCommand)_viewModel.OnCetakJraDetailCommand).Method = CetakApiMethod.GET;
                ((OnCetakCommand)_viewModel.OnCetakJraDetailCommand).TemplateName = "JurnalRekeningAir(JRA)Detail";
                await ((AsyncCommandBase)_viewModel.OnCetakJraDetailCommand).ExecuteAsync(bodyCetak);
                ((OnCetakCommand)_viewModel.OnCetakJraDetailCommand).Method = CetakApiMethod.GET;
            }
            else
            {
                ((OnCetakCommand)_viewModel.OnCetakJraRekapCommand).IsCetak = true;
                ((OnCetakCommand)_viewModel.OnCetakJraRekapCommand).IsPreview = true;
                ((OnCetakCommand)_viewModel.OnCetakJraRekapCommand).Method = CetakApiMethod.GET;
                ((OnCetakCommand)_viewModel.OnCetakJraRekapCommand).TemplateName = "JurnalRekeningAir(JRA)Rekap";
                await ((AsyncCommandBase)_viewModel.OnCetakJraRekapCommand).ExecuteAsync(bodyCetak);
                ((OnCetakCommand)_viewModel.OnCetakJraRekapCommand).Method = CetakApiMethod.GET;
            }

        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
