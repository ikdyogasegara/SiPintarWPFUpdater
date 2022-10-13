using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.ViewModels.Hublang
{
    public class AtributViewModel : ViewModelBase
    {
        public readonly JenisNonAirViewModel JenisNonAir;
        public readonly TipePermohonanViewModel TipePermohonan;
        public readonly TarifAirTangkiViewModel TarifAirTangki;
        public readonly TipePendaftaranViewModel TipePendaftaran;
        public readonly PekerjaanViewModel Pekerjaan;
        public readonly JenisBangunanViewModel JenisBangunan;
        public readonly PeruntukanViewModel Peruntukan;

        public AtributViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            JenisNonAir = new JenisNonAirViewModel(restApi, isTest);
            TipePermohonan = new TipePermohonanViewModel(restApi, isTest);
            TarifAirTangki = new TarifAirTangkiViewModel(this, restApi, isTest);
            TipePendaftaran = new TipePendaftaranViewModel(restApi, isTest);
            Pekerjaan = new PekerjaanViewModel(restApi, isTest);
            JenisBangunan = new JenisBangunanViewModel(restApi, isTest);
            Peruntukan = new PeruntukanViewModel(restApi, isTest);

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
                "Jenis Non-Air" => JenisNonAir,
                "Tipe Permohonan" => TipePermohonan,
                "Tarif Air Tangki" => TarifAirTangki,
                "Tipe Pendaftaran" => TipePendaftaran,
                "Pekerjaan" => Pekerjaan,
                "Jenis Bangunan" => JenisBangunan,
                "Peruntukan" => Peruntukan,


                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>
            {
                    new HorizontalNavigationItem() {Label = "Tipe Permohonan", IsSelected = true},
                    new HorizontalNavigationItem() {Label = "Jenis Non-Air", IsSelected = false},
                    new HorizontalNavigationItem() { Label = "Tarif Air Tangki", IsSelected = false },
                    new HorizontalNavigationItem() { Label = "Tipe Pendaftaran", IsSelected = false },
                    new HorizontalNavigationItem() { Label = "Pekerjaan", IsSelected = false },
                    new HorizontalNavigationItem() { Label = "Jenis Bangunan", IsSelected = false },
                    new HorizontalNavigationItem() { Label = "Peruntukan", IsSelected = false }
            };

            return nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Tipe Permohonan":
                        ((TipePermohonanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Jenis Non-Air":
                        //((JenisNonAirViewModel)PageViewModel).OnLoadCommand.Execute(null)
                        break;
                    case "Tarif Air Tangki":
                        //((TarifAirTangkiViewModel)PageViewModel).OnLoadCommand.Execute(null)
                        break;
                    case "Tipe Pendaftaran":
                        //((TipePendaftaranViewModel)PageViewModel).OnLoadCommand.Execute(null)
                        break;
                    case "Pekerjaan":
                        //((PekerjaanViewModel)PageViewModel).OnLoadCommand.Execute(null)
                        break;
                    case "Jenis Bangunan":
                        //((JenisBangunanViewModel)PageViewModel).OnLoadCommand.Execute(null)
                        break;
                    case "Peruntukan":
                        //((PeruntukanViewModel)PageViewModel).OnLoadCommand.Execute(null)
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
