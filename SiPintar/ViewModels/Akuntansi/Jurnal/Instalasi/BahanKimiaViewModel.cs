using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Jurnal.Instalasi.BahanKimia;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi
{
    public class BahanKimiaViewModel : VmBase
    {
        public BahanKimiaViewModel(InstalasiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenConfirmProsesJurnalCommand = new OnOpenConfirmProsesJurnalCommand(this, restApi, isTest);
            OnProsesJurnalCommand = new OnProsesJurnalCommand(this, restApi, isTest);
            OnCetakDataCommand = new OnCetakDataCommand(this, restApi, isTest);
            OnCetakCommand = new OnCetakCommand(restApi, "akuntansi", "payment-cetak", "Jurnal Bahan Kimia Instalasi", ErrorHandlingCetak, isTest);
        }

        public ICommand OnOpenConfirmProsesJurnalCommand { get; }
        public ICommand OnProsesJurnalCommand { get; }
        public ICommand OnCetakDataCommand { get; }
        public ICommand OnCetakCommand { get; }
    }
}
