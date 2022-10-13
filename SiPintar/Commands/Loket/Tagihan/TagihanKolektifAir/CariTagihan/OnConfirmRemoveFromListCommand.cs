using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Tagihan;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.CariTagihan
{
    public class OnConfirmRemoveFromListCommand : AsyncCommandBase
    {
        private readonly CariTagihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnConfirmRemoveFromListCommand(CariTagihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "LoketRootDialog");

            _viewModel.ListSelectedPelangganAir = new ObservableCollection<MasterPelangganAirWpf>();
            _viewModel.ListSelectedNonAir = new ObservableCollection<RekeningNonAirWpf>();

            _viewModel.IsEmptyAir = true;
            _viewModel.IsEmptyNonAir = true;
            _viewModel.IsEmpty = true;
            await Task.FromResult(_isTest);
        }
    }
}
