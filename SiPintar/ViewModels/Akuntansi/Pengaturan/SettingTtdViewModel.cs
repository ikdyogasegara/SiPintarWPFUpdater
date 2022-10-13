using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Pengaturan.SettingTtd;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Pengaturan
{
    public class SettingTtdViewModel : ViewModelBase
    {
        public SettingTtdViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnRefreshCommand { get; }

        private bool _isLoading { get; set; }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get { return _isAdd; }
            set { _isAdd = value; OnPropertyChanged(); }
        }
    }
}
