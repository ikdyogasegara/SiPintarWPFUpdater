using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views.Akuntansi.PostingKeuangan.PengeluaranLainnya;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(PengeluaranLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdDaftarBiayaKas == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Koreksi Data Posting Pengeluaran Lainnya",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            if (_viewModel.SelectedData?.StatusPostingText == "Sudah Posting")
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Koreksi Data Posting Pengeluaran Lainnya",
                    "Maaf data tidak dapat dikoreksi, karena sudah mengalami Proses POSTING",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            _viewModel.IsAdd = false;
            _viewModel.PengeluaranLainnyaForm.NomorTransaksi = _viewModel.SelectedData.NomorTransaksi;
            _viewModel.PengeluaranLainnyaForm.WaktuInput = _viewModel.SelectedData.WaktuInput;
            _viewModel.PengeluaranLainnyaForm.Kategori = _viewModel.SelectedData.Kategori;
            _viewModel.PengeluaranLainnyaForm.Uraian = _viewModel.SelectedData.Uraian;
            _viewModel.PengeluaranLainnyaForm.JumlahNominal = _viewModel.SelectedData.JumlahNominal;
            _viewModel.PengeluaranLainnyaForm.IdJenisHutang = 1;
            _viewModel.SelectedDebet = _viewModel.Perkiraan3List.FirstOrDefault(x => x.IdPerkiraan3 == _viewModel.SelectedData.IdPerkiraan3Debet);
            _viewModel.SelectedKredit = _viewModel.Perkiraan3List.FirstOrDefault(x => x.IdPerkiraan3 == _viewModel.SelectedData.IdPerkiraan3Kredit);
            _viewModel.SelectedWilayah = _viewModel.WilayahList.FirstOrDefault(x => x.IdWilayah == _viewModel.SelectedData.IdWilayah);
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
