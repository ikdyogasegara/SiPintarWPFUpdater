using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Billing.Supervisi.PelangganL2T2;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class PelangganL2T2ViewModel : ViewModelBase
    {
        public PelangganL2T2ViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnExportCommand = new OnExportCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this);
            OnOpenPerbaruiDataRekeningCommand = new OnOpenPerbaruiDataRekeningCommand(this, restApi);
            OnOpenRiwayatPembayaranCommand = new OnOpenRiwayatPembayaranCommand(this, restApi);
            OnOpenPiutangCommand = new OnOpenPiutangCommand(this, restApi);

            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this);
            OnSubmitPerbaruiDataRekeningCommand = new OnSubmitPerbaruiDataRekeningCommand(this, restApi);
            OnPerbaruiTarifCommand = new OnPerbaruiTarifCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnOpenPerbaruiDataRekeningCommand { get; }
        public ICommand OnOpenRiwayatPembayaranCommand { get; }
        public ICommand OnOpenPiutangCommand { get; }

        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnSubmitPerbaruiDataRekeningCommand { get; }
        public ICommand OnPerbaruiTarifCommand { get; }


        private bool _isEdit;
        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
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

        private bool _isEmptyDialog;
        public bool IsEmptyDialog
        {
            get { return _isEmptyDialog; }
            set
            {
                _isEmptyDialog = value;
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

        private bool _isFilterVisible = true;
        public bool IsFilterVisible
        {
            get { return _isFilterVisible; }
            set
            {
                _isFilterVisible = value;
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

        private ObservableCollection<MasterPelangganLlttDto> _masterPelangganLlttList;
        public ObservableCollection<MasterPelangganLlttDto> MasterPelangganLlttList
        {
            get { return _masterPelangganLlttList; }
            set
            {
                _masterPelangganLlttList = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLlttDto _selectedData;
        public MasterPelangganLlttDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLlttDto _pelangganForm;
        public MasterPelangganLlttDto PelangganForm
        {
            get { return _pelangganForm; }
            set
            {
                _pelangganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLlttDto _pelangganFilter = new MasterPelangganLlttDto();
        public MasterPelangganLlttDto PelangganFilter
        {
            get { return _pelangganFilter; }
            set
            {
                _pelangganFilter = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningLlttPelunasanDanPembatalanDto> _pembayaranList;
        public ObservableCollection<RekeningLlttPelunasanDanPembatalanDto> PembayaranList
        {
            get { return _pembayaranList; }
            set
            {
                _pembayaranList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningLlttPiutangDto> _piutangList;
        public ObservableCollection<RekeningLlttPiutangDto> PiutangList
        {
            get { return _piutangList; }
            set
            {
                _piutangList = value;
                OnPropertyChanged();
            }
        }

        private decimal? _totalPiutang;
        public decimal? TotalPiutang
        {
            get { return _totalPiutang; }
            set
            {
                _totalPiutang = value;
                OnPropertyChanged();
            }
        }

        #region Pagination

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(10, "10");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
                _ = ((AsyncCommandBase)OnFilterCommand).ExecuteAsync(null);
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var record = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(10, "10"),
                    new KeyValuePair<int, string>(20, "20"),
                    new KeyValuePair<int, string>(50, "50"),
                    new KeyValuePair<int, string>(100, "100"),
                    new KeyValuePair<int, string>(200, "200"),
                    new KeyValuePair<int, string>(300, "300"),
                    new KeyValuePair<int, string>(500, "500"),
                    //new KeyValuePair<int, string>(0, "Semua")
                };

                return record;
            }
        }

        private long _totalRecord;
        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;
        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisible;
        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;
        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;
        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;
        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;
        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Filter Combobox List

        private ObservableCollection<MasterStatusDto> _statusList;
        public ObservableCollection<MasterStatusDto> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterFlagDto> _flagList;
        public ObservableCollection<MasterFlagDto> FlagList
        {
            get { return _flagList; }
            set
            {
                _flagList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTarifLlttDto> _tarifLlttList;
        public ObservableCollection<MasterTarifLlttDto> TarifLlttList
        {
            get { return _tarifLlttList; }
            set
            {
                _tarifLlttList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKolektifDto> _kolektifList;
        public ObservableCollection<MasterKolektifDto> KolektifList
        {
            get { return _kolektifList; }
            set
            {
                _kolektifList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList;
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
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

        private ObservableCollection<MasterRayonDto> _rayonList;
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Filter Checkbox
        private bool _isFlagChecked;
        public bool IsFlagChecked
        {
            get { return _isFlagChecked; }
            set
            {
                _isFlagChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdFlag = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isStatusChecked;
        public bool IsStatusChecked
        {
            get { return _isStatusChecked; }
            set
            {
                _isStatusChecked = value;
                var temp = PelangganFilter;
                if (!value)
                {
                    temp.IdStatus = null;
                }
                PelangganFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isNoSambChecked;
        public bool IsNoSambChecked
        {
            get { return _isNoSambChecked; }
            set
            {
                _isNoSambChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.NoSamb = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNomorLlttChecked;
        public bool IsNomorLlttChecked
        {
            get { return _isNomorLlttChecked; }
            set
            {
                _isNomorLlttChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.NomorLltt = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get { return _isNamaChecked; }
            set
            {
                _isNamaChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.Nama = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKecamatanChecked;
        public bool IsKecamatanChecked
        {
            get { return _isKecamatanChecked; }
            set
            {
                _isKecamatanChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdKecamatan = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTarifLlttChecked;
        public bool IsTarifLlttChecked
        {
            get { return _isTarifLlttChecked; }
            set
            {
                _isTarifLlttChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdTarifLltt = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isAlamatChecked;
        public bool IsAlamatChecked
        {
            get { return _isAlamatChecked; }
            set
            {
                _isAlamatChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.Alamat = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKelurahanChecked;
        public bool IsKelurahanChecked
        {
            get { return _isKelurahanChecked; }
            set
            {
                _isKelurahanChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdKelurahan = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isRayonChecked;
        public bool IsRayonChecked
        {
            get { return _isRayonChecked; }
            set
            {
                _isRayonChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdRayon = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKolektifChecked;
        public bool IsKolektifChecked
        {
            get { return _isKolektifChecked; }
            set
            {
                _isKolektifChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdKolektif = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdWilayah = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        #endregion

        private ObservableCollection<MasterPeriodeDto> _listPeriodePerbaruiData;
        public ObservableCollection<MasterPeriodeDto> ListPeriodePerbaruiData
        {
            get => _listPeriodePerbaruiData;
            set
            {
                _listPeriodePerbaruiData = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _periodePerbaruiData;
        public MasterPeriodeDto PeriodePerbaruiData
        {
            get { return _periodePerbaruiData; }
            set
            {
                _periodePerbaruiData = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYearPembayaran = new List<int>();
        public List<int> ListYearPembayaran
        {
            get { return _listYearPembayaran; }
            set
            {
                _listYearPembayaran = value;
                OnPropertyChanged();
            }
        }

        private int? _yearPembayaran = DateTime.Now.Year;
        public int? YearPembayaran
        {
            get { return _yearPembayaran; }
            set
            {
                _yearPembayaran = value;
                OnPropertyChanged();
                OnOpenRiwayatPembayaranCommand.Execute(null);
            }
        }

    }
}
