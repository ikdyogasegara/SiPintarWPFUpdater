using System.Windows.Input;
using SiPintar.Commands.Akuntansi.PostingKeuangan.PostingTransKas.DataPenerimaan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.PostingKeuangan.PostingTransKas
{
    public class DataPenerimaanViewModel : VmBase
    {
        public DataPenerimaanViewModel(PostingTransKasViewModel parent, PostingKeuanganViewModel parentPage, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;
            ParentPage = parentPage;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnCancelCommand = new OnCancelCommand(this, isTest);
        }

        public ICommand OnCancelCommand { get; }

        private PostingTransKasViewModel _parent;
        public PostingTransKasViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private PostingKeuanganViewModel _parentPage;
        public PostingKeuanganViewModel ParentPage
        {
            get => _parentPage;
            set { _parentPage = value; OnPropertyChanged(); }
        }

    }
}
