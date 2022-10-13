using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi;

namespace SiPintar.Commands.Akuntansi.Jurnal.Instalasi.DaftarHutang
{
    public class OnCetakDataCommand : AsyncCommandBase
    {
        private readonly DaftarHutangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCetakDataCommand(DaftarHutangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                { "TglAwal", _viewModel.PeriodeAwal!.Value.ToString("yyyy/MM/dd",null) },
                { "TglAkhir", _viewModel.PeriodeAkhir!.Value.ToString("yyyy/MM/dd",null) },
            };

            if (!_viewModel.IsKonsolidasi)
            {
                bodyCetak.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah);
            }

            switch (_viewModel.SelectedProses!)
            {
                case TipeProses.DHHD:
                    ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = _viewModel.IsKonsolidasi ? "JurnalDaftarHutangSudah&HarusDibayar(DHHD)DHHDKonsolidasi" : "JurnalDaftarHutangSudah&HarusDibayar(DHHD)DHHD";
                    ((OnCetakCommand)_viewModel.OnCetakCommand).LinkApi = "cetak-daftar-hutang-yang-sudah-dan-harus-di-bayar";
                    break;
                case TipeProses.SISA_HUTANG:
                    ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = _viewModel.IsKonsolidasi ? "JurnalDaftarHutangSudah&HarusDibayar(DHHD)SisaHutangKonsolidasi" : "JurnalDaftarHutangSudah&HarusDibayar(DHHD)SisaHutang";
                    ((OnCetakCommand)_viewModel.OnCetakCommand).LinkApi = "sisa-hutang";
                    break;
                case TipeProses.DHJP:
                    ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = _viewModel.IsKonsolidasi ? "JurnalDaftarHutangSudah&HarusDibayar(DHHD)DHJPKonsolidasi" : "JurnalDaftarHutangSudah&HarusDibayar(DHHD)DHJP";
                    ((OnCetakCommand)_viewModel.OnCetakCommand).LinkApi = "daftar-hutang-jangka-pendek";
                    break;
                case TipeProses.DHHD_POTRAIT:
                    ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = _viewModel.IsKonsolidasi ? "JurnalDaftarHutangSudah&HarusDibayar(DHHD)DHHDPotraitKonsolidasi" : "JurnalDaftarHutangSudah&HarusDibayar(DHHD)DHHDPotrait";
                    ((OnCetakCommand)_viewModel.OnCetakCommand).LinkApi = "cetak-daftar-hutang-yang-sudah-dan-harus-di-bayar-ptr";
                    break;
                default:
                    break;
            }

            ((OnCetakCommand)_viewModel.OnCetakCommand).IsCetak = true;
            ((OnCetakCommand)_viewModel.OnCetakCommand).IsPreview = true;
            ((OnCetakCommand)_viewModel.OnCetakCommand).Method = CetakApiMethod.GET;
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
