using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPermohonan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.Info
{
    public class HistoriPermohonanViewModel : ViewModelBase
    {
        private readonly InfoViewModel _parent;

        public HistoriPermohonanViewModel(InfoViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _parent = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
            OnOpenLihatFotoPermohonanCommand = new OnOpenLihatFotoPermohonanCommand(this, restApi);
            OnOpenLihatFotoSPKCommand = new OnOpenLihatFotoSPKCommand(this, restApi);
            OnOpenLihatFotoRABCommand = new OnOpenLihatFotoRABCommand(this, restApi);
            OnOpenLihatFotoBACommand = new OnOpenLihatFotoBACommand(this, restApi);
            OnDownloadFotoCommand = new OnDownloadFotoCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnOpenLihatFotoPermohonanCommand { get; }
        public ICommand OnOpenLihatFotoSPKCommand { get; }
        public ICommand OnOpenLihatFotoRABCommand { get; }
        public ICommand OnOpenLihatFotoBACommand { get; }
        public ICommand OnDownloadFotoCommand { get; }

        private string _tipe;
        public string Tipe
        {
            get => _tipe;
            set { _tipe = value; OnPropertyChanged(); }
        }

        private string _labelNamaTipePermohonan;
        public string LabelNamaTipePermohonan
        {
            get => _labelNamaTipePermohonan;
            set { _labelNamaTipePermohonan = value; OnPropertyChanged(); }
        }

        private string _namaTipePermohonan;
        public string NamaTipePermohonan
        {
            get => _namaTipePermohonan;
            set { _namaTipePermohonan = value; OnPropertyChanged(); }
        }

        private string _labelNomor;
        public string LabelNomor
        {
            get => _labelNomor;
            set { _labelNomor = value; OnPropertyChanged(); }
        }

        private string _nomor;
        public string Nomor
        {
            get => _nomor;
            set { _nomor = value; OnPropertyChanged(); }
        }

        private dynamic _selectedPelanggan;
        public dynamic SelectedPelanggan
        {
            get => _selectedPelanggan;
            set { _selectedPelanggan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SummaryHistoriPermohonanDto> _HistoriPermohonanList;
        public ObservableCollection<SummaryHistoriPermohonanDto> HistoriPermohonanList
        {
            get => _HistoriPermohonanList;
            set { _HistoriPermohonanList = value; OnPropertyChanged(); }
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

        private SummaryHistoriPermohonanDto _selectedData;
        public SummaryHistoriPermohonanDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private List<BitmapImage> _fotoForm;
        public List<BitmapImage> FotoForm
        {
            get => _fotoForm;
            set
            {
                _fotoForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        public void SetupData()
        {
            SelectedPelanggan = _parent.SelectedPelanggan;
        }
    }
}
