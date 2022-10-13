using System;
using System.Windows.Input;
using SiPintar.Commands.Gudang.Pengolahan.HibahBarang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.Pengolahan
{
    public class StockBarangViewModel : ViewModelBase
    {
        public bool IsHibah { get; }
        public StockBarangViewModel(PengolahanViewModel parent, IRestApiClientModel restApi, bool isTest = false, bool isHibah = true)
        {
            _ = parent;
            IsHibah = isHibah;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
    }
}
