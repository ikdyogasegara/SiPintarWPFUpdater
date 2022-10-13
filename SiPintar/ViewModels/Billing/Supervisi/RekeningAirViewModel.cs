using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Supervisi.RekeningAir;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class RekeningAirViewModel : ViewModelBase
    {
        public delegate void UpdateFotoThumbnailAction();

        public event UpdateFotoThumbnailAction UpdateFotoThumbnailEvent;

        public delegate void UpdateDataGridAction();

        public event UpdateDataGridAction UpdateDataGridEvent;

        public RekeningAirViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parentViewModel;
            _restApi = restApi;

            OnLoadCommand = new OnLoadCommand(this);
            OnFilterCommand = new OnFilterCommand(this, restApi, isTest);
            OnCalculationCommand = new OnCalculationCommand(this, restApi, isTest);
            OnOpenTerbitkanPelangganCommand = new OnOpenTerbitkanPelangganCommand(this, restApi, isTest);
            OnSettingTableCommand = new OnSettingTableCommand(this, isTest);
            OnDownloadBacameterCommand = new OnDownloadBacameterCommand(this, restApi, isTest);
            OnOpenHapusRekeningCommand = new OnOpenHapusRekeningCommand(this, isTest);
            OnOpenSetRekeningCommand = new OnOpenSetRekeningCommand(this, isTest);
            OnOpenRiwayatPembayaranCommand = new OnOpenRiwayatPembayaranCommand(this, isTest);
            OnGetRiwayatPembayaranCommand = new OnGetRiwayatPembayaranCommand(this, restApi, isTest);
            OnOpenRiwayatPemakaianCommand = new OnOpenRiwayatPemakaianCommand(this, isTest);
            OnGetRiwayatPemakaianCommand = new OnGetRiwayatPemakaianCommand(this, restApi, isTest);
            OnOpenRiwayatPiutangCommand = new OnOpenRiwayatPiutangCommand(this, restApi, isTest);
            OnOpenKoreksiRekeningCommand = new OnOpenKoreksiRekeningCommand(this, isTest: isTest);
            OnSubmitTerbitkanCommand = new OnSubmitTerbitkanCommand(this, restApi, isTest);
            OnUpdatePublishListCommand = new OnUpdatePublishListCommand(this, restApi, isTest);
            OnOpenPublishListCommand = new OnOpenPublishListCommand(this);
            OnPerbaruiDataRekeningCommand = new OnPerbaruiDataRekeningCommand(this, restApi, isTest);
            OnCalCulationKoreksiCommand = new OnCalCulationKoreksiCommand(this, isTest);
            OnSubmitKoreksiRekeningCommand = new OnSubmitKoreksiRekeningCommand(this, restApi, isTest);
            OnSubmitDeleteRekeningCommand = new OnSubmitDeleteRekeningCommand(this, restApi, isTest);
            OnSetRekeningCommand = new OnSetRekeningCommand(this, restApi, isTest);
            OnUploadRekeningCommand = new OnUploadRekeningCommand(this, restApi, isTest);
            OnOpenDownloadBacameterCommand = new OnOpenDownloadBacameterCommand(this, isTest);
            OnOpenVerifikasiBacameterCommand = new OnOpenVerifikasiBacameterCommand(this, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnUpdatePublishCommand = new OnUpdatePublishCommand(this, restApi, isTest);
            OnUpdateVerifikasiCommand = new OnUpdateVerifikasiCommand(this, restApi, isTest);
            OnRefreshTerbitkanPelangganCommand = new OnRefreshTerbitkanPelangganCommand(this, restApi, isTest);
            OnSubmitTaksasiCommand = new OnSubmitTaksasiCommand(this, restApi, isTest);
            OnSubmitMeterTerbalikCommand = new OnSubmitMeterTerbalikCommand(this, restApi, isTest);
            OnSubmitStanKembaliMudaCommand = new OnSubmitStanKembaliMudaCommand(this, restApi, isTest);
            OnSubmitSetBelumBacaCommand = new OnSubmitSetBelumBacaCommand(this, restApi, isTest);
            OnSubmitPermintaanBacaUlangRequestCommand = new OnSubmitPermintaanBacaUlangRequestCommand(this, restApi, isTest);
            OnSubmitPermintaanBacaUlangBatalCommand = new OnSubmitPermintaanBacaUlangBatalCommand(this, restApi, isTest);

            OnOpenHasilBacaUlangDialogCommand = new OnOpenHasilBacaUlangDialogCommand(this, restApi);
            OnOpenLihatVideoCommand = new OnOpenLihatVideoCommand(this, isTest);
            GetFotoStanCommand = new GetFotoStanCommand(this, isTest);
            GetFotoRumahCommand = new GetFotoRumahCommand(this, isTest);
            GetVideoCommand = new GetVideoCommand(this, isTest);

            OnOpenLampiranDialogCommand = new OnOpenLampiranDialogCommand(this, isTest);
            OnSubmitLampiranCommand = new OnSubmitLampiranCommand(this, restApi, isTest);
        }

        public readonly SupervisiViewModel Parent;
        private readonly IRestApiClientModel _restApi;
        public ICommand OnLoadCommand { get; }
        public ICommand OnCalculationCommand { get; }
        public ICommand OnSettingTableCommand { get; }
        public ICommand OnDownloadBacameterCommand { get; }
        public ICommand OnOpenHapusRekeningCommand { get; }
        public ICommand OnOpenSetRekeningCommand { get; }
        public ICommand OnOpenRiwayatPembayaranCommand { get; }
        public ICommand OnGetRiwayatPembayaranCommand { get; }
        public ICommand OnOpenRiwayatPemakaianCommand { get; }
        public ICommand OnGetRiwayatPemakaianCommand { get; }
        public ICommand OnOpenRiwayatPiutangCommand { get; }
        public ICommand OnOpenKoreksiRekeningCommand { get; }
        public ICommand OnOpenTerbitkanPelangganCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnSubmitTerbitkanCommand { get; }
        public ICommand OnUpdatePublishListCommand { get; }
        public ICommand OnOpenPublishListCommand { get; }
        public ICommand OnPerbaruiDataRekeningCommand { get; }
        public ICommand OnCalCulationKoreksiCommand { get; }
        public ICommand OnSubmitKoreksiRekeningCommand { get; }
        public ICommand OnSubmitDeleteRekeningCommand { get; }
        public ICommand OnSetRekeningCommand { get; }
        public ICommand OnUploadRekeningCommand { get; }
        public ICommand OnOpenDownloadBacameterCommand { get; }
        public ICommand OnOpenVerifikasiBacameterCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnUpdatePublishCommand { get; }
        public ICommand OnUpdateVerifikasiCommand { get; }
        public ICommand OnRefreshTerbitkanPelangganCommand { get; }
        public ICommand OnSubmitTaksasiCommand { get; }
        public ICommand OnSubmitMeterTerbalikCommand { get; }
        public ICommand OnSubmitStanKembaliMudaCommand { get; }
        public ICommand OnSubmitSetBelumBacaCommand { get; }
        public ICommand OnSubmitPermintaanBacaUlangRequestCommand { get; }
        public ICommand OnSubmitPermintaanBacaUlangBatalCommand { get; }

        public ICommand OnOpenHasilBacaUlangDialogCommand { get; }
        public ICommand OnOpenLihatVideoCommand { get; }
        public ICommand GetFotoStanCommand { get; }
        public ICommand GetFotoRumahCommand { get; }
        public ICommand GetVideoCommand { get; }

        public ICommand OnOpenLampiranDialogCommand { get; }
        public ICommand OnSubmitLampiranCommand { get; }

        #region HakAkses

        private bool _aksesDownloadBacameter = PermissionHelper.HasPermission("Billing.Rekening_Air_DownloadBacameter");

        public bool AksesDownloadBacameter
        {
            get => _aksesDownloadBacameter;
            set
            {
                _aksesDownloadBacameter = value;
                OnPropertyChanged();
            }
        }

        private bool _aksesTerbitkan = PermissionHelper.HasPermission("Billing.Rekening_Air_Terbitkan");

        public bool AksesTerbitkan
        {
            get => _aksesTerbitkan;
            set
            {
                _aksesTerbitkan = value;
                OnPropertyChanged();
            }
        }

        private bool _aksesPublishUnpublish = PermissionHelper.HasPermission("Billing.Rekening_Air_PublishUnpublish");

        public bool AksesPublishUnpublish
        {
            get => _aksesPublishUnpublish;
            set
            {
                _aksesPublishUnpublish = value;
                OnPropertyChanged();
            }
        }

        private bool _aksesPerbaruiData = PermissionHelper.HasPermission("Billing.Rekening_Air_PerbaruiData");

        public bool AksesPerbaruiData
        {
            get => _aksesPerbaruiData;
            set
            {
                _aksesPerbaruiData = value;
                OnPropertyChanged();
            }
        }

        private bool _aksesKoreksi = PermissionHelper.HasPermission("Billing.Rekening_Air_Koreksi");

        public bool AksesKoreksi
        {
            get => _aksesKoreksi;
            set
            {
                _aksesKoreksi = value;
                OnPropertyChanged();
            }
        }

        private bool _aksesHapus = PermissionHelper.HasPermission("Billing.Rekening_Air_Hapus");

        public bool AksesHapus
        {
            get => _aksesHapus;
            set
            {
                _aksesHapus = value;
                OnPropertyChanged();
            }
        }

        private bool _aksesSetFlag = PermissionHelper.HasPermission("Billing.Rekening_Air_SetFlag");

        public bool AksesSetFlag
        {
            get => _aksesSetFlag;
            set
            {
                _aksesSetFlag = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private MasterStatusDto _selectedDataStatus = new MasterStatusDto();

        public MasterStatusDto SelectedDataStatus
        {
            get => _selectedDataStatus;
            set
            {
                _selectedDataStatus = value;
                OnPropertyChanged();
            }
        }

        private MasterKelainanDto _selectedDataKelainan = new MasterKelainanDto();

        public MasterKelainanDto SelectedDataKelainan
        {
            get => _selectedDataKelainan;
            set
            {
                _selectedDataKelainan = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _selectedDataPetugasBaca = new MasterPetugasBacaDto();

        public MasterPetugasBacaDto SelectedDataPetugasBaca
        {
            get => _selectedDataPetugasBaca;
            set
            {
                _selectedDataPetugasBaca = value;
                OnPropertyChanged();
            }
        }

        private MasterKolektifDto _selectedDataKolektif = new MasterKolektifDto();

        public MasterKolektifDto SelectedDataKolektif
        {
            get => _selectedDataKolektif;
            set
            {
                _selectedDataKolektif = value;
                OnPropertyChanged();
            }
        }

        private MasterFlagDto _selectedDataFlag = new MasterFlagDto();

        public MasterFlagDto SelectedDataFlag
        {
            get => _selectedDataFlag;
            set
            {
                _selectedDataFlag = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _selectedDataKasir = new MasterUserDto();

        public MasterUserDto SelectedDataKasir
        {
            get => _selectedDataKasir;
            set
            {
                _selectedDataKasir = value;
                OnPropertyChanged();
            }
        }

        private MasterLoketDto _selectedDataLoket = new MasterLoketDto();

        public MasterLoketDto SelectedDataLoket
        {
            get => _selectedDataLoket;
            set
            {
                _selectedDataLoket = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _selectedDataGolongan = new MasterGolonganDto();

        public MasterGolonganDto SelectedDataGolongan
        {
            get => _selectedDataGolongan;
            set
            {
                _selectedDataGolongan = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _pelangganForm = new MasterPelangganAirDto();

        public MasterPelangganAirDto PelangganForm
        {
            get => _pelangganForm;
            set
            {
                _pelangganForm = value;
                OnPropertyChanged();
            }
        }


        private string _namaPelangganForm;

        public string NamaPelangganForm
        {
            get => _namaPelangganForm;
            set
            {
                _namaPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _nosambPelangganForm;

        public string NosambPelangganForm
        {
            get => _nosambPelangganForm;
            set
            {
                _nosambPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _alamatPelangganForm;

        public string AlamatPelangganForm
        {
            get => _alamatPelangganForm;
            set
            {
                _alamatPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _noRekPelangganForm;

        public string NoRekPelangganForm
        {
            get => _noRekPelangganForm;
            set
            {
                _noRekPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private decimal? _selectedDataRekairawal;

        public decimal? SelectedDataRekairAwal
        {
            get => _selectedDataRekairawal;
            set
            {
                _selectedDataRekairawal = value;
                OnPropertyChanged();
            }
        }

        // setting table

        private object _tableSetting;

        public object TableSetting
        {
            get { return _tableSetting; }
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        // end setting table

        private decimal? _selectedDataRekairakhir;

        public decimal? SelectedDataRekairAkhir
        {
            get => _selectedDataRekairakhir;
            set
            {
                _selectedDataRekairakhir = value;
                OnPropertyChanged();
            }
        }

        private decimal? _selectedDataPakaiAwal;

        public decimal? SelectedDataPakaiAwal
        {
            get => _selectedDataPakaiAwal;
            set
            {
                _selectedDataPakaiAwal = value;
                OnPropertyChanged();
            }
        }

        private decimal? _selectedDataPakaiAkhir;

        public decimal? SelectedDataPakaiAkhir
        {
            get => _selectedDataPakaiAkhir;
            set
            {
                _selectedDataPakaiAkhir = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyDialog;

        public bool IsEmptyDialog
        {
            get { return _isEmptyDialog; }
            set
            {
                _isEmptyDialog = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyPeriode;

        public bool IsEmptyPeriode
        {
            get { return _isEmptyPeriode; }
            set
            {
                _isEmptyPeriode = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<RekeningAirPiutangDto> _piutangList;

        public ObservableCollection<RekeningAirPiutangDto> PiutangList
        {
            get { return _piutangList; }
            set
            {
                _piutangList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedDataPeriode = new MasterPeriodeDto();

        public MasterPeriodeDto SelectedDataPeriode
        {
            get => _selectedDataPeriode;
            set
            {
                _selectedDataPeriode = value;
                OnPropertyChanged();

                if (value is { KodePeriode: { } })
                {
                    NamaBulan = GetNameHistoriBulan(value.KodePeriode, 0);
                    NamaBulanLalu = GetNameHistoriBulan(value.KodePeriode, -1);
                    Nama2BulanLalu = GetNameHistoriBulan(value.KodePeriode, -2);
                    Nama3BulanLalu = GetNameHistoriBulan(value.KodePeriode, -3);
                }
            }
        }

        private MasterWilayahDto _selectedDataWilayah = new MasterWilayahDto();

        public MasterWilayahDto SelectedDataWilayah
        {
            get => _selectedDataWilayah;
            set
            {
                _selectedDataWilayah = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _selectedDataKelurahan = new MasterKelurahanDto();

        public MasterKelurahanDto SelectedDataKelurahan
        {
            get => _selectedDataKelurahan;
            set
            {
                _selectedDataKelurahan = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _selectedDataRayon = new MasterRayonDto();

        public MasterRayonDto SelectedDataRayon
        {
            get => _selectedDataRayon;
            set
            {
                _selectedDataRayon = value;
                OnPropertyChanged();
            }
        }

        private MasterBlokDto _selectedDataBlok = new MasterBlokDto();

        public MasterBlokDto SelectedDataBlok
        {
            get => _selectedDataBlok;
            set
            {
                _selectedDataBlok = value;
                OnPropertyChanged();
            }
        }

        private MasterCabangDto _selectedDataCabang = new MasterCabangDto();

        public MasterCabangDto SelectedDataCabang
        {
            get => _selectedDataCabang;
            set
            {
                _selectedDataCabang = value;
                OnPropertyChanged();
            }
        }

        private MasterMerekMeterDto _selectedDataMerkMeter = new MasterMerekMeterDto();

        public MasterMerekMeterDto SelectedDataMerkMeter
        {
            get => _selectedDataMerkMeter;
            set
            {
                _selectedDataMerkMeter = value;
                OnPropertyChanged();
            }
        }

        private string _seriMeterForm;

        public string SeriMeterForm
        {
            get => _seriMeterForm;
            set
            {
                _seriMeterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _selectedDataDiameter = new MasterDiameterDto();

        public MasterDiameterDto SelectedDataDiameter
        {
            get => _selectedDataDiameter;
            set
            {
                _selectedDataDiameter = value;
                OnPropertyChanged();
            }
        }


        private string _bulan;

        public string Bulan
        {
            get => _bulan;
            set
            {
                _bulan = value;
                OnPropertyChanged();
            }
        }

        private string _tahun;

        public string Tahun
        {
            get => _tahun;
            set
            {
                _tahun = value;
                OnPropertyChanged();
            }
        }

        private MasterKondisiMeterDto _selectedDataKondisiMeter = new MasterKondisiMeterDto();

        public MasterKondisiMeterDto SelectedDataKondisiMeter
        {
            get => _selectedDataKondisiMeter;
            set
            {
                _selectedDataKondisiMeter = value;
                OnPropertyChanged();
            }
        }

        private string _selectedLainnya;

        public string SelectedLainnya
        {
            get => _selectedLainnya;
            set
            {
                _selectedLainnya = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirWpf> _rekeningAirList;
        public ObservableCollection<RekeningAirWpf> RekeningAirList
        {
            get { return _rekeningAirList; }
            set
            {
                _rekeningAirList = value;
                OnPropertyChanged();
            }
        }

        private List<int?> _idPelangganAirList;
        public List<int?> IdPelangganAirList
        {
            get { return _idPelangganAirList; }
            set
            {
                _idPelangganAirList = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirDto _hitungKoreksi = new RekeningAirDto();

        public RekeningAirDto HitungKoreksi
        {
            get { return _hitungKoreksi; }
            set
            {
                _hitungKoreksi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RiwayatPemakaianAirDto> _pemakaianList;

        public ObservableCollection<RiwayatPemakaianAirDto> PemakaianList
        {
            get { return _pemakaianList; }
            set
            {
                _pemakaianList = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYearPemakaian = new List<int>();

        public List<int> ListYearPemakaian
        {
            get { return _listYearPemakaian; }
            set
            {
                _listYearPemakaian = value;
                OnPropertyChanged();
            }
        }

        private int? _yearPemakaian;

        public int? YearPemakaian
        {
            get { return _yearPemakaian; }
            set
            {
                _yearPemakaian = value;
                if (_yearPemakaian.HasValue)
                {
                    OnGetRiwayatPemakaianCommand.Execute(null);
                }

                OnPropertyChanged();
            }
        }


        private RekeningAirDetailDto _hitungKoreksiDetail = new RekeningAirDetailDto();

        public RekeningAirDetailDto HitungKoreksiDetail
        {
            get { return _hitungKoreksiDetail; }
            set
            {
                _hitungKoreksiDetail = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirWpf> _penerbitanPelangganList;

        public ObservableCollection<RekeningAirWpf> PenerbitanPelangganList
        {
            get { return _penerbitanPelangganList; }
            set
            {
                _penerbitanPelangganList = value;
                OnPropertyChanged();
            }
        }

        private bool _tanpaDenda;

        public bool TanpaDenda
        {
            get => _tanpaDenda;
            set
            {
                _tanpaDenda = value;
                OnPropertyChanged();
            }
        }

        private List<dynamic> _rekeningAirDetailList;

        public List<dynamic> RekeningAirDetailList
        {
            get { return _rekeningAirDetailList; }
            set
            {
                _rekeningAirDetailList = value;
                OnPropertyChanged();
            }
        }

        private List<MasterPeriodeDto> _masterPeriodeList;
        public List<MasterPeriodeDto> MasterPeriodeList
        {
            get { return _masterPeriodeList; }
            set
            {
                _masterPeriodeList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterStatusDto>? _masterStatusList;
        public ObservableCollection<MasterStatusDto>? MasterStatusList
        {
            get { return _masterStatusList; }
            set
            {
                _masterStatusList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterStatus));
            }
        }
        public bool LoadMasterStatus { get => _masterStatusList == null; }

        private ObservableCollection<MasterPetugasBacaDto>? _masterPetugasBacaList;
        public ObservableCollection<MasterPetugasBacaDto>? MasterPetugasBacaList
        {
            get { return _masterPetugasBacaList; }
            set
            {
                _masterPetugasBacaList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPetugasBaca));
            }
        }
        public bool LoadMasterPetugasBaca { get => _masterPetugasBacaList == null; }

        private ObservableCollection<MasterKelainanDto>? _masterKelainanList;
        public ObservableCollection<MasterKelainanDto>? MasterKelainanList
        {
            get { return _masterKelainanList; }
            set
            {
                _masterKelainanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKelainan));
            }
        }
        public bool LoadMasterKelainan { get => _masterKelainanList == null; }

        private ObservableCollection<MasterKolektifDto>? _masterKolektifList;
        public ObservableCollection<MasterKolektifDto>? MasterKolektifList
        {
            get { return _masterKolektifList; }
            set
            {
                _masterKolektifList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKolektif));
            }
        }
        public bool LoadMasterKolektif { get => _masterKolektifList == null; }

        private ObservableCollection<MasterFlagDto>? _masterFlagList;
        public ObservableCollection<MasterFlagDto>? MasterFlagList
        {
            get { return _masterFlagList; }
            set
            {
                _masterFlagList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterFlag));
            }
        }
        public bool LoadMasterFlag { get => _masterFlagList == null; }

        private ObservableCollection<MasterUserDto>? _masterUserDto;
        public ObservableCollection<MasterUserDto>? MasterUserList
        {
            get { return _masterUserDto; }
            set
            {
                _masterUserDto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterUser));
            }
        }
        public bool LoadMasterUser { get => _masterUserDto == null; }

        private ObservableCollection<MasterLoketDto>? _masterLoketList;
        public ObservableCollection<MasterLoketDto>? MasterLoketList
        {
            get { return _masterLoketList; }
            set
            {
                _masterLoketList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterLoket));
            }
        }
        public bool LoadMasterLoket { get => _masterLoketList == null; }

        private ObservableCollection<MasterGolonganDto>? _masterGolonganList;
        public ObservableCollection<MasterGolonganDto>? MasterGolonganList
        {
            get { return _masterGolonganList; }
            set
            {
                _masterGolonganList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterGolongan));
            }
        }
        public bool LoadMasterGolongan { get => _masterGolonganList == null; }

        private ObservableCollection<MasterAdministrasiLainDto>? _masterAdministrasiLainList;
        public ObservableCollection<MasterAdministrasiLainDto>? MasterAdministrasiLainList
        {
            get { return _masterAdministrasiLainList; }
            set
            {
                _masterAdministrasiLainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterAdministrasiLain));
            }
        }
        public bool LoadMasterAdministrasiLain { get => _masterAdministrasiLainList == null; }

        private ObservableCollection<MasterPemeliharaanLainDto>? _masterPemeliharaanLainList;
        public ObservableCollection<MasterPemeliharaanLainDto>? MasterPemeliharaanLainList
        {
            get { return _masterPemeliharaanLainList; }
            set
            {
                _masterPemeliharaanLainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPemeliharaanLain));
            }
        }
        public bool LoadMasterPemeliharaanLain { get => _masterPemeliharaanLainList == null; }

        private ObservableCollection<MasterRetribusiLainDto>? _masterRetribusiLainList;
        public ObservableCollection<MasterRetribusiLainDto>? MasterRetribusiLainList
        {
            get { return _masterRetribusiLainList; }
            set
            {
                _masterRetribusiLainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterRetribusiLain));
            }
        }
        public bool LoadMasterRetribusiLain { get => _masterRetribusiLainList == null; }

        private ObservableCollection<MasterMeteraiDto>? _masterMeteraiList;
        public ObservableCollection<MasterMeteraiDto>? MasterMeteraiList
        {
            get { return _masterMeteraiList; }
            set
            {
                _masterMeteraiList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterMeterai));
            }
        }
        public bool LoadMasterMeterai { get => _masterMeteraiList == null; }

        private ObservableCollection<MasterWilayahDto>? _masterWilayahList;
        public ObservableCollection<MasterWilayahDto>? MasterWilayahList
        {
            get { return _masterWilayahList; }
            set
            {
                _masterWilayahList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterWilayah));
            }
        }
        public bool LoadMasterWilayah { get => _masterWilayahList == null; }

        private ObservableCollection<MasterKelurahanDto>? _masterKelurahanList;
        public ObservableCollection<MasterKelurahanDto>? MasterKelurahanList
        {
            get { return _masterKelurahanList; }
            set
            {
                _masterKelurahanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKelurahan));
            }
        }
        public bool LoadMasterKelurahan { get => _masterKelurahanList == null; }

        private ObservableCollection<MasterRayonDto>? _masterRayonList;
        public ObservableCollection<MasterRayonDto>? MasterRayonList
        {
            get { return _masterRayonList; }
            set
            {
                _masterRayonList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterRayon));
            }
        }
        public bool LoadMasterRayon { get => _masterRayonList == null; }

        private ObservableCollection<MasterBlokDto>? _masterBlokList;
        public ObservableCollection<MasterBlokDto>? MasterBlokList
        {
            get { return _masterBlokList; }
            set
            {
                _masterBlokList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterBlok));
            }
        }
        public bool LoadMasterBlok { get => _masterBlokList == null; }

        private ObservableCollection<MasterCabangDto>? _masterCabangList;
        public ObservableCollection<MasterCabangDto>? MasterCabangList
        {
            get { return _masterCabangList; }
            set
            {
                _masterCabangList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterCabang));
            }
        }
        public bool LoadMasterCabang { get => _masterCabangList == null; }

        private ObservableCollection<MasterMerekMeterDto>? _masterMerekMeterList;
        public ObservableCollection<MasterMerekMeterDto>? MasterMerekMeterList
        {
            get { return _masterMerekMeterList; }
            set
            {
                _masterMerekMeterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterMerekMeter));
            }
        }
        public bool LoadMasterMerekMeter { get => _masterMerekMeterList == null; }

        private ObservableCollection<MasterDiameterDto>? _masterDiameterList;
        public ObservableCollection<MasterDiameterDto>? MasterDiameterList
        {
            get { return _masterDiameterList; }
            set
            {
                _masterDiameterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDiameter));
            }
        }
        public bool LoadMasterDiameter { get => _masterDiameterList == null; }

        private ObservableCollection<MasterKondisiMeterDto>? _masterKondisiMeterList;
        public ObservableCollection<MasterKondisiMeterDto>? MasterKondisiMeterList
        {
            get { return _masterKondisiMeterList; }
            set
            {
                _masterKondisiMeterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKondisiMeter));
            }
        }
        public bool LoadMasterKondisiMeter { get => _masterKondisiMeterList == null; }

        private ObservableCollection<FilterLainnyaDto> _masterLainnyaList;
        public ObservableCollection<FilterLainnyaDto> MasterLainnyaList
        {
            get { return _masterLainnyaList; }
            set
            {
                _masterLainnyaList = value;
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

        private bool _isLoadingForm;

        public bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingPenerbitan;

        public bool IsLoadingPenerbitan
        {
            get { return _isLoadingPenerbitan; }
            set
            {
                _isLoadingPenerbitan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingKoreksi;

        public bool IsLoadingKoreksi
        {
            get { return _isLoadingKoreksi; }
            set
            {
                _isLoadingKoreksi = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingBukti;
        public bool IsLoadingBukti
        {
            get => _isLoadingBukti;
            set
            {
                _isLoadingBukti = value;
                OnPropertyChanged();
            }
        }



        private bool _isEmpty;

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private bool _isProgressifExpand;

        public bool IsProgressifExpand
        {
            get { return _isProgressifExpand; }
            set
            {
                if (!_isProgressifExpand && value && SelectedData != null)
                {
                    _ = Task.Run(() => DetailRekairAsync(SelectedData));
                }

                _isProgressifExpand = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirWpf _selectedData;

        public RekeningAirWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                PersentaseBulanIni = 0;
                Ratarata3bulan = 0;

                IsEnabled = value != null && (bool)value.FlagPublishWpf;
                OnPropertyChanged("IsEnabled");
                if (IsProgressifExpand)
                {
                    _ = Task.Run(() => DetailRekairAsync(value));
                }

                if (value != null)
                {
                    Ratarata3bulan = (value.PakaiBulanLalu ?? 0 + value.Pakai2BulanLalu ?? 0 + value.Pakai3BulanLalu ?? 0) / 3;

                    if (value.FlagKoreksiWpf == true)
                    {
                        var pakai = value.PakaiWpf ?? 0;
                        var pakaiBulanLalu = value.PakaiBulanLalu ?? 0;
                        var pembagian = pakaiBulanLalu == 0 ? pakai : pakaiBulanLalu;

                        if (pakai == pakaiBulanLalu)
                        {
                            PersentaseBulanIni = 0;
                        }
                        else if (pembagian == 0)
                        {
                            PersentaseBulanIni = 0;
                        }
                        else
                        {
                            PersentaseBulanIni = (pakai - value.PakaiBulanLalu ?? 0) / pembagian * 100;
                        }
                    }
                }
            }
        }

        private RekeningAirHasilBacaUlangDto _hasilBacaUlang;
        public RekeningAirHasilBacaUlangDto HasilBacaUlang
        {
            get => _hasilBacaUlang;
            set
            {
                _hasilBacaUlang = value;
                OnPropertyChanged();
            }
        }

        private string _namaBulan;

        public string NamaBulan
        {
            get { return _namaBulan; }
            set
            {
                _namaBulan = value;
                OnPropertyChanged();
            }
        }

        private string _namaBulanLalu;

        public string NamaBulanLalu
        {
            get { return _namaBulanLalu; }
            set
            {
                _namaBulanLalu = value;
                OnPropertyChanged();
            }
        }

        private string _nama2BulanLalu;

        public string Nama2BulanLalu
        {
            get { return _nama2BulanLalu; }
            set
            {
                _nama2BulanLalu = value;
                OnPropertyChanged();
            }
        }

        private string _nama3BulanLalu;

        public string Nama3BulanLalu
        {
            get { return _nama3BulanLalu; }
            set
            {
                _nama3BulanLalu = value;
                OnPropertyChanged();
            }
        }

        private decimal? _ratarata3bulan = 0;

        public decimal? Ratarata3bulan
        {
            get { return _ratarata3bulan; }
            set
            {
                _ratarata3bulan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _persentaseBulanIni = 0;

        public decimal? PersentaseBulanIni
        {
            get { return _persentaseBulanIni; }
            set
            {
                _persentaseBulanIni = value;
                OnPropertyChanged();
            }
        }

        private ParamRekeningAirFilterDto _rekeningFilter = new ParamRekeningAirFilterDto();

        public ParamRekeningAirFilterDto RekeningFilter
        {
            get { return _rekeningFilter; }
            set
            {
                _rekeningFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckStatus;

        public bool IsCheckStatus
        {
            get { return _isCheckStatus; }
            set
            {
                _isCheckStatus = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdStatus = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckKolektif;

        public bool IsCheckKolektif
        {
            get { return _isCheckKolektif; }
            set
            {
                _isCheckKolektif = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdKolektif = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckFlag;

        public bool IsCheckFlag
        {
            get { return _isCheckFlag; }
            set
            {
                _isCheckFlag = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdFlag = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckTglBayar;

        public bool IsCheckTglBayar
        {
            get { return _isCheckTglBayar; }
            set
            {
                _isCheckTglBayar = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.TglBayarAwal = null;
                    temp.TglBayarAkhir = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckRekair;

        public bool IsCheckRekair
        {
            get { return _isCheckRekair; }
            set
            {
                _isCheckRekair = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.RekairAwal = null;
                    temp.RekairAkhir = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckPakai;

        public bool IsCheckPakai
        {
            get { return _isCheckPakai; }
            set
            {
                _isCheckPakai = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.PakaiAwal = null;
                    temp.PakaiAkhir = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckKasir;

        public bool IsCheckKasir
        {
            get { return _isCheckKasir; }
            set
            {
                _isCheckKasir = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdUserTransaksi = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckLoket;

        public bool IsCheckLoket
        {
            get { return _isCheckLoket; }
            set
            {
                _isCheckLoket = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdLoket = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckNama;

        public bool IsCheckNama
        {
            get { return _isCheckNama; }
            set
            {
                _isCheckNama = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.Nama = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckNosamb;

        public bool IsCheckNosamb
        {
            get { return _isCheckNosamb; }
            set
            {
                _isCheckNosamb = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.NoSamb = null;
                    RekeningFilter = temp;
                }

                OnPropertyChanged();
            }
        }

        private bool _isCheckNorek;

        public bool IsCheckNorek
        {
            get { return _isCheckNorek; }
            set
            {
                _isCheckNorek = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.NoRekening = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckGolongan;

        public bool IsCheckGolongan
        {
            get { return _isCheckGolongan; }
            set
            {
                _isCheckGolongan = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdGolongan = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckAlamat;

        public bool IsCheckAlamat
        {
            get { return _isCheckAlamat; }
            set
            {
                _isCheckAlamat = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.Alamat = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckWilayah;

        public bool IsCheckWilayah
        {
            get { return _isCheckWilayah; }
            set
            {
                _isCheckWilayah = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdWilayah = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckKelurahan;

        public bool IsCheckKelurahan
        {
            get { return _isCheckKelurahan; }
            set
            {
                _isCheckKelurahan = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdKelurahan = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckRayon;

        public bool IsCheckRayon
        {
            get { return _isCheckRayon; }
            set
            {
                _isCheckRayon = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdRayon = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckBlok;

        public bool IsCheckBlok
        {
            get { return _isCheckBlok; }
            set
            {
                _isCheckBlok = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdBlok = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckCabang;

        public bool IsCheckCabang
        {
            get { return _isCheckCabang; }
            set
            {
                _isCheckCabang = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdBlok = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckMerekWM;

        public bool IsCheckMerekWM
        {
            get { return _isCheckMerekWM; }
            set
            {
                _isCheckMerekWM = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdMerekMeter = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckDiameter;

        public bool IsCheckDiameter
        {
            get { return _isCheckDiameter; }
            set
            {
                _isCheckDiameter = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdDiameter = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckSeriWM;

        public bool IsCheckSeriWM
        {
            get { return _isCheckSeriWM; }
            set
            {
                _isCheckSeriWM = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.NoSeriMeter = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckKondisiWM;

        public bool IsCheckKondisiWM
        {
            get { return _isCheckKondisiWM; }
            set
            {
                _isCheckKondisiWM = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.IdKondisiMeter = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckKelainan;

        public bool IsCheckKelainan
        {
            get { return _isCheckKelainan; }
            set
            {
                _isCheckKelainan = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.Kelainan = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckLainnya;

        public bool IsCheckLainnya
        {
            get { return _isCheckLainnya; }
            set
            {
                _isCheckLainnya = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.Lainnya = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isCheckPetugasBaca;

        public bool IsCheckPetugasBaca
        {
            get { return _isCheckPetugasBaca; }
            set
            {
                _isCheckPetugasBaca = value;
                var temp = RekeningFilter;
                if (!value)
                {
                    temp.PetugasBaca = null;
                }

                RekeningFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isHitungUlangSemua;

        public bool IsHitungUlangSemua
        {
            get { return _isHitungUlangSemua; }
            set
            {
                _isHitungUlangSemua = value;
                OnPropertyChanged();
            }
        }

        private bool _isPerbaruiSemua;

        public bool IsPerbaruiSemua
        {
            get { return _isPerbaruiSemua; }
            set
            {
                _isPerbaruiSemua = value;
                OnPropertyChanged();
            }
        }

        private bool _isLangsungPublish;

        public bool IsLangsungPublish
        {
            get { return _isLangsungPublish; }
            set
            {
                _isLangsungPublish = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirDetailDto _selectedDataDetail;

        public RekeningAirDetailDto SelectedDataDetail
        {
            get => _selectedDataDetail;
            set
            {
                _selectedDataDetail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var listOptionLimit = new int[] { 10, 20, 50, 100, 200, 300, 500, 1000, 2000, 5000, 10000, 200000 };

                var dataListLimit = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in listOptionLimit)
                    dataListLimit.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return dataListLimit;
            }
        }

        private int _limit = 50;

        public int Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabled;

        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage;

        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecord;

        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;

        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool _isLeftPageNumberFillerVisible;

        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;

        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;

        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;

        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;

        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        // koreksi hasil baca form

        private bool _isPakaiChecked = true;

        public bool IsPakaiChecked
        {
            get => _isPakaiChecked;
            set
            {
                _isPakaiChecked = value;

                if (value)
                {
                    IsStanLaluChecked = false;
                    IsStanKiniChecked = false;
                    IsStanAngkatChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isStanLaluChecked;

        public bool IsStanLaluChecked
        {
            get => _isStanLaluChecked;
            set
            {
                _isStanLaluChecked = value;

                if (value)
                {
                    IsPakaiChecked = false;
                    IsStanKiniChecked = false;
                    IsStanAngkatChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isStanKiniChecked;

        public bool IsStanKiniChecked
        {
            get => _isStanKiniChecked;
            set
            {
                _isStanKiniChecked = value;

                if (value)
                {
                    IsPakaiChecked = false;
                    IsStanLaluChecked = false;
                    IsStanAngkatChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isStanAngkatChecked;

        public bool IsStanAngkatChecked
        {
            get => _isStanAngkatChecked;
            set
            {
                _isStanAngkatChecked = value;

                if (value)
                {
                    IsPakaiChecked = false;
                    IsStanLaluChecked = false;
                    IsStanKiniChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isKelainanChecked;

        public bool IsKelainanChecked
        {
            get { return _isKelainanChecked; }
            set
            {
                _isKelainanChecked = value;
                if (!value)
                {
                    SelectedDataKelainan = null;
                }

                OnPropertyChanged();
            }
        }

        private bool _isPetugasBacaChecked;

        public bool IsPetugasBacaChecked
        {
            get { return _isPetugasBacaChecked; }
            set
            {
                _isPetugasBacaChecked = value;
                if (!value)
                {
                    SelectedDataPetugasBaca = null;
                }

                OnPropertyChanged();
            }
        }

        private MasterKelainanDto _koreksiSelectedKelainan;

        public MasterKelainanDto KoreksiSelectedKelainan
        {
            get { return _koreksiSelectedKelainan; }
            set
            {
                _koreksiSelectedKelainan = value;
                OnPropertyChanged();

                if (KoreksiForm != null)
                    KoreksiForm.Kelainan = _koreksiSelectedKelainan?.Kelainan;
            }
        }

        private MasterPetugasBacaDto _koreksiSelectedPetugasBaca;

        public MasterPetugasBacaDto KoreksiSelectedPetugasBaca
        {
            get { return _koreksiSelectedPetugasBaca; }
            set
            {
                _koreksiSelectedPetugasBaca = value;
                OnPropertyChanged();

                if (KoreksiForm != null)
                    KoreksiForm.PetugasBaca = _koreksiSelectedPetugasBaca?.PetugasBaca;
            }
        }

        // end koreksi hasil baca form

        private RekeningAirDto _koreksiForm;

        public RekeningAirDto KoreksiForm
        {
            get { return _koreksiForm; }
            set
            {
                _koreksiForm = value;
                OnPropertyChanged();
            }
        }

        public async Task DetailRekairAsync(RekeningAirDto param)
        {
            RekeningAirDetailList = null;
            if (param is null)
                RekeningAirDetailList = new List<dynamic>();
            else
            {
                var paramDetail = new Dictionary<string, dynamic> { { "IdPelangganAir", param.IdPelangganAir }, { "IdPeriode", param.IdPeriode }, };

                var responseDetail = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-detail", paramDetail));

                if (!responseDetail.IsError)
                {
                    var resultDetail = responseDetail.Data;

                    if (resultDetail.Status && resultDetail.Data != null)
                    {
                        var data = resultDetail.Data.ToObject<ObservableCollection<RekeningAirDetailDto>>();
                        if (data.Count > 0)
                        {
                            var temp = new List<dynamic>
                            {
                                new {Blok = "Blok1", Biaya = data[0].Blok1},
                                new {Blok = "Blok2", Biaya = data[0].Blok2},
                                new {Blok = "Blok3", Biaya = data[0].Blok3},
                                new {Blok = "Blok4", Biaya = data[0].Blok4},
                                new {Blok = "Blok5", Biaya = data[0].Blok5},
                                new {Blok = "Prog1", Biaya = data[0].Prog1},
                                new {Blok = "Prog2", Biaya = data[0].Prog2},
                                new {Blok = "Prog3", Biaya = data[0].Prog3},
                                new {Blok = "Prog4", Biaya = data[0].Prog4},
                                new {Blok = "Prog5", Biaya = data[0].Prog5}
                            };

                            RekeningAirDetailList = temp;
                        }
                        else
                            RekeningAirDetailList = new List<dynamic>();
                    }
                }
            }
        }

        public void InvokeThumbnail()
        {
            UpdateFotoThumbnailEvent?.Invoke();
        }

        public void InvokeUpdateDataGrid()
        {
            UpdateDataGridEvent?.Invoke();
        }

        private int _totalSelectedPenerbitanPelangganList;

        public int TotalSelectedPenerbitanPelangganList
        {
            get => _totalSelectedPenerbitanPelangganList;
            set
            {
                _totalSelectedPenerbitanPelangganList = value;
                OnPropertyChanged();
            }
        }

        private bool _isAllSelectedTerbitkanPelanggan;

        public bool IsAllSelectedTerbitkanPelanggan
        {
            get => _isAllSelectedTerbitkanPelanggan;
            set
            {
                _isAllSelectedTerbitkanPelanggan = value;
                OnPropertyChanged();

                if (PenerbitanPelangganList != null)
                {
                    var temp = new ObservableCollection<RekeningAirWpf>(PenerbitanPelangganList);
                    foreach (var item in temp)
                        item.IsSelected = value;

                    PenerbitanPelangganList = temp;
                    TotalSelectedPenerbitanPelangganList = temp.Where(i => i.IsSelected == true).ToList().Count;
                }
            }
        }

        private bool _isTaksirChecked;

        public bool IsTaksirChecked
        {
            get { return _isTaksirChecked; }
            set
            {
                _isTaksirChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTaksasiChecked;

        public bool IsTaksasiChecked
        {
            get { return _isTaksasiChecked; }
            set
            {
                _isTaksasiChecked = value;
                OnPropertyChanged();
            }
        }

        #region Riwayat Pembayaran

        private int? _yearPembayaran;

        public int? YearPembayaran
        {
            get { return _yearPembayaran; }
            set
            {
                _yearPembayaran = value;
                if (_yearPembayaran.HasValue)
                {
                    OnGetRiwayatPembayaranCommand.Execute(null);
                }

                OnPropertyChanged();
            }
        }

        private bool _isEmptyRiwayatBayar = true;

        public bool IsEmptyRiwayatBayar
        {
            get { return _isEmptyRiwayatBayar; }
            set
            {
                _isEmptyRiwayatBayar = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyRiwayatBayarNonAir = true;

        public bool IsEmptyRiwayatBayarNonAir
        {
            get { return _isEmptyRiwayatBayarNonAir; }
            set
            {
                _isEmptyRiwayatBayarNonAir = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadRiwayatBayar = true;

        public bool IsLoadRiwayatBayar
        {
            get { return _isLoadRiwayatBayar; }
            set
            {
                _isLoadRiwayatBayar = value;
                OnPropertyChanged();
            }
        }

        private bool _isRiwayatTypeAir = true;

        public bool IsRiwayatTypeAir
        {
            get { return _isRiwayatTypeAir; }
            set
            {
                _isRiwayatTypeAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalRekAirRiwayatBayarAir = decimal.Zero;

        public decimal TotalRekAirRiwayatBayarAir
        {
            get { return _totalRekAirRiwayatBayarAir; }
            set
            {
                _totalRekAirRiwayatBayarAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalDendaRiwayatBayarAir = decimal.Zero;

        public decimal TotalDendaRiwayatBayarAir
        {
            get { return _totalDendaRiwayatBayarAir; }
            set
            {
                _totalDendaRiwayatBayarAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalJumlahRiwayatBayarAir = decimal.Zero;

        public decimal TotalJumlahRiwayatBayarAir
        {
            get { return _totalJumlahRiwayatBayarAir; }
            set
            {
                _totalJumlahRiwayatBayarAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalJumlahRiwayatBayarNonAir = decimal.Zero;

        public decimal TotalJumlahRiwayatBayarNonAir
        {
            get { return _totalJumlahRiwayatBayarNonAir; }
            set
            {
                _totalJumlahRiwayatBayarNonAir = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirPelunasanDanPembatalanDto> _pembayaranList;

        public ObservableCollection<RekeningAirPelunasanDanPembatalanDto> PembayaranList
        {
            get { return _pembayaranList; }
            set
            {
                _pembayaranList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningNonAirPelunasanDanPembatalanDto> _pembayaranNonAirList;

        public ObservableCollection<RekeningNonAirPelunasanDanPembatalanDto> PembayaranNonAirList
        {
            get { return _pembayaranNonAirList; }
            set
            {
                _pembayaranNonAirList = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYearPembayaran = new List<int>();

        public List<int> ListYearPembayaran
        {
            get { return _listYearPembayaran; }
            set
            {
                _listYearPembayaran = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private bool _hasVideo;
        public bool HasVideo
        {
            get => _hasVideo;
            set
            {
                _hasVideo = value;
                OnPropertyChanged();
            }
        }

        private bool _hasVideoUlang;
        public bool HasVideoUlang
        {
            get => _hasVideoUlang;
            set
            {
                _hasVideoUlang = value;
                OnPropertyChanged();
            }
        }

        private Uri _fileVideo;
        public Uri FileVideo
        {
            get => _fileVideo;
            set
            {
                _fileVideo = value;
                OnPropertyChanged();
            }
        }

        private bool _isOpenBacaUlang;
        public bool IsOpenBacaUlang
        {
            get => _isOpenBacaUlang;
            set
            {
                _isOpenBacaUlang = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoStan = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoStan
        {
            get => _thumbnailFotoStan;
            set
            {
                _thumbnailFotoStan = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoStanUlang = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoStanUlang
        {
            get => _thumbnailFotoStanUlang;
            set
            {
                _thumbnailFotoStanUlang = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoStan;
        public bool HasFotoStan
        {
            get => _hasFotoStan;
            set
            {
                _hasFotoStan = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoStanUlang;
        public bool HasFotoStanUlang
        {
            get => _hasFotoStanUlang;
            set
            {
                _hasFotoStanUlang = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _fileFotoStan = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage FileFotoStan
        {
            get => _fileFotoStan;
            set
            {
                _fileFotoStan = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoRumah = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoRumah
        {
            get => _thumbnailFotoRumah;
            set
            {
                _thumbnailFotoRumah = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoRumahUlang = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoRumahUlang
        {
            get => _thumbnailFotoRumahUlang;
            set
            {
                _thumbnailFotoRumahUlang = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoRumah;
        public bool HasFotoRumah
        {
            get => _hasFotoRumah;
            set
            {
                _hasFotoRumah = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoRumahUlang;
        public bool HasFotoRumahUlang
        {
            get => _hasFotoRumahUlang;
            set
            {
                _hasFotoRumahUlang = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _fileFotoRumah = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage FileFotoRumah
        {
            get => _fileFotoRumah;
            set
            {
                _fileFotoRumah = value;
                OnPropertyChanged();
            }
        }

        [ExcludeFromCodeCoverage]
        private string GetNameHistoriBulan(int? value, int intervalMonth = 0)
        {
            if (value != null)
            {
                var kodePeriode = DateTime.ParseExact(string.Concat(value, "01"),
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);

                return kodePeriode.AddMonths(intervalMonth).ToString("MMM yyyy", new CultureInfo("id-ID"));
            }

            return "-";
        }
    }
}
