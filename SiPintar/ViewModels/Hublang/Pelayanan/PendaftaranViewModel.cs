using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Hublang.Pelayanan.Pendaftaran;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class PendaftaranViewModel : ViewModelBase
    {
        private readonly bool _isTest;

        public PendaftaranViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            OnLoadCommand = new OnLoadCommand(this, restApi, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, _isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, _isTest);
            OnOpenKolektifCommand = new OnOpenKolektifCommand(this, _isTest);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this, _isTest);
            OnOpenTableSettingCommand = new OnOpenTableSettingCommand(this, _isTest);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, restApi, _isTest);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi, _isTest);
            OnSubmitKolektifCommand = new OnSubmitKolektifCommand(this, restApi, _isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, _isTest);
            OnSubmitTableSettingCommand = new OnSubmitTableSettingCommand(this, _isTest);
            OnBrowseCsvCommand = new OnBrowseCsvCommand(this, _isTest);
            OnBrowseFileBuktiCommand = new OnBrowseFileBuktiCommand(this, _isTest);
            OnOpenImageCommand = new OnOpenImageCommand(this, _isTest);
            OnDownloadCsvSampleCommand = new OnDownloadCsvSampleCommand(_isTest);
            CalculateBiayaCommand = new CalculateBiayaCommand(this, _isTest);
            OnCetakCommandNonPelanggan = new OnCetakCommand(restApi, "hublang", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenKolektifCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnOpenTableSettingCommand { get; }
        public ICommand OnSubmitAddFormCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }
        public ICommand OnSubmitKolektifCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitTableSettingCommand { get; }
        public ICommand OnBrowseCsvCommand { get; }
        public ICommand OnBrowseFileBuktiCommand { get; }
        public ICommand OnOpenImageCommand { get; }
        public ICommand OnDownloadCsvSampleCommand { get; }
        public ICommand CalculateBiayaCommand { get; }
        public ICommand OnCetakCommandNonPelanggan { get; }

        private string _labelJudul;
        public string LabelJudul
        {
            get { return _labelJudul; }
            set
            {
                _labelJudul = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePermohonanDto _selectedTipePermohonanComboBox;
        public MasterTipePermohonanDto SelectedTipePermohonanComboBox
        {
            get { return _selectedTipePermohonanComboBox; }
            set
            {
                _selectedTipePermohonanComboBox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PermohonanNonPelangganWpf> _dataList;
        public ObservableCollection<PermohonanNonPelangganWpf> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private bool _isExists;
        public bool IsExists
        {
            get => _isExists;
            set
            {
                _isExists = value; OnPropertyChanged();
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

        private PermohonanNonPelangganWpf _selectedData;
        public PermohonanNonPelangganWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();


                IsDataSelected = _selectedData != null;
            }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set { _isDetail = value; OnPropertyChanged(); }
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

        private PermohonanNonPelangganForm _formData;
        public PermohonanNonPelangganForm FormData
        {
            get { return _formData; }
            set { _formData = value; OnPropertyChanged(); }
        }

        private MasterPekerjaanDto _pekerjaanForm;
        public MasterPekerjaanDto PekerjaanForm
        {
            get { return _pekerjaanForm; }
            set
            {
                _pekerjaanForm = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.IdPekerjaan = _pekerjaanForm?.IdPekerjaan;
            }
        }

        private MasterTipePendaftaranSambunganDto _tipePendaftaranForm;
        public MasterTipePendaftaranSambunganDto TipePendaftaranForm
        {
            get => _tipePendaftaranForm;
            set
            {
                _tipePendaftaranForm = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.IdTipePendaftaranSambungan = _tipePendaftaranForm?.IdTipePendaftaranSambungan;
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

                if (FormData != null)
                    FormData.IdKelurahan = _kelurahanForm?.IdKelurahan;
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

                if (FormData != null)
                    FormData.IdRayon = _rayonForm?.IdRayon;
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

                if (FormData != null)
                    FormData.IdBlok = _blokForm?.IdBlok;
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

                if (FormData != null)
                    FormData.IdJenisBangunan = _jenisBangunanForm?.IdJenisBangunan;
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

                if (FormData != null)
                    FormData.IdKepemilikan = _kepemilikanBangunanForm?.IdKepemilikan;
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

                if (FormData != null)
                    FormData.IdPeruntukan = _peruntukanForm?.IdPeruntukan;
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

                if (FormData != null)
                    FormData.IdSumberAir = _sumberAirForm?.IdSumberAir;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> JenisPelangganList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Pelanggan Air"),
                    new KeyValuePair<int, string>(2, "Pelanggan Limbah"),
                    new KeyValuePair<int, string>(3, "Pelanggan L2T2")
                };
            }
        }

        private KeyValuePair<int, string>? _jenisPelangganForm;
        public KeyValuePair<int, string>? JenisPelangganForm
        {
            get => _jenisPelangganForm;
            set
            {
                _jenisPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _filenameCsvForm;
        public string FilenameCsvForm
        {
            get => _filenameCsvForm;
            set
            {
                _filenameCsvForm = value;
                OnPropertyChanged();
            }
        }

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

        private Uri _fotoKtpUri;
        public Uri FotoKtpUri
        {
            get => _fotoKtpUri;
            set
            {
                _fotoKtpUri = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.FotoKtp = _fotoKtpUri?.OriginalString;
            }
        }

        private bool _isNewFotoKtp;
        public bool IsNewFotoKtp
        {
            get => _isNewFotoKtp;
            set
            {
                _isNewFotoKtp = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoImbUri;
        public Uri FotoImbUri
        {
            get => _fotoImbUri;
            set
            {
                _fotoImbUri = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.FotoImb = _fotoImbUri?.OriginalString;
            }
        }

        private bool _isNewFotoImb;
        public bool IsNewFotoImb
        {
            get => _isNewFotoImb;
            set
            {
                _isNewFotoImb = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoKkUri;
        public Uri FotoKkUri
        {
            get => _fotoKkUri;
            set
            {
                _fotoKkUri = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.FotoKk = _fotoKkUri?.OriginalString;
            }
        }

        private bool _isNewFotoKk;
        public bool IsNewFotoKk
        {
            get => _isNewFotoKk;
            set
            {
                _isNewFotoKk = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoSuratPernyataanUri;
        public Uri FotoSuratPernyataanUri
        {
            get => _fotoSuratPernyataanUri;
            set
            {
                _fotoSuratPernyataanUri = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.FotoSuratPernyataan = _fotoSuratPernyataanUri?.OriginalString;
            }
        }

        private bool _isNewFotoSuratPernyataan;
        public bool IsNewFotoSuratPernyataan
        {
            get => _isNewFotoSuratPernyataan;
            set
            {
                _isNewFotoSuratPernyataan = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoSuratPernyataanFormChecked;
        public bool IsFotoSuratPernyataanFormChecked
        {
            get => _isFotoSuratPernyataanFormChecked;
            set
            {
                _isFotoSuratPernyataanFormChecked = value;
                OnPropertyChanged();

                if (!_isFotoSuratPernyataanFormChecked)
                {
                    FotoSuratPernyataanUri = null;
                    if (FormData != null)
                        FormData.FotoSuratPernyataan = null;
                }
            }
        }

        private bool _isFotoKkFormChecked;
        public bool IsFotoKkFormChecked
        {
            get => _isFotoKkFormChecked;
            set
            {
                _isFotoKkFormChecked = value;
                OnPropertyChanged();

                if (!_isFotoKkFormChecked)
                {
                    FotoKkUri = null;
                    if (FormData != null)
                        FormData.FotoKk = null;
                }
            }
        }

        private bool _isFotoKtpFormChecked;
        public bool IsFotoKtpFormChecked
        {
            get => _isFotoKtpFormChecked;
            set
            {
                _isFotoKtpFormChecked = value;
                OnPropertyChanged();

                if (!_isFotoKtpFormChecked)
                {
                    FotoKtpUri = null;
                    if (FormData != null)
                        FormData.FotoKtp = null;
                }
            }
        }

        private bool _isFotoImbFormChecked;
        public bool IsFotoImbFormChecked
        {
            get => _isFotoImbFormChecked;
            set
            {
                _isFotoImbFormChecked = value;
                OnPropertyChanged();

                if (!_isFotoImbFormChecked)
                {
                    FotoImbUri = null;
                    if (FormData != null)
                        FormData.FotoImb = null;
                }
            }
        }

        private bool _isMeteraiFormChecked;
        public bool IsMeteraiFormChecked
        {
            get => _isMeteraiFormChecked;
            set
            {
                _isMeteraiFormChecked = value;
                OnPropertyChanged();

                if (!_isMeteraiFormChecked && FormData != null)
                    FormData.FotoBukti2 = null;

                if (FormData != null && FormData.Parameter != null)
                {
                    var temp = new List<PermohonanNonPelangganDetailDto>();
                    foreach (var item in FormData.Parameter)
                    {
                        if (item.Parameter.ToLower().Contains("materai"))
                            item.Value = value;
                        temp.Add(item);
                    }
                    FormData.Parameter = temp;
                }
            }
        }

        private bool _isMapPlastikFormChecked;
        public bool IsMapPlastikFormChecked
        {
            get => _isMapPlastikFormChecked;
            set
            {
                _isMapPlastikFormChecked = value;
                OnPropertyChanged();

                if (!_isMapPlastikFormChecked && FormData != null)
                    FormData.FotoBukti3 = null;

                if (FormData != null && FormData.Parameter != null)
                {
                    var temp = new List<PermohonanNonPelangganDetailDto>();
                    foreach (var item in FormData.Parameter)
                    {
                        if (item.Parameter.ToLower().Contains("map"))
                            item.Value = value;
                        temp.Add(item);
                    }
                    FormData.Parameter = temp;
                }
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

                if (!_isDenahLokasiFormChecked)
                {
                    DenahLokasiForm = null;

                    if (FormData != null)
                    {
                        FormData.Latitude = null;
                        FormData.Longitude = null;
                    }
                }

                if (FormData != null && FormData.Parameter != null)
                {
                    var temp = new List<PermohonanNonPelangganDetailDto>();
                    foreach (var item in FormData.Parameter)
                    {
                        if (item.Parameter.ToLower().Contains("denah lokasi"))
                            item.Value = value;
                        temp.Add(item);
                    }
                    FormData.Parameter = temp;
                }
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

        private MasterJenisNonAirDto _jenisNonAir;
        public MasterJenisNonAirDto JenisNonAir
        {
            get => _jenisNonAir;
            set
            {
                _jenisNonAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _biayaPendaftaranForm = 0;
        public decimal BiayaPendaftaranForm
        {
            get => _biayaPendaftaranForm;
            set
            {
                _biayaPendaftaranForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _biayaOpnameForm = 0;
        public decimal BiayaOpnameForm
        {
            get => _biayaOpnameForm;
            set
            {
                _biayaOpnameForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _biayaFormulirForm = 0;
        public decimal BiayaFormulirForm
        {
            get => _biayaFormulirForm;
            set
            {
                _biayaFormulirForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _biayaJaminanForm = 0;
        public decimal BiayaJaminanForm
        {
            get => _biayaJaminanForm;
            set
            {
                _biayaJaminanForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _ppnBiayaPendaftaranForm = 0;
        public decimal PpnBiayaPendaftaranForm
        {
            get => _ppnBiayaPendaftaranForm;
            set
            {
                _ppnBiayaPendaftaranForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _ppnBiayaOpnameForm = 0;
        public decimal PpnBiayaOpnameForm
        {
            get => _ppnBiayaOpnameForm;
            set
            {
                _ppnBiayaOpnameForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _ppnBiayaFormulirForm = 0;
        public decimal PpnBiayaFormulirForm
        {
            get => _ppnBiayaFormulirForm;
            set
            {
                _ppnBiayaFormulirForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _ppnBiayaJaminanForm = 0;
        public decimal PpnBiayaJaminanForm
        {
            get => _ppnBiayaJaminanForm;
            set
            {
                _ppnBiayaJaminanForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalBiayaPendaftaranForm = 0;
        public decimal TotalBiayaPendaftaranForm
        {
            get => _totalBiayaPendaftaranForm;
            set
            {
                _totalBiayaPendaftaranForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalBiayaOpnameForm = 0;
        public decimal TotalBiayaOpnameForm
        {
            get => _totalBiayaOpnameForm;
            set
            {
                _totalBiayaOpnameForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalBiayaFormulirForm = 0;
        public decimal TotalBiayaFormulirForm
        {
            get => _totalBiayaFormulirForm;
            set
            {
                _totalBiayaFormulirForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalBiayaJaminanForm = 0;
        public decimal TotalBiayaJaminanForm
        {
            get => _totalBiayaJaminanForm;
            set
            {
                _totalBiayaJaminanForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalBiayaSambunganBaruForm = 0;
        public decimal TotalBiayaSambunganBaruForm
        {
            get => _totalBiayaSambunganBaruForm;
            set
            {
                _totalBiayaSambunganBaruForm = value;
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

        private ParamPermohonanNonPelangganFilterDto _filterData = new ParamPermohonanNonPelangganFilterDto();
        public ParamPermohonanNonPelangganFilterDto FilterData
        {
            get => _filterData;
            set { _filterData = value; OnPropertyChanged(); }
        }

        #region filter list
        private ObservableCollection<MasterMapPelangganDto> _mapPelangganList = new ObservableCollection<MasterMapPelangganDto>();
        public ObservableCollection<MasterMapPelangganDto> MapPelangganList
        {
            get { return _mapPelangganList; }
            set
            {
                _mapPelangganList = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<CalonPelangganCsvObject> _kolektifList = new ObservableCollection<CalonPelangganCsvObject>();
        public ObservableCollection<CalonPelangganCsvObject> KolektifList
        {
            get { return _kolektifList; }
            set
            {
                _kolektifList = value;
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

        private ObservableCollection<MasterTipePendaftaranSambunganDto> _tipePendaftaranList = new ObservableCollection<MasterTipePendaftaranSambunganDto>();
        public ObservableCollection<MasterTipePendaftaranSambunganDto> TipePendaftaranList
        {
            get { return _tipePendaftaranList; }
            set
            {
                _tipePendaftaranList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region enable/disable filter

        private bool _isStatusPermohonanChecked;
        public bool IsStatusPermohonanChecked
        {
            get { return _isStatusPermohonanChecked; }
            set
            {
                _isStatusPermohonanChecked = value;
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

        private bool _isNomorRegisterChecked;
        public bool IsNomorRegisterChecked
        {
            get { return _isNomorRegisterChecked; }
            set
            {
                _isNomorRegisterChecked = value;
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

        private bool _isTanggalInputChecked;
        public bool IsTanggalInputChecked
        {
            get { return _isTanggalInputChecked; }
            set
            {
                _isTanggalInputChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isUserInputChecked;
        public bool IsUserInputChecked
        {
            get { return _isUserInputChecked; }
            set
            {
                _isUserInputChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isUserBeritaAcaraChecked;
        public bool IsUserBeritaAcaraChecked
        {
            get { return _isUserBeritaAcaraChecked; }
            set
            {
                _isUserBeritaAcaraChecked = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter variable

        private string _filterStatusPermohonan;
        public string FilterStatusPermohonan
        {
            get { return _filterStatusPermohonan; }
            set
            {
                _filterStatusPermohonan = value;
                OnPropertyChanged();
            }
        }

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

        private string _filterNomorRegister;
        public string FilterNomorRegister
        {
            get { return _filterNomorRegister; }
            set
            {
                _filterNomorRegister = value;
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

        private DateTime? _filterTanggalInputAwal;
        public DateTime? FilterTanggalInputAwal
        {
            get { return _filterTanggalInputAwal; }
            set
            {
                _filterTanggalInputAwal = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalInputAkhir;
        public DateTime? FilterTanggalInputAkhir
        {
            get { return _filterTanggalInputAkhir; }
            set
            {
                _filterTanggalInputAkhir = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _filterUserInput;
        public MasterUserDto FilterUserInput
        {
            get { return _filterUserInput; }
            set
            {
                _filterUserInput = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _filterUserBeritaAcara;
        public MasterUserDto FilterUserBeritaAcara
        {
            get { return _filterUserBeritaAcara; }
            set
            {
                _filterUserBeritaAcara = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class CalonPelangganCsvObject
    {
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeRayon { get; set; }
        public string KodeKelurahan { get; set; }
        public string KodeDiameter { get; set; }
        public string MerkMeter { get; set; }
        public string KodeGol { get; set; }
        public string NoHp { get; set; }
        public string SeriMeter { get; set; }
        public string TglDaftar { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string NamaKolektif { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PermohonanNonPelangganForm : PermohonanNonPelangganWpf
    {
    }
}
