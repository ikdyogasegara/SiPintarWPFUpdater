using System.Windows.Input;
using SiPintar.Commands.Bacameter.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Bacameter.Bantuan
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
