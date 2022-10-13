using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Voucher.Kuitansi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Voucher
{
    public class KuitansiViewModel : ViewModelBase
    {
        public KuitansiViewModel(VoucherViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
