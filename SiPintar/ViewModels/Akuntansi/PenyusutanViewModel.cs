using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Penyusutan;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.ViewModels.Akuntansi
{
    public class PenyusutanViewModel : ViewModelBase
    {
        private readonly DataAktivaViewModel _dataAktiva;
        private readonly CetakTabelPenyusutanViewModel _cetakTabelPenyusutan;

        public PenyusutanViewModel(IRestApiClientModel restApi)
        {
            _dataAktiva = new DataAktivaViewModel(this, restApi);
            _cetakTabelPenyusutan = new CetakTabelPenyusutanViewModel(this, restApi);
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
                "Data Aktiva" => _dataAktiva,
                "Cetak Tabel Penyusutan" => _cetakTabelPenyusutan,
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
                    case "Data Aktiva":
                        ((DataAktivaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Cetak Tabel Penyusutan":
                        ((CetakTabelPenyusutanViewModel)PageViewModel).OnLoadCommand.Execute(null);
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

            Nav.Add(new HorizontalNavigationItem() { Label = "Data Aktiva", IsSelected = false });
            Nav.Add(new HorizontalNavigationItem() { Label = "Cetak Tabel Penyusutan", IsSelected = false });

            return Nav;
        }
    }
}
