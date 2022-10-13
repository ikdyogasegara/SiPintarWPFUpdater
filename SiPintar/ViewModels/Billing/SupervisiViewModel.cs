using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.ViewModels.Billing
{
    public class SupervisiViewModel : ViewModelBase
    {
        private readonly PelangganAirViewModel _pelangganAir;
        private readonly PelangganLimbahViewModel _pelangganLimbah;
        private readonly PelangganL2T2ViewModel _pelangganL2T2;
        private readonly KoreksiRekeningAirViewModel _koreksi;
        private readonly RekeningAirViewModel _rekeningAir;
        private readonly RekeningLimbahViewModel _rekeningLimbah;
        private readonly RekeningL2T2ViewModel _rekeningL2T2;
        private readonly PenghapusanRekeningViewModel _penghapusanRekening;
        private readonly PeriodeViewModel _periode;
        private readonly UploadDownloadViewModel _uploadDownload;
        private readonly PostingViewModel _posting;
        private readonly bool _isTest;


        public SupervisiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            _koreksi = new KoreksiRekeningAirViewModel(restApi, _isTest);
            _pelangganAir = new PelangganAirViewModel(this, restApi, _isTest);
            _pelangganLimbah = new PelangganLimbahViewModel(this, restApi);
            _pelangganL2T2 = new PelangganL2T2ViewModel(this, restApi);
            _rekeningAir = new RekeningAirViewModel(this, restApi, _isTest);
            _rekeningLimbah = new RekeningLimbahViewModel(this, restApi);
            _rekeningL2T2 = new RekeningL2T2ViewModel(this, restApi);
            _penghapusanRekening = new PenghapusanRekeningViewModel(this, restApi);
            _periode = new PeriodeViewModel(this, restApi, _isTest);
            _uploadDownload = new UploadDownloadViewModel(this, restApi, _isTest);
            _posting = new PostingViewModel(this, restApi);
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
                "Periode" => _periode,
                "Rekening Air" => _rekeningAir,
                "Rekening Limbah" => _rekeningLimbah,
                "Rekening L2T2" => _rekeningL2T2,
                "Pelanggan Air" => _pelangganAir,
                "Pelanggan Limbah" => _pelangganLimbah,
                "Pelanggan L2T2" => _pelangganL2T2,
                "Usulan Koreksi Rekening" => _koreksi,
                "Penghapusan Rekening" => _penghapusanRekening,
                "Upload & Download" => _uploadDownload,
                "Posting" => _posting,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", nameof(pageName))
            };

            LoadPageCommand(pageName);
        }

        private string _module;
        public string Module
        {
            get => _module;
            set
            {
                _module = value;
                OnPropertyChanged();

                if (value == "billing")
                {
                    IsBilling = true;
                    IsBacameter = false;
                }
                else
                {
                    IsBilling = false;
                    IsBacameter = true;
                }
            }
        }

        private bool _isBilling;
        public bool IsBilling
        {
            get => _isBilling;
            set
            {
                _isBilling = value;
                OnPropertyChanged();
            }
        }

        private bool _isBacameter;
        public bool IsBacameter
        {
            get => _isBacameter;
            set
            {
                _isBacameter = value;
                OnPropertyChanged();
            }
        }


        [ExcludeFromCodeCoverage]
        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>();

            if (PermissionHelper.HasPermission("Billing.Periode") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Periode", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Rekening_Air") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Rekening Air", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Rekening_Limbah") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Rekening Limbah", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Rekening_L2T2") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Rekening L2T2", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Pelanggan_Air") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Pelanggan Air", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Pelanggan_Limbah") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Pelanggan Limbah", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Pelanggan_L2T2") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Pelanggan L2T2", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Koreksi_Rekening_Air") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Usulan Koreksi Rekening", IsSelected = false, TotalBadge = 0 });
            if (PermissionHelper.HasPermission("Billing.Penghapusan_Rekening") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Penghapusan Rekening", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Upload_Download") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Upload & Download", IsSelected = false });
            if (PermissionHelper.HasPermission("Billing.Posting") || _isTest)
                nav.Add(new HorizontalNavigationItem() { Label = "Posting", IsSelected = false });

            return nav;
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Periode":
                        ((PeriodeViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Rekening Air":
                        ((RekeningAirViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Rekening Limbah":
                        ((RekeningLimbahViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Rekening L2T2":
                        ((RekeningL2T2ViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Pelanggan Air":
                        Application.Current.Dispatcher.Invoke(delegate { ((PelangganAirViewModel)PageViewModel).OnLoadCommand.Execute(null); });
                        break;
                    case "Pelanggan Limbah":
                        Application.Current.Dispatcher.Invoke(delegate { ((PelangganLimbahViewModel)PageViewModel).OnLoadCommand.Execute(null); });
                        break;
                    case "Pelanggan L2T2":
                        ((PelangganL2T2ViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Usulan Koreksi Rekening":
                        ((KoreksiRekeningAirViewModel)PageViewModel).IsBilling = true;
                        ((KoreksiRekeningAirViewModel)PageViewModel).OnLoadCommand.Execute("Usulan");
                        break;
                    case "Penghapusan Rekening":
                        ((PenghapusanRekeningViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Upload & Download":
                        ((UploadDownloadViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Posting":
                        ((PostingViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
