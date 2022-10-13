using System.Windows.Input;
using SiPintar.Commands.Akuntansi.PostingAkuntansi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi
{
    public class PostingAkuntansiViewModel : ViewModelBase
    {
        public PostingAkuntansiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenConfirmProsesPostingCommand = new OnOpenConfirmProsesPostingCommand(this, restApi, isTest);
            OnSubmitConfirmProsesPostingCommand = new OnSubmitConfirmProsesPostingCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenConfirmProsesPostingCommand { get; }
        public ICommand OnSubmitConfirmProsesPostingCommand { get; }

        private bool _isProsesProgressBar;
        public bool IsProsesProgressBar
        {
            get => _isProsesProgressBar;
            set { _isProsesProgressBar = value; OnPropertyChanged(); }
        }
    }
}
