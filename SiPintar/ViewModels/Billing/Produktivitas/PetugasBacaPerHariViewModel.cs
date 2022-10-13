using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Produktivitas.PetugasBacaPerHari;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Produktivitas
{
    [ExcludeFromCodeCoverage]
    public class PetugasBacaPerHariViewModel : ViewModelBase
    {
        public PetugasBacaPerHariViewModel(ProduktivitasViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            GetDataHarianCommand = new GetDataHarianCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand GetDataHarianCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }


        private ProduktivitasViewModel _parent { get; set; }
        public ProduktivitasViewModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingPetugas { get; set; }
        public bool IsLoadingPetugas
        {
            get { return _isLoadingPetugas; }
            set
            {
                _isLoadingPetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyPetugas { get; set; }
        public bool IsEmptyPetugas
        {
            get { return _isEmptyPetugas; }
            set
            {
                _isEmptyPetugas = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPetugasBacaDto> _dataListPetugas { get; set; }
        public ObservableCollection<MasterPetugasBacaDto> DataListPetugas
        {
            get { return _dataListPetugas; }
            set
            {
                _dataListPetugas = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _selectedPetugas { get; set; }
        public MasterPetugasBacaDto SelectedPetugas
        {
            get { return _selectedPetugas; }
            set
            {
                _selectedPetugas = value;
                OnPropertyChanged();

                GetDataHarianCommand.Execute(null);
            }
        }

        private bool _isLoadingHarian { get; set; }
        public bool IsLoadingHarian
        {
            get { return _isLoadingHarian; }
            set
            {
                _isLoadingHarian = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyHarian { get; set; }
        public bool IsEmptyHarian
        {
            get { return _isEmptyHarian; }
            set
            {
                _isEmptyHarian = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SummaryProduktifitasBacameterPetugasBacaPerHariDto> _dataListHarian { get; set; }
        public ObservableCollection<SummaryProduktifitasBacameterPetugasBacaPerHariDto> DataListHarian
        {
            get { return _dataListHarian; }
            set
            {
                _dataListHarian = value;
                OnPropertyChanged();
            }
        }

        private SummaryProduktifitasBacameterPetugasBacaPerHariDto _selectedHarian { get; set; }
        public SummaryProduktifitasBacameterPetugasBacaPerHariDto SelectedHarian
        {
            get { return _selectedHarian; }
            set
            {
                _selectedHarian = value;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
                OnLoadCommand.Execute(null);
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

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var ListOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return data;
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;
        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;
        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisible;
        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;
        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;
        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;
        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }



        private int _targetBacaSummary;
        public int TargetBacaSummary
        {
            get => _targetBacaSummary;
            set { _targetBacaSummary = value; OnPropertyChanged(); }
        }

        private int _sisaBacaSummary;
        public int SisaBacaSummary
        {
            get => _sisaBacaSummary;
            set { _sisaBacaSummary = value; OnPropertyChanged(); }
        }

        private int _totalBacaSummary;
        public int TotalBacaSummary
        {
            get => _totalBacaSummary;
            set { _totalBacaSummary = value; OnPropertyChanged(); }
        }

        private DateTime? _lastUpdateSummary;
        public DateTime? LastUpdateSummary
        {
            get => _lastUpdateSummary;
            set { _lastUpdateSummary = value; OnPropertyChanged(); }
        }
    }
}
