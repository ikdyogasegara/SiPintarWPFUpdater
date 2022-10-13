using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Loket.Angsuran;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.ViewModels.Loket.Tagihan;

namespace SiPintar.ViewModels.Loket
{
    public class AngsuranViewModel : ViewModelBase
    {
        public readonly AngsuranTunggakanViewModel _tunggakan;
        public readonly AngsuranSambunganBaruViewModel _sambungan;
        public readonly AngsuranNonAirViewModel _nonair;
        public readonly DetailAngsuranViewModel _detailAngsuran;

        public AngsuranViewModel(IRestApiClientModel restApi)
        {
            _tunggakan = new AngsuranTunggakanViewModel(this, restApi);
            _sambungan = new AngsuranSambunganBaruViewModel(this, restApi);
            _nonair = new AngsuranNonAirViewModel(this, restApi);
            _detailAngsuran = new DetailAngsuranViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenAddTunggakanCommand = new OnOpenAddTunggakanCommand(this);
            OnSubmitAddTunggakanCommand = new OnSubmitAddTunggakanCommand(this, restApi);
            OnOpenAddSambunganBaruCommand = new OnOpenAddSambunganBaruCommand(this);
            OnSubmitAddSambunganBaruCommand = new OnSubmitAddSambunganBaruCommand(this, restApi);
            OnOpenAddNonAirLainnyaCommand = new OnOpenAddNonAirLainnyaCommand(this);
            OnSubmitAddNonAirLainnyaCommand = new OnSubmitAddNonAirLainnyaCommand(this, restApi);
            GetPelangganListCommand = new GetPelangganListCommand(this, restApi);
            GetTagihanDetailCommand = new GetTagihanDetailCommand(this, restApi);
            GetNonAirListCommand = new GetNonAirListCommand(this, restApi);
            GetSambunganBaruListCommand = new GetSambunganBaruListCommand(this, restApi);
            GetAngsuranKalkulasiCommand = new GetAngsuranKalkulasiCommand(this, restApi);
            GetAngsuranNonAirKalkulasiCommand = new GetAngsuranNonAirKalkulasiCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenAddTunggakanCommand { get; }
        public ICommand OnSubmitAddTunggakanCommand { get; }
        public ICommand OnOpenAddSambunganBaruCommand { get; }
        public ICommand OnSubmitAddSambunganBaruCommand { get; }
        public ICommand OnOpenAddNonAirLainnyaCommand { get; }
        public ICommand OnSubmitAddNonAirLainnyaCommand { get; }
        public ICommand GetPelangganListCommand { get; }
        public ICommand GetTagihanDetailCommand { get; }
        public ICommand GetNonAirListCommand { get; }
        public ICommand GetSambunganBaruListCommand { get; }
        public ICommand GetAngsuranKalkulasiCommand { get; }
        public ICommand GetAngsuranNonAirKalkulasiCommand { get; }


        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private string _currentSection;
        public string CurrentSection
        {
            get { return _currentSection; }
            set
            {
                _currentSection = value;
                OnPropertyChanged();

                if (_currentSection != null)
                    UpdatePage(_currentSection);
            }
        }

        private string _parentCurrentSection;
        public string ParentCurrentSection
        {
            get { return _parentCurrentSection; }
            set
            {
                _parentCurrentSection = value;
                OnPropertyChanged();
            }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Tunggakan" => _tunggakan,
                "SambunganBaru" => _sambungan,
                "NonAirLainnya" => _nonair,
                "DetailAngsuran" => _detailAngsuran,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand();
        }

        public void LoadPageCommand()
        {
            _ = Task.Run(() =>
            {
                switch (CurrentSection)
                {
                    case "Tunggakan":
                        ((AngsuranTunggakanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "SambunganBaru":
                        ((AngsuranSambunganBaruViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "NonAirLainnya":
                        ((AngsuranNonAirViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "DetailAngsuran":
                        ((DetailAngsuranViewModel)PageViewModel).OnLoadCommandAngsuran.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        // filter

        #region Jenis Pelanggan
        private bool _isJenisPelangganChecked;
        public bool IsJenisPelangganChecked
        {
            get { return _isJenisPelangganChecked; }
            set
            {
                _isJenisPelangganChecked = value;
                OnPropertyChanged();

                if (!value)
                    JenisPelangganFilter = null;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> JenisPelangganList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Pelanggan"),
                    new KeyValuePair<int, string>(2, "Non pelanggan"),
                };
            }
        }

        private KeyValuePair<int, string>? _jenisPelangganFilter;
        public KeyValuePair<int, string>? JenisPelangganFilter
        {
            get => _jenisPelangganFilter;
            set
            {
                _jenisPelangganFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Kategori
        private bool _isKategoriChecked;
        public bool IsKategoriChecked
        {
            get { return _isKategoriChecked; }
            set
            {
                _isKategoriChecked = value;
                OnPropertyChanged();

                if (!value)
                    JenisNonAirFilter = null;
            }
        }

        private ObservableCollection<MasterJenisNonAirDto> _jenisNonAirList = new ObservableCollection<MasterJenisNonAirDto>();
        public ObservableCollection<MasterJenisNonAirDto> JenisNonAirList
        {
            get => _jenisNonAirList;
            set
            {
                _jenisNonAirList = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisNonAirDto _jenisNonAirFilter;
        public MasterJenisNonAirDto JenisNonAirFilter
        {
            get => _jenisNonAirFilter;
            set
            {
                _jenisNonAirFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Status Bayar
        private bool _isStatusBayarChecked;
        public bool IsStatusBayarChecked
        {
            get { return _isStatusBayarChecked; }
            set
            {
                _isStatusBayarChecked = value;
                OnPropertyChanged();

                if (!value)
                    StatusBayarFilter = null;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusBayarList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Lunas"),
                    new KeyValuePair<int, string>(2, "Belum Lunas"),
                };
            }
        }

        private KeyValuePair<int, string>? _statusBayarFilter;
        public KeyValuePair<int, string>? StatusBayarFilter
        {
            get => _statusBayarFilter;
            set
            {
                _statusBayarFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region No Samb
        private bool _isNoSambChecked;
        public bool IsNoSambChecked
        {
            get { return _isNoSambChecked; }
            set
            {
                _isNoSambChecked = value;
                OnPropertyChanged();

                if (!value)
                    NoSambFilter = null;
            }
        }

        private string _noSambFilter;
        public string NoSambFilter
        {
            get => _noSambFilter;
            set
            {
                _noSambFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Nama Pelanggan
        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get { return _isNamaChecked; }
            set
            {
                _isNamaChecked = value;
                OnPropertyChanged();

                if (!value)
                    NamaFilter = null;
            }
        }

        private string _namaFilter;
        public string NamaFilter
        {
            get => _namaFilter;
            set
            {
                _namaFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Status Publish
        private bool _isStatusPublishChecked;
        public bool IsStatusPublishChecked
        {
            get { return _isStatusPublishChecked; }
            set
            {
                _isStatusPublishChecked = value;
                OnPropertyChanged();

                if (!value)
                    StatusPublishFilter = null;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusPublishList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Sudah Publish"),
                    new KeyValuePair<int, string>(2, "Belum Publish"),
                };
            }
        }

        private KeyValuePair<int, string>? _statusPublishFilter;
        public KeyValuePair<int, string>? StatusPublishFilter
        {
            get => _statusPublishFilter;
            set
            {
                _statusPublishFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Petugas
        private bool _isPetugasChecked;
        public bool IsPetugasChecked
        {
            get { return _isPetugasChecked; }
            set
            {
                _isPetugasChecked = value;
                OnPropertyChanged();

                if (!value)
                    PetugasFilter = null;
            }
        }

        private ObservableCollection<MasterUserDto> _petugasList;
        public ObservableCollection<MasterUserDto> PetugasList
        {
            get { return _petugasList; }
            set
            {
                _petugasList = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _petugasFilter;
        public MasterUserDto PetugasFilter
        {
            get => _petugasFilter;
            set
            {
                _petugasFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        // end filter

        // form add

        private int _currentStep = 1;
        public int CurrentStep
        {
            get => _currentStep;
            set
            {
                _currentStep = value;
                OnPropertyChanged();
            }
        }

        private bool _isPiutangAirAllSelected;
        public bool IsPiutangAirAllSelected
        {
            get { return _isPiutangAirAllSelected; }
            set
            {
                _isPiutangAirAllSelected = value;
                var temp = new ObservableCollection<TagihanRekeningAirWpf>(RekeningAirList);
                foreach (var item in temp)
                {
                    item.IsSelected = value;
                }
                if (RekeningAirList != null)
                {
                    RekeningAirList = temp;
                }
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

        private bool _isEmptyForm1;
        public bool IsEmptyForm1
        {
            get { return _isEmptyForm1; }
            set
            {
                _isEmptyForm1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyForm2;
        public bool IsEmptyForm2
        {
            get { return _isEmptyForm2; }
            set
            {
                _isEmptyForm2 = value;
                OnPropertyChanged();
            }
        }

        private string _jenisAngsuran;
        public string JenisAngsuran
        {
            get { return _jenisAngsuran; }
            set
            {
                _jenisAngsuran = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganAirDto> _pelangganList;
        public ObservableCollection<MasterPelangganAirDto> PelangganList
        {
            get { return _pelangganList; }
            set
            {
                _pelangganList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PermohonanNonPelangganDto> _nonPelangganList;
        public ObservableCollection<PermohonanNonPelangganDto> NonPelangganList
        {
            get { return _nonPelangganList; }
            set
            {
                _nonPelangganList = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _selectedPelanggan;
        public MasterPelangganAirDto SelectedPelanggan
        {
            get { return _selectedPelanggan; }
            set
            {
                _selectedPelanggan = value;
                OnPropertyChanged();
            }
        }

        private RekeningNonAirDto _selectedNonPelanggan;
        public RekeningNonAirDto SelectedNonPelanggan
        {
            get { return _selectedNonPelanggan; }
            set
            {
                _selectedNonPelanggan = value;
                OnPropertyChanged();
            }
        }

        private ParamRekeningAngsuranAirDto _formSubmitAngsuran;
        public ParamRekeningAngsuranAirDto FormSubmitAngsuran
        {
            get { return _formSubmitAngsuran; }
            set
            {
                _formSubmitAngsuran = value;
                OnPropertyChanged();
            }
        }

        private ParamRekeningAngsuranNonAirDto _formSubmitAngsuranNonAir;
        public ParamRekeningAngsuranNonAirDto FormSubmitAngsuranNonAir
        {
            get { return _formSubmitAngsuranNonAir; }
            set
            {
                _formSubmitAngsuranNonAir = value;
                OnPropertyChanged();
            }
        }

        private string _namaPelangganForm;
        public string NamaPelangganForm
        {
            get => _namaPelangganForm;
            set
            {
                _namaPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _noSambForm;
        public string NoSambForm
        {
            get => _noSambForm;
            set
            {
                _noSambForm = value;
                OnPropertyChanged();
            }
        }

        private string _alamatForm;
        public string AlamatForm
        {
            get => _alamatForm;
            set
            {
                _alamatForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _jumlahDpMinForm;
        public decimal JumlahDpMinForm
        {
            get => _jumlahDpMinForm;
            set
            {
                _jumlahDpMinForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _jumlahDpMaxForm;
        public decimal JumlahDpMaxForm
        {
            get => _jumlahDpMaxForm;
            set
            {
                _jumlahDpMaxForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _dpMaxForm = 0;
        public decimal DpMaxForm
        {

            get => _dpMaxForm;
            set
            {
                _dpMaxForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _jumlahPokok;
        public decimal JumlahPokok
        {
            get => _jumlahPokok;
            set
            {
                _jumlahPokok = value;
                OnPropertyChanged();
            }
        }

        private decimal _sisaAngsur = 0;
        public decimal SisaAngsur
        {
            get => _sisaAngsur;
            set
            {
                _sisaAngsur = value;
                OnPropertyChanged();
            }
        }

        private string _noTelpForm;
        public string NoTelpForm
        {
            get => _noTelpForm;
            set
            {
                _noTelpForm = value;
                OnPropertyChanged();
            }
        }

        private string _noHpForm;
        public string NoHpForm
        {
            get => _noHpForm;
            set
            {
                _noHpForm = value;
                OnPropertyChanged();
            }
        }

        private decimal _uangMukaForm;
        public decimal UangMukaForm
        {
            get => _uangMukaForm;
            set
            {
                _uangMukaForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isDetailAngsuranVisible;
        public bool IsDetailAngsuranVisible
        {
            get { return _isDetailAngsuranVisible; }
            set
            {
                _isDetailAngsuranVisible = value;
                OnPropertyChanged();
            }
        }

        private dynamic _dataSelected;
        public dynamic DataSelected
        {
            get { return _dataSelected; }
            set
            {
                _dataSelected = value;
                OnPropertyChanged();
            }
        }

        private int _terminForm;
        public int TerminForm
        {
            get => _terminForm;
            set
            {
                _terminForm = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tglTerminForm;
        public DateTime TglTerminForm
        {
            get => _tglTerminForm;
            set
            {
                _tglTerminForm = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TagihanRekeningAirWpf> _rekeningAirList;
        public ObservableCollection<TagihanRekeningAirWpf> RekeningAirList
        {
            get => _rekeningAirList;
            set
            {
                _rekeningAirList = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirAngsuranDto _angsuranFilter { get; set; }
        public RekeningAirAngsuranDto AngsuranFilter
        {
            get => _angsuranFilter;
            set
            {
                _angsuranFilter = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ParamRekeningAngsuranAirDetailDto> _angsuranKalkulasi { get; set; }
        public ObservableCollection<ParamRekeningAngsuranAirDetailDto> AngsuranKalkulasi
        {
            get => _angsuranKalkulasi;
            set
            {
                _angsuranKalkulasi = value;
                OnPropertyChanged();
            }
        }

        private ListCollectionView _angsuranKalkulasiGroup;
        public ListCollectionView AngsuranKalkulasiGroup
        {
            get { return _angsuranKalkulasiGroup; }
            set
            {
                _angsuranKalkulasiGroup = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ParamRekeningAngsuranNonAirDetailDto> _angsuranNonAirKalkulasi { get; set; }
        public ObservableCollection<ParamRekeningAngsuranNonAirDetailDto> AngsuranNonAirKalkulasi
        {
            get => _angsuranNonAirKalkulasi;
            set
            {
                _angsuranNonAirKalkulasi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TagihanRekeningLimbahWpf> _rekeningLimbahList;
        public ObservableCollection<TagihanRekeningLimbahWpf> RekeningLimbahList
        {
            get => _rekeningLimbahList;
            set { _rekeningLimbahList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TagihanRekeningLlttWpf> _rekeningLlttList;
        public ObservableCollection<TagihanRekeningLlttWpf> RekeningLlttList
        {
            get => _rekeningLlttList;
            set { _rekeningLlttList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TagihanRekeningNonAirWpf> _rekeningNonAirList;
        public ObservableCollection<TagihanRekeningNonAirWpf> RekeningNonAirList
        {
            get => _rekeningNonAirList;
            set { _rekeningNonAirList = value; OnPropertyChanged(); }
        }

        private TagihanRekeningNonAirWpf _selectedRekeningNonAir;
        public TagihanRekeningNonAirWpf SelectedRekeningNonAir
        {
            get { return _selectedRekeningNonAir; }
            set
            {
                _selectedRekeningNonAir = value;
                OnPropertyChanged();
            }
        }

        // end form add

        public void CheckUpdate()
        {
            if (RekeningAirList != null)
            {
                var totalCheck = 0;
                foreach (var item in RekeningAirList)
                {
                    if (item.IsSelected)
                    {
                        totalCheck += 1;
                    }
                }
                if (totalCheck == RekeningAirList.Count)
                {
                    _isPiutangAirAllSelected = true;
                }
                else
                {
                    _isPiutangAirAllSelected = false;
                }
                OnPropertyChanged("IsPiutangAirAllSelected");
            }
            MaxDpUpdate();
        }

        public void MaxDpUpdate()
        {
            JumlahPokok = RekeningAirList.Where(x => x.IsSelected).Sum(x => x.Total) ?? 0;
            JumlahDpMaxForm = RekeningAirList.Where(x => x.IsSelected).Sum(x => x.Total) * DpMaxForm / 100 ?? 0;
            JumlahDpMinForm = RekeningAirList.Where(x => x.IsSelected).Sum(x => x.Administrasi + x.AdministrasiLain + x.Pemeliharaan + x.PemeliharaanLain + x.Retribusi + x.RetribusiLain + x.Denda + x.Meterai) ?? 0;
        }
    }
}
