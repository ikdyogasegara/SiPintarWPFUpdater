using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Bantuan.CaraPenggunaan;

namespace SiPintar.ViewModels.Akuntansi.Bantuan
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
