using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.Produktivitas;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.Produktivitas;

namespace SiPintar.ViewModels.Bacameter
{
    public class ProduktivitasViewModel : ViewModelBase
    {
        private readonly PetugasViewModel _petugas;
        private readonly PembacaanPerTanggalViewModel _pembacaanPerTgl;
        private readonly ProgressPembacaanViewModel _progressPembacaan;
        private readonly PelangganViewModel _pelanggan;
        private readonly PetugasBacaPerHariViewModel _petugasBacaPerHari;

        public ProduktivitasViewModel(IRestApiClientModel restApi)
        {
            _petugas = new PetugasViewModel(this, restApi);
            _pembacaanPerTgl = new PembacaanPerTanggalViewModel(this, restApi);
            _progressPembacaan = new ProgressPembacaanViewModel(this, restApi);
            _pelanggan = new PelangganViewModel(this, restApi);
            _petugasBacaPerHari = new PetugasBacaPerHariViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
            OnFilterCommand = new OnFilterCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }


        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> JenisProduktivitasList
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    "Petugas",
                    "Pembacaan per Tanggal",
                    "Progress Pembacaan",
                    "Pelanggan",
                    "Petugas Baca per Hari"
                };
            }
        }

        private string _jenisProduktivitas;
        public string JenisProduktivitas
        {
            get { return _jenisProduktivitas; }
            set
            {
                _jenisProduktivitas = value;
                OnPropertyChanged();
            }
        }

        public void UpdatePage(string pageName)
        {
            JenisProduktivitas = pageName;

            PageViewModel = pageName switch
            {
                "Petugas" => _petugas,
                "Pembacaan per Tanggal" => _pembacaanPerTgl,
                "Progress Pembacaan" => _progressPembacaan,
                "Pelanggan" => _pelanggan,
                "Petugas Baca per Hari" => _petugasBacaPerHari,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Petugas":
                        ((PetugasViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pembacaan per Tanggal":
                        ((PembacaanPerTanggalViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Progress Pembacaan":
                        ((ProgressPembacaanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pelanggan":
                        ((PelangganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Petugas Baca per Hari":
                        ((PetugasBacaPerHariViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        private ObservableCollection<MasterPeriodeDto> _periodeList;
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get { return _periodeList; }
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedPeriode;

        [ExcludeFromCodeCoverage]
        public MasterPeriodeDto SelectedPeriode
        {
            get { return _selectedPeriode; }
            set
            {
                _selectedPeriode = value;
                OnPropertyChanged();
            }
        }

        // filter

        #region Pembaca Meter
        private bool _isPembacaMeterChecked;
        public bool IsPembacaMeterChecked
        {
            get { return _isPembacaMeterChecked; }
            set
            {
                _isPembacaMeterChecked = value;
                OnPropertyChanged();

                if (!value)
                    PembacaMeterFilter = null;
            }
        }

        private ObservableCollection<MasterPetugasBacaDto> _petugasList;
        public ObservableCollection<MasterPetugasBacaDto> PetugasList
        {
            get { return _petugasList; }
            set
            {
                _petugasList = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _pembacaMeterFilter;
        public MasterPetugasBacaDto PembacaMeterFilter
        {
            get { return _pembacaMeterFilter; }
            set
            {
                _pembacaMeterFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Tanggal Baca
        private bool _isTglBacaChecked;
        public bool IsTglBacaChecked
        {
            get { return _isTglBacaChecked; }
            set
            {
                _isTglBacaChecked = value;
                OnPropertyChanged();

                if (!value)
                {
                    TglBacaAwalFilter = null;
                    TglBacaAkhirFilter = null;
                }
            }
        }

        private DateTime? _tglBacaAwalFilter;
        public DateTime? TglBacaAwalFilter
        {
            get { return _tglBacaAwalFilter; }
            set
            {
                _tglBacaAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglBacaAkhirFilter;
        public DateTime? TglBacaAkhirFilter
        {
            get { return _tglBacaAkhirFilter; }
            set
            {
                _tglBacaAkhirFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Tanggal Kirim Hasil Baca
        private bool _isTglKirimHasilBacaChecked;
        public bool IsTglKirimHasilBacaChecked
        {
            get { return _isTglKirimHasilBacaChecked; }
            set
            {
                _isTglKirimHasilBacaChecked = value;
                OnPropertyChanged();

                if (!value)
                {
                    TglKirimAwalFilter = null;
                    TglKirimAkhirFilter = null;
                }
            }
        }

        private DateTime? _tglKirimAwalFilter;
        public DateTime? TglKirimAwalFilter
        {
            get { return _tglKirimAwalFilter; }
            set
            {
                _tglKirimAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglKirimAkhirFilter;
        public DateTime? TglKirimAkhirFilter
        {
            get { return _tglKirimAkhirFilter; }
            set
            {
                _tglKirimAkhirFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Rayon
        private bool _isRayonChecked;
        public bool IsRayonChecked
        {
            get { return _isRayonChecked; }
            set
            {
                _isRayonChecked = value;
                OnPropertyChanged();

                if (!value)
                    RayonFilter = null;
            }
        }

        private ObservableCollection<MasterRayonDto> _rayonList;
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _rayonFilter;
        public MasterRayonDto RayonFilter
        {
            get { return _rayonFilter; }
            set
            {
                _rayonFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Kelurahan
        private bool _isKelurahanChecked;
        public bool IsKelurahanChecked
        {
            get { return _isKelurahanChecked; }
            set
            {
                _isKelurahanChecked = value;
                OnPropertyChanged();

                if (!value)
                    KelurahanFilter = null;
            }
        }

        private ObservableCollection<MasterKelurahanDto> _kelurahanList;
        public ObservableCollection<MasterKelurahanDto> KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _KelurahanFilter;
        public MasterKelurahanDto KelurahanFilter
        {
            get { return _KelurahanFilter; }
            set
            {
                _KelurahanFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Wilayah
        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                OnPropertyChanged();

                if (!value)
                    WilayahFilter = null;
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList;
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _wilayahFilter;
        public MasterWilayahDto WilayahFilter
        {
            get { return _wilayahFilter; }
            set
            {
                _wilayahFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion


        // end filter
    }
}
