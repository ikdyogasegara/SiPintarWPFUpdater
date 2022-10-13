using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.Commands.Billing.Bantuan.FAQ;

namespace SiPintar.ViewModels.Billing.Bantuan
{
    [ExcludeFromCodeCoverage]
    public class FaqViewModel : ViewModelBase
    {
        public FaqViewModel(BantuanViewModel parentViewModel)
        {
            Parent = parentViewModel;

            SearchCommand = new OnSearchCommand(this);
            OnLoadCommand = new OnLoadCommand(this);
            OnOpenDetailCommand = new OnOpenDetailCommand(this);
            OnCloseDetailCommand = OnLoadCommand;
        }

        public ICommand SearchCommand { get; private set; }
        public ICommand OnLoadCommand { get; private set; }
        public ICommand OnOpenDetailCommand { get; private set; }
        public ICommand OnCloseDetailCommand { get; private set; }


        private BantuanViewModel _parent;
        public BantuanViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; OnPropertyChanged(); }
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get { return _isDetail; }
            set { _isDetail = value; OnPropertyChanged(); }
        }

        private List<string> _currentPageInfo;
        public List<string> CurrentPageInfo
        {
            get { return _currentPageInfo; }
            set { _currentPageInfo = value; OnPropertyChanged(); }
        }

        private string _currentContent;
        public string CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(); }
        }

        private bool _isEmptyContent;
        public bool IsEmptyContent
        {
            get { return _isEmptyContent; }
            set { _isEmptyContent = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FaqListDto> _listContent;
        public ObservableCollection<FaqListDto> ListContent
        {
            get { return _listContent; }
            set { _listContent = value; OnPropertyChanged(); }
        }

        [ExcludeFromCodeCoverage]
        public class FaqListDto
        {
            public string MainTitle { get; set; }
            public List<FaqContentListDto> DataList { get; set; }
        }

        [ExcludeFromCodeCoverage]
        public class FaqContentListDto
        {
            public string ContentTitle { get; set; }
            public string ContentPath { get; set; }
            public List<FaqContentDetailDto> ContentList { get; set; }
        }

        [ExcludeFromCodeCoverage]
        public class FaqContentDetailDto
        {
            public string FileTitle { get; set; }
            public List<string> BreadcrumbInfo { get; set; }
            public string HtmlString { get; set; }
        }
    }
}
