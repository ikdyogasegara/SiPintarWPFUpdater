using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.SupervisiGaji;

namespace SiPintar.ViewModels.Personalia
{
    public class SupervisiGajiViewModel : ViewModelBase
    {
        public readonly RekapAbsensiViewModel _RekapAbsensi;
        public readonly PeriodePostingViewModel _PeriodePosting;
        public readonly PostingGajiViewModel _PostingGaji;
        public readonly SupervisiGajiThrViewModel _SupervisiGajiThr;
        public readonly SupervisiGaji.LaporanViewModel _Laporan;

        public SupervisiGajiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _RekapAbsensi = new RekapAbsensiViewModel(restApi, isTest);
            _PeriodePosting = new PeriodePostingViewModel(restApi, isTest);
            _PostingGaji = new PostingGajiViewModel(restApi, isTest);
            _SupervisiGajiThr = new SupervisiGajiThrViewModel(restApi, isTest);
            _Laporan = new SupervisiGaji.LaporanViewModel(restApi, isTest);
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
                "Rekap Absensi" => _RekapAbsensi,
                "Periode Posting" => _PeriodePosting,
                "Posting Gaji" => _PostingGaji,
                "Supervisi Gaji & THR" => _SupervisiGajiThr,
                "Laporan" => _Laporan,
                _ => null,
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var Nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Rekap Absensi", IsSelected = true},
                new HorizontalNavigationItem() { Label = "Periode Posting" },
                new HorizontalNavigationItem() { Label = "Posting Gaji" },
                new HorizontalNavigationItem() { Label = "Supervisi Gaji & THR" },
                new HorizontalNavigationItem() { Label = "Laporan" },
            };
            return Nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Rekap Absensi":
                        ((RekapAbsensiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Periode Posting":
                        ((PeriodePostingViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Posting Gaji":
                        ((PostingGajiViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Supervisi Gaji & THR":
                        ((SupervisiGajiThrViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Laporan":
                        ((Kepegawaian.LaporanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        _ = pageName;
                        break;
                }
            });
        }
    }
}
