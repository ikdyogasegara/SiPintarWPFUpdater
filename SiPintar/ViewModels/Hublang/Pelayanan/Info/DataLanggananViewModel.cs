using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.Info.DataLangganan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.Info
{
    public class DataLanggananViewModel : ViewModelBase
    {
        private readonly InfoViewModel _parent;

        public DataLanggananViewModel(InfoViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _parent = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }

        private dynamic _selectedPelanggan;
        public dynamic SelectedPelanggan
        {
            get => _selectedPelanggan;
            set { _selectedPelanggan = value; OnPropertyChanged(); }
        }

        private MasterPelangganAirDto _detailPelangganAir;
        public MasterPelangganAirDto DetailPelangganAir
        {
            get => _detailPelangganAir;
            set { _detailPelangganAir = value; OnPropertyChanged(); }
        }

        private MasterPelangganLimbahDto _detailPelangganLimbah;
        public MasterPelangganLimbahDto DetailPelangganLimbah
        {
            get => _detailPelangganLimbah;
            set { _detailPelangganLimbah = value; OnPropertyChanged(); }
        }

        private MasterPelangganLlttDto _detailPelangganLltt;
        public MasterPelangganLlttDto DetailPelangganLltt
        {
            get => _detailPelangganLltt;
            set { _detailPelangganLltt = value; OnPropertyChanged(); }
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

        public void SetupData()
        {
            SelectedPelanggan = _parent.SelectedPelanggan;
        }
    }
}
