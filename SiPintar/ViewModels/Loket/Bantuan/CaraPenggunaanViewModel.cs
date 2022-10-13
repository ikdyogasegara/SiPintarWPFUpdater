using System.Windows.Input;
using SiPintar.Commands.Loket.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Loket.Bantuan
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
