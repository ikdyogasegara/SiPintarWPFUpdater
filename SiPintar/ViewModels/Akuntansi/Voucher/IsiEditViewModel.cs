using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Voucher.IsiEdit;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Voucher
{
    public class IsiEditViewModel : ViewModelBase
    {
        public IsiEditViewModel(VoucherViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnOpenKodePerkiraanCommand = new OnOpenKodePerkiraanCommand(this, isTest);
            OnOpenPosTandinganCommand = new OnOpenPosTandinganCommand(this, isTest);
        }


        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnOpenKodePerkiraanCommand { get; }
        public ICommand OnOpenPosTandinganCommand { get; }

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

        private bool _isAdd;
        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                _isAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyDataTransaksi = true;
        public bool IsEmptyDataTransaksi
        {
            get { return _isEmptyDataTransaksi; }
            set
            {
                _isEmptyDataTransaksi = value;
                OnPropertyChanged();
            }
        }

        #region Table Setting
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

        #region Filter
        private bool _isNomorBuktiChecked;
        public bool IsNomorBuktiChecked
        {
            get => _isNomorBuktiChecked;
            set
            {
                _isNomorBuktiChecked = value;
                if (!value)
                {
                    FilterNomorBukti = null;
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

        private bool _isNilaiVoucherChecked;
        public bool IsNilaiVoucherChecked
        {
            get => _isNilaiVoucherChecked;
            set
            {
                _isNilaiVoucherChecked = value;
                if (!value)
                {
                    FilterNilaiVoucher = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isDibayarkanKepadaChecked;
        public bool IsDibayarkanKepadaChecked
        {
            get => _isDibayarkanKepadaChecked;
            set
            {
                _isDibayarkanKepadaChecked = value;
                if (!value)
                {
                    FilterDibayarkanKepada = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTanggalTransChecked;
        public bool IsTanggalTransChecked
        {
            get => _isTanggalTransChecked;
            set
            {
                _isTanggalTransChecked = value;
                if (!value)
                {
                    FilterTanggalTransMulai = null;
                    FilterTanggalTransAkhir = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTanggalKoreksiChecked;
        public bool IsTanggalKoreksiChecked
        {
            get => _isTanggalKoreksiChecked;
            set
            {
                _isTanggalKoreksiChecked = value;
                if (!value)
                {
                    FilterTanggalKoreksiMulai = null;
                    FilterTanggalKoreksiAkhir = null;
                }
                OnPropertyChanged();
            }
        }


        private string _filterNomorBukti;
        public string FilterNomorBukti
        {
            get => _filterNomorBukti;
            set
            {
                _filterNomorBukti = value;
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

        private string _filterNilaiVoucher;
        public string FilterNilaiVoucher
        {
            get => _filterNilaiVoucher;
            set
            {
                _filterNilaiVoucher = value;
                OnPropertyChanged();
            }
        }

        private string _filterDibayarkanKepada;
        public string FilterDibayarkanKepada
        {
            get => _filterDibayarkanKepada;
            set
            {
                _filterDibayarkanKepada = value;
                OnPropertyChanged();
            }
        }

        private string _FilterTanggalTransMulai;
        public string FilterTanggalTransMulai
        {
            get => _FilterTanggalTransMulai;
            set
            {
                _FilterTanggalTransMulai = value;
                OnPropertyChanged();
            }
        }

        private string _FilterTanggalTransAkhir;
        public string FilterTanggalTransAkhir
        {
            get => _FilterTanggalTransAkhir;
            set
            {
                _FilterTanggalTransAkhir = value;
                OnPropertyChanged();
            }
        }

        private string _FilterTanggalKoreksiMulai;
        public string FilterTanggalKoreksiMulai
        {
            get => _FilterTanggalKoreksiMulai;
            set
            {
                _FilterTanggalKoreksiMulai = value;
                OnPropertyChanged();
            }
        }

        private string _FilterTanggalKoreksiAkhir;
        public string FilterTanggalKoreksiAkhir
        {
            get => _FilterTanggalKoreksiAkhir;
            set
            {
                _FilterTanggalKoreksiAkhir = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
