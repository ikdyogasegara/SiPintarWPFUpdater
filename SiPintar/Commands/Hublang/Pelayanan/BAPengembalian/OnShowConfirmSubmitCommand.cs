using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnShowConfirmSubmitCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly bool _isTest;

        public OnShowConfirmSubmitCommand(BaPengembalianViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ParamSubmit = parameter as ParamRekeningAirPengembalianDto;
            DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog");
            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "HublangRootDialog",
                //"Konfirmasi " + (_viewModel.IsAdd ? "Tambah" : "Koreksi") + $" Berita Acara Pengembalian Dengan Nomor { _viewModel.SelectedData.NomorBa}",
                "Konfirmasi " + (_viewModel.IsAdd ? "Tambah" : "Koreksi") + $" Berita Acara Pengembalian Dengan Nomor",
                "Anda akan " + (_viewModel.IsAdd ? "memproses" : "mengubah") + " berita acara ini ?",
                "confirmation",
                _viewModel.OnSubmitFormCommand,
                 $"Proses {(_viewModel.IsAdd ? "Tambah" : "Koreksi")} BA",
                "Batal",
                false,
                false,
                "hublang",
                _viewModel.OnRefreshCommand);
            await Task.FromResult(_isTest);
        }
    }
}
