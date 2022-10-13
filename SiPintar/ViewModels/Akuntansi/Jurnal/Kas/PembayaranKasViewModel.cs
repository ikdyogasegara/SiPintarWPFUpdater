using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Jurnal.Kas.PembayaranKas;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Jurnal.Kas
{
    public class PembayaranKasViewModel : VmBase
    {
        public PembayaranKasViewModel(KasViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnCetakDataCommand = new OnCetakDataCommand(this, restApi, isTest);
            OnCetakJbkDetailCommand = new OnCetakCommand(restApi, "akuntansi", "cetak-jbk-detail", "Jurnal Pembayaran Kas", ErrorHandlingCetak, isTest);
            OnCetakJbkRekapCommand = new OnCetakCommand(restApi, "akuntansi", "cetak-jbk-rekap", "Jurnal Pembayaran Kas", ErrorHandlingCetak, isTest);
        }

        public ICommand OnCetakDataCommand { get; }
        public ICommand OnCetakJbkDetailCommand { get; }
        public ICommand OnCetakJbkRekapCommand { get; }


        private string _periodeAwal;
        public string PeriodeAwal
        {
            get => _periodeAwal;
            set
            {
                _periodeAwal = value;
                OnPropertyChanged();
            }
        }

        private string _periodeAkhir;
        public string PeriodeAkhir
        {
            get => _periodeAkhir;
            set
            {
                _periodeAkhir = value;
                OnPropertyChanged();
            }
        }
    }
}
