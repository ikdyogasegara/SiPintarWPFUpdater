using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Main.ProfilSaya;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Main
{
    public class ProfilSayaViewModel : ViewModelBase
    {
        public ProfilSayaViewModel(MainViewModel parent, IRestApiClientModel restApi)
        {
            PdamName = parent.PdamName;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenResetPasswordConfirmationCommand = new OnOpenResetPasswordConfirmationCommand(this);
            OnOpenResetPasswordFormCommand = new OnOpenResetPasswordFormCommand(this);
            OnSubmitResetPasswordCommand = new OnSubmitResetPasswordCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenResetPasswordConfirmationCommand { get; }
        public ICommand OnOpenResetPasswordFormCommand { get; }
        public ICommand OnSubmitResetPasswordCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private MasterUserDto _detailData;
        public MasterUserDto DetailData
        {
            get => _detailData;
            set { _detailData = value; OnPropertyChanged(); }
        }

        private object _detailPermission;
        public object DetailPermission
        {
            get { return _detailPermission; }
            set
            {
                _detailPermission = value;
                OnPropertyChanged();
            }
        }

        private ParamMasterUserDto _formData;
        public ParamMasterUserDto FormData
        {
            get => _formData;
            set { _formData = value; OnPropertyChanged(); }
        }

        private string _pdamName;
        public string PdamName
        {
            get => _pdamName;
            set { _pdamName = value; OnPropertyChanged(); }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
