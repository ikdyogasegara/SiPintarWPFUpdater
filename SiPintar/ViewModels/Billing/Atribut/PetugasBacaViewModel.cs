using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.PetugasBaca;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class PetugasBacaViewModel : ViewModelBase
    {
        public PetugasBacaViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = OnLoadCommand;
            OnExportCommand = new OnExportCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, restApi);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            UploadFileCommand = new UploadFileCommand(this, restApi);
            OnOpenFotoCommand = new OnOpenFotoCommand(this);
            OnOpenResetPasswordConfirmationCommand = new OnOpenResetPasswordConfirmationCommand(this);
            OnOpenResetPasswordFormCommand = new OnOpenResetPasswordFormCommand(this);
            OnSubmitResetPasswordCommand = new OnSubmitResetPasswordCommand(this, restApi);
            OnBrowseFileFotoCommand = new OnBrowseFileFotoCommand(this);
            GetFotoCommand = new GetFotoCommand(this, restApi);
            OnGetFotoDatagridCommand = new OnGetFotoDatagridCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnSubmitAddFormCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand OnOpenFotoCommand { get; }
        public ICommand OnOpenResetPasswordConfirmationCommand { get; }
        public ICommand OnOpenResetPasswordFormCommand { get; }
        public ICommand OnSubmitResetPasswordCommand { get; }
        public ICommand OnBrowseFileFotoCommand { get; }
        public ICommand GetFotoCommand { get; }
        public ICommand OnGetFotoDatagridCommand { get; }

        private bool _isEdit;
        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
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

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingFoto;
        public bool IsLoadingFoto
        {
            get { return _isLoadingFoto; }
            set
            {
                _isLoadingFoto = value;
                OnPropertyChanged();
            }
        }

        private bool _isOnGrid = true;
        public bool IsOnGrid
        {
            get { return _isOnGrid; }
            set
            {
                _isOnGrid = value;
                OnPropertyChanged();
            }
        }

        private bool _isDataSelected;
        public bool IsDataSelected
        {
            get { return _isDataSelected; }
            set
            {
                _isDataSelected = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
                OnLoadCommand.Execute(null);
            }
        }

        private long _totalRecord;
        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var ListOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return data;
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;
        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;
        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisible;
        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;
        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;
        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;
        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPetugasBacaDto> _dataList;
        public ObservableCollection<MasterPetugasBacaDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _selectedData;
        public MasterPetugasBacaDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
                FotoThumbnailURI = null;
                OnGetFotoDatagridCommand.Execute(null);
            }
        }

        private ParamMasterPetugasBacaDto _formData;
        public ParamMasterPetugasBacaDto FormData
        {
            get { return _formData; }
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> JenisPembacaList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(0, "Pembaca Meter"),
                    new KeyValuePair<int, string>(1, "Pengawas")
                };
            }
        }

        private KeyValuePair<int, string>? _jenisPembacaForm;
        public KeyValuePair<int, string>? JenisPembacaForm
        {
            get { return _jenisPembacaForm; }
            set
            {
                _jenisPembacaForm = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.JenisPembaca = _jenisPembacaForm?.Value;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Aktif"),
                    new KeyValuePair<int, string>(0, "Tidak Aktif")
                };
            }
        }

        private KeyValuePair<int, string>? _statusForm;
        public KeyValuePair<int, string>? StatusForm
        {
            get { return _statusForm; }
            set
            {
                _statusForm = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.Status = _statusForm?.Key == 1;
            }
        }

        private Uri _fotoThumbnailURI;
        public Uri FotoThumbnailURI
        {
            get => _fotoThumbnailURI;
            set
            {
                _fotoThumbnailURI = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoPetugasURI;
        public Uri FotoPetugasURI
        {
            get => _fotoPetugasURI;
            set
            {
                _fotoPetugasURI = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.FotoPetugasBaca = _fotoPetugasURI?.OriginalString;
            }
        }

        private bool _isFotoExist;
        public bool IsFotoExist
        {
            get => _isFotoExist;
            set
            {
                _isFotoExist = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _previewFile;
        public BitmapImage PreviewFile
        {
            get => _previewFile;
            set
            {
                _previewFile = value;
                OnPropertyChanged();
            }
        }

        private string _passwordForm;
        public string PasswordForm
        {
            get => _passwordForm;
            set
            {
                _passwordForm = value;
                OnPropertyChanged();
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
