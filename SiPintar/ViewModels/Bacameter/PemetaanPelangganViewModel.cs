using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.PemetaanPelanggan;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter
{
    public class PemetaanPelangganViewModel : ViewModelBase
    {
        public PemetaanPelangganViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }

        public ObservableCollection<string> JenisPetaList
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    "Peta Pelanggan",
                    "Peta Posisi Pelanggan & Pembacaan"
                };
            }
        }

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

        private bool _isShowPeriodeOption;
        public bool IsShowPeriodeOption
        {
            get { return _isShowPeriodeOption; }
            set
            {
                _isShowPeriodeOption = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeDto> _periodeList = new ObservableCollection<MasterPeriodeDto>();
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get => _periodeList;
            set { _periodeList = value; OnPropertyChanged(); }
        }

        private MasterPeriodeDto _selectedPeriode = new MasterPeriodeDto();
        public MasterPeriodeDto SelectedPeriode
        {
            get => _selectedPeriode;
            set { _selectedPeriode = value; OnPropertyChanged(); }
        }

        private string _selectedJenisPeta;
        public string SelectedJenisPeta
        {
            get { return _selectedJenisPeta; }
            set
            {
                _selectedJenisPeta = value;
                OnPropertyChanged();

                IsShowPeriodeOption = !string.IsNullOrEmpty(_selectedJenisPeta) && _selectedJenisPeta != "Peta Pelanggan";
            }
        }

        private bool _isRayonChecked;
        public bool IsRayonChecked
        {
            get => _isRayonChecked;
            set
            {
                _isRayonChecked = value;
                OnPropertyChanged();

                if (!value)
                    SelectedRayon = null;
            }
        }

        private ObservableCollection<MasterRayonDto> _rayonList = new ObservableCollection<MasterRayonDto>();
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get => _rayonList;
            set { _rayonList = value; OnPropertyChanged(); }
        }

        private MasterRayonDto _selectedRayon;
        public MasterRayonDto SelectedRayon
        {
            get => _selectedRayon;
            set { _selectedRayon = value; OnPropertyChanged(); }
        }

        private List<MapMarkerObject> _markerList;
        public List<MapMarkerObject> MarkerList
        {
            get { return _markerList; }
            set
            {
                _markerList = value;
                OnPropertyChanged();
            }
        }

        public string CurrentPdamName
        {
            get { return Application.Current.Resources["NamaPdam"]?.ToString(); }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
