using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Loket.LoketTerdaftar;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Loket
{
    public class LoketTerdaftarViewModel : ViewModelBase
    {
        public LoketTerdaftarViewModel(LoketViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnExportCommand { get; }

        private ObservableCollection<MasterLoketDto> _masterLoketList;
        public ObservableCollection<MasterLoketDto> MasterLoketList
        {
            get => _masterLoketList;
            set { _masterLoketList = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private MasterLoketDto _selectedData;
        public MasterLoketDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private string _kodeLoketForm;
        public string KodeLoketForm
        {
            get => _kodeLoketForm;
            set { _kodeLoketForm = value; OnPropertyChanged(); }
        }

        private string _namaLoketForm;
        public string NamaLoketForm
        {
            get => _namaLoketForm;
            set { _namaLoketForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList;
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); }
        }

        private MasterWilayahDto _wilayahForm;
        public MasterWilayahDto WilayahForm
        {
            get => _wilayahForm;
            set { _wilayahForm = value; OnPropertyChanged(); }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Aktif"),
                    new KeyValuePair<int, string>(0, "Non Aktif")
                };
            }
        }

        private KeyValuePair<int, string>? _statusForm;
        public KeyValuePair<int, string>? StatusForm
        {
            get => _statusForm;
            set { _statusForm = value; OnPropertyChanged(); }
        }

        private bool _flagMitraForm;
        public bool FlagMitraForm
        {
            get => _flagMitraForm;
            set { _flagMitraForm = value; OnPropertyChanged(); }
        }
    }
}
