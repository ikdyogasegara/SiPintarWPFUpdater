using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FastReport.Format;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;

namespace SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekNonAir
{
    public class OnCetakDataCommand : AsyncCommandBase
    {
        private readonly RekNonAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCetakDataCommand(RekNonAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var date = DateTime.Parse("1" + _viewModel.SelectedPeriode.NamaPeriode, new CultureInfo("id-ID"));
            var dateLast = DateTime.DaysInMonth(date.Year, date.Month);


            var bodyCetak = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.SelectedPeriode.IdPeriode ?? 0 },
                { "TanggalAwal", date.ToString("yyyy-MM-dd", new CultureInfo("id-ID"))},
                { "TanggalAkhir", date.AddDays(dateLast-1).ToString("yyyy-MM-dd", new CultureInfo("id-ID"))},
            };

            if (pilihanCetak == "rincian" && _viewModel.SelectedWilayah?.IdWilayah != null)
                bodyCetak.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah);

            if (pilihanCetak == "rincian")
            {
                ((OnCetakCommand)_viewModel.OnCetakJrnaRincianCommand).IsCetak = true;
                ((OnCetakCommand)_viewModel.OnCetakJrnaRincianCommand).IsPreview = true;
                ((OnCetakCommand)_viewModel.OnCetakJrnaRincianCommand).Method = CetakApiMethod.GET;
                ((OnCetakCommand)_viewModel.OnCetakJrnaRincianCommand).TemplateName = "JurnalRekeningNonAir(JRNA)Detail";
                await ((AsyncCommandBase)_viewModel.OnCetakJrnaRincianCommand).ExecuteAsync(bodyCetak);
                ((OnCetakCommand)_viewModel.OnCetakJrnaRincianCommand).Method = CetakApiMethod.GET;
            }
            else
            {
                ((OnCetakCommand)_viewModel.OnCetakJrnaKonsolidasiCommand).IsCetak = true;
                ((OnCetakCommand)_viewModel.OnCetakJrnaKonsolidasiCommand).IsPreview = true;
                ((OnCetakCommand)_viewModel.OnCetakJrnaKonsolidasiCommand).Method = CetakApiMethod.GET;
                ((OnCetakCommand)_viewModel.OnCetakJrnaKonsolidasiCommand).TemplateName = "JurnalRekeningNonAir(JRNA)Kosolidasi";
                await ((AsyncCommandBase)_viewModel.OnCetakJrnaKonsolidasiCommand).ExecuteAsync(bodyCetak);
                ((OnCetakCommand)_viewModel.OnCetakJrnaKonsolidasiCommand).Method = CetakApiMethod.GET;
            }

        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
