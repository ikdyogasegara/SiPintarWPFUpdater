using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class KoreksiRekeningAirViewModel : ViewModelBase
    {
        private readonly PermohonanKoreksiViewModel _permohonan;
        private readonly UsulanKoreksiViewModel _usulan;
        private readonly bool _isTest;

        public KoreksiRekeningAirViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            _permohonan = new PermohonanKoreksiViewModel(this, restApi, _isTest);
            _usulan = new UsulanKoreksiViewModel(this, restApi, _isTest);
            OnLoadCommand = new OnLoadCommand(this, restApi, _isTest);
        }

        public ICommand OnLoadCommand { get; }

        private ViewModelBase _pageViewModel;

        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set
            {
                _pageViewModel = value;
                OnPropertyChanged();
            }
        }

        private bool _isBilling;

        public bool IsBilling
        {
            get { return _isBilling; }
            set
            {
                _isBilling = value;
                OnPropertyChanged();
            }
        }

        private string _jenisKoreksi;

        public string JenisKoreksi
        {
            get { return _jenisKoreksi; }
            set
            {
                _jenisKoreksi = value;
                OnPropertyChanged();

                if (_jenisKoreksi != null)
                    UpdatePage(_jenisKoreksi);
            }
        }

        private int _notifUsulanKoreksiRekening;
        public int NotifUsulanKoreksiRekening
        {
            get { return _notifUsulanKoreksiRekening; }
            set
            {
                _notifUsulanKoreksiRekening = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePermohonanDto _tipeJenisKoreksiAir;

        public MasterTipePermohonanDto TipeJenisKoreksiAir
        {
            get { return _tipeJenisKoreksiAir; }
            set
            {
                _tipeJenisKoreksiAir = value;
                OnPropertyChanged();
            }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Permohonan" => _permohonan,
                "Usulan" => _usulan,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
            IsDaftarPage = pageName == "Daftar Koreksi Rekening Air";
        }

        [ExcludeFromCodeCoverage]
        private void LoadPageCommand(string pageName)
        {
            switch (pageName)
            {
                case "Permohonan":
                    ((PermohonanKoreksiViewModel)PageViewModel).OnRefreshCommand.Execute(null);
                    break;
                case "Usulan":
                    {
                        if (!IsBilling)
                        {
                            _usulan.StatusUsulanList = new List<StatusVerifikasiKoreksi>
                            {
                                new StatusVerifikasiKoreksi {Nama = "Menunggu Verifikasi Lapangan"},
                                new StatusVerifikasiKoreksi {Nama = "(Selesai) Ditolak Cabang/Lapangan"},
                                new StatusVerifikasiKoreksi {Nama = "Menunggu Verifikasi Pusat"},
                                new StatusVerifikasiKoreksi {Nama = "(Selesai) Ditolak Pusat"},
                                new StatusVerifikasiKoreksi {Nama = "(Selesai) Sudah Verifikasi Pusat"}
                            };

                            _usulan.FilterStatusPermohonan = _usulan.StatusUsulanList[0];
                        }
                        else
                        {
                            _usulan.StatusUsulanList = new List<StatusVerifikasiKoreksi>
                            {
                                new StatusVerifikasiKoreksi {Nama = "Menunggu Verifikasi Pusat"},
                                new StatusVerifikasiKoreksi {Nama = "(Selesai) Ditolak Pusat"},
                                new StatusVerifikasiKoreksi {Nama = "(Selesai) Sudah Verifikasi Pusat"}
                            };

                            _usulan.FilterStatusPermohonan = _usulan.StatusUsulanList[0];
                        }
                    }
                    break;
            }
        }

        private bool _isDaftarPage;

        public bool IsDaftarPage
        {
            get { return _isDaftarPage; }
            set
            {
                _isDaftarPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty = true;

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        #region filter data list

        private ObservableCollection<MasterGolonganDto> _golonganList = new ObservableCollection<MasterGolonganDto>();

        public ObservableCollection<MasterGolonganDto> GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDiameterDto> _diameterList = new ObservableCollection<MasterDiameterDto>();

        public ObservableCollection<MasterDiameterDto> DiameterList
        {
            get { return _diameterList; }
            set
            {
                _diameterList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRayonDto> _rayonList = new ObservableCollection<MasterRayonDto>();

        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList = new ObservableCollection<MasterWilayahDto>();

        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKelurahanDto> _kelurahanList = new ObservableCollection<MasterKelurahanDto>();

        public ObservableCollection<MasterKelurahanDto> KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterCabangDto> _cabangList = new ObservableCollection<MasterCabangDto>();

        public ObservableCollection<MasterCabangDto> CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserDto> _userList = new ObservableCollection<MasterUserDto>();

        public ObservableCollection<MasterUserDto> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAdministrasiLainDto> _administrasiLainList = new ObservableCollection<MasterAdministrasiLainDto>();

        public ObservableCollection<MasterAdministrasiLainDto> AdministrasiLainList
        {
            get { return _administrasiLainList; }
            set
            {
                _administrasiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPemeliharaanLainDto> _pemeliharanLainList = new ObservableCollection<MasterPemeliharaanLainDto>();

        public ObservableCollection<MasterPemeliharaanLainDto> PemeliharaanLainList
        {
            get { return _pemeliharanLainList; }
            set
            {
                _pemeliharanLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRetribusiLainDto> _retribusiLainList = new ObservableCollection<MasterRetribusiLainDto>();
        public ObservableCollection<MasterRetribusiLainDto> RetribusiLainList
        {
            get { return _retribusiLainList; }
            set
            {
                _retribusiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterMeteraiDto> _meteraiList = new ObservableCollection<MasterMeteraiDto>();

        public ObservableCollection<MasterMeteraiDto> MeteraiList
        {
            get { return _meteraiList; }
            set
            {
                _meteraiList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterStatusDto> _statusList = new ObservableCollection<MasterStatusDto>();

        public ObservableCollection<MasterStatusDto> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
