using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Penyusutan
{
    public class DataAktivaViewModel : ViewModelBase
    {
        public DataAktivaViewModel(PenyusutanViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnOpenPenyusutanFormCommand = new OnOpenPenyusutanFormCommand(this, restApi, isTest);
            OnOpenPenyusutanConfirmCommand = new OnOpenPenyusutanConfirmCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnOpenPenyusutanFormCommand { get; }
        public ICommand OnOpenPenyusutanConfirmCommand { get; }


        private bool _isAdd = true;
        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                _isAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading { get; set; }
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
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        #region table setting
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
        #endregion

        #region Pagination prop
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
        #endregion

        #region Filter
        private bool _isKodeAktivaChecked;
        public bool IsKodeAktivaChecked
        {
            get => _isKodeAktivaChecked;
            set
            {
                _isKodeAktivaChecked = value;
                if (!value)
                {
                    FilterKodeAktiva = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isGolonganAktivaChecked;
        public bool IsGolonganAktivaChecked
        {
            get => _isGolonganAktivaChecked;
            set
            {
                _isGolonganAktivaChecked = value;
                if (!value)
                {
                    FilterKodeGolonganAktiva = null;
                    FilterNamaGolonganAktiva = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isUraianChecked;
        public bool IsUraianChecked
        {
            get => _isUraianChecked;
            set
            {
                _isUraianChecked = value;
                if (!value)
                {
                    FilterUraian = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNomorBarangChecked;
        public bool IsNomorBarangChecked
        {
            get => _isNomorBarangChecked;
            set
            {
                _isNomorBarangChecked = value;
                if (!value)
                {
                    FilterNomorBarang = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTanggalPerolehChecked;
        public bool IsTanggalPerolehChecked
        {
            get => _isTanggalPerolehChecked;
            set
            {
                _isTanggalPerolehChecked = value;
                if (!value)
                {
                    FilterTanggalMulai = null;
                    FilterTanggalAkhir = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isGantiJadiChecked;
        public bool IsGantiJadiChecked
        {
            get => _isGantiJadiChecked;
            set
            {
                _isGantiJadiChecked = value;
                if (!value)
                {
                    FilterTanggalGantiJadi = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isSisaChecked;
        public bool IsSisaChecked
        {
            get => _isSisaChecked;
            set
            {
                _isSisaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isHabisChecked;
        public bool IsHabisChecked
        {
            get => _isHabisChecked;
            set
            {
                _isHabisChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAtdpChecked;
        public bool IsAtdpChecked
        {
            get => _isAtdpChecked;
            set
            {
                _isAtdpChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAktivaChecked;
        public bool IsAktivaChecked
        {
            get => _isAktivaChecked;
            set
            {
                _isAktivaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAmortisasiChecked;
        public bool IsAmortisasiChecked
        {
            get => _isAmortisasiChecked;
            set
            {
                _isAmortisasiChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterKodeAktiva;
        public string FilterKodeAktiva
        {
            get => _filterKodeAktiva;
            set
            {
                _filterKodeAktiva = value;
                OnPropertyChanged();
            }
        }

        private string _filterKodeGolonganAktiva;
        public string FilterKodeGolonganAktiva
        {
            get => _filterKodeGolonganAktiva;
            set
            {
                _filterKodeGolonganAktiva = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaGolonganAktiva;
        public string FilterNamaGolonganAktiva
        {
            get => _filterNamaGolonganAktiva;
            set
            {
                _filterNamaGolonganAktiva = value;
                OnPropertyChanged();
            }
        }

        private string _filterUraian;
        public string FilterUraian
        {
            get => _filterUraian;
            set
            {
                _filterUraian = value;
                OnPropertyChanged();
            }
        }

        private string _filterNomorBarang;
        public string FilterNomorBarang
        {
            get => _filterNomorBarang;
            set
            {
                _filterNomorBarang = value;
                OnPropertyChanged();
            }
        }

        private string _filterTanggalMulai;
        public string FilterTanggalMulai
        {
            get => _filterTanggalMulai;
            set
            {
                _filterTanggalMulai = value;
                OnPropertyChanged();
            }
        }

        private string _filterTanggalAkhir;
        public string FilterTanggalAkhir
        {
            get => _filterTanggalAkhir;
            set
            {
                _filterTanggalAkhir = value;
                OnPropertyChanged();
            }
        }

        private string _filterTanggalGantiJadi;
        public string FilterTanggalGantiJadi
        {
            get => _filterTanggalGantiJadi;
            set
            {
                _filterTanggalGantiJadi = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
