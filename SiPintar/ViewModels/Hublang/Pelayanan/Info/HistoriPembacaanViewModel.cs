using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPembacaan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.Info
{
    public class HistoriPembacaanViewModel : ViewModelBase
    {
        private readonly InfoViewModel _parent;

        public HistoriPembacaanViewModel(InfoViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
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

        private ObservableCollection<RiwayatPemakaianAirDto> _HistoriPembacaanList;
        public ObservableCollection<RiwayatPemakaianAirDto> HistoriPembacaanList
        {
            get => _HistoriPembacaanList;
            set { _HistoriPembacaanList = value; OnPropertyChanged(); }
        }

        private RiwayatPemakaianAirDto _selectedData;
        public RiwayatPemakaianAirDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
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
