using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Voucher.LembarHarian;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Voucher
{
    public class LembarHarianViewModel : ViewModelBase
    {
        public LembarHarianViewModel(VoucherViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
