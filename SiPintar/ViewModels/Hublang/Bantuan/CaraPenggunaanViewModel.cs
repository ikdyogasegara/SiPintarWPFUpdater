using System.Windows.Input;
using SiPintar.Commands.Hublang.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Hublang.Bantuan
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
