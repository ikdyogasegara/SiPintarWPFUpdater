using System.Windows.Input;
using SiPintar.Commands.Perencanaan.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Perencanaan.Bantuan
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
