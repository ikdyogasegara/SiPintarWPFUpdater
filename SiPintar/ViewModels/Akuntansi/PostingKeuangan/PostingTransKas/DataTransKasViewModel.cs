using System.Windows.Input;
using SiPintar.Commands.Akuntansi.PostingKeuangan.PostingTransKas.DataTransKas;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.PostingKeuangan.PostingTransKas
{
    public class DataTransKasViewModel : VmBase
    {
        public readonly PostingTransKasViewModel Parent;

        public DataTransKasViewModel(PostingTransKasViewModel parent, PostingKeuanganViewModel parentPage, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;
            ParentPage = parentPage;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenTransferDataPenerimaCommand = new OnOpenConfirmTransferDataPenerimaCommand(this, restApi, isTest);
            OnSubmitTransferDataPenerimaCommand = new OnSubmitConfirmTransferDataPenerimaCommand(this, isTest);
        }

        public ICommand OnOpenTransferDataPenerimaCommand { get; }
        public ICommand OnSubmitTransferDataPenerimaCommand { get; }

        private PostingKeuanganViewModel _parentPage;
        public PostingKeuanganViewModel ParentPage
        {
            get => _parentPage;
            set { _parentPage = value; OnPropertyChanged(); }
        }
    }
}
