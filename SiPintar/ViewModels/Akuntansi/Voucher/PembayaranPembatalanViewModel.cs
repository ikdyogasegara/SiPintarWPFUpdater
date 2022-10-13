using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Voucher.PembayaranPembatalan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Voucher
{
    public class PembayaranPembatalanViewModel : ViewModelBase
    {
        public PembayaranPembatalanViewModel(VoucherViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);

            OnOpenPembayaranFormCommand = new OnOpenPembayaranFormCommand(this, restApi, isTest);
            OnOpenPembatalanFormCommand = new OnOpenPembatalanFormCommand(this, restApi, isTest);

            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenPembayaranFormCommand { get; }
        public ICommand OnOpenPembatalanFormCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }

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

        private bool _isTanggalBayarChecked;
        public bool IsTanggalBayarChecked
        {
            get => _isTanggalBayarChecked;
            set
            {
                _isTanggalBayarChecked = value;
                if (!value)
                {
                    FilterTanggalBayarMulai = null;
                    FilterTanggalBayarAkhir = null;
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

        private string _FilterTanggalBayarMulai;
        public string FilterTanggalBayarMulai
        {
            get => _FilterTanggalBayarMulai;
            set
            {
                _FilterTanggalBayarMulai = value;
                OnPropertyChanged();
            }
        }

        private string _FilterTanggalBayarAkhir;
        public string FilterTanggalBayarAkhir
        {
            get => _FilterTanggalBayarAkhir;
            set
            {
                _FilterTanggalBayarAkhir = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
