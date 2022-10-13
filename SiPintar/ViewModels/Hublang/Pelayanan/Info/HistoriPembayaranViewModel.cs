using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPembayaran;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.Info
{
    public class HistoriPembayaranViewModel : ViewModelBase
    {
        private readonly InfoViewModel _parent;

        public HistoriPembayaranViewModel(InfoViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _parent = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi);
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

        private ObservableCollection<SummaryHistoriPembayaranDto> _HistoriPembayaranList;
        public ObservableCollection<SummaryHistoriPembayaranDto> HistoriPembayaranList
        {
            get => _HistoriPembayaranList;
            set { _HistoriPembayaranList = value; OnPropertyChanged(); }
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

        private string _tipeHistori;
        public string TipeHistori
        {
            get => _tipeHistori;
            set { _tipeHistori = value; OnPropertyChanged(); }
        }

        public void SetupData(string tipe)
        {
            SelectedPelanggan = _parent.SelectedPelanggan;
            _tipeHistori = tipe;
        }
    }
}
