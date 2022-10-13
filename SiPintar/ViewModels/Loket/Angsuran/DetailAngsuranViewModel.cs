using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Loket.Angsuran.DetailAngsuran;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket.Angsuran
{
    public class DetailAngsuranViewModel : ViewModelBase
    {

        public DetailAngsuranViewModel(AngsuranViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;

            OnLoadCommandAngsuran = new OnLoadCommandAngsuran(this, restApi);
            OnTutupDetailAngsuranCommand = new OnTutupDetailAngsuranCommand(this);
            OnOpenKoreksiTanggalMulaiTagihCommand = new OnOpenKoreksiTanggalMulaiTagihCommand(this);
            OnOpenKoreksiSemuaTanggalMulaiTagihCommand = new OnOpenKoreksiSemuaTanggalMulaiTagihCommand(this);
            OnOpenMundurkanBulanTagihCommand = new OnOpenMundurkanBulanTagihCommand(this);
            OnOpenUbahPenanggungBebanAngsuranCommand = new OnOpenUbahPenanggungBebanAngsuranCommand(this, restApi);
            OnConfirmUbahDibebankanKepadaCommand = new OnConfirmUbahDibebankanKepadaCommand(this);
            OnSubmitTglMulaiTagihCommand = new OnSubmitTglMulaiTagihCommand(this, restApi);
            OnSubmitSemuaTglMulaiTagihCommand = new OnSubmitSemuaTglMulaiTagihCommand(this, restApi);
            OnSubmitUbahPenanggungBebanCommand = new OnSubmitUbahPenanggungBebanCommand(this, restApi);
            OnSubmitMundurkanBulanTagihCommand = new OnSubmitMundurkanBulanTagihCommand(this, restApi);
            OnSearchPelangganCommand = new OnSearchPelangganCommand(this, restApi);
            OnCetakBeritaAcaraCommand = new OnCetakCommand(restApi, "Loket",
                null, "Cetak Berita Acara Angsuran", ErrorHandlingCetak, isTest);
            OnCetakSuratPernyataanCommand = new OnCetakCommand(restApi, "Loket",
                null, "Cetak Surat Pernyataan Angsuran", ErrorHandlingCetak, isTest);
        }

        public ICommand OnLoadCommandAngsuran { get; }
        public ICommand OnTutupDetailAngsuranCommand { get; }
        public ICommand OnOpenKoreksiTanggalMulaiTagihCommand { get; }
        public ICommand OnOpenKoreksiSemuaTanggalMulaiTagihCommand { get; }
        public ICommand OnOpenMundurkanBulanTagihCommand { get; }
        public ICommand OnOpenUbahPenanggungBebanAngsuranCommand { get; }
        public ICommand OnConfirmUbahDibebankanKepadaCommand { get; }
        public ICommand OnSearchPelangganCommand { get; }
        public ICommand OnSubmitTglMulaiTagihCommand { get; }
        public ICommand OnSubmitSemuaTglMulaiTagihCommand { get; }
        public ICommand OnSubmitUbahPenanggungBebanCommand { get; }
        public ICommand OnSubmitMundurkanBulanTagihCommand { get; }
        public ICommand OnCetakBeritaAcaraCommand { get; }
        public ICommand OnCetakSuratPernyataanCommand { get; }

        private AngsuranViewModel _parent;
        public AngsuranViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; OnPropertyChanged(); }
        }

        private string _templateBeritaAcara { get; set; }
        public string TemplateBeritaAcara
        {
            get { return _templateBeritaAcara; }
            set
            {
                _templateBeritaAcara = value;
                OnPropertyChanged();
            }
        }

        private string _templateSpa { get; set; }
        public string TemplateSpa
        {
            get { return _templateSpa; }
            set
            {
                _templateSpa = value;
                OnPropertyChanged();
            }
        }

        private string _linkApi { get; set; }
        public string LinkApi
        {
            get { return _linkApi; }
            set
            {
                _linkApi = value;
                OnPropertyChanged();
            }
        }

        private dynamic _selectedData { get; set; }
        public dynamic SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate { get; set; } = DateTime.Now;

        public string MundurBulan { get; set; } = "0";

        public bool VisBeritaAcara { get => string.IsNullOrWhiteSpace(_selectedData.NoBeritaAcara); }


        private dynamic _detailAngsuranList;
        public dynamic DetailAngsuranList
        {
            get { return _detailAngsuranList; }
            set
            {
                _detailAngsuranList = value;
                OnPropertyChanged();
            }
        }


        private dynamic _detailAngsuran;
        public dynamic DetailAngsuran
        {
            get { return _detailAngsuran; }
            set
            {
                _detailAngsuran = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirAngsuranDetailDto> _detailAngsuranAirList;
        public ObservableCollection<RekeningAirAngsuranDetailDto> DetailAngsuranAirList
        {
            get { return _detailAngsuranAirList; }
            set
            {
                _detailAngsuranAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningNonAirAngsuranDetailDto> _detailAngsuranNonAirList;
        public ObservableCollection<RekeningNonAirAngsuranDetailDto> DetailAngsuranNonAirList
        {
            get { return _detailAngsuranNonAirList; }
            set
            {
                _detailAngsuranNonAirList = value;
                OnPropertyChanged();
            }
        }

        private dynamic _selectedPelanggan;
        public dynamic SelectedPelanggan
        {
            get { return _selectedPelanggan; }
            set
            {
                _selectedPelanggan = value;
                OnPropertyChanged();
            }
        }


        private bool _isDetailAngsuranVisible { get; set; }
        public bool IsDetailAngsuranVisible
        {
            get => _isDetailAngsuranVisible;
            set
            {
                _isDetailAngsuranVisible = value;
                OnPropertyChanged("IsDetailAngsuranVisible");
                Parent.IsDetailAngsuranVisible = value;
                if (value == false)
                    Parent.CurrentSection = Parent.ParentCurrentSection;
            }
        }

        private bool _isLoadingFormDetail { get; set; }
        public bool IsLoadingFormDetail
        {
            get => _isLoadingFormDetail;
            set
            {
                _isLoadingFormDetail = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyFormDetail { get; set; }
        public bool IsEmptyFormDetail
        {
            get => _isEmptyFormDetail;
            set
            {
                _isEmptyFormDetail = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganAirDto> _dataPelanggan = new ObservableCollection<MasterPelangganAirDto>();
        public ObservableCollection<MasterPelangganAirDto> DataPelanggan
        {
            get { return _dataPelanggan; }
            set
            {
                _dataPelanggan = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _pelangganSelected;
        public MasterPelangganAirDto PelangganSelected
        {
            get { return _pelangganSelected; }
            set
            {
                _pelangganSelected = value;
                OnPropertyChanged();
            }
        }

        private string _searchName;
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                OnPropertyChanged();
            }
        }
        private string _searchNosamb;
        public string SearchNosamb
        {
            get { return _searchNosamb; }
            set
            {
                _searchNosamb = value;
                OnPropertyChanged();
            }
        }
        private string _nosambBefore;
        public string NosambBefore
        {
            get { return _nosambBefore; }
            set
            {
                _nosambBefore = value;
                OnPropertyChanged();
            }
        }
        private string _namabBefore;
        public string NamaBefore
        {
            get { return _namabBefore; }
            set
            {
                _namabBefore = value;
                OnPropertyChanged();
            }
        }

    }
}
