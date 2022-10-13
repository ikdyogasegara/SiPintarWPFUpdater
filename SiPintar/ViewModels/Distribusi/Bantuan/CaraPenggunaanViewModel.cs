using System.Windows.Input;
using SiPintar.Commands.Distribusi.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Distribusi.Bantuan
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
