using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekAir;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Jurnal.Rekening
{
    public class RekAirViewModel : ViewModelBase
    {

        public RekAirViewModel(RekeningViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnOpenConfirmProsesJurnalCommand = new OnOpenConfirmProsesJurnalCommand(this, restApi, isTest);
            OnProsesJurnalCommand = new OnProsesJurnalCommand(this, restApi, isTest);
            OnCetakDataCommand = new OnCetakDataCommand(this, restApi, isTest);
            OnCetakJraDetailCommand = new OnCetakCommand(restApi, "akuntansi", "cetak-jra-detail", "Jurnal Rekening Air", ErrorHandlingCetak, isTest);
            OnCetakJraRekapCommand = new OnCetakCommand(restApi, "akuntansi", "cetak-jra-rekap", "Jurnal Rekening Air", ErrorHandlingCetak, isTest);
        }

        public ICommand OnOpenConfirmProsesJurnalCommand { get; }
        public ICommand OnProsesJurnalCommand { get; }
        public ICommand OnCetakDataCommand { get; }
        public ICommand OnCetakJraDetailCommand { get; }
        public ICommand OnCetakJraRekapCommand { get; }
        public ICommand OnLoadCommand { get; }


        private MasterPeriodeAkuntansiDto _selectedPeriode = new();
        public MasterPeriodeAkuntansiDto SelectedPeriode
        {
            get => _selectedPeriode;
            set
            {
                _selectedPeriode = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeAkuntansiDto> _periodeList;
        public ObservableCollection<MasterPeriodeAkuntansiDto> PeriodeList
        {
            get => _periodeList;
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }


    }
}
