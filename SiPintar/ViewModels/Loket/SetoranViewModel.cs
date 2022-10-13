using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Loket.Setoran;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket
{
    public class SetoranViewModel : VmBase
    {
        public SetoranViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            GetSetoranCommand = new GetSetoranCommand(this, restApi);
            OnRefreshCommand = GetSetoranCommand;
            OnOpenDetailSetoranCommand = new OnOpenDetailSetoranCommand(this, restApi);
            OnOpenSetorPenerimanaanCommand = new OnOpenSetorPenerimanaanCommand(this);
            OnOpenUpdateSetoranCommand = new OnOpenUpdateSetoranCommand(this, restApi);
            OnSubmitSetoranCommand = new OnSubmitSetoranCommand(this, restApi);
            OnDownloadResiCommand = new OnDownloadResiCommand(this);
        }

        public ICommand GetSetoranCommand { get; }
        public ICommand OnOpenDetailSetoranCommand { get; }
        public ICommand OnOpenUpdateSetoranCommand { get; }
        public ICommand OnOpenSetorPenerimanaanCommand { get; }
        public ICommand OnSubmitSetoranCommand { get; }
        public ICommand OnDownloadResiCommand { get; }

        private string _loketName { get; set; }
        public string LoketName
        {
            get { return _loketName; }
            set
            {
                _loketName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SetoranLoketWpf> _dataList { get; set; }
        public ObservableCollection<SetoranLoketWpf> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private SetoranLoketWpf _selectedData { get; set; }
        public SetoranLoketWpf SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ParamSetoranLoketDto _formSetoran;
        public ParamSetoranLoketDto FormSetoran
        {
            get { return _formSetoran; }
            set { _formSetoran = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPeriodeDto> _periodeList { get; set; }
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get { return _periodeList; }
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedPeriode { get; set; }
        public MasterPeriodeDto SelectedPeriode
        {
            get { return _selectedPeriode; }
            set
            {
                _selectedPeriode = value;
                OnPropertyChanged();
            }
        }

        private string _resiFormPath;
        public string ResiFormPath
        {
            get => _resiFormPath;
            set
            {
                _resiFormPath = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBankDto> _bankList;
        public ObservableCollection<MasterBankDto> BankList
        {
            get => _bankList;
            set
            {
                _bankList = value;
                OnPropertyChanged();
            }
        }

        private MasterBankDto _bankForm;
        public MasterBankDto BankForm
        {
            get => _bankForm;
            set
            {
                _bankForm = value;
                OnPropertyChanged();
            }
        }

        private decimal? _jumlahLpp;
        public decimal? JumlahLpp
        {
            get => _jumlahLpp;
            set
            {
                _jumlahLpp = value;
                OnPropertyChanged();
            }
        }
        private decimal? _jumlahSetor;
        public decimal? JumlahSetor
        {
            get => _jumlahSetor;
            set
            {
                _jumlahSetor = value;
                OnPropertyChanged();
            }
        }
        private decimal? _jumlahSelisih;
        public decimal? JumlahSelisih
        {
            get => _jumlahSelisih;
            set
            {
                _jumlahSelisih = value;
                OnPropertyChanged();
            }
        }
    }

    public class SetoranLoketWpf : SetoranLoketDto
    {
        public bool IsSetor
        {
            get
            {
                return TglSetor.HasValue;
            }
        }
    }
}
