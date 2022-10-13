using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Distribusi.Distribusi.GantiMeter.RotasiMeter;
using SiPintar.Utilities;


namespace SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter
{
    public class RotasiMeterViewModel : ViewModelBase
    {
        public RotasiMeterViewModel(GantiMeterViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, isTest);
            OnOpenBeritaAcaraCommand = new OnOpenBeritaAcaraCommand(this, restApi, isTest);
            OnSubmitBeritaAcaraCommand = new OnSubmitBeritaAcaraCommand(this, restApi, isTest);
            OnSubmitSpkSurveiCommand = new OnSubmitSpkSurveiCommand(this, restApi, isTest);
            OnOpenSpkSurveiCommand = new OnOpenSpkSurveiCommand(this, restApi, isTest);
            OnAddPetugasCommand = new OnAddPetugasCommand(this);
            OnRemovePetugasCommand = new OnRemovePetugasCommand(this);
            OnBrowseFotoCommand = new OnBrowseFotoCommand(this, isTest);
        }
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnOpenSpkSurveiCommand { get; }
        public ICommand OnOpenBeritaAcaraCommand { get; }
        public ICommand OnSubmitBeritaAcaraCommand { get; }
        public ICommand OnSubmitSpkSurveiCommand { get; }
        public ICommand OnAddPetugasCommand { get; }
        public ICommand OnRemovePetugasCommand { get; }
        public ICommand OnBrowseFotoCommand { get; }

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


        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(20, "20");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecord;
        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private GantiMeterViewModel _parent;
        public GantiMeterViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _formPetugasTambahan = new ObservableCollection<MasterPegawaiDto>();
        public ObservableCollection<MasterPegawaiDto> FormPetugasTambahan
        {
            get { return _formPetugasTambahan; }
            set { _formPetugasTambahan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _petugasListForm;
        public ObservableCollection<MasterPegawaiDto> PetugasListForm
        {
            get { return _petugasListForm; }
            set { _petugasListForm = value; OnPropertyChanged(); }
        }

        private Uri _fotoMeterLamaUri;
        public Uri FotoMeterLamaUri
        {
            get { return _fotoMeterLamaUri; }
            set { _fotoMeterLamaUri = value; OnPropertyChanged(); }
        }

        private Uri _fotoMeterBaruUri;
        public Uri FotoMeterBaruUri
        {
            get { return _fotoMeterBaruUri; }
            set { _fotoMeterBaruUri = value; OnPropertyChanged(); }
        }

        private bool _isCekFotoMeterLama;
        public bool IsCekFotoMeterLama
        {
            get { return _isCekFotoMeterLama; }
            set
            {
                _isCekFotoMeterLama = value;
                OnPropertyChanged();
            }
        }

        private bool _isCekFotoMeterBaru;
        public bool IsCekFotoMeterBaru
        {
            get { return _isCekFotoMeterBaru; }
            set
            {
                _isCekFotoMeterBaru = value;
                OnPropertyChanged();
            }
        }

        private string _noHp;
        public string NoHp
        {
            get { return _noHp; }
            set
            {
                _noHp = value;
                OnPropertyChanged();
            }
        }

        private decimal? _angkaMeterLama;
        public decimal? AngkaMeterLama
        {
            get => _angkaMeterLama;
            set
            {
                _angkaMeterLama = value;
                OnPropertyChanged();
            }
        }

        private decimal? _angkaMeterBaru;
        public decimal? AngkaMeterBaru
        {
            get => _angkaMeterBaru;
            set
            {
                _angkaMeterBaru = value;
                OnPropertyChanged();
            }
        }

        private string _noSeriMeter;
        public string NoSeriMeter
        {
            get => _noSeriMeter;
            set
            {
                _noSeriMeter = value;
                OnPropertyChanged();
            }
        }

        private string _keterangan;
        public string Keterangan
        {
            get => _keterangan;
            set
            {
                _keterangan = value;
                OnPropertyChanged();
            }
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
        private ObservableCollection<GantimeterWpf> _dataWpfList { get; set; }
        public ObservableCollection<GantimeterWpf> DataWpfList
        {
            get => _dataWpfList;
            set
            {
                _dataWpfList = value;
                OnPropertyChanged();
            }
        }


        private JadwalGantiMeterDto _selectedData { get; set; }
        public JadwalGantiMeterDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private object _tableSetting;
        public object TableSetting
        {
            get { return _tableSetting; }
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        private int _totalSelectedList;
        public int TotalSelectedList
        {
            get => _totalSelectedList;
            set { _totalSelectedList = value; OnPropertyChanged(); }
        }

        private bool _isAllSelected;
        public bool IsAllSelected
        {
            get => _isAllSelected;
            set
            {
                _isAllSelected = value;
                OnPropertyChanged();

                if (DataWpfList != null)
                {
                    var temp = new ObservableCollection<GantimeterWpf>(DataWpfList);
                    foreach (var item in temp)
                        item.IsSelected = value;

                    DataWpfList = temp;
                    TotalSelectedList = temp.Where(i => i.IsSelected == true).ToList().Count;
                }
            }
        }
    }
}
