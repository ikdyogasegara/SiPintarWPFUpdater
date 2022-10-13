using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class AnggaranLabaRugiViewModel : ViewModelBase
    {
        public AnggaranLabaRugiViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnRefreshCommand { get; set; }
        public ICommand OnOpenEditFormCommand { get; set; }
        public ICommand OnSubmitFormCommand { get; set; }

        private AnggaranLabaRugiDto _anggaranLabaRugiForm = new();
        public AnggaranLabaRugiDto AnggaranLabaRugiForm
        {
            get => _anggaranLabaRugiForm;
            set
            {
                _anggaranLabaRugiForm = value;
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
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isPerTahun = true;
        public bool IsPerTahun
        {
            get => _isPerTahun;
            set
            {
                _isPerTahun = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyJenis = true;
        public bool IsEmptyJenis
        {
            get { return _isEmptyJenis; }
            set
            {
                _isEmptyJenis = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyUraian = true;
        public bool IsEmptyUraian
        {
            get => _isEmptyUraian;
            set
            {
                _isEmptyUraian = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AnggaranLabaRugiDto> _dataList = new();
        public ObservableCollection<AnggaranLabaRugiDto> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeAkuntansiDto> _periodeTutupBukuList = new();
        public ObservableCollection<MasterPeriodeAkuntansiDto> PeriodeTutupBukuList
        {
            get => _periodeTutupBukuList;
            set
            {
                _periodeTutupBukuList = value;
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

        private ObservableCollection<AnggaranLabaRugiDto> _dataUraianList = new();
        public ObservableCollection<AnggaranLabaRugiDto> DataUraianList
        {
            get => _dataUraianList;
            set
            {
                _dataUraianList = value;
                OnPropertyChanged();
            }
        }

        private AnggaranLabaRugiDto _selectedDataUraian = new();
        public AnggaranLabaRugiDto SelectedDataUraian
        {
            get => _selectedDataUraian;
            set
            {
                _selectedDataUraian = value;
                OnPropertyChanged();
            }
        }

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

        #region Filter

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
