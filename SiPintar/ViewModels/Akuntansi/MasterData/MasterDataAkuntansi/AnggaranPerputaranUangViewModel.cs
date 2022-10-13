using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class AnggaranPerputaranUangViewModel : ViewModelBase
    {
        public AnggaranPerputaranUangViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnRefreshCommand { get; set; }
        public ICommand OnOpenEditFormCommand { get; set; }
        public ICommand OnSubmitEditCommand { get; set; }



        private bool _isKonsolidasi;
        public bool IsKonsolidasi
        {
            get => _isKonsolidasi;
            set
            {
                _isKonsolidasi = value;
                OnPropertyChanged();
            }
        }


        private string _selectedJenisAnggaran = string.Empty;
        public string SelectedJenisAnggaran
        {
            get { return _selectedJenisAnggaran; }
            set
            {
                _selectedJenisAnggaran = value;
                OnPropertyChanged();

                if (_selectedJenisAnggaran == "Detail")
                {
                    IsRekap = false;
                }
                else if (_selectedJenisAnggaran == "Rekap")
                {
                    IsRekap = true;
                }
            }
        }

        private bool _isRekap;
        public bool IsRekap
        {
            get => _isRekap;
            set
            {
                _isRekap = value;
                OnPropertyChanged();
            }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }


        #region Data Tahun
        private ObservableCollection<MasterPeriodeAkuntansiDto> _masterPeriodeAkuntansiList = new();
        public ObservableCollection<MasterPeriodeAkuntansiDto> MasterPeriodeAkuntansiList
        {
            get => _masterPeriodeAkuntansiList;
            set
            {
                _masterPeriodeAkuntansiList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _tahunList = new();
        public ObservableCollection<KeyValuePair<int, string>> TahunList
        {
            get => _tahunList;
            set
            {
                _tahunList = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _selectedTahun;
        public KeyValuePair<int, string> SelectedTahun
        {
            get => _selectedTahun;
            set
            {
                _selectedTahun = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Data Wilayah
        private ObservableCollection<MasterWilayahDto> _wilayahList = new();
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get => _wilayahList;
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _selectedWilayah = new();
        public MasterWilayahDto SelectedWilayah
        {
            get => _selectedWilayah;
            set
            {
                _selectedWilayah = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Data Laporan Perputaran Uang
        private ObservableCollection<LaporanPerputaranUangDto> _laporanPerputaranUang = new();
        public ObservableCollection<LaporanPerputaranUangDto> LaporanPerputaranUang
        {
            get => _laporanPerputaranUang;
            set
            {
                _laporanPerputaranUang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KeyValuePair<string, string>> _dataJenisList = new();
        public ObservableCollection<KeyValuePair<string, string>> DataJenisList
        {
            get => _dataJenisList;
            set
            {
                _dataJenisList = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _selectedDataJenis;
        public KeyValuePair<string, string> SelectedDataJenis
        {
            get => _selectedDataJenis;
            set
            {
                _selectedDataJenis = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LaporanPerputaranUangDto> _dataUraianList = new();
        public ObservableCollection<LaporanPerputaranUangDto> DataUraianList
        {
            get => _dataUraianList;
            set
            {
                _dataUraianList = value;
                OnPropertyChanged();
            }
        }


        private LaporanPerputaranUangDto _selectedDataUraian = new();
        public LaporanPerputaranUangDto SelectedDataUraian
        {
            get => _selectedDataUraian;
            set
            {
                _selectedDataUraian = value;
                OnPropertyChanged();

            }
        }

        private LaporanPerputaranUangDto _anggaranPerputaranUangForm = new();
        public LaporanPerputaranUangDto AnggaranPerputaranUangForm
        {
            get { return _anggaranPerputaranUangForm; }
            set
            {
                _anggaranPerputaranUangForm = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Filter

        private bool _isKodePerkiraanChecked;
        public bool IsKodePerkiraanChecked
        {
            get => _isKodePerkiraanChecked;
            set
            {
                _isKodePerkiraanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaPerkiraanChecked;
        public bool IsNamaPerkiraanChecked
        {
            get => _isNamaPerkiraanChecked;
            set
            {
                _isNamaPerkiraanChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterKodePerkiraan = string.Empty;
        public string FilterKodePerkiraan
        {
            get => _filterKodePerkiraan;
            set
            {
                _filterKodePerkiraan = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaPerkiraan = string.Empty;
        public string FilterNamaPerkiraan
        {
            get => _filterNamaPerkiraan;
            set
            {
                _filterNamaPerkiraan = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
