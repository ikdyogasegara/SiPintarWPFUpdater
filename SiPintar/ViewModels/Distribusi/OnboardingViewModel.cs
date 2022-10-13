using System;
using System.Windows.Input;
using System.Windows.Media;
using SiPintar.Commands.Distribusi.Onboarding;

namespace SiPintar.ViewModels.Distribusi
{
    public class OnboardingViewModel : ViewModelBase
    {
        public OnboardingViewModel(ViewModels.DistribusiViewModel parentViewModel)
        {
            OpenPageCommand = new OnOpenPageCommand(this, parentViewModel);
            CloseDialogPageCommand = new OnCloseDialogPageCommand(this);
            CloseBackPageCommand = new OnCloseBackPageCommand(this);
            ClosePageCommand = new OnClosePageCommand(this);
            NextPageCommand = new OnNextPageCommand(this);
            PreviousPageCommand = new OnPreviousPageCommand(this);
            LoadFigureCommand = new OnLoadFigureCommand(this);
            OpenBantuanCommand = new OnOpenBantuanPageCommand(this, parentViewModel);
        }

        public ICommand OpenPageCommand { get; private set; }
        public ICommand CloseDialogPageCommand { get; private set; }
        public ICommand CloseBackPageCommand { get; private set; }
        public ICommand ClosePageCommand { get; private set; }
        public ICommand NextPageCommand { get; private set; }
        public ICommand PreviousPageCommand { get; private set; }
        public ICommand LoadFigureCommand { get; private set; }
        public ICommand OpenBantuanCommand { get; private set; }

        private string _currentPageName;
        public string CurrentPageName
        {
            get { return _currentPageName; }
            set { _currentPageName = value; OnPropertyChanged(); }
        }

        public string TempCurrentPageName;

        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set { _currentPageIndex = value; OnPropertyChanged(); OnPropertyChanged("CurrentPageIndexShow"); }
        }

        public int CurrentPageIndexShow
        {
            get { return _currentPageIndex - 1; }
            set { throw new NotImplementedException(); }
        }

        private int _pageTotal;
        public int PageTotal
        {
            get { return _pageTotal; }
            set { _pageTotal = value; OnPropertyChanged(); OnPropertyChanged("PageTotalShow"); }
        }

        public int PageTotalShow
        {
            get { return _pageTotal - 1; }
            set { throw new NotImplementedException(); }
        }

        private bool _isOverlayActive;
        public bool IsOverlayActive
        {
            get { return _isOverlayActive; }
            set { _isOverlayActive = value; OnPropertyChanged(); }
        }

        private ImageSource _onboardFigure;
        public ImageSource OnboardFigure
        {
            get => _onboardFigure;
            set
            {
                _onboardFigure = value;
                OnPropertyChanged();
            }
        }
    }
}
