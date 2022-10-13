using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Atribut;

namespace SiPintar.ViewModels.Distribusi
{
    public class AtributViewModel : ViewModelBase
    {

        private readonly KelainanGantiMeterViewModel _kelainanGantiMeter;
        public AtributViewModel(IRestApiClientModel restApi)
        {
            _kelainanGantiMeter = new KelainanGantiMeterViewModel(this, restApi);
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private List<HorizontalNavigationItem> _navigationItems;
        public List<HorizontalNavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Kelainan Ganti Meter" => _kelainanGantiMeter,
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
                    case "Kelainan Ganti Meter":
                        ((KelainanGantiMeterViewModel)PageViewModel).NavigationItems = ((KelainanGantiMeterViewModel)PageViewModel).GetNavigationItems();
                        ((KelainanGantiMeterViewModel)PageViewModel).UpdatePage("Rutin");
                        break;
                    default:
                        break;
                }
            });
        }

        [ExcludeFromCodeCoverage]
        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>();

            //if (PermissionHelper.HasPermission("Distribusi.Atribut_Kelainan_Ganti_Meter"))
            Nav.Add(new HorizontalNavigationItem() { Label = "Kelainan Ganti Meter", IsSelected = true });

            return Nav;
        }

    }
}
