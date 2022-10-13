using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.DistribusiPelanggan;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.DistribusiPelanggan;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class DistribusiPelangganViewModel : ViewModelBase
    {
        public readonly PerKecamatanViewModel _perKecamatan;
        public readonly PerKelurahanViewModel _perKelurahan;
        public readonly PerBlokViewModel _perBlok;
        public readonly PerRayonViewModel _perRayon;
        public readonly PerGolonganViewModel _perGolongan;
        public readonly PerDiameterViewModel _perDiameter;

        public DistribusiPelangganViewModel(IRestApiClientModel restApi)
        {
            _perKecamatan = new PerKecamatanViewModel(this, restApi);
            _perKelurahan = new PerKelurahanViewModel(this, restApi);
            _perBlok = new PerBlokViewModel(this, restApi);
            _perRayon = new PerRayonViewModel(this, restApi);
            _perGolongan = new PerGolonganViewModel(this, restApi);
            _perDiameter = new PerDiameterViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            GetKecamatanCommand = new GetKecamatanCommand(this, restApi);
            GetKelurahanCommand = new GetKelurahanCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand GetKecamatanCommand { get; }
        public ICommand GetKelurahanCommand { get; }


        private bool _isPageKelurahan;
        public bool IsPageKelurahan
        {
            get { return _isPageKelurahan; }
            set { _isPageKelurahan = value; OnPropertyChanged(); }
        }

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
                "per_kecamatan" => _perKecamatan,
                "per_kelurahan" => _perKelurahan,
                "per_blok" => _perBlok,
                "per_rayon" => _perRayon,
                "per_golongan" => _perGolongan,
                "per_diameter" => _perDiameter,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand();
            IsPageKelurahan = pageName == "per_kelurahan";
        }

        public void LoadPageCommand()
        {
            _ = Task.Run(() =>
            {
                switch (CurrentSection)
                {
                    case "per_kecamatan":
                        ((PerKecamatanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "per_kelurahan":
                        ((PerKelurahanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "per_blok":
                        ((PerBlokViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "per_rayon":
                        ((PerRayonViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "per_golongan":
                        ((PerGolonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "per_diameter":
                        ((PerDiameterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public ObservableCollection<KeyValuePair<string, string>> JenisList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("per_kecamatan", "Per Kecamatan"),
                    new KeyValuePair<string, string>("per_kelurahan", "Per Kelurahan"),
                    new KeyValuePair<string, string>("per_blok", "Per Blok"),
                    new KeyValuePair<string, string>("per_rayon", "Per Rayon"),
                    new KeyValuePair<string, string>("per_golongan", "Berdasarkan Golongan"),
                    new KeyValuePair<string, string>("per_diameter", "Berdasarkan Diameter"),
                };
            }
        }

        private KeyValuePair<string, string>? _selectedJenis;
        public KeyValuePair<string, string>? SelectedJenis
        {
            get { return _selectedJenis; }
            set
            {
                _selectedJenis = value;
                OnPropertyChanged();

                if (_selectedJenis != null)
                    CurrentSection = _selectedJenis?.Key;
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

        private MasterPeriodeDto _selectedPeriode;
        public MasterPeriodeDto SelectedPeriode
        {
            get { return _selectedPeriode; }
            set
            {
                _selectedPeriode = value;
                OnPropertyChanged();

                if (value != null)
                    LoadPageCommand();
            }
        }

        private ObservableCollection<MasterKecamatanDto> _kecamatanList;
        public ObservableCollection<MasterKecamatanDto> KecamatanList
        {
            get { return _kecamatanList; }
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
            }
        }

        private MasterKecamatanDto _selectedKecamatan;
        public MasterKecamatanDto SelectedKecamatan
        {
            get { return _selectedKecamatan; }
            set
            {
                _selectedKecamatan = value;
                OnPropertyChanged();

                if (value != null)
                {
                    GetKelurahanCommand.Execute(null);
                    LoadPageCommand();
                }
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

        private MasterKelurahanDto _selectedKelurahan;
        public MasterKelurahanDto SelectedKelurahan
        {
            get { return _selectedKelurahan; }
            set
            {
                _selectedKelurahan = value;
                OnPropertyChanged();

                if (value != null)
                    LoadPageCommand();
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
