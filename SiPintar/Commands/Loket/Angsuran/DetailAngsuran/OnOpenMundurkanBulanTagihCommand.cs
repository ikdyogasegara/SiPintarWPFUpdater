using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.DetailAngsuran;

namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnOpenMundurkanBulanTagihCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenMundurkanBulanTagihCommand(DetailAngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.MundurBulan = "1";
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "LoketRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new MundurkanBulanTagihView(_viewModel);
    }
}
