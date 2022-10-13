using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.ViewModels.Billing
{
    public class AtributViewModel : ViewModelBase
    {
        private readonly TarifViewModel _tarif;
        private readonly WilayahAdministrasiViewModel _wilayah;
        private readonly Atribut.LoketViewModel _loket;
        private readonly MeteranViewModel _merkMeter;
        private readonly SumberAirViewModel _sumberAir;
        private readonly KolektifViewModel _kolektif;
        private readonly KepemilikanViewModel _kepemilikan;
        private readonly StatusViewModel _status;
        private readonly FlagViewModel _flag;
        private readonly PetugasBacaViewModel _petugasBaca;
        private readonly RuteBacaMeterViewModel _ruteBaca;
        private readonly KelainanViewModel _kelainan;
        public AtributViewModel(IRestApiClientModel restApi)
        {
            _tarif = new TarifViewModel(this, restApi);
            _wilayah = new WilayahAdministrasiViewModel(this, restApi);
            _loket = new Atribut.LoketViewModel(this, restApi);
            _merkMeter = new MeteranViewModel(this, restApi);
            _sumberAir = new SumberAirViewModel(this, restApi);
            _kolektif = new KolektifViewModel(this, restApi);
            _kepemilikan = new KepemilikanViewModel(this, restApi);
            _status = new StatusViewModel(this, restApi);
            _flag = new FlagViewModel(this, restApi);
            _petugasBaca = new PetugasBacaViewModel(this, restApi);
            _ruteBaca = new RuteBacaMeterViewModel(this, restApi);
            _kelainan = new KelainanViewModel(this, restApi);
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
                "Tarif" => _tarif,
                "Wilayah Adm." => _wilayah,
                "Loket" => _loket,
                "Meteran" => _merkMeter,
                "Sumber Air" => _sumberAir,
                "Kolektif" => _kolektif,
                "Kepemilikan" => _kepemilikan,
                "Status" => _status,
                "Flag" => _flag,
                "Petugas Baca" => _petugasBaca,
                "Rute Baca" => _ruteBaca,
                "Kelainan" => _kelainan,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            switch (pageName)
            {
                case "Tarif":
                    ((TarifViewModel)PageViewModel).NavigationItems = ((TarifViewModel)PageViewModel).GetNavigationItems();
                    ((TarifViewModel)PageViewModel).NavigationItems2 = ((TarifViewModel)PageViewModel).GetNavigationItems2();
                    ((TarifViewModel)PageViewModel).UpdatePage("Golongan");
                    break;
                case "Wilayah Adm.":
                    ((WilayahAdministrasiViewModel)PageViewModel).NavigationItems = ((WilayahAdministrasiViewModel)PageViewModel).GetNavigationItems();
                    ((WilayahAdministrasiViewModel)PageViewModel).UpdatePage("Area Wilayah");
                    break;
                case "Loket":
                    ((Atribut.LoketViewModel)PageViewModel).NavigationItems = ((Atribut.LoketViewModel)PageViewModel).GetNavigationItems();
                    ((Atribut.LoketViewModel)PageViewModel).UpdatePage("Loket Terdaftar");
                    break;
                case "Meteran":
                    ((MeteranViewModel)PageViewModel).NavigationItems = ((MeteranViewModel)PageViewModel).GetNavigationItems();
                    ((MeteranViewModel)PageViewModel).UpdatePage("Merk Meter");
                    break;
                case "Sumber Air":
                    ((SumberAirViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Kolektif":
                    ((KolektifViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Kepemilikan":
                    ((KepemilikanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Status":
                    ((StatusViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Flag":
                    ((FlagViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Petugas Baca":
                    ((PetugasBacaViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Rute Baca":
                    ((RuteBacaMeterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "Kelainan":
                    ((KelainanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }

        [ExcludeFromCodeCoverage]
        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>();

            if (PermissionHelper.HasPermission("Billing.Atribut_Tarif"))
                nav.Add(new HorizontalNavigationItem() { Label = "Tarif", IsSelected = true });
            if (PermissionHelper.HasPermission("Billing.Atribut_Wilayah_Administrasi"))
                nav.Add(new HorizontalNavigationItem() { Label = "Wilayah Adm.", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Loket"))
                nav.Add(new HorizontalNavigationItem() { Label = "Loket", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Meteran"))
                nav.Add(new HorizontalNavigationItem() { Label = "Meteran", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Sumber_Air"))
                nav.Add(new HorizontalNavigationItem() { Label = "Sumber Air", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Kolektif"))
                nav.Add(new HorizontalNavigationItem() { Label = "Kolektif", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Kepemilikan"))
                nav.Add(new HorizontalNavigationItem() { Label = "Kepemilikan", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Status"))
                nav.Add(new HorizontalNavigationItem() { Label = "Status", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Flag"))
                nav.Add(new HorizontalNavigationItem() { Label = "Flag", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Flag"))
                nav.Add(new HorizontalNavigationItem() { Label = "Petugas Baca", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Flag"))
                nav.Add(new HorizontalNavigationItem() { Label = "Rute Baca", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Atribut_Flag"))
                nav.Add(new HorizontalNavigationItem() { Label = "Kelainan", IsSelected = false });
            return nav;
        }
    }
}
