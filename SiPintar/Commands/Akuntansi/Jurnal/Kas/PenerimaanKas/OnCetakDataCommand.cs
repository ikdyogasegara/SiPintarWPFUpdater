using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;

namespace SiPintar.Commands.Akuntansi.Jurnal.Kas.PenerimaanKas
{
    public class OnCetakDataCommand : AsyncCommandBase
    {
        private readonly PenerimaanKasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCetakDataCommand(PenerimaanKasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                { "IdPeriode", _viewModel.SelectedPeriode!.IdPeriode! },
            };

            if (!_viewModel.IsKonsolidasi && _viewModel.SelectedWilayah!.IdWilayah != null)
            {
                bodyCetak.Add("IdWilayah", _viewModel.SelectedWilayah!.IdWilayah!);
            }

            switch (_viewModel.SelectedUraian!)
            {
                case TipeUraian.URAIAN_TRANSAKSI:
                    ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).IsCetak = true;
                    ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).IsPreview = true;
                    ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).Method = CetakApiMethod.GET;
                    if (_viewModel.IsKonsolidasi)
                    {
                        ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).LinkApi = "cetak-jpk-transaksi-konsolidasi";
                        ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).TemplateName = "JurnalPenerimaanKas(JPK)TransaksiKonsolidasi";
                    }
                    else
                    {
                        ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).LinkApi = "cetak-jpk-transaksi";
                        ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).TemplateName = "JurnalPenerimaanKas(JPK)Transaksi";
                    }
                    await ((AsyncCommandBase)_viewModel.OnCetakJpkTransaksiCommand).ExecuteAsync(bodyCetak);
                    ((OnCetakCommand)_viewModel.OnCetakJpkTransaksiCommand).Method = CetakApiMethod.GET;
                    break;
                case TipeUraian.URAIAN_PERKIRAAN:
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).IsCetak = true;
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).IsPreview = true;
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).Method = CetakApiMethod.GET;
                    if (_viewModel.IsKonsolidasi)
                    {
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).LinkApi = "cetak-jpk-perkiraan-konsolidasi";
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).TemplateName = "JurnalPenerimaanKas(JPK)PerkiraanKonsolidasi";
                    }
                    else
                    {
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).LinkApi = "cetak-jpk-perkiraan";
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).TemplateName = "JurnalPenerimaanKas(JPK)Perkiraan";
                    }
                    await ((AsyncCommandBase)_viewModel.OnCetakJpkPerkiraanCommand).ExecuteAsync(bodyCetak);
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanCommand).Method = CetakApiMethod.GET;
                    break;
                case TipeUraian.REKAP_URAIAN_PERKIRAAN:
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).IsCetak = true;
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).IsPreview = true;
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).Method = CetakApiMethod.GET;
                    if (_viewModel.IsKonsolidasi)
                    {
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).LinkApi = "cetak-jpk-perkiraan-rekap-konsolidasi";
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).TemplateName = "JurnalPenerimaanKas(JPK)PerkiraanRekapKonsolidasi";
                    }
                    else
                    {
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).LinkApi = "cetak-jpk-perkiraan-rekap";
                        ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).TemplateName = "JurnalPenerimaanKas(JPK)PerkiraanRekap";
                    }
                    await ((AsyncCommandBase)_viewModel.OnCetakJpkPerkiraanRekapCommand).ExecuteAsync(bodyCetak);
                    ((OnCetakCommand)_viewModel.OnCetakJpkPerkiraanRekapCommand).Method = CetakApiMethod.GET;
                    break;
                default:
                    break;
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
