using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.Info.Tagihan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.Info
{
    public class TagihanViewModel : ViewModelBase
    {
        private readonly InfoViewModel _parent;

        public TagihanViewModel(InfoViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _parent = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }

        private dynamic _selectedPelanggan;
        public dynamic SelectedPelanggan
        {
            get => _selectedPelanggan;
            set { _selectedPelanggan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DaftarTagihanDto> _TagihanList;
        public ObservableCollection<DaftarTagihanDto> TagihanList
        {
            get => _TagihanList;
            set { _TagihanList = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }



        private int? _rekeningAirCount;
        public int? RekeningAirCount
        {
            get => _rekeningAirCount;
            set { _rekeningAirCount = value; OnPropertyChanged(); }
        }

        private decimal? _rekeningAirTotal;
        public decimal? RekeningAirTotal
        {
            get => _rekeningAirTotal;
            set { _rekeningAirTotal = value; OnPropertyChanged(); }
        }

        private int? _rekeningLimbahCount;
        public int? RekeningLimbahCount
        {
            get => _rekeningLimbahCount;
            set { _rekeningLimbahCount = value; OnPropertyChanged(); }
        }

        private decimal? _rekeningLimbahTotal;
        public decimal? RekeningLimbahTotal
        {
            get => _rekeningLimbahTotal;
            set { _rekeningLimbahTotal = value; OnPropertyChanged(); }
        }

        private int? _rekeningLlttCount;
        public int? RekeningLlttCount
        {
            get => _rekeningLlttCount;
            set { _rekeningLlttCount = value; OnPropertyChanged(); }
        }

        private decimal? _rekeningLlttTotal;
        public decimal? RekeningLlttTotal
        {
            get => _rekeningLlttTotal;
            set { _rekeningLlttTotal = value; OnPropertyChanged(); }
        }

        private int? _rekeningNonAirCount;
        public int? RekeningNonAirCount
        {
            get => _rekeningNonAirCount;
            set { _rekeningNonAirCount = value; OnPropertyChanged(); }
        }

        private decimal? _rekeningNonAirTotal;
        public decimal? RekeningNonAirTotal
        {
            get => _rekeningNonAirTotal;
            set { _rekeningNonAirTotal = value; OnPropertyChanged(); }
        }


        private int? _rekeningAmountCount;
        public int? RekeningAmountCount
        {
            get => _rekeningAmountCount;
            set { _rekeningAmountCount = value; OnPropertyChanged(); }
        }

        private decimal? _rekeningAmountTotal;
        public decimal? RekeningAmountTotal
        {
            get => _rekeningAmountTotal;
            set { _rekeningAmountTotal = value; OnPropertyChanged(); }
        }

        public void SetupData()
        {
            SelectedPelanggan = _parent.SelectedPelanggan;
        }
    }
}
