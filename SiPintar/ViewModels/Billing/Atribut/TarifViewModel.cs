using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Tarif;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class TarifViewModel : ViewModelBase
    {
        private readonly GolonganViewModel _golongan;
        private readonly GolonganLimbahViewModel _golonganLimbah;
        private readonly GolonganLlttViewModel _golonganLltt;
        private readonly DiameterViewModel _diameter;
        private readonly AdministrasiLainViewModel _administrasi;
        private readonly PemeliharaanViewModel _pemeliharaan;
        private readonly RetribusiViewModel _retribusi;
        private readonly MateraiViewModel _materai;
        public TarifViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            _golongan = new GolonganViewModel(this, restApi);
            _diameter = new DiameterViewModel(this, restApi);
            _administrasi = new AdministrasiLainViewModel(this, restApi);
            _pemeliharaan = new PemeliharaanViewModel(this, restApi);
            _retribusi = new RetribusiViewModel(this, restApi);
            _materai = new MateraiViewModel(this, restApi);
            _golonganLimbah = new GolonganLimbahViewModel(this, restApi);
            _golonganLltt = new GolonganLlttViewModel(this, restApi);
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private bool _isFullPage;
        public bool IsFullPage
        {
            get { return _isFullPage; }
            set { _isFullPage = value; OnPropertyChanged(); }
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        private List<INavigationItem> _navigationItems2;
        public List<INavigationItem> NavigationItems2
        {
            get { return _navigationItems2; }
            set { _navigationItems2 = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Golongan" => _golongan,
                "Diameter" => _diameter,
                "Administrasi lain" => _administrasi,
                "Pemeliharaan lain" => _pemeliharaan,
                "Retribusi lain" => _retribusi,
                "Materai" => _materai,
                "Tarif Gol Limbah" => _golonganLimbah,
                "Tarif Gol L2T2" => _golonganLltt,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            if (pageName == "Tarif Gol Limbah" || pageName == "Tarif Gol L2T2")
            {
                var temp = GetNavigationItems();
                temp.ForEach(x => x.IsSelected = false);
                NavigationItems = temp;
            }
            else
            {
                NavigationItems2 = GetNavigationItems2();
            }

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Golongan":
                        ((GolonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Diameter":
                        ((DiameterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Administrasi lain":
                        ((AdministrasiLainViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pemeliharaan lain":
                        ((PemeliharaanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Retribusi lain":
                        ((RetribusiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Materai":
                        ((MateraiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tarif Gol Limbah":
                        ((GolonganLimbahViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Tarif Gol L2T2":
                        ((GolonganLlttViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public List<INavigationItem> GetNavigationItems()
        {
            IsFullPage = false;

            var Nav = new List<INavigationItem>();

            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Golongan", Icon = PackIconKind.AccountFilter, IsSelected = true, IconForeground = "#028DDB" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Diameter", Icon = PackIconKind.Diameter, IsSelected = false, IconForeground = "#F5A629" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Administrasi lain", Icon = PackIconKind.FileDocumentEdit, IsSelected = false, IconForeground = "#4BCA81" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Pemeliharaan lain", Icon = PackIconKind.HammerWrench, IsSelected = false, IconForeground = "#3CB1F2" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Retribusi lain", Icon = PackIconKind.ArchivePlus, IsSelected = false, IconForeground = "#A51D16" }
            );
            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Materai", Icon = PackIconKind.PostageStamp, IsSelected = false, IconForeground = "#FC6740" }
            );

            return Nav;
        }

        public List<INavigationItem> GetNavigationItems2()
        {
            IsFullPage = false;

            var Nav = new List<INavigationItem>();

            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Tarif Gol Limbah", Icon = PackIconKind.WaterPump, IsSelected = false, IconForeground = "#FF4A6D" }
            );

            Nav.Add(
                new ColoredFirstLevelNavigationItem() { Label = "Tarif Gol L2T2", Icon = PackIconKind.PipeDisconnected, IsSelected = false, IconForeground = "#E2451B" }
            );

            return Nav;
        }
    }
}
