using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.PostingKeuangan.PostingTransKas;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan.PostingTransKas;

namespace SiPintar.ViewModels.Akuntansi.PostingKeuangan
{
    public class PostingTransKasViewModel : ViewModelBase
    {
        private readonly DataTransKasViewModel _dataTransKas;
        private readonly DataPenerimaanViewModel _dataPenerimaan;

        public PostingTransKasViewModel(PostingKeuanganViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;

            _dataTransKas = new DataTransKasViewModel(this, parent, restApi);
            _dataPenerimaan = new DataPenerimaanViewModel(this, parent, restApi);

            OnLoadCommand = new OnLoadCommand(this);
        }

        public ICommand OnLoadCommand { get; }

        private PostingKeuanganViewModel _parent;
        public PostingKeuanganViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "DataTransKas" => _dataTransKas,
                "DataPenerimaan" => _dataPenerimaan,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "DataTransKas":
                        ((DataTransKasViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "DataPenerimaan":
                        ((DataPenerimaanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
