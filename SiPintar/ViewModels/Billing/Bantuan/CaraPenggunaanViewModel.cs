using System.Windows.Input;
using SiPintar.Commands.Billing.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Billing.Bantuan
{
    public class CaraPenggunaanViewModel : ViewModelBase
    {
        public CaraPenggunaanViewModel(BantuanViewModel parentViewModel)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this);
        }

        public ICommand OnLoadCommand { get; }
    }
}
