using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Pengaturan.DaftarPosting;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Pengaturan
{
    public class DaftarPostingViewModel : ViewModelBase
    {
        public DaftarPostingViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
    }
}
