using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Authentication;
using SiPintar.Utilities;

namespace SiPintar.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IRestApiClientModel restApi)
        {
            OnLoginCommand = new OnLoginCommand(this, restApi);

            Version = Application.Current?.Resources["Versi"]?.ToString();
        }

        public ICommand OnLoginCommand { get; }

        private string _namaUser;
        public string NamaUser
        {
            get { return _namaUser; }
            set { _namaUser = value; OnPropertyChanged(); }
        }

        private string _errorUsername;
        public string ErrorUsername
        {
            get { return _errorUsername; }
            set { _errorUsername = value; OnPropertyChanged(); }
        }

        private string _errorPassword;
        public string ErrorPassword
        {
            get { return _errorPassword; }
            set { _errorPassword = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { _version = value; OnPropertyChanged(); }
        }

        public void UpdateState()
        {
            Version = Application.Current.Resources["Versi"].ToString();
        }
    }
}
