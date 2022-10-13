using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi;

namespace SiPintar.Commands.Akuntansi.Jurnal.Instalasi.BahanKimia
{
    public class OnCetakDataCommand : AsyncCommandBase
    {
        private readonly BahanKimiaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCetakDataCommand(BahanKimiaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            await CetakDataAsync();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task CetakDataAsync()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

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
            ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = "JurnalPemakaianBahanInstalasi&Kimia(JPBIK)";
            await ((AsyncCommandBase)_viewModel.OnCetakCommand).ExecuteAsync(bodyCetak);
            ((OnCetakCommand)_viewModel.OnCetakCommand).Method = CetakApiMethod.GET;
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
