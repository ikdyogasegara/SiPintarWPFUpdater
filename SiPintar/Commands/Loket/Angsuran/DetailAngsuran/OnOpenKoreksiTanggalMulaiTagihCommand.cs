using System;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.DetailAngsuran;


namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnOpenKoreksiTanggalMulaiTagihCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenKoreksiTanggalMulaiTagihCommand(DetailAngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SelectedDate = _viewModel.DetailAngsuran.TglMulaiTagih;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "LoketRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new KoreksiTanggalMulaiTagihView(_viewModel);
    }
}
