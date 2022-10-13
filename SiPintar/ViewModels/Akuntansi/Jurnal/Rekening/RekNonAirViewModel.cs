using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekNonAir;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Jurnal.Rekening
{
    public class RekNonAirViewModel : VmBase
    {
        public RekNonAirViewModel(RekeningViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnProsesJurnalCommand = new OnProsesJurnalCommand(this, restApi, isTest);
            OnOpenConfirmProsesJurnalCommand = new OnProsesJurnalCommand(this, restApi, isTest);
            OnCetakDataCommand = new OnCetakDataCommand(this, restApi, isTest);
            OnCetakJrnaRincianCommand = new OnCetakCommand(restApi, "akuntansi", "cetak-jrna", "Jurnal Rekening Non Air", ErrorHandlingCetak, isTest);
            OnCetakJrnaKonsolidasiCommand = new OnCetakCommand(restApi, "akuntansi", "cetak-jrna-konsolidasi", "Jurnal Rekening Non Air", ErrorHandlingCetak, isTest);
        }

        public ICommand OnProsesJurnalCommand { get; }
        public ICommand OnOpenConfirmProsesJurnalCommand { get; }
        public ICommand OnCetakDataCommand { get; }
        public ICommand OnCetakCommand { get; }
        public ICommand OnCetakJrnaRincianCommand { get; }
        public ICommand OnCetakJrnaKonsolidasiCommand { get; }


        private MasterPeriodeAkuntansiDto _selectedPeriode;
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

        private MasterWilayahDto _selectedWilayah;
        public MasterWilayahDto SelectedWilayah
        {
            get => _selectedWilayah;
            set
            {
                _selectedWilayah = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilyahList;
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get => _wilyahList;
            set
            {
                _wilyahList = value;
                OnPropertyChanged();
            }
        }
    }
}
