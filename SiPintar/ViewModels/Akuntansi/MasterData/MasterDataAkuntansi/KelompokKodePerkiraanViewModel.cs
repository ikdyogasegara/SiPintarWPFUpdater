using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class KelompokKodePerkiraanViewModel : ViewModelBase
    {
        private readonly KelompokKodePerkiraan1ViewModel _kelompokKodePerkiraan1;
        private readonly KelompokKodePerkiraan2ViewModel _kelompokKodePerkiraan2;
        private readonly KelompokKodePerkiraan3ViewModel _kelompokKodePerkiraan3;

        public KelompokKodePerkiraanViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parentViewModel;

            _kelompokKodePerkiraan1 = new KelompokKodePerkiraan1ViewModel(this, restApi);
            _kelompokKodePerkiraan2 = new KelompokKodePerkiraan2ViewModel(this, restApi);
            _kelompokKodePerkiraan3 = new KelompokKodePerkiraan3ViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this, isTest);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnResetFilterCommand { get; set; }
        public ICommand OnRefreshCommand { get; set; }

        private MasterDataAkuntansiViewModel _parent;
        public MasterDataAkuntansiViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
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

        private string _selectedPage;
        public string SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
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

        private string _header1;
        public string Header1
        {
            get { return _header1; }
            set
            {
                _header1 = value;
                OnPropertyChanged();
            }
        }

        private string _header2;
        public string Header2
        {
            get { return _header2; }
            set
            {
                _header2 = value;
                OnPropertyChanged();
            }
        }

        private bool? _isHeaderChecked = false;
        public bool? IsHeaderChecked
        {
            get => _isHeaderChecked;
            set
            {
                _isHeaderChecked = value;
                OnPropertyChanged();
            }
        }

        private bool? _isSubKodeChecked = true;
        public bool? IsSubKodeChecked
        {
            get => _isSubKodeChecked;
            set
            {
                _isSubKodeChecked = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterTipePerkiraanDto> _tipePerkiraanList;
        public ObservableCollection<MasterTipePerkiraanDto> TipePerkiraanList
        {
            get => _tipePerkiraanList;
            set { _tipePerkiraanList = value; OnPropertyChanged(); }
        }

        private MasterTipePerkiraanDto _selectedTipePerkiraan;
        public MasterTipePerkiraanDto SelectedTipePerkiraan
        {
            get => _selectedTipePerkiraan;
            set { _selectedTipePerkiraan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPenyusutanAktivaDto> _penyusutanAktivaList;
        public ObservableCollection<MasterPenyusutanAktivaDto> PenyusutanAktivaList
        {
            get => _penyusutanAktivaList;
            set { _penyusutanAktivaList = value; OnPropertyChanged(); }
        }

        private MasterPenyusutanAktivaDto _selectedPenyusutanAktiva;
        public MasterPenyusutanAktivaDto SelectedPenyusutanAktiva
        {
            get => _selectedPenyusutanAktiva;
            set { _selectedPenyusutanAktiva = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPerkiraan1Dto> _dataPerkiraan1List;
        public ObservableCollection<MasterPerkiraan1Dto> DataPerkiraan1List
        {
            get => _dataPerkiraan1List;
            set
            {
                _dataPerkiraan1List = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan2Dto> _dataPerkiraan2List;
        public ObservableCollection<MasterPerkiraan2Dto> DataPerkiraan2List
        {
            get => _dataPerkiraan2List;
            set
            {
                _dataPerkiraan2List = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<NeracaMasterDto> _neracaMasterList;
        public ObservableCollection<NeracaMasterDto> NeracaMasterList
        {
            get => _neracaMasterList;
            set
            {
                _neracaMasterList = value;
                OnPropertyChanged();
            }
        }

        private NeracaMasterDto _selectedNeracaMaster;
        public NeracaMasterDto SelectedNeracaMaster
        {
            get => _selectedNeracaMaster;
            set
            {
                _selectedNeracaMaster = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ArusKasTidakLangsungMasterDto> _arusKasMasterList;
        public ObservableCollection<ArusKasTidakLangsungMasterDto> ArusKasMasterList
        {
            get => _arusKasMasterList;
            set
            {
                _arusKasMasterList = value;
                OnPropertyChanged();
            }
        }

        private ArusKasTidakLangsungMasterDto _selectedArusKasMaster;
        public ArusKasTidakLangsungMasterDto SelectedArusKasMaster
        {
            get => _selectedArusKasMaster;
            set
            {
                _selectedArusKasMaster = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterJenisVoucherDto> _jenisVoucherList;
        public ObservableCollection<MasterJenisVoucherDto> JenisVoucherList
        {
            get => _jenisVoucherList;
            set
            {
                _jenisVoucherList = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisVoucherDto _selectedJenisVoucher;
        public MasterJenisVoucherDto SelectedJenisVoucher
        {
            get => _selectedJenisVoucher;
            set
            {
                _selectedJenisVoucher = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterHarianKasDto> _harianKasList;
        public ObservableCollection<MasterHarianKasDto> HarianKasList
        {
            get => _harianKasList;
            set
            {
                _harianKasList = value;
                OnPropertyChanged();
            }
        }

        private MasterHarianKasDto _selectedHarianKas;
        public MasterHarianKasDto SelectedHarianKas
        {
            get => _selectedHarianKas;
            set
            {
                _selectedHarianKas = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LaporanPerputaranUangMasterDto> _perputaranUangList;
        public ObservableCollection<LaporanPerputaranUangMasterDto> PerputaranUangList
        {
            get => _perputaranUangList;
            set
            {
                _perputaranUangList = value;
                OnPropertyChanged();
            }
        }

        private LaporanPerputaranUangMasterDto _selectedPerputaranUang;
        public LaporanPerputaranUangMasterDto SelectedPerputaranUang
        {
            get => _selectedPerputaranUang;
            set
            {
                _selectedPerputaranUang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EkuitasMasterDto> _ekuitasMasterList;
        public ObservableCollection<EkuitasMasterDto> EkuitasMasterList
        {
            get => _ekuitasMasterList;
            set
            {
                _ekuitasMasterList = value;
                OnPropertyChanged();
            }
        }

        private EkuitasMasterDto _selectedEkuitasMaster;
        public EkuitasMasterDto SelectedEkuitasMaster
        {
            get => _selectedEkuitasMaster;
            set
            {
                _selectedEkuitasMaster = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AnggaranLabaRugiMasterDto> _labaRugiMasterList;
        public ObservableCollection<AnggaranLabaRugiMasterDto> LabaRugiMasterList
        {
            get => _labaRugiMasterList;
            set
            {
                _labaRugiMasterList = value;
                OnPropertyChanged();
            }
        }

        private AnggaranLabaRugiMasterDto _selectedLabaRugiMaster;
        public AnggaranLabaRugiMasterDto SelectedLabaRugiMaster
        {
            get => _selectedLabaRugiMaster;
            set
            {
                _selectedLabaRugiMaster = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan1Dto _selectedDataPerkiraan1;
        public MasterPerkiraan1Dto SelectedDataPerkiraan1
        {
            get => _selectedDataPerkiraan1;
            set
            {
                _selectedDataPerkiraan1 = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan2Dto _selectedDataPerkiraan2;
        public MasterPerkiraan2Dto SelectedDataPerkiraan2
        {
            get => _selectedDataPerkiraan2;
            set
            {
                _selectedDataPerkiraan2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAkunEtapDto> _dataAkunEtapList;
        public ObservableCollection<MasterAkunEtapDto> DataAkunEtapList
        {
            get => _dataAkunEtapList;
            set
            {
                _dataAkunEtapList = value;
                OnPropertyChanged();
            }
        }

        private MasterAkunEtapDto _selectedAkunEtap;
        public MasterAkunEtapDto SelectedAkunEtap
        {
            get => _selectedAkunEtap;
            set
            {
                _selectedAkunEtap = value;
                OnPropertyChanged();
            }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Kelompok Kode Perkiraan(XX)" => _kelompokKodePerkiraan1,
                "Kelompok Kode Perkiraan(XX,YY)" => _kelompokKodePerkiraan2,
                "Kelompok Kode Perkiraan(XX,YY,ZZ)" => _kelompokKodePerkiraan3,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Kelompok Kode Perkiraan(XX)":
                        Header1 = "Kelompok Kode Perkiraan(XX)";
                        Header2 = "Pengaturan data Kelompok Kode Perkiraan(XX)";
                        _ = ((AsyncCommandBase)((KelompokKodePerkiraan1ViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                        break;
                    case "Kelompok Kode Perkiraan(XX,YY)":
                        Header1 = "Kelompok Kode Perkiraan(XX,YY)";
                        Header2 = "Pengaturan data Kelompok Kode Perkiraan(XX,YY)";
                        _ = ((AsyncCommandBase)((KelompokKodePerkiraan2ViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);

                        break;
                    case "Kelompok Kode Perkiraan(XX,YY,ZZ)":
                        Header1 = "Kelompok Kode Perkiraan(XX,YY,ZZ)";
                        Header2 = "Pengaturan data Kelompok Kode Perkiraan(XX,YY,ZZ)";
                        _ = ((AsyncCommandBase)((KelompokKodePerkiraan3ViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);

                        break;
                    default:
                        break;
                }
            });
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

}
