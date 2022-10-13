using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.Golongan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class GolonganViewModel : ViewModelBase
    {
        public GolonganViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDetailFormCommand = new OnOpenDetailFormCommand(this);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDetailFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                if (value)
                {
                    OnSubmitCommand = OnSubmitEditCommand;
                }
                else
                {
                    OnSubmitCommand = OnSubmitAddCommand;
                }
                OnPropertyChanged();
            }
        }

        private bool _isFromDetail;
        public bool IsFromDetail
        {
            get => _isFromDetail;
            set { _isFromDetail = value; OnPropertyChanged(); }
        }

        private MasterGolonganDto _selectedData;
        public MasterGolonganDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGolonganDto> _masterGolonganList = new ObservableCollection<MasterGolonganDto>();
        public ObservableCollection<MasterGolonganDto> MasterGolonganList
        {
            get { return _masterGolonganList; }
            set
            {
                _masterGolonganList = value;
                OnPropertyChanged();
            }
        }

        private List<dynamic> _detailData = new List<dynamic>();
        public List<dynamic> DetailData
        {
            get { return _detailData; }
            set
            {
                _detailData = value;
                OnPropertyChanged();
            }
        }

        private List<dynamic> _detailDenda = new List<dynamic>();
        public List<dynamic> DetailDenda
        {
            get { return _detailDenda; }
            set
            {
                _detailDenda = value;
                OnPropertyChanged();
            }
        }

        private List<dynamic> _detailKeterangan = new List<dynamic>();
        public List<dynamic> DetailKeterangan
        {
            get { return _detailKeterangan; }
            set
            {
                _detailKeterangan = value;
                OnPropertyChanged();
            }
        }

        private ListCollectionView _masterGolonganListGroup;
        public ListCollectionView MasterGolonganListGroup
        {
            get { return _masterGolonganListGroup; }
            set
            {
                _masterGolonganListGroup = value;
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

        private MasterGolonganDto _golonganForm;
        public MasterGolonganDto GolonganForm
        {
            get { return _golonganForm; }
            set
            {
                _golonganForm = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYear = new List<int>();
        public List<int> ListYear
        {
            get { return _listYear; }
            set
            {
                _listYear = value;
                OnPropertyChanged();
            }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }

        private int _month;
        public int Month
        {
            get { return _month; }
            set
            {
                _month = value;
                OnPropertyChanged();
            }
        }

        public void ReloadData()
        {
            MasterGolonganListGroup = new ListCollectionView(MasterGolonganList);
            using (MasterGolonganListGroup.DeferRefresh())
            {
                MasterGolonganListGroup.GroupDescriptions.Add(new PropertyGroupDescription("KodeGolongan"));
            }
        }

    }
}
