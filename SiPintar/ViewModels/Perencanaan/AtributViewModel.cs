using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.ViewModels.Perencanaan
{
    public class AtributViewModel : ViewModelBase
    {
        private readonly MaterialViewModel _material;
        private readonly OngkosViewModel _ongkos;
        private readonly PaketViewModel _paket;
        private readonly RumusVolumeViewModel _rumusVolume;

        public AtributViewModel(IRestApiClientModel restApi)
        {
            _material = new MaterialViewModel(this, restApi);
            _ongkos = new OngkosViewModel(this, restApi);
            _paket = new PaketViewModel(this, restApi);
            _rumusVolume = new RumusVolumeViewModel(this, restApi);
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
                "Material" => _material,
                "Ongkos" => _ongkos,
                "Paket" => _paket,
                "Rumus Volume" => _rumusVolume,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
            IndikasiFooter = pageName;
        }

        private string _indikasiFooter;
        public string IndikasiFooter
        {
            get { return _indikasiFooter; }
            set { _indikasiFooter = value; OnPropertyChanged(); }
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Material":
                        ((MaterialViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Ongkos":
                        ((OngkosViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Paket":
                        ((PaketViewModel)PageViewModel).NavigationItems = ((PaketViewModel)PageViewModel).GetNavigationItems();
                        ((PaketViewModel)PageViewModel).UpdatePage("Material");
                        break;
                    case "Rumus Volume":
                        ((RumusVolumeViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Material", IsSelected = true },
                new HorizontalNavigationItem() { Label = "Ongkos", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Paket", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Rumus Volume", IsSelected = false },
            };

            return Nav;
        }
    }
}
