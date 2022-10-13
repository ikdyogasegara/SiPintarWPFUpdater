using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Billing.Supervisi.PelangganLimbah;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class PelangganLimbahViewModel : ViewModelBase
    {
        public PelangganLimbahViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnOpenDetailPelangganCommand = new OnOpenDetailPelangganCommand(this, restApi);
            OnOpenPerbaruiDataRekeningCommand = new OnOpenPerbaruiDataRekeningCommand(this, restApi);
            OnOpenPiutangCommand = new OnOpenPiutangCommand(this, restApi);
            OnOpenRiwayatPembayaranCommand = new OnOpenRiwayatPembayaranCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this);
            OnSubmitPerbaruiDataRekeningCommand = new OnSubmitPerbaruiDataRekeningCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this);
            OnPerbaruiTarifCommand = new OnPerbaruiTarifCommand(this, restApi);
            OnSearchRiwayatPembayaranCommand = new OnSearchRiwayatPembayaranCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnOpenDetailPelangganCommand { get; }
        public ICommand OnOpenPerbaruiDataRekeningCommand { get; }
        public ICommand OnOpenPiutangCommand { get; }
        public ICommand OnOpenRiwayatPembayaranCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitPerbaruiDataRekeningCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnPerbaruiTarifCommand { get; }
        public ICommand OnSearchRiwayatPembayaranCommand { get; }
        public ICommand OnExportCommand { get; }

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

        private ObservableCollection<MasterPelangganLimbahDto> _masterPelangganList;
        public ObservableCollection<MasterPelangganLimbahDto> MasterPelangganList
        {
            get { return _masterPelangganList; }
            set
            {
                _masterPelangganList = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLimbahDto _selectedData;
        public MasterPelangganLimbahDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLimbahDto _detailData;
        public MasterPelangganLimbahDto DetailData
        {
            get { return _detailData; }
            set
            {
                _detailData = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLimbahDto _pelangganForm;
        public MasterPelangganLimbahDto PelangganForm
        {
            get { return _pelangganForm; }
            set
            {
                _pelangganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganLimbahDto _pelangganFilter = new MasterPelangganLimbahDto { FlagHapus = null };
        public MasterPelangganLimbahDto PelangganFilter
        {
            get { return _pelangganFilter; }
            set
            {
                _pelangganFilter = value;
                OnPropertyChanged();
            }
        }

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
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdStatus = null;
                    PelangganFilter = temp;
                }
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

        private bool _isTarifChecked;
        public bool IsTarifChecked
        {
            get { return _isTarifChecked; }
            set
            {
                _isTarifChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdTarifLimbah = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNoLimbahChecked;
        public bool IsNoLimbahChecked
        {
            get { return _isNoLimbahChecked; }
            set
            {
                _isNoLimbahChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.NomorLimbah = null;
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

        private bool _isCabangChecked;
        public bool IsCabangChecked
        {
            get { return _isCabangChecked; }
            set
            {
                _isCabangChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdCabang = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isAreaChecked;
        public bool IsAreaChecked
        {
            get { return _isAreaChecked; }
            set
            {
                _isAreaChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdArea = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private MasterStatusDto _statusFilter;
        public MasterStatusDto StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdStatus = _statusFilter?.IdStatus;
            }
        }

        private MasterFlagDto _flagFilter;
        public MasterFlagDto FlagFilter
        {
            get { return _flagFilter; }
            set
            {
                _flagFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdFlag = _flagFilter?.IdFlag;
            }
        }

        private MasterTarifLimbahDto _tarifLimbahFilter;
        public MasterTarifLimbahDto TarifLimbahFilter
        {
            get { return _tarifLimbahFilter; }
            set
            {
                _tarifLimbahFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdTarifLimbah = _tarifLimbahFilter?.IdTarifLimbah;
            }
        }

        private MasterKolektifDto _kolektifFilter;
        public MasterKolektifDto KolektifFilter
        {
            get { return _kolektifFilter; }
            set
            {
                _kolektifFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdKolektif = _kolektifFilter?.IdKolektif;
            }
        }

        private MasterWilayahDto _wilayahFilter;
        public MasterWilayahDto WilayahFilter
        {
            get { return _wilayahFilter; }
            set
            {
                _wilayahFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdWilayah = _wilayahFilter?.IdWilayah;
            }
        }

        private MasterKelurahanDto _kelurahanFilter;
        public MasterKelurahanDto KelurahanFilter
        {
            get { return _kelurahanFilter; }
            set
            {
                _kelurahanFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdKelurahan = _kelurahanFilter?.IdKelurahan;
            }
        }

        private MasterKecamatanDto _kecamatanFilter;
        public MasterKecamatanDto KecamatanFilter
        {
            get { return _kecamatanFilter; }
            set
            {
                _kecamatanFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdKecamatan = _kecamatanFilter?.IdKecamatan;
            }
        }

        private MasterRayonDto _rayonFilter;
        public MasterRayonDto RayonFilter
        {
            get { return _rayonFilter; }
            set
            {
                _rayonFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdRayon = _rayonFilter?.IdRayon;
            }
        }

        private MasterAreaDto _areaFilter;
        public MasterAreaDto AreaFilter
        {
            get { return _areaFilter; }
            set
            {
                _areaFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdArea = _areaFilter?.IdArea;
            }
        }

        private MasterCabangDto _cabangFilter;
        public MasterCabangDto CabangFilter
        {
            get { return _cabangFilter; }
            set
            {
                _cabangFilter = value;
                OnPropertyChanged();

                if (PelangganFilter != null)
                    PelangganFilter.IdCabang = _cabangFilter?.IdCabang;
            }
        }


        private ObservableCollection<RekeningLimbahPiutangDto> _piutangList;
        public ObservableCollection<RekeningLimbahPiutangDto> PiutangList
        {
            get { return _piutangList; }
            set
            {
                _piutangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningLimbahPelunasanDanPembatalanDto> _pembayaranList;
        public ObservableCollection<RekeningLimbahPelunasanDanPembatalanDto> PembayaranList
        {
            get { return _pembayaranList; }
            set
            {
                _pembayaranList = value;
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

        public ObservableCollection<KeyValuePair<string, string>> BulanList
        {
            get
            {
                var result = new ObservableCollection<KeyValuePair<string, string>>();

                int index = 1;
                foreach (var item in new CultureInfo("id-ID").DateTimeFormat.MonthNames)
                {
                    string IndexMonth = index.ToString();
                    if (IndexMonth.Length < 2)
                        IndexMonth = "0" + IndexMonth;

                    if (!string.IsNullOrWhiteSpace(item))
                        result.Add(new KeyValuePair<string, string>(IndexMonth, item));
                    index++;
                }

                return result;
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

        public ObservableCollection<KeyValuePair<string, string>> TahunList
        {
            get
            {
                var result = new ObservableCollection<KeyValuePair<string, string>>();

                int yearStart = 2016;
                int yearEnd = DateTime.Now.Year + 1;
                for (int i = yearStart; i <= yearEnd; i++)
                    result.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));

                return result;
            }
        }

        private MasterPeriodeDto _periodePerbaruiForm;
        public MasterPeriodeDto PeriodePerbaruiForm
        {
            get => _periodePerbaruiForm;
            set
            {
                _periodePerbaruiForm = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _tahunPembayaranForm;
        public KeyValuePair<string, string> TahunPembayaranForm
        {
            get => _tahunPembayaranForm;
            set
            {
                _tahunPembayaranForm = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTarifLimbahDto> _tarifLimbahList;
        public ObservableCollection<MasterTarifLimbahDto> TarifLimbahList
        {
            get { return _tarifLimbahList; }
            set
            {
                _tarifLimbahList = value;
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

        private ObservableCollection<MasterCabangDto> _cabangList;
        public ObservableCollection<MasterCabangDto> CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
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

        private ObservableCollection<MasterAreaDto> _areaList;
        public ObservableCollection<MasterAreaDto> AreaList
        {
            get { return _areaList; }
            set
            {
                _areaList = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<MasterPelangganAirDto> _pelangganAirList;
        public ObservableCollection<MasterPelangganAirDto> PelangganAirList
        {
            get { return _pelangganAirList; }
            set
            {
                _pelangganAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeDto> _periodeList;
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get => _periodeList;
            set { _periodeList = value; OnPropertyChanged(); }
        }

        private MasterTarifLimbahDto _selectedTarifLimbahForm;
        public MasterTarifLimbahDto SelectedTarifLimbahForm
        {
            get { return _selectedTarifLimbahForm; }
            set
            {
                _selectedTarifLimbahForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdTarifLimbah = _selectedTarifLimbahForm?.IdTarifLimbah;
            }
        }

        private MasterRayonDto _selectedRayonForm;
        public MasterRayonDto SelectedRayonForm
        {
            get { return _selectedRayonForm; }
            set
            {
                _selectedRayonForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdRayon = _selectedRayonForm?.IdRayon;
            }
        }

        private MasterKelurahanDto _selectedKelurahanForm;
        public MasterKelurahanDto SelectedKelurahanForm
        {
            get { return _selectedKelurahanForm; }
            set
            {
                _selectedKelurahanForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdKelurahan = _selectedKelurahanForm?.IdKelurahan;
            }
        }

        private MasterKolektifDto _selectedKolektifForm;
        public MasterKolektifDto SelectedKolektifForm
        {
            get { return _selectedKolektifForm; }
            set
            {
                _selectedKolektifForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdKolektif = _selectedKolektifForm?.IdKolektif;
            }
        }

        private MasterStatusDto _selectedStatusForm;
        public MasterStatusDto SelectedStatusForm
        {
            get { return _selectedStatusForm; }
            set
            {
                _selectedStatusForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdStatus = _selectedStatusForm?.IdStatus;
            }
        }

        private MasterFlagDto _selectedFlagForm;
        public MasterFlagDto SelectedFlagForm
        {
            get { return _selectedFlagForm; }
            set
            {
                _selectedFlagForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdFlag = _selectedFlagForm?.IdFlag;
            }
        }

        private MasterPelangganAirDto _selectedPelangganAirForm;
        public MasterPelangganAirDto SelectedPelangganAirForm
        {
            get { return _selectedPelangganAirForm; }
            set
            {
                _selectedPelangganAirForm = value;
                OnPropertyChanged();

                if (PelangganForm != null)
                    PelangganForm.IdPelangganAir = _selectedPelangganAirForm?.IdPelangganAir;
            }
        }

        // setting table

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

        // end setting table
    }
}
