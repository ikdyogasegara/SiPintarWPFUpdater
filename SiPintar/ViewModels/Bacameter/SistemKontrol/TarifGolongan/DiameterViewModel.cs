using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Diameter;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan
{
    public class DiameterViewModel : ViewModelBase
    {
        public DiameterViewModel(TarifGolonganViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDetailFormCommand = new OnOpenDetailFormCommand(this);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDetailFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }

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

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private MasterDiameterDto _selectedData;
        public MasterDiameterDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _diameterForm;
        public MasterDiameterDto DiameterForm
        {
            get { return _diameterForm; }
            set
            {
                _diameterForm = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterDiameterDto> _masterDiameterList = new ObservableCollection<MasterDiameterDto>();
        public ObservableCollection<MasterDiameterDto> MasterDiameterList
        {
            get { return _masterDiameterList; }
            set
            {
                _masterDiameterList = value;
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

        private ListCollectionView _masterDiameterListGroup;
        public ListCollectionView MasterDiameterListGroup
        {
            get { return _masterDiameterListGroup; }
            set
            {
                _masterDiameterListGroup = value;
                OnPropertyChanged();
            }
        }

        public void ReloadData()
        {
            MasterDiameterListGroup = new ListCollectionView(MasterDiameterList);
            using (MasterDiameterListGroup.DeferRefresh())
            {
                MasterDiameterListGroup.GroupDescriptions.Add(new PropertyGroupDescription("KodeDiameter"));
            }
        }
    }
}
