using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class PengaturanPutstampViewModel : ViewModelBase
    {
        public PengaturanPutstampViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this);
            OnBrowseFileCommand = new OnBrowseFileCommand(this);
            OnSaveCommand = new OnSaveCommand(this);
            OnResetCommand = new OnResetCommand(this);
            OnOpenOptionPDACommand = new OnOpenOptionPDACommand(this);
            OnOpenManualFormPDACommand = new OnOpenManualFormPDACommand(this);
            OnOpenConfirmationPDACommand = new OnOpenConfirmationPDACommand(this);
            OnSubmitPasswordPDACommand = new OnSubmitPasswordPDACommand(this);
            GetUserListPDACommand = new GetUserListPDACommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnBrowseFileCommand { get; }
        public ICommand OnSaveCommand { get; }
        public ICommand OnResetCommand { get; }
        public ICommand OnOpenOptionPDACommand { get; }
        public ICommand OnOpenManualFormPDACommand { get; }
        public ICommand OnOpenConfirmationPDACommand { get; }
        public ICommand OnSubmitPasswordPDACommand { get; }
        public ICommand GetUserListPDACommand { get; }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set { _fileName = value; OnPropertyChanged(); }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set { _content = value; OnPropertyChanged(); }
        }

        private bool _isEncryptedPassword;
        public bool IsEncryptedPassword
        {
            get => _isEncryptedPassword;
            set { _isEncryptedPassword = value; OnPropertyChanged(); }
        }

        private string _passwordForm;
        public string PasswordForm
        {
            get => _passwordForm;
            set { _passwordForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterUserDto> _userList;
        public ObservableCollection<MasterUserDto> UserList
        {
            get => _userList;
            set { _userList = value; OnPropertyChanged(); }
        }

        private MasterUserDto _selectedUser;
        public MasterUserDto SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmptyUser;
        public bool IsEmptyUser
        {
            get => _isEmptyUser;
            set { _isEmptyUser = value; OnPropertyChanged(); }
        }
    }
}
