using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views.Akuntansi.PostingKeuangan.PenerimaanLainnya;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(PenerimaanLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.IdDaftarPenerimaanKas == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Koreksi Data Posting Penerimaan Lainnya",
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
                    "Koreksi Data Posting Penerimaan Lainnya",
                    "Maaf data tidak dapat dikoreksi, karena sudah mengalami Proses POSTING",
                    "error",
                    "OK",
                    false,
                    "akuntansi");

                return;
            }

            _viewModel.IsLoadingForm = true;
            _viewModel.IsAdd = false;

            _viewModel.WaktuInputForm = (DateTime)_viewModel.SelectedData!.WaktuInput!;
            _viewModel.NotransForm = _viewModel.SelectedData.NomorTransaksi;
            _viewModel.SelectedWilayah = _viewModel.WilayahList.Where(x => x.IdWilayah == _viewModel.SelectedData!.IdWilayah).First();
            _viewModel.SelectedDebet = _viewModel.Perkiraan3List.Where(x => x.IdPerkiraan3 == _viewModel.SelectedData!.IdPerkiraan3Debet).First();
            _viewModel.SelectedKredit = _viewModel.Perkiraan3List.Where(x => x.IdPerkiraan3 == _viewModel.SelectedData!.IdPerkiraan3Kredit).First();
            _viewModel.UraianForm = _viewModel.SelectedData!.Uraian;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
