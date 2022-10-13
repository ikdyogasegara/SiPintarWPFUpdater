using System.Windows.Input;
using SiPintar.Commands.Akuntansi.PostingKeuangan.ProsesPiutangBulanan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.PostingKeuangan
{
    public class ProsesPiutangBulananViewModel : VmBase
    {
        public ProsesPiutangBulananViewModel(PostingKeuanganViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenConfirmProsesPiutangCommand = new OnOpenConfirmProsesPiutangCommand(this, restApi, isTest);
            OnSubmitConfirmProsesPiutangCommand = new OnSubmitConfirmProsesPiutangCommand(this, restApi, isTest);
        }

        public ICommand OnOpenConfirmProsesPiutangCommand { get; }
        public ICommand OnSubmitConfirmProsesPiutangCommand { get; }

        private bool _isProsesProgressBar;
        public bool IsProsesProgressBar
        {
            get => _isProsesProgressBar;
            set { _isProsesProgressBar = value; OnPropertyChanged(); }
        }

    }
}
