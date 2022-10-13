using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.ViewModels.Akuntansi
{
    public class JurnalViewModel : ViewModelBase
    {
        private readonly RekeningViewModel _rekening;
        private readonly InstalasiViewModel _instalasi;
        private readonly KasViewModel _kas;
        private readonly UmumViewModel _umum;

        public JurnalViewModel(IRestApiClientModel restApi)
        {
            _rekening = new RekeningViewModel(this, restApi);
            _instalasi = new InstalasiViewModel(this, restApi);
            _kas = new KasViewModel(this, restApi);
            _umum = new UmumViewModel(this, restApi);
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
                "Rekening" => _rekening,
                "Instalasi" => _instalasi,
                "Kas" => _kas,
                "Umum" => _umum,
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
                    case "Rekening":
                        ((RekeningViewModel)PageViewModel).NavigationItems = ((RekeningViewModel)PageViewModel).GetNavigationItems();
                        ((RekeningViewModel)PageViewModel).PageViewModel = null!;
                        break;
                    case "Instalasi":
                        ((InstalasiViewModel)PageViewModel).NavigationItems = ((InstalasiViewModel)PageViewModel).GetNavigationItems();
                        ((InstalasiViewModel)PageViewModel).PageViewModel = null!;
                        break;
                    case "Kas":
                        ((KasViewModel)PageViewModel).NavigationItems = ((KasViewModel)PageViewModel).GetNavigationItems();
                        ((KasViewModel)PageViewModel).PageViewModel = null!;
                        break;
                    case "Umum":
                        ((UmumViewModel)PageViewModel).OnLoadCommand.Execute(null);
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

            Nav.Add(new HorizontalNavigationItem() { Label = "Rekening", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Instalasi", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Kas", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Umum", IsSelected = false });

            return Nav;
        }
    }
}
