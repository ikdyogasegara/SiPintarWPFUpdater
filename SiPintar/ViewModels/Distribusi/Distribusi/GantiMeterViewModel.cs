using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Distribusi.Distribusi.GantiMeter;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.ViewModels.Distribusi.Distribusi
{
    [ExcludeFromCodeCoverage]
    public class GantiMeterViewModel : ViewModelBase
    {
        public readonly KelainanBacameterViewModel KelainanBacameter;
        public readonly RotasiMeterViewModel RotasiMeter;

        public GantiMeterViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            KelainanBacameter = new KelainanBacameterViewModel(this, restApi);
            RotasiMeter = new RotasiMeterViewModel(this, restApi);
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnFilterCommand = new OnFilterCommand(this, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnAddRotasiMeterCommand = new OnAddRotasiMeterCommand(this, isTest);
            OnSubmitAddRotasiMeterCommand = new OnSubmitAddRotasiMeterCommand(this, restApi, isTest);
            OnShowConfirmSubmitCommand = new OnShowConfirmSubmitCommand(this, isTest);
            GetPelangganListCommand = new GetPelangganListCommand(this, restApi, isTest);
            OnShowConfirmDeleteCommand = new OnShowConfirmDeleteCommand(this, isTest);
            OnShowSpkCommand = new OnShowSpkCommand(this, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnAddRotasiMeterCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand GetPelangganListCommand { get; }
        public ICommand OnSubmitAddRotasiMeterCommand { get; }
        public ICommand OnShowConfirmSubmitCommand { get; }
        public ICommand OnShowConfirmDeleteCommand { get; }
        public ICommand OnShowSpkCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get => _pageViewModel;
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private int _currentStep = 1;
        public int CurrentStep
        {
            get => _currentStep;
            set
            {
                _currentStep = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganAirDto> _pelangganList;
        public ObservableCollection<MasterPelangganAirDto> PelangganList
        {
            get { return _pelangganList; }
            set
            {
                _pelangganList = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _selectedPelanggan;
        public MasterPelangganAirDto SelectedPelanggan
        {
            get { return _selectedPelanggan; }
            set
            {
                _selectedPelanggan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganAirDetailDto> _pelangganDetail;
        public ObservableCollection<MasterPelangganAirDetailDto> PelangganDetail
        {
            get { return _pelangganDetail; }
            set
            {
                _pelangganDetail = value;
                OnPropertyChanged();
            }
        }

        private string _namaPelangganForm;
        public string NamaPelangganForm
        {
            get => _namaPelangganForm;
            set
            {
                _namaPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _noSambForm;
        public string NoSambForm
        {
            get => _noSambForm;
            set
            {
                _noSambForm = value;
                OnPropertyChanged();
            }
        }

        private string _alamatForm;
        public string AlamatForm
        {
            get => _alamatForm;
            set
            {
                _alamatForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyForm1;
        public bool IsEmptyForm1
        {
            get { return _isEmptyForm1; }
            set
            {
                _isEmptyForm1 = value;
                OnPropertyChanged();
            }
        }


        #region Tahun
        private bool _isTahunChecked;
        public bool IsTahunChecked
        {
            get => _isTahunChecked;
            set
            {
                _isTahunChecked = value;
                OnPropertyChanged();
            }
        }

        private dynamic _tahunList;
        public dynamic TahunList
        {
            get => _tahunList;
            set { _tahunList = value; OnPropertyChanged(); }
        }

        private int? _tahunFilter;
        public int? TahunFilter
        {
            get => _tahunFilter;
            set
            {
                _tahunFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Kategori
        private bool _isKategoriChecked;
        public bool IsKategoriChecked
        {
            get => _isKategoriChecked;
            set
            {
                _isKategoriChecked = value;
                OnPropertyChanged();

                if (!value)
                    KategoriFilter = null;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> KategoriList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Rotasi Meter"),
                    new KeyValuePair<int, string>(2, "Kelainan"),
                };
            }
        }

        private KeyValuePair<int, string>? _kategoriFilter;
        public KeyValuePair<int, string>? KategoriFilter
        {
            get => _kategoriFilter;
            set
            {
                _kategoriFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Kelainan
        private bool _isKelainanChecked;
        public bool IsKelainanChecked
        {
            get => _isKelainanChecked;
            set
            {
                _isKelainanChecked = value;
                OnPropertyChanged();

                if (!value)
                    KelainanFilter = null;
            }
        }

        private List<MasterKelainanDto> _kelainanList;
        public List<MasterKelainanDto> KelainanList
        {
            get => _kelainanList;
            set { _kelainanList = value; OnPropertyChanged(); }
        }

        private MasterKelainanDto _kelainanFilter;
        public MasterKelainanDto KelainanFilter
        {
            get => _kelainanFilter;
            set
            {
                _kelainanFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NomorSPK
        private bool _isNomorSpkChecked;
        public bool IsNomorSpkChecked
        {
            get => _isNomorSpkChecked;
            set
            {
                _isNomorSpkChecked = value;
                OnPropertyChanged();

                if (!value)
                    NomorSpkFilter = null;
            }
        }

        private string _NomorSpkFilter;
        public string NomorSpkFilter
        {
            get => _NomorSpkFilter;
            set
            {
                _NomorSpkFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NomorBA
        private bool _isNomorBaChecked;
        public bool IsNomorBaChecked
        {
            get => _isNomorBaChecked;
            set
            {
                _isNomorBaChecked = value;
                OnPropertyChanged();

                if (!value)
                    NomorBaFilter = null;
            }
        }

        private string _NomorBaFilter;
        public string NomorBaFilter
        {
            get => _NomorBaFilter;
            set
            {
                _NomorBaFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Nama
        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get => _isNamaChecked;
            set
            {
                _isNamaChecked = value;
                OnPropertyChanged();

                if (!value)
                    NamaFilter = null;
            }
        }

        private string _NamaFilter;
        public string NamaFilter
        {
            get => _NamaFilter;
            set
            {
                _NamaFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NoSamb
        private bool _isNoSambChecked;
        public bool IsNoSambChecked
        {
            get => _isNoSambChecked;
            set
            {
                _isNoSambChecked = value;
                OnPropertyChanged();

                if (!value)
                    NoSambFilter = null;
            }
        }

        private string _NoSambFilter;
        public string NoSambFilter
        {
            get => _NoSambFilter;
            set
            {
                _NoSambFilter = value;
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

        private List<MasterWilayahDto> _wilayahList;
        public List<MasterWilayahDto> WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); }
        }

        private MasterWilayahDto _wilayahFilter;
        public MasterWilayahDto WilayahFilter
        {
            get => _wilayahFilter;
            set
            {
                _wilayahFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Golongan
        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                OnPropertyChanged();

                if (!value)
                    GolonganFilter = null;
            }
        }

        private List<MasterGolonganDto> _golonganList;
        public List<MasterGolonganDto> GolonganList
        {
            get { return _golonganList; }
            set { _golonganList = value; OnPropertyChanged(); }
        }

        private MasterGolonganDto _golonganFilter;
        public MasterGolonganDto GolonganFilter
        {
            get => _golonganFilter;
            set
            {
                _golonganFilter = value;
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

        private List<MasterRayonDto> _rayonList;
        public List<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set { _rayonList = value; OnPropertyChanged(); }
        }

        private MasterRayonDto _rayonFilter;
        public MasterRayonDto RayonFilter
        {
            get => _rayonFilter;
            set
            {
                _rayonFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Area
        private bool _isAreaChecked;
        public bool IsAreaChecked
        {
            get { return _isAreaChecked; }
            set
            {
                _isAreaChecked = value;
                OnPropertyChanged();

                if (!value)
                    AreaFilter = null;
            }
        }

        private List<MasterAreaDto> _areaList;
        public List<MasterAreaDto> AreaList
        {
            get { return _areaList; }
            set { _areaList = value; OnPropertyChanged(); }
        }

        private MasterAreaDto _areaFilter;
        public MasterAreaDto AreaFilter
        {
            get => _areaFilter;
            set
            {
                _areaFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Status Data
        private bool _isStatusDataChecked;
        public bool IsStatusDataChecked
        {
            get { return _isStatusDataChecked; }
            set
            {
                _isStatusDataChecked = value;
                OnPropertyChanged();

                if (!value)
                    StatusDataFilter = null;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusDataList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Sudah SPK Pengecekan"),
                    new KeyValuePair<int, string>(2, "Sudah BA Pengecekan"),
                };
            }
        }

        private KeyValuePair<int, string>? _statusDataFilter;
        public KeyValuePair<int, string>? StatusDataFilter
        {
            get => _statusDataFilter;
            set
            {
                _statusDataFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Merek Meter
        private List<MasterMerekMeterDto> _merekMeterList;
        public List<MasterMerekMeterDto> MerekMeterList
        {
            get { return _merekMeterList; }
            set { _merekMeterList = value; OnPropertyChanged(); }
        }

        private MasterMerekMeterDto _merekMeter;
        public MasterMerekMeterDto MerekMeter
        {
            get => _merekMeter;
            set
            {
                _merekMeter = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Diameter
        private List<MasterDiameterDto> _diameterList;
        public List<MasterDiameterDto> DiameterList
        {
            get { return _diameterList; }
            set { _diameterList = value; OnPropertyChanged(); }
        }

        private MasterDiameterDto _diameter;
        public MasterDiameterDto Diameter
        {
            get => _diameter;
            set
            {
                _diameter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ParamJadwalGantiMeterFilterDto _filter;
        public ParamJadwalGantiMeterFilterDto Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(); }
        }

        private string _currentSection;
        public string CurrentSection
        {
            get { return _currentSection; }
            set
            {
                _currentSection = value;
                OnPropertyChanged();

                if (_currentSection != null)
                    UpdatePage(_currentSection);
            }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "KelainanBacameter" => KelainanBacameter,
                "RotasiMeter" => RotasiMeter,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand();
        }

        public void LoadPageCommand()
        {
            _ = Task.Run(() =>
            {
                switch (CurrentSection)
                {
                    case "KelainanBacameter":
                        ((KelainanBacameterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "RotasiMeter":
                        ((RotasiMeterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }
        private ObservableCollection<JadwalGantiMeterDto> _dataList { get; set; }
        public ObservableCollection<JadwalGantiMeterDto> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class GantimeterWpf : JadwalGantiMeterDto
    {
        public bool IsSelected { get; set; }
    }
}
