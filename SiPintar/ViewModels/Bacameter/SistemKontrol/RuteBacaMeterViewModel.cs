using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class RuteBacaMeterViewModel : ViewModelBase
    {
        public readonly DataRayonViewModel _rayon;
        public readonly RuteBacaMeter.PetugasBacaViewModel _petugas;

        public RuteBacaMeterViewModel(IRestApiClientModel restApi)
        {
            _rayon = new DataRayonViewModel(this, restApi);
            _petugas = new RuteBacaMeter.PetugasBacaViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
            GetDataRayonCommand = new GetDataRayonCommand(this, restApi);
            GetDataPetugasCommand = new GetDataPetugasCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand GetDataRayonCommand { get; }
        public ICommand GetDataPetugasCommand { get; }


        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
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
                "rayon" => _rayon,
                "petugas" => _petugas,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
            IsRayonPage = pageName == "rayon";
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "rayon":
                        ((DataRayonViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "petugas":
                        ((RuteBacaMeter.PetugasBacaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }


        private bool _isRayonPage;
        public bool IsRayonPage
        {
            get { return _isRayonPage; }
            set { _isRayonPage = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterRayonDto> _rayonList = new ObservableCollection<MasterRayonDto>();
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _selectedRayon;
        public MasterRayonDto SelectedRayon
        {
            get { return _selectedRayon; }
            set
            {
                _selectedRayon = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPetugasBacaDto> _petugasBacaList = new ObservableCollection<MasterPetugasBacaDto>();
        public ObservableCollection<MasterPetugasBacaDto> PetugasBacaList
        {
            get { return _petugasBacaList; }
            set
            {
                _petugasBacaList = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _selectedPetugasBaca;
        public MasterPetugasBacaDto SelectedPetugasBaca
        {
            get { return _selectedPetugasBaca; }
            set
            {
                _selectedPetugasBaca = value;
                OnPropertyChanged();
            }
        }

        private string _kodePetugasFilter;
        public string KodePetugasFilter
        {
            get { return _kodePetugasFilter; }
            set
            {
                _kodePetugasFilter = value;
                OnPropertyChanged();
            }
        }

        private string _namaPetugasFilter;
        public string NamaPetugasFilter
        {
            get { return _namaPetugasFilter; }
            set
            {
                _namaPetugasFilter = value;
                OnPropertyChanged();
            }
        }

        private string _kodeRayonFilter;
        public string KodeRayonFilter
        {
            get { return _kodeRayonFilter; }
            set
            {
                _kodeRayonFilter = value;
                OnPropertyChanged();
            }
        }

        private string _namaRayonFilter;
        public string NamaRayonFilter
        {
            get { return _namaRayonFilter; }
            set
            {
                _namaRayonFilter = value;
                OnPropertyChanged();
            }
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

        private int? _selectedIdPeriode;
        public int? SelectedIdPeriode
        {
            get { return _selectedIdPeriode; }
            set
            {
                _selectedIdPeriode = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> TanggalList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>();

                for (int i = 1; i <= 31; i++)
                    data.Add(new KeyValuePair<int, string>(i, i.ToString()));

                return data;
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

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
