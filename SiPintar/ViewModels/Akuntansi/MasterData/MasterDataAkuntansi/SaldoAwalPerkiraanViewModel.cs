using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class SaldoAwalPerkiraanViewModel : ViewModelBase
    {
        public SaldoAwalPerkiraanViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableFormCommand = new OnSubmitSettingTableFormCommand(this, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenConfirmEditCommand = new OnOpenConfirmEditCommand(this, isTest);
            OnOpenConfirmHitungUlangCommand = new OnOpenConfirmHitungUlangCommand(this, isTest);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi, isTest);
            OnSubmitHitungUlangCommand = new OnSubmitHitungUlangCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnResetFilterCommand { get; set; }
        public ICommand OnOpenEditFormCommand { get; set; }
        public ICommand OnRefreshCommand { get; set; }
        public ICommand OnOpenSettingTableFormCommand { get; set; }
        public ICommand OnSubmitSettingTableFormCommand { get; set; }
        public ICommand OnOpenConfirmEditCommand { get; set; }
        public ICommand OnOpenConfirmHitungUlangCommand { get; set; }
        public ICommand OnSubmitEditCommand { get; set; }
        public ICommand OnSubmitHitungUlangCommand { get; set; }


        private bool _isCloseDialog;
        public bool IsCloseDialog
        {
            get { return _isCloseDialog; }
            set
            {
                _isCloseDialog = value;
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


        private decimal? _totalSaldoAwal;
        public decimal? TotalSaldoAwal
        {
            get { return _totalSaldoAwal; }
            set
            {
                _totalSaldoAwal = value;
                OnPropertyChanged();
            }
        }

        private decimal? _totalSaldoAkhir;
        public decimal? TotalSaldoAkhir
        {
            get { return _totalSaldoAkhir; }
            set
            {
                _totalSaldoAkhir = value;
                OnPropertyChanged();
            }
        }


        private SaldoAwalPerkiraanDto _formPerkiraan;
        public SaldoAwalPerkiraanDto FormPerkiraan
        {
            get => _formPerkiraan;
            set
            {
                _formPerkiraan = value;
                OnPropertyChanged();
            }
        }

        private SaldoAwalPerkiraanWpf _selectedData;
        public SaldoAwalPerkiraanWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SaldoAwalPerkiraanWpf> _dataList;
        public ObservableCollection<SaldoAwalPerkiraanWpf> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
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


        #region Filter
        private bool _isKodePerkiraanChecked;
        public bool IsKodePerkiraanChecked
        {
            get => _isKodePerkiraanChecked;
            set
            {
                _isKodePerkiraanChecked = value;
                OnPropertyChanged();

                if (!value)
                    FilterKodePerkiraan = null;
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

                if (!value)
                    FilterNamaPerkiraan = null;
            }
        }

        private string _filterKodePerkiraan;
        public string FilterKodePerkiraan
        {
            get => _filterKodePerkiraan;
            set
            {
                _filterKodePerkiraan = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaPerkiraan;
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
    public class SaldoAwalPerkiraanWpf : SaldoAwalPerkiraanDto, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private string _perkiraanWpf;
        public string PerkiraanWpf
        {
            get { return _perkiraanWpf; }
            set
            {
                _perkiraanWpf = value;
                OnPropertyChanged();
            }
        }

        private string _kodePerkiraanWpf;
        public string KodePerkiraanWpf
        {
            get { return _kodePerkiraanWpf; }
            set
            {
                _kodePerkiraanWpf = value;
                OnPropertyChanged();
            }
        }

        private string _namaPerkiraanWpf;
        public string NamaPerkiraanWpf
        {
            get { return _namaPerkiraanWpf; }
            set
            {
                _namaPerkiraanWpf = value;
                OnPropertyChanged();
            }
        }

        public decimal? DebetAwalWpf
        {
            get { return DebetAwal; }
            set
            {
                DebetAwal = value;
                OnPropertyChanged();
            }
        }

        public decimal? KreditAwalWpf
        {
            get { return KreditAwal; }
            set
            {
                KreditAwal = value;
                OnPropertyChanged();
            }
        }

        private decimal? _saldoAwalWpf;
        public decimal? SaldoAwalWpf
        {
            get { return _saldoAwalWpf; }
            set
            {
                _saldoAwalWpf = value;
                OnPropertyChanged();
            }
        }

        public decimal? SaldoAkhirWpf
        {
            get { return SaldoAkhir; }
            set
            {
                SaldoAkhir = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TglSaldoWpf
        {
            get { return TglSaldo; }
            set
            {
                TglSaldo = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
