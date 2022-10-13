using System;
using System.Windows.Input;
using System.Windows.Media;
using SiPintar.Commands.Billing.Onboarding;

namespace SiPintar.ViewModels.Billing
{
    public class OnboardingViewModel : ViewModelBase
    {
        public OnboardingViewModel(BillingViewModel parentViewModel, bool isTest = false)
        {
            OnOpenDialogCommand = new OnOpenDialogCommand(this, parentViewModel, isTest);
            OnCloseDialogCommand = new OnCloseDialogCommand(this);

            OnNextPageCommand = new OnNextPageCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);

            OnCloseBackCommand = new OnCloseBackCommand(this);
            OnCloseConfirmCommand = new OnCloseConfirmCommand(this, isTest);
            OnOpenBantuanPageCommand = new OnOpenBantuanPageCommand(this, parentViewModel);
        }

        public ICommand OnOpenDialogCommand { get; private set; }
        public ICommand OnCloseDialogCommand { get; private set; }
        public ICommand OnNextPageCommand { get; private set; }
        public ICommand OnPreviousPageCommand { get; private set; }
        public ICommand OnCloseBackCommand { get; private set; }
        public ICommand OnCloseConfirmCommand { get; private set; }
        public ICommand OnOpenBantuanPageCommand { get; private set; }

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
