using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Kelurahan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi
{
    public class KelurahanViewModel : ViewModelBase
    {
        public KelurahanViewModel(WilayahAdministrasiViewModel parentViewModel, IRestApiClientModel _restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, _restApi);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, _restApi);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, _restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, _restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private string _section;
        public string Section
        {
            get => _section;
            set { _section = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterCabangDto> _cabangList = new ObservableCollection<MasterCabangDto>();
        public ObservableCollection<MasterCabangDto> CabangList
        {
            get => _cabangList;
            set { _cabangList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyCabang"); }
        }

        private MasterCabangDto _selectedCabang;
        public MasterCabangDto SelectedCabang
        {
            get => _selectedCabang;
            set
            {
                _selectedCabang = value;
                OnPropertyChanged();
                OnPropertyChanged("IsCabangSelected");

                SelectedKecamatan = null;
                KecamatanList.Clear();
                _ = Task.Run(() => OnLoadCommand.Execute("kecamatan"));
            }
        }

        private bool _isLoadingCabang;
        public bool IsLoadingCabang
        {
            get => _isLoadingCabang;
            set { _isLoadingCabang = value; OnPropertyChanged(); }
        }

        private bool _isCabangSelected;
        public bool IsCabangSelected
        {
            get => _isCabangSelected || SelectedCabang != null;
            set { _isCabangSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyCabang;
        public bool IsEmptyCabang
        {
            get => _isEmptyCabang || CabangList == null || CabangList.Count == 0;
            set { _isEmptyCabang = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKecamatanDto> _kecamatanList = new ObservableCollection<MasterKecamatanDto>();
        public ObservableCollection<MasterKecamatanDto> KecamatanList
        {
            get => _kecamatanList;
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyKecamatan");
            }
        }

        private MasterKecamatanDto _selectedKecamatan;
        public MasterKecamatanDto SelectedKecamatan
        {
            get => _selectedKecamatan;
            set
            {
                _selectedKecamatan = value;
                OnPropertyChanged();
                OnPropertyChanged("IsKecamatanSelected");

                SelectedKelurahan = null;
                KelurahanList.Clear();
                _ = Task.Run(() => OnLoadCommand.Execute("kelurahan"));
            }
        }

        private bool _isLoadingKecamatan;
        public bool IsLoadingKecamatan
        {
            get => _isLoadingKecamatan;
            set { _isLoadingKecamatan = value; OnPropertyChanged(); }
        }

        private bool _isKecamatanSelected;
        public bool IsKecamatanSelected
        {
            get => _isKecamatanSelected || SelectedKecamatan != null;
            set { _isKecamatanSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyKecamatan;
        public bool IsEmptyKecamatan
        {
            get => _isEmptyKecamatan || KecamatanList == null || KecamatanList.Count == 0;
            set { _isEmptyKecamatan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKelurahanDto> _kelurahanList = new ObservableCollection<MasterKelurahanDto>();
        public ObservableCollection<MasterKelurahanDto> KelurahanList
        {
            get => _kelurahanList;
            set { _kelurahanList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyKelurahan"); }
        }

        private MasterKelurahanDto _selectedKelurahan;
        public MasterKelurahanDto SelectedKelurahan
        {
            get => _selectedKelurahan;
            set { _selectedKelurahan = value; OnPropertyChanged(); OnPropertyChanged("IsKelurahanSelected"); }
        }

        private bool _isLoadingKelurahan;
        public bool IsLoadingKelurahan
        {
            get => _isLoadingKelurahan;
            set { _isLoadingKelurahan = value; OnPropertyChanged(); }
        }

        private bool _isKelurahanSelected;
        public bool IsKelurahanSelected
        {
            get => _isKelurahanSelected || SelectedKelurahan != null;
            set { _isKelurahanSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyKelurahan;
        public bool IsEmptyKelurahan
        {
            get => _isEmptyKelurahan || KelurahanList == null || KelurahanList.Count == 0;
            set { _isEmptyKelurahan = value; OnPropertyChanged(); }
        }


        private string _kodeForm;
        public string KodeForm
        {
            get => _kodeForm;
            set { _kodeForm = value; OnPropertyChanged(); }
        }

        private string _namaForm;
        public string NamaForm
        {
            get => _namaForm;
            set { _namaForm = value; OnPropertyChanged(); }
        }

        private string _jumlahJiwaForm;
        public string JumlahJiwaForm
        {
            get => _jumlahJiwaForm;
            set { _jumlahJiwaForm = value; OnPropertyChanged(); }
        }
    }
}
