using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.JadwalRuteBaca;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.JadwalRuteBaca;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class JadwalRuteBacaViewModel : ViewModelBase
    {
        public readonly PetugasBacaGrafikViewModel _petugasBacaGrafik;
        public readonly PetugasBacaGridViewModel _petugasBacaGrid;
        public readonly RayonBelumDijadwalViewModel _rayonBelumDijadwal;
        public readonly RayonTanpaTanggalBacaViewModel _rayonTanpaTanggalBaca;

        public JadwalRuteBacaViewModel(IRestApiClientModel restApi)
        {
            _petugasBacaGrafik = new PetugasBacaGrafikViewModel(this, restApi);
            _petugasBacaGrid = new PetugasBacaGridViewModel(this, restApi);
            _rayonBelumDijadwal = new RayonBelumDijadwalViewModel(this, restApi);
            _rayonTanpaTanggalBaca = new RayonTanpaTanggalBacaViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }


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
                "petugas_grafik" => _petugasBacaGrafik,
                "petugas_grid" => _petugasBacaGrid,
                "rayon_belum_dijadwal" => _rayonBelumDijadwal,
                "rayon_tanpa_tanggal_baca" => _rayonTanpaTanggalBaca,
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
                    case "petugas_grafik":
                        ((PetugasBacaGrafikViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "petugas_grid":
                        ((PetugasBacaGridViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "rayon_belum_dijadwal":
                        ((RayonBelumDijadwalViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "rayon_tanpa_tanggal_baca":
                        ((RayonTanpaTanggalBacaViewModel)PageViewModel).OnLoadCommand.Execute(null);
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
                    new KeyValuePair<string, string>("petugas_grafik", "Petugas Baca (Grafik)"),
                    new KeyValuePair<string, string>("petugas_grid", "Petugas Baca (Grid)"),
                    new KeyValuePair<string, string>("rayon_belum_dijadwal", "Rayon Belum Dijadwal"),
                    new KeyValuePair<string, string>("rayon_tanpa_tanggal_baca", "Rayon Tanpa Tanggal Baca"),
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

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
