using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Hublang.Pelayanan.Langganan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class LanggananViewModel : ViewModelBase
    {
        private readonly bool _isTest;

        public LanggananViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            OnLoadCommand = new OnLoadCommand(this, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, _isTest);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this, _isTest);
            OnOpenTableSettingCommand = new OnOpenTableSettingCommand(this, _isTest);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi, _isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, _isTest);
            OnSubmitTableSettingCommand = new OnSubmitTableSettingCommand(this, _isTest);
            OnOpenImageCommand = new OnOpenImageCommand(this, _isTest);
            OnBrowseFileBuktiCommand = new OnBrowseFileBuktiCommand(this, _isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnOpenTableSettingCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitTableSettingCommand { get; }
        public ICommand OnOpenImageCommand { get; }
        public ICommand OnBrowseFileBuktiCommand { get; }

        private string _selectedJenisPelanggan;
        public string SelectedJenisPelanggan
        {
            get { return _selectedJenisPelanggan; }
            set
            {
                _selectedJenisPelanggan = value;
                OnPropertyChanged();

                if (_selectedJenisPelanggan == "Pelanggan Air")
                {
                    IsTabKoreksi = false;
                    IsPelangganAir = true;
                    IsPelangganLimbah = false;
                    IsPelangganLltt = false;
                }
                else if (_selectedJenisPelanggan == "Pelanggan Limbah")
                {
                    IsTabKoreksi = false;
                    IsPelangganAir = false;
                    IsPelangganLimbah = true;
                    IsPelangganLltt = false;
                }
                else if (_selectedJenisPelanggan == "Pelanggan L2T2")
                {
                    IsTabKoreksi = false;
                    IsPelangganAir = false;
                    IsPelangganLimbah = false;
                    IsPelangganLltt = true;
                }
            }
        }

        private bool _isTabKoreksi;
        public bool IsTabKoreksi
        {
            get { return _isTabKoreksi; }
            set
            {
                _isTabKoreksi = value;
                OnPropertyChanged();

                if (_selectedJenisPelanggan == "Pelanggan Air")
                {
                    EndPoint = IsTabKoreksi ? "master-pelanggan-air-data-koreksi" : "master-pelanggan-air";
                }
                else if (_selectedJenisPelanggan == "Pelanggan Limbah")
                {
                    EndPoint = IsTabKoreksi ? "master-pelanggan-limbah-data-koreksi" : "master-pelanggan-limbah";
                }
                else if (_selectedJenisPelanggan == "Pelanggan L2T2")
                {
                    EndPoint = IsTabKoreksi ? "master-pelanggan-limbah-data-koreksi" : "master-pelanggan-lltt";
                }
            }
        }

        private bool _isPelangganAir;
        public bool IsPelangganAir
        {
            get { return _isPelangganAir; }
            set
            {
                _isPelangganAir = value;
                OnPropertyChanged();
            }
        }

        private bool _isPelangganLimbah;
        public bool IsPelangganLimbah
        {
            get { return _isPelangganLimbah; }
            set
            {
                _isPelangganLimbah = value;
                OnPropertyChanged();
            }
        }

        private bool _isPelangganLltt;
        public bool IsPelangganLltt
        {
            get { return _isPelangganLltt; }
            set
            {
                _isPelangganLltt = value;
                OnPropertyChanged();
            }
        }

        private string _endPoint;
        public string EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganGlobalWpf> _dataList;
        public ObservableCollection<MasterPelangganGlobalWpf> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganGlobalKoreksiWpf> _dataKoreksiPelangganList;
        public ObservableCollection<MasterPelangganGlobalKoreksiWpf> DataKoreksiPelangganList
        {
            get { return _dataKoreksiPelangganList; }
            set
            {
                _dataKoreksiPelangganList = value;
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

        private bool _isLoadingMap;
        public bool IsLoadingMap
        {
            get => _isLoadingMap;
            set { _isLoadingMap = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmptyDialog;
        public bool IsEmptyDialog
        {
            get => _isEmptyDialog;
            set { _isEmptyDialog = value; OnPropertyChanged(); }
        }

        private MasterPelangganGlobalWpf _selectedData;
        public MasterPelangganGlobalWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
            }
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set { _isDetail = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private bool _isDataSelected;
        public bool IsDataSelected
        {
            get { return _isDataSelected; }
            set
            {
                _isDataSelected = value;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(20, "20");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
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
                var listOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in listOption)
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

        private bool _isStep1FormValid;
        public bool IsStep1FormValid
        {
            get => _isStep1FormValid;
            set { _isStep1FormValid = value; OnPropertyChanged(); }
        }

        private bool _isStep2FormValid;
        public bool IsStep2FormValid
        {
            get => _isStep2FormValid;
            set { _isStep2FormValid = value; OnPropertyChanged(); }
        }

        private bool _isStep3FormValid;
        public bool IsStep3FormValid
        {
            get => _isStep3FormValid;
            set { _isStep3FormValid = value; OnPropertyChanged(); }
        }

        private MasterPelangganGlobalForm _formData;
        public MasterPelangganGlobalForm FormData
        {
            get { return _formData; }
            set { _formData = value; OnPropertyChanged(); }
        }

        private MasterGolonganDto _golonganForm;
        public MasterGolonganDto GolonganForm
        {
            get { return _golonganForm; }
            set
            {
                _golonganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _diameterForm;
        public MasterDiameterDto DiameterForm
        {
            get { return _diameterForm; }
            set
            {
                _diameterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLimbahDto _tarifLimbahForm;
        public MasterTarifLimbahDto TarifLimbahForm
        {
            get { return _tarifLimbahForm; }
            set
            {
                _tarifLimbahForm = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLlttDto _tarifLlttForm;
        public MasterTarifLlttDto TarifLlttForm
        {
            get { return _tarifLlttForm; }
            set
            {
                _tarifLlttForm = value;
                OnPropertyChanged();
            }
        }

        private MasterKolektifDto _kolektifForm;
        public MasterKolektifDto KolektifForm
        {
            get { return _kolektifForm; }
            set
            {
                _kolektifForm = value;
                OnPropertyChanged();
            }
        }

        private MasterFlagDto _flagForm;
        public MasterFlagDto FlagForm
        {
            get { return _flagForm; }
            set
            {
                _flagForm = value;
                OnPropertyChanged();
            }
        }

        private MasterStatusDto _statusForm;
        public MasterStatusDto StatusForm
        {
            get { return _statusForm; }
            set
            {
                _statusForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPekerjaanDto _pekerjaanForm;
        public MasterPekerjaanDto PekerjaanForm
        {
            get { return _pekerjaanForm; }
            set
            {
                _pekerjaanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _kelurahanForm;
        public MasterKelurahanDto KelurahanForm
        {
            get => _kelurahanForm;
            set
            {
                _kelurahanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _rayonForm;
        public MasterRayonDto RayonForm
        {
            get => _rayonForm;
            set
            {
                _rayonForm = value;
                OnPropertyChanged();
            }
        }

        private MasterBlokDto _blokForm;
        public MasterBlokDto BlokForm
        {
            get => _blokForm;
            set
            {
                _blokForm = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBangunanDto _jenisBangunanForm;
        public MasterJenisBangunanDto JenisBangunanForm
        {
            get => _jenisBangunanForm;
            set
            {
                _jenisBangunanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterKepemilikanDto _kepemilikanBangunanForm;
        public MasterKepemilikanDto KepemilikanBangunanForm
        {
            get => _kepemilikanBangunanForm;
            set
            {
                _kepemilikanBangunanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPeruntukanDto _peruntukanForm;
        public MasterPeruntukanDto PeruntukanForm
        {
            get => _peruntukanForm;
            set
            {
                _peruntukanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterSumberAirDto _sumberAirForm;
        public MasterSumberAirDto SumberAirForm
        {
            get => _sumberAirForm;
            set
            {
                _sumberAirForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDmaDto _dmaForm;
        public MasterDmaDto DmaForm
        {
            get => _dmaForm;
            set
            {
                _dmaForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDmzDto _dmzForm;
        public MasterDmzDto DmzForm
        {
            get => _dmzForm;
            set
            {
                _dmzForm = value;
                OnPropertyChanged();
            }
        }

        private MasterMerekMeterDto _merekMeterForm;
        public MasterMerekMeterDto MerekMeterForm
        {
            get => _merekMeterForm;
            set
            {
                _merekMeterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterKondisiMeterDto _kondisiMeterForm;
        public MasterKondisiMeterDto KondisiMeterForm
        {
            get => _kondisiMeterForm;
            set
            {
                _kondisiMeterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterAdministrasiLainDto _administrasiLainForm;
        public MasterAdministrasiLainDto AdministrasiLainForm
        {
            get => _administrasiLainForm;
            set
            {
                _administrasiLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPemeliharaanLainDto _pemeliharaanLainForm;
        public MasterPemeliharaanLainDto PemeliharaanLainForm
        {
            get => _pemeliharaanLainForm;
            set
            {
                _pemeliharaanLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterRetribusiLainDto _retribusiLainForm;
        public MasterRetribusiLainDto RetribusiLainForm
        {
            get => _retribusiLainForm;
            set
            {
                _retribusiLainForm = value;
                OnPropertyChanged();
            }
        }

        #region foto map
        private bool _isAlamatMapFormChecked;
        public bool IsAlamatMapFormChecked
        {
            get => _isAlamatMapFormChecked;
            set
            {
                _isAlamatMapFormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoRumah1Uri;
        public Uri FotoRumah1Uri
        {
            get => _fotoRumah1Uri;
            set
            {
                _fotoRumah1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoRumah1;
        public bool IsNewFotoRumah1
        {
            get => _isNewFotoRumah1;
            set
            {
                _isNewFotoRumah1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoRumah1FormChecked;
        public bool IsFotoRumah1FormChecked
        {
            get => _isFotoRumah1FormChecked;
            set
            {
                _isFotoRumah1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoRumah2Uri;
        public Uri FotoRumah2Uri
        {
            get => _fotoRumah2Uri;
            set
            {
                _fotoRumah2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoRumah2;
        public bool IsNewFotoRumah2
        {
            get => _isNewFotoRumah2;
            set
            {
                _isNewFotoRumah2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoRumah2FormChecked;
        public bool IsFotoRumah2FormChecked
        {
            get => _isFotoRumah2FormChecked;
            set
            {
                _isFotoRumah2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoRumah3Uri;
        public Uri FotoRumah3Uri
        {
            get => _fotoRumah3Uri;
            set
            {
                _fotoRumah3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoRumah3;
        public bool IsNewFotoRumah3
        {
            get => _isNewFotoRumah3;
            set
            {
                _isNewFotoRumah3 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoRumah3FormChecked;
        public bool IsFotoRumah3FormChecked
        {
            get => _isFotoRumah3FormChecked;
            set
            {
                _isFotoRumah3FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isDenahLokasiFormChecked;
        public bool IsDenahLokasiFormChecked
        {
            get => _isDenahLokasiFormChecked;
            set
            {
                _isDenahLokasiFormChecked = value;
                OnPropertyChanged();
            }
        }

        private string _denahLokasiForm;
        public string DenahLokasiForm
        {
            get => _denahLokasiForm;
            set
            {
                _denahLokasiForm = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _previewFile;
        public BitmapImage PreviewFile
        {
            get => _previewFile;
            set
            {
                _previewFile = value;
                OnPropertyChanged();
            }
        }
        #endregion

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

        #region filter list
        private ObservableCollection<MasterJenisBangunanDto> _jenisBangunanList = new ObservableCollection<MasterJenisBangunanDto>();
        public ObservableCollection<MasterJenisBangunanDto> JenisBangunanList
        {
            get { return _jenisBangunanList; }
            set
            {
                _jenisBangunanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKepemilikanDto> _kepemilikanBangunanList = new ObservableCollection<MasterKepemilikanDto>();
        public ObservableCollection<MasterKepemilikanDto> KepemilikanBangunanList
        {
            get { return _kepemilikanBangunanList; }
            set
            {
                _kepemilikanBangunanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeruntukanDto> _peruntukanList = new ObservableCollection<MasterPeruntukanDto>();
        public ObservableCollection<MasterPeruntukanDto> PeruntukanList
        {
            get { return _peruntukanList; }
            set
            {
                _peruntukanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterStatusDto> _statusList = new ObservableCollection<MasterStatusDto>();
        public ObservableCollection<MasterStatusDto> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterFlagDto> _flagList = new ObservableCollection<MasterFlagDto>();
        public ObservableCollection<MasterFlagDto> FlagList
        {
            get { return _flagList; }
            set
            {
                _flagList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterGolonganDto> _golonganList = new ObservableCollection<MasterGolonganDto>();
        public ObservableCollection<MasterGolonganDto> GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTarifLimbahDto> _tarifLimbahList = new ObservableCollection<MasterTarifLimbahDto>();
        public ObservableCollection<MasterTarifLimbahDto> TarifLimbahList
        {
            get { return _tarifLimbahList; }
            set
            {
                _tarifLimbahList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTarifLlttDto> _tarifLlttList = new ObservableCollection<MasterTarifLlttDto>();
        public ObservableCollection<MasterTarifLlttDto> TarifLlttList
        {
            get { return _tarifLlttList; }
            set
            {
                _tarifLlttList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDiameterDto> _diameterList = new ObservableCollection<MasterDiameterDto>();
        public ObservableCollection<MasterDiameterDto> DiameterList
        {
            get { return _diameterList; }
            set
            {
                _diameterList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterSumberAirDto> _sumberAirList = new ObservableCollection<MasterSumberAirDto>();
        public ObservableCollection<MasterSumberAirDto> SumberAirList
        {
            get { return _sumberAirList; }
            set
            {
                _sumberAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRayonDto> _rayonList = new ObservableCollection<MasterRayonDto>();
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAreaDto> _areaList = new ObservableCollection<MasterAreaDto>();
        public ObservableCollection<MasterAreaDto> AreaList
        {
            get { return _areaList; }
            set
            {
                _areaList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList = new ObservableCollection<MasterWilayahDto>();
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKelurahanDto> _kelurahanList = new ObservableCollection<MasterKelurahanDto>();
        public ObservableCollection<MasterKelurahanDto> KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKecamatanDto> _kecamatanList = new ObservableCollection<MasterKecamatanDto>();
        public ObservableCollection<MasterKecamatanDto> KecamatanList
        {
            get { return _kecamatanList; }
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterCabangDto> _cabangList = new ObservableCollection<MasterCabangDto>();
        public ObservableCollection<MasterCabangDto> CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKolektifDto> _kolektifList = new ObservableCollection<MasterKolektifDto>();
        public ObservableCollection<MasterKolektifDto> KolektifList
        {
            get { return _kolektifList; }
            set
            {
                _kolektifList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPekerjaanDto> _pekerjaanList = new ObservableCollection<MasterPekerjaanDto>();
        public ObservableCollection<MasterPekerjaanDto> PekerjaanList
        {
            get { return _pekerjaanList; }
            set
            {
                _pekerjaanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBlokDto> _blokList = new ObservableCollection<MasterBlokDto>();
        public ObservableCollection<MasterBlokDto> BlokList
        {
            get { return _blokList; }
            set
            {
                _blokList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserDto> _userList = new ObservableCollection<MasterUserDto>();
        public ObservableCollection<MasterUserDto> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDmaDto> _dmaList = new ObservableCollection<MasterDmaDto>();
        public ObservableCollection<MasterDmaDto> DmaList
        {
            get { return _dmaList; }
            set
            {
                _dmaList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDmzDto> _dmzList = new ObservableCollection<MasterDmzDto>();
        public ObservableCollection<MasterDmzDto> DmzList
        {
            get { return _dmzList; }
            set
            {
                _dmzList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAdministrasiLainDto> _administrasiLainList = new ObservableCollection<MasterAdministrasiLainDto>();
        public ObservableCollection<MasterAdministrasiLainDto> AdministrasiLainList
        {
            get { return _administrasiLainList; }
            set
            {
                _administrasiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPemeliharaanLainDto> _pemeliharaanLainList = new ObservableCollection<MasterPemeliharaanLainDto>();
        public ObservableCollection<MasterPemeliharaanLainDto> PemeliharaanLainList
        {
            get { return _pemeliharaanLainList; }
            set
            {
                _pemeliharaanLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRetribusiLainDto> _retribusiLainList = new ObservableCollection<MasterRetribusiLainDto>();
        public ObservableCollection<MasterRetribusiLainDto> RetribusiLainList
        {
            get { return _retribusiLainList; }
            set
            {
                _retribusiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterMerekMeterDto> _merekMeterList = new ObservableCollection<MasterMerekMeterDto>();
        public ObservableCollection<MasterMerekMeterDto> MerekMeterList
        {
            get { return _merekMeterList; }
            set
            {
                _merekMeterList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKondisiMeterDto> _kondisiMeterList = new ObservableCollection<MasterKondisiMeterDto>();
        public ObservableCollection<MasterKondisiMeterDto> KondisiMeterList
        {
            get { return _kondisiMeterList; }
            set
            {
                _kondisiMeterList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region enable/disable filter

        private bool _isStatusChecked;
        public bool IsStatusChecked
        {
            get { return _isStatusChecked; }
            set
            {
                _isStatusChecked = value;
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
                OnPropertyChanged();
            }
        }

        private bool _isNomorSambunganChecked;
        public bool IsNomorSambunganChecked
        {
            get { return _isNomorSambunganChecked; }
            set
            {
                _isNomorSambunganChecked = value;
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
                OnPropertyChanged();
            }
        }

        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTarifLimbahChecked;
        public bool IsTarifLimbahChecked
        {
            get { return _isTarifLimbahChecked; }
            set
            {
                _isTarifLimbahChecked = value;
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
                OnPropertyChanged();
            }
        }

        private bool _isDiameterChecked;
        public bool IsDiameterChecked
        {
            get { return _isDiameterChecked; }
            set
            {
                _isDiameterChecked = value;
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
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter variable

        private MasterRayonDto _filterRayon;
        public MasterRayonDto FilterRayon
        {
            get { return _filterRayon; }
            set
            {
                _filterRayon = value;
                OnPropertyChanged();
            }
        }

        private MasterAreaDto _filterArea;
        public MasterAreaDto FilterArea
        {
            get { return _filterArea; }
            set
            {
                _filterArea = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _filterWilayah;
        public MasterWilayahDto FilterWilayah
        {
            get { return _filterWilayah; }
            set
            {
                _filterWilayah = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _filterKelurahan;
        public MasterKelurahanDto FilterKelurahan
        {
            get { return _filterKelurahan; }
            set
            {
                _filterKelurahan = value;
                OnPropertyChanged();
            }
        }

        private MasterKecamatanDto _filterKecamatan;
        public MasterKecamatanDto FilterKecamatan
        {
            get { return _filterKecamatan; }
            set
            {
                _filterKecamatan = value;
                OnPropertyChanged();
            }
        }

        private MasterCabangDto _filterCabang;
        public MasterCabangDto FilterCabang
        {
            get { return _filterCabang; }
            set
            {
                _filterCabang = value;
                OnPropertyChanged();
            }
        }

        private MasterStatusDto _filterStatus;
        public MasterStatusDto FilterStatus
        {
            get { return _filterStatus; }
            set
            {
                _filterStatus = value;
                OnPropertyChanged();
            }
        }

        private MasterFlagDto _filterFlag;
        public MasterFlagDto FilterFlag
        {
            get { return _filterFlag; }
            set
            {
                _filterFlag = value;
                OnPropertyChanged();
            }
        }


        private string _filterNama;
        public string FilterNama
        {
            get { return _filterNama; }
            set
            {
                _filterNama = value;
                OnPropertyChanged();
            }
        }

        private string _filterNomorSambungan;
        public string FilterNomorSambungan
        {
            get { return _filterNomorSambungan; }
            set
            {
                _filterNomorSambungan = value;
                OnPropertyChanged();
            }
        }

        private string _filterAlamat;
        public string FilterAlamat
        {
            get { return _filterAlamat; }
            set
            {
                _filterAlamat = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _filterGolongan;
        public MasterGolonganDto FilterGolongan
        {
            get { return _filterGolongan; }
            set
            {
                _filterGolongan = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLimbahDto _filterTarifLimbah;
        public MasterTarifLimbahDto FilterTarifLimbah
        {
            get { return _filterTarifLimbah; }
            set
            {
                _filterTarifLimbah = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLlttDto _filterTarifLltt;
        public MasterTarifLlttDto FilterTarifLltt
        {
            get { return _filterTarifLltt; }
            set
            {
                _filterTarifLltt = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _filterDiameter;
        public MasterDiameterDto FilterDiameter
        {
            get { return _filterDiameter; }
            set
            {
                _filterDiameter = value;
                OnPropertyChanged();
            }
        }

        private MasterKolektifDto _filterKolektif;
        public MasterKolektifDto FilterKolektif
        {
            get { return _filterKolektif; }
            set
            {
                _filterKolektif = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }

    public class MasterPelangganGlobalForm : MasterPelangganGlobalDto
    {
    }
}
