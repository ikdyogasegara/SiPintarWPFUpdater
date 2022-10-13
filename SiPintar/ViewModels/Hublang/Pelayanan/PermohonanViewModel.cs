using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Hublang.Pelayanan.Permohonan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class PermohonanViewModel : ViewModelBase
    {
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;
        public PermohonanViewModel(PelayananViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            _isTest = isTest;
            _restApi = restApi;
            DataList = new ObservableCollection<PermohonanWpf>();
            DataPelanggan = new ObservableCollection<MasterPelangganGlobalWpf>();
            DataPermohonan = new ObservableCollection<PermohonanWpf>();
            TipePermohonan = new ObservableCollection<MasterTipePermohonanDto>();
            DataParamPermohonan = new ObservableCollection<ParamPermohonanList>();
            OnLoadCommand = new OnLoadCommand(this, restApi, _isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, _isTest);
            OnOpenAddSpkFormCommand = new OnOpenAddSpkFormCommand(this, restApi, _isTest);
            OnOpenAddRabFormCommand = new OnOpenAddRabFormCommand(this, restApi, _isTest);
            OnOpenAddBaFormCommand = new OnOpenAddBaFormCommand(this, restApi, _isTest);
            OnOpenAddUsulanFormCommand = new OnOpenAddUsulanFormCommand(this, restApi, _isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, _isTest);
            OnOpenEditUsulanFormCommand = new OnOpenEditUsulanFormCommand(this, restApi, _isTest);
            OnOpenEditSpkFormCommand = new OnOpenEditSpkFormCommand(this, restApi, _isTest);
            OnOpenEditBaFormCommand = new OnOpenEditBaFormCommand(this, restApi, _isTest);
            OnOpenBppiFormCommand = new OnOpenBppiFormCommand(this, _isTest);
            OnOpenSpkpFormCommand = new OnOpenSpkpFormCommand(this, _isTest);
            OnOpenEditRabFormCommand = new OnOpenEditRabFormCommand(this, restApi, _isTest);
            OnCariPelangganCommand = new OnCariPelangganCommand(this, restApi, _isTest);
            OnCariPermohonanCommand = new OnCariPermohonanCommand(this, restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, _isTest);
            OnSubmitUsulanFormCommand = new OnSubmitUsulanFormCommand(this, restApi, _isTest);
            OnSubmitSpkFormCommand = new OnSubmitSpkFormCommand(this, restApi, _isTest);
            OnSubmitSpkpFormCommand = new OnSubmitSpkpFormCommand(this, restApi, _isTest);
            OnSubmitRabFormCommand = new OnSubmitRabFormCommand(this, restApi, _isTest);
            OnSubmitBaFormCommand = new OnSubmitBaFormCommand(this, restApi, _isTest);
            OnSubmitRabPerbaruiDataSambBaruFormCommand = new OnSubmitRabPerbaruiDataSambBaruFormCommand(restApi, _isTest);
            OnSubmitBaPerbaruiDataSambBaruFormCommand = new OnSubmitBaPerbaruiDataSambBaruFormCommand(restApi, _isTest);
            OnSubmitBaPerbaruiDataMeteranFormCommand = new OnSubmitBaPerbaruiDataMeteranFormCommand(restApi, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, _isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, _isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, _isTest);
            OnSubmitDeleteUsulanFormCommand = new OnSubmitDeleteUsulanFormCommand(this, restApi, _isTest);
            OnSubmitDeleteSpkFormCommand = new OnSubmitDeleteSpkFormCommand(this, restApi, _isTest);
            OnSubmitDeleteRabFormCommand = new OnSubmitDeleteRabFormCommand(this, restApi, _isTest);
            OnSubmitDeleteBaFormCommand = new OnSubmitDeleteBaFormCommand(this, restApi, _isTest);
            OnSubmitBatalSpkFormCommand = new OnSubmitBatalSpkFormCommand(this, restApi, _isTest);
            OnHitungTarifAirTangkiCommand = new OnHitungTarifAirTangkiCommand(this, restApi, _isTest);
            OnOpenImageCommand = new OnOpenImageCommand(this, _isTest);
            OnCariMaterialCommand = new OnCariMaterialCommand(this, restApi, _isTest);
            OnSubmitBppiFormCommand = new OnSubmitBppiFormCommand(this, restApi, _isTest);
            OnCariOngkosCommand = new OnCariOngkosCommand(this, restApi, _isTest);
            OnBrowseFileBuktiCommand = new OnBrowseFileBuktiCommand(this, _isTest);
            OnOpenBatalSpkFormCommand = new OnOpenBatalSpkFormCommand(this, _isTest);
            OnHitungRabCommand = new OnHitungRabCommand(this, _isTest);

            OnCetakCommandPelangganAir = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbah = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLltt = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelanggan = new OnCetakCommand(restApi, "hublang", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganAirSpk = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbahSpk = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLlttSpk = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelangganSpk = new OnCetakCommand(restApi, "perencanaan", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);

            OnCetakCommandPelangganAirRab = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbahRab = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLlttRab = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelangganRab = new OnCetakCommand(restApi, "perencanaan", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);

            OnCetakCommandPelangganAirBppi = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbahBppi = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLlttBppi = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelangganBppi = new OnCetakCommand(restApi, "perencanaan", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);

            OnCetakCommandPelangganAirBeritaAcara = new OnCetakCommand(restApi, "distribusi", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbahBeritaAcara = new OnCetakCommand(restApi, "distribusi", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLlttBeritaAcara = new OnCetakCommand(restApi, "distribusi", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelangganBeritaAcara = new OnCetakCommand(restApi, "distribusi", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);

            OnCetakCommandPelangganAirBeritaAcaraViewOnly = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbahBeritaAcaraViewOnly = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLlttBeritaAcaraViewOnly = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelangganBeritaAcaraViewOnly = new OnCetakCommand(restApi, "hublang", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);

            OnCetakCommandPelangganAirSpkp = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLimbahSpkp = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-limbah-cetak", "Cetak Permohonan-Pengaduan Pelanggan Limbah", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganLlttSpkp = new OnCetakCommand(restApi, "perencanaan", "permohonan-pelanggan-lltt-cetak", "Cetak Permohonan-Pengaduan Pelanggan L2T2", ErrorHandlingCetak, _isTest);
            OnCetakCommandNonPelangganSpkp = new OnCetakCommand(restApi, "perencanaan", "permohonan-non-pelanggan-cetak", "Cetak Permohonan-Pengaduan Non Pelanggan", ErrorHandlingCetak, _isTest);

            OnCetakCommandPelangganAirUsulan = new OnCetakCommand(restApi, "distribusi", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenAddSpkFormCommand { get; }
        public ICommand OnOpenAddRabFormCommand { get; }
        public ICommand OnOpenAddBaFormCommand { get; }
        public ICommand OnOpenAddUsulanFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenEditUsulanFormCommand { get; }
        public ICommand OnOpenSpkpFormCommand { get; }
        public ICommand OnOpenBppiFormCommand { get; }
        public ICommand OnOpenEditSpkFormCommand { get; }
        public ICommand OnOpenEditBaFormCommand { get; }
        public ICommand OnOpenEditRabFormCommand { get; }
        public ICommand OnCariPelangganCommand { get; }
        public ICommand OnCariPermohonanCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitUsulanFormCommand { get; }
        public ICommand OnSubmitSpkFormCommand { get; }
        public ICommand OnSubmitRabFormCommand { get; }
        public ICommand OnSubmitBaFormCommand { get; }
        public ICommand OnSubmitRabPerbaruiDataSambBaruFormCommand { get; }
        public ICommand OnSubmitBaPerbaruiDataSambBaruFormCommand { get; }
        public ICommand OnSubmitBaPerbaruiDataMeteranFormCommand { get; }
        public ICommand OnSubmitBatalSpkFormCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnSubmitDeleteSpkFormCommand { get; }
        public ICommand OnSubmitDeleteRabFormCommand { get; }
        public ICommand OnSubmitDeleteBaFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteUsulanFormCommand { get; }
        public ICommand OnHitungTarifAirTangkiCommand { get; }
        public ICommand OnCariMaterialCommand { get; }
        public ICommand OnCariOngkosCommand { get; }
        public ICommand OnOpenImageCommand { get; }
        public ICommand OnBrowseFileBuktiCommand { get; }
        public ICommand OnOpenBatalSpkFormCommand { get; }
        public ICommand OnHitungRabCommand { get; }
        public ICommand OnSubmitBppiFormCommand { get; }
        public ICommand OnSubmitSpkpFormCommand { get; }



        public ICommand OnCetakCommandPelangganAir { get; }
        public ICommand OnCetakCommandPelangganLimbah { get; }
        public ICommand OnCetakCommandPelangganLltt { get; }
        public ICommand OnCetakCommandNonPelanggan { get; }
        public ICommand OnCetakCommandPelangganAirSpk { get; }
        public ICommand OnCetakCommandPelangganLimbahSpk { get; }
        public ICommand OnCetakCommandPelangganLlttSpk { get; }
        public ICommand OnCetakCommandNonPelangganSpk { get; }

        public ICommand OnCetakCommandPelangganAirRab { get; }
        public ICommand OnCetakCommandPelangganLimbahRab { get; }
        public ICommand OnCetakCommandPelangganLlttRab { get; }
        public ICommand OnCetakCommandNonPelangganRab { get; }

        public ICommand OnCetakCommandPelangganAirBeritaAcara { get; }
        public ICommand OnCetakCommandPelangganLimbahBeritaAcara { get; }
        public ICommand OnCetakCommandPelangganLlttBeritaAcara { get; }
        public ICommand OnCetakCommandNonPelangganBeritaAcara { get; }

        public ICommand OnCetakCommandPelangganAirBeritaAcaraViewOnly { get; }
        public ICommand OnCetakCommandPelangganLimbahBeritaAcaraViewOnly { get; }
        public ICommand OnCetakCommandPelangganLlttBeritaAcaraViewOnly { get; }
        public ICommand OnCetakCommandNonPelangganBeritaAcaraViewOnly { get; }

        public ICommand OnCetakCommandPelangganAirBppi { get; }
        public ICommand OnCetakCommandPelangganLimbahBppi { get; }
        public ICommand OnCetakCommandPelangganLlttBppi { get; }
        public ICommand OnCetakCommandNonPelangganBppi { get; }

        public ICommand OnCetakCommandPelangganAirSpkp { get; }
        public ICommand OnCetakCommandPelangganLimbahSpkp { get; }
        public ICommand OnCetakCommandPelangganLlttSpkp { get; }
        public ICommand OnCetakCommandNonPelangganSpkp { get; }

        public ICommand OnCetakCommandPelangganAirUsulan { get; }


        private string _identfire = "GlobalRootDialog";
        public string Identfire
        {
            get { return _identfire; }
            set
            {
                _identfire = value;
                OnPropertyChanged();
            }
        }

        private string _fiturName = "Permohonan";
        public string FiturName
        {
            get { return _fiturName; }
            set
            {
                _fiturName = value;
                OnPropertyChanged();

                IsBeritaAcara = value == "Berita Acara View Only";

                switch (value)
                {
                    case "Permohonan":
                        Identfire = "HublangRootDialog";
                        break;
                    case "Pengaduan":
                        Identfire = "HublangRootDialog";
                        break;
                    case "SPK":
                        Identfire = "PerencanaanRootDialog";
                        break;
                    case "RAB":
                        Identfire = "PerencanaanRootDialog";
                        break;
                    case "Berita Acara View Only":
                        Identfire = "DistribusiRootDialog";
                        break;
                    case "Berita Acara":
                        Identfire = "DistribusiRootDialog";
                        break;
                    case "Usulan":
                        Identfire = "DistribusiRootDialog";
                        break;
                }
            }
        }

        private bool _isBeritaAcara;
        public bool IsBeritaAcara
        {
            get { return _isBeritaAcara; }
            set
            {
                _isBeritaAcara = value;
                OnPropertyChanged();
            }
        }

        private string _selectedJenisPelanggan;
        public string SelectedJenisPelanggan
        {
            get { return _selectedJenisPelanggan; }
            set
            {
                _selectedJenisPelanggan = value;
                OnPropertyChanged();

                if (_selectedJenisPelanggan == "Pelanggan Air")
                {
                    IsPelangganAir = true;
                    IsPelangganLimbah = false;
                    IsPelangganLltt = false;
                    IsNonPelanggan = false;
                    EndPoint = "permohonan-pelanggan-air";
                    EndPointPelanggan = "master-pelanggan-air";
                    EndPointPermohonan = "permohonan-pelanggan-air";
                    EndPointSpk = "permohonan-pelanggan-air-spk";
                    EndPointRab = "permohonan-pelanggan-air-rab";
                    EndPointBa = "permohonan-pelanggan-air-berita-acara";
                    EndPointBppi = "permohonan-pelanggan-air-bppi";
                    EndPointSpkp = "permohonan-pelanggan-air-spkp-sppb";
                }
                else if (_selectedJenisPelanggan == "Pelanggan Limbah")
                {
                    IsPelangganAir = false;
                    IsPelangganLimbah = true;
                    IsPelangganLltt = false;
                    IsNonPelanggan = false;
                    EndPoint = "permohonan-pelanggan-limbah";
                    EndPointPelanggan = "master-pelanggan-limbah";
                    EndPointPermohonan = "permohonan-pelanggan-limbah";
                    EndPointSpk = "permohonan-pelanggan-limbah-spk";
                    EndPointRab = "permohonan-pelanggan-limbah-rab";
                    EndPointBa = "permohonan-pelanggan-limbah-berita-acara";
                    EndPointBppi = "permohonan-pelanggan-limbah-bppi";
                    EndPointSpkp = "permohonan-pelanggan-limbah-spkp-sppb";
                }
                else if (_selectedJenisPelanggan == "Pelanggan L2T2")
                {
                    IsPelangganAir = false;
                    IsPelangganLimbah = false;
                    IsPelangganLltt = true;
                    IsNonPelanggan = false;
                    EndPoint = "permohonan-pelanggan-lltt";
                    EndPointPelanggan = "master-pelanggan-lltt";
                    EndPointPermohonan = "permohonan-pelanggan-lltt";
                    EndPointSpk = "permohonan-pelanggan-lltt-spk";
                    EndPointRab = "permohonan-pelanggan-lltt-rab";
                    EndPointBa = "permohonan-pelanggan-lltt-berita-acara";
                    EndPointBppi = "permohonan-pelanggan-lltt-bppi";
                    EndPointSpkp = "permohonan-pelanggan-lltt-spkp-sppb";
                }
                else if (_selectedJenisPelanggan == "Non Pelanggan")
                {
                    IsPelangganAir = false;
                    IsPelangganLimbah = false;
                    IsPelangganLltt = false;
                    IsNonPelanggan = true;
                    EndPoint = "permohonan-non-pelanggan";
                    EndPointPelanggan = null;
                    EndPointPermohonan = "permohonan-non-pelanggan";
                    EndPointSpk = "permohonan-non-pelanggan-spk";
                    EndPointRab = "permohonan-non-pelanggan-rab";
                    EndPointBa = "permohonan-non-pelanggan-berita-acara";
                    EndPointBppi = "permohonan-non-pelanggan-bppi";
                    EndPointSpkp = "permohonan-non-pelanggan-spkp-sppb";
                }

                if (FiturName == "SPK")
                {
                    LabelJudul = $"Surat Perintah Kerja - {_selectedJenisPelanggan}";
                }
                else if (FiturName == "RAB")
                {
                    LabelJudul = $"Rencana Anggaran Biaya - {_selectedJenisPelanggan}";
                }
                else
                {
                    LabelJudul = $"{FiturName} {_selectedJenisPelanggan}";
                }
            }
        }

        private string _labelJudul;
        public string LabelJudul
        {
            get { return _labelJudul; }
            set
            {
                _labelJudul = value;
                OnPropertyChanged();
            }
        }

        private string _endPoint;
        public string EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged();
            }
        }

        private string _endPointSpk;
        public string EndPointSpk
        {
            get { return _endPointSpk; }
            set
            {
                _endPointSpk = value;
                OnPropertyChanged();
            }
        }

        private string _endPointRab;
        public string EndPointRab
        {
            get { return _endPointRab; }
            set
            {
                _endPointRab = value;
                OnPropertyChanged();
            }
        }

        private string _endPointBppi;
        public string EndPointBppi
        {
            get { return _endPointBppi; }
            set
            {
                _endPointBppi = value;
                OnPropertyChanged();
            }
        }

        private string _endPointSpkp;
        public string EndPointSpkp
        {
            get { return _endPointSpkp; }
            set
            {
                _endPointSpkp = value;
                OnPropertyChanged();
            }
        }

        private string _endPointBa;
        public string EndPointBa
        {
            get { return _endPointBa; }
            set
            {
                _endPointBa = value;
                OnPropertyChanged();
            }
        }


        private string _endPointPelanggan;
        public string EndPointPelanggan
        {
            get { return _endPointPelanggan; }
            set
            {
                _endPointPelanggan = value;
                OnPropertyChanged();
            }
        }

        private string _endPointPermohonan;
        public string EndPointPermohonan
        {
            get { return _endPointPermohonan; }
            set
            {
                _endPointPermohonan = value;
                OnPropertyChanged();
            }
        }

        private string _isFor;
        public string IsFor
        {
            get { return _isFor; }
            set
            {
                _isFor = value;
                OnPropertyChanged();

                if (_isFor == "detail")
                {
                    IsCanEdit = false;
                }
                else
                {
                    IsCanEdit = true;
                }
            }
        }

        private bool _isExists;
        public bool IsExists
        {
            get => _isExists;
            set
            {
                _isExists = value; OnPropertyChanged();
            }
        }

        private bool _adaPiutang;
        public bool AdaPiutang
        {
            get => _adaPiutang;
            set
            {
                _adaPiutang = value; OnPropertyChanged();
            }
        }

        private bool _isCanEdit = true;
        public bool IsCanEdit
        {
            get => _isCanEdit;
            set
            {
                _isCanEdit = value; OnPropertyChanged();
            }
        }

        private bool _isPelangganAir;
        public bool IsPelangganAir
        {
            get { return _isPelangganAir; }
            set
            {
                _isPelangganAir = value;
                OnPropertyChanged();
            }
        }

        private bool _isPelangganLimbah;
        public bool IsPelangganLimbah
        {
            get { return _isPelangganLimbah; }
            set
            {
                _isPelangganLimbah = value;
                OnPropertyChanged();
            }
        }

        private bool _isPelangganLltt;
        public bool IsPelangganLltt
        {
            get { return _isPelangganLltt; }
            set
            {
                _isPelangganLltt = value;
                OnPropertyChanged();
            }
        }

        private bool _isNonPelanggan;
        public bool IsNonPelanggan
        {
            get { return _isNonPelanggan; }
            set
            {
                _isNonPelanggan = value;
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
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isLoadingMap;
        public bool IsLoadingMap
        {
            get => _isLoadingMap;
            set { _isLoadingMap = value; OnPropertyChanged(); }
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

        private bool _isLoadingCariPelanggan;
        public bool IsLoadingCariPelanggan
        {
            get { return _isLoadingCariPelanggan; }
            set
            {
                _isLoadingCariPelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isCariPelangganForm = true;
        public bool IsCariPelangganForm
        {
            get { return _isCariPelangganForm; }
            set
            {
                _isCariPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingCariMaterialOngkos;
        public bool IsLoadingCariMaterialOngkos
        {
            get { return _isLoadingCariMaterialOngkos; }
            set
            {
                _isLoadingCariMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        #region enable/disable filter

        private bool _isStatusPermohonanChecked;
        public bool IsStatusPermohonanChecked
        {
            get { return _isStatusPermohonanChecked; }
            set
            {
                _isStatusPermohonanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTarifLimbahChecked;
        public bool IsTarifLimbahChecked
        {
            get { return _isTarifLimbahChecked; }
            set
            {
                _isTarifLimbahChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTarifLlttChecked;
        public bool IsTarifLlttChecked
        {
            get { return _isTarifLlttChecked; }
            set
            {
                _isTarifLlttChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isRayonChecked;
        public bool IsRayonChecked
        {
            get { return _isRayonChecked; }
            set
            {
                _isRayonChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKelurahanChecked;
        public bool IsKelurahanChecked
        {
            get { return _isKelurahanChecked; }
            set
            {
                _isKelurahanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isCabangChecked;
        public bool IsCabangChecked
        {
            get { return _isCabangChecked; }
            set
            {
                _isCabangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNomorRegisterChecked;
        public bool IsNomorRegisterChecked
        {
            get { return _isNomorRegisterChecked; }
            set
            {
                _isNomorRegisterChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNoSambunganChecked;
        public bool IsNoSambunganChecked
        {
            get { return _isNoSambunganChecked; }
            set
            {
                _isNoSambunganChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get { return _isNamaChecked; }
            set
            {
                _isNamaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAlamatChecked;
        public bool IsAlamatChecked
        {
            get { return _isAlamatChecked; }
            set
            {
                _isAlamatChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTanggalInputChecked;
        public bool IsTanggalInputChecked
        {
            get { return _isTanggalInputChecked; }
            set
            {
                _isTanggalInputChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isUserInputChecked;
        public bool IsUserInputChecked
        {
            get { return _isUserInputChecked; }
            set
            {
                _isUserInputChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isUserBeritaAcaraChecked;
        public bool IsUserBeritaAcaraChecked
        {
            get { return _isUserBeritaAcaraChecked; }
            set
            {
                _isUserBeritaAcaraChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAlamatMapFormChecked;
        public bool IsAlamatMapFormChecked
        {
            get { return _isAlamatMapFormChecked; }
            set
            {
                _isAlamatMapFormChecked = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter variable

        private string _filterStatusPermohonan;
        public string FilterStatusPermohonan
        {
            get { return _filterStatusPermohonan; }
            set
            {
                _filterStatusPermohonan = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _filterGolongan;
        public MasterGolonganDto FilterGolongan
        {
            get { return _filterGolongan; }
            set
            {
                _filterGolongan = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLimbahDto _filterTarifLimbah;
        public MasterTarifLimbahDto FilterTarifLimbah
        {
            get { return _filterTarifLimbah; }
            set
            {
                _filterTarifLimbah = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLlttDto _filterTarifLltt;
        public MasterTarifLlttDto FilterTarifLltt
        {
            get { return _filterTarifLltt; }
            set
            {
                _filterTarifLltt = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _filterRayon;
        public MasterRayonDto FilterRayon
        {
            get { return _filterRayon; }
            set
            {
                _filterRayon = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _filterWilayah;
        public MasterWilayahDto FilterWilayah
        {
            get { return _filterWilayah; }
            set
            {
                _filterWilayah = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _filterKelurahan;
        public MasterKelurahanDto FilterKelurahan
        {
            get { return _filterKelurahan; }
            set
            {
                _filterKelurahan = value;
                OnPropertyChanged();
            }
        }

        private MasterCabangDto _filterCabang;
        public MasterCabangDto FilterCabang
        {
            get { return _filterCabang; }
            set
            {
                _filterCabang = value;
                OnPropertyChanged();
            }
        }

        private string _filterNomorRegister;
        public string FilterNomorRegister
        {
            get { return _filterNomorRegister; }
            set
            {
                _filterNomorRegister = value;
                OnPropertyChanged();
            }
        }

        private string _filterNoSambungan;
        public string FilterNoSambungan
        {
            get { return _filterNoSambungan; }
            set
            {
                _filterNoSambungan = value;
                OnPropertyChanged();
            }
        }

        private string _filterNama;
        public string FilterNama
        {
            get { return _filterNama; }
            set
            {
                _filterNama = value;
                OnPropertyChanged();
            }
        }

        private string _filterAlamat;
        public string FilterAlamat
        {
            get { return _filterAlamat; }
            set
            {
                _filterAlamat = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalInputAwal;
        public DateTime? FilterTanggalInputAwal
        {
            get { return _filterTanggalInputAwal; }
            set
            {
                _filterTanggalInputAwal = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalInputAkhir;
        public DateTime? FilterTanggalInputAkhir
        {
            get { return _filterTanggalInputAkhir; }
            set
            {
                _filterTanggalInputAkhir = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _filterUserInput;
        public MasterUserDto FilterUserInput
        {
            get { return _filterUserInput; }
            set
            {
                _filterUserInput = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _filterUserBeritaAcara;
        public MasterUserDto FilterUserBeritaAcara
        {
            get { return _filterUserBeritaAcara; }
            set
            {
                _filterUserBeritaAcara = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter data list
        private ObservableCollection<MasterGolonganDto>? _golonganList = new();
        public ObservableCollection<MasterGolonganDto>? GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();

            }
        }
        public bool LoadMasterGolongan { get => _golonganList == null; }

        private ObservableCollection<MasterDiameterDto>? _diameterList = new();
        public ObservableCollection<MasterDiameterDto>? DiameterList
        {
            get { return _diameterList; }
            set
            {
                _diameterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDiameter));
            }
        }
        public bool LoadMasterDiameter { get => _diameterList == null; }

        private ObservableCollection<MasterAdministrasiLainDto>? _administrasiLainList = new();
        public ObservableCollection<MasterAdministrasiLainDto>? AdministrasiLainList
        {
            get { return _administrasiLainList; }
            set
            {
                _administrasiLainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterAdministrasiLain));
            }
        }
        public bool LoadMasterAdministrasiLain { get => _administrasiLainList == null; }

        private ObservableCollection<MasterPemeliharaanLainDto>? _PemeliharaanLainList = new();
        public ObservableCollection<MasterPemeliharaanLainDto>? PemeliharaanLainList
        {
            get { return _PemeliharaanLainList; }
            set
            {
                _PemeliharaanLainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPemeliharaanLain));
            }
        }
        public bool LoadMasterPemeliharaanLain { get => _PemeliharaanLainList == null; }

        private ObservableCollection<MasterRetribusiLainDto>? _RetribusiLainList = new();
        public ObservableCollection<MasterRetribusiLainDto>? RetribusiLainList
        {
            get { return _RetribusiLainList; }
            set
            {
                _RetribusiLainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterRetribusiLain));
            }
        }
        public bool LoadMasterRetribusiLain { get => _RetribusiLainList == null; }

        private ObservableCollection<MasterTarifLimbahDto>? _tarifLimbahList = new();
        public ObservableCollection<MasterTarifLimbahDto>? TarifLimbahList
        {
            get { return _tarifLimbahList; }
            set
            {
                _tarifLimbahList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterTarifLimbah));
            }
        }
        public bool LoadMasterTarifLimbah { get => _tarifLimbahList == null; }

        private ObservableCollection<MasterTarifLlttDto>? _tarifLlttList = new();
        public ObservableCollection<MasterTarifLlttDto>? TarifLlttList
        {
            get { return _tarifLlttList; }
            set
            {
                _tarifLlttList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterTarifLltt));
            }
        }
        public bool LoadMasterTarifLltt { get => _tarifLlttList == null; }

        private ObservableCollection<MasterRayonDto>? _rayonList = new();
        public ObservableCollection<MasterRayonDto>? RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterRayon));
            }
        }
        public bool LoadMasterRayon { get => _rayonList == null; }

        private ObservableCollection<MasterAreaDto>? _areaList = new();
        public ObservableCollection<MasterAreaDto>? AreaList
        {
            get { return _areaList; }
            set
            {
                _areaList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterArea));
            }
        }
        public bool LoadMasterArea { get => _areaList == null; }

        private ObservableCollection<MasterWilayahDto>? _wilayahList = new();
        public ObservableCollection<MasterWilayahDto>? WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterWilayah));
            }
        }
        public bool LoadMasterWilayah { get => _wilayahList == null; }


        private ObservableCollection<MasterKecamatanDto>? _kecamatanList = new();
        public ObservableCollection<MasterKecamatanDto>? KecamatanList
        {
            get { return _kecamatanList; }
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKecamatan));
            }
        }
        public bool LoadMasterKecamatan { get => _kecamatanList == null; }

        private ObservableCollection<MasterKelurahanDto>? _kelurahanList = new();
        public ObservableCollection<MasterKelurahanDto>? KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKelurahan));
            }
        }
        public bool LoadMasterKelurahan { get => _kelurahanList == null; }

        private ObservableCollection<MasterCabangDto>? _cabangList = new();
        public ObservableCollection<MasterCabangDto>? CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterCabang));
            }
        }
        public bool LoadMasterCabang { get => _cabangList == null; }

        private ObservableCollection<MasterUserDto>? _userList = new();
        public ObservableCollection<MasterUserDto>? UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterUser));
            }
        }
        public bool LoadMasterUser { get => _userList == null; }


        private ObservableCollection<MasterMerekMeterDto>? _merekMeterList = new();
        public ObservableCollection<MasterMerekMeterDto>? MerekMeterList
        {
            get { return _merekMeterList; }
            set
            {
                _merekMeterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterMerekMeter));
            }
        }
        public bool LoadMasterMerekMeter { get => _merekMeterList == null; }


        private ObservableCollection<MasterPekerjaanDto>? _pekerjaanList = new ObservableCollection<MasterPekerjaanDto>();
        public ObservableCollection<MasterPekerjaanDto>? PekerjaanList
        {
            get { return _pekerjaanList; }
            set
            {
                _pekerjaanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPekerjaan));
            }
        }
        public bool LoadMasterPekerjaan { get => _pekerjaanList == null; }

        private ObservableCollection<MasterBlokDto>? _blokList = new();
        public ObservableCollection<MasterBlokDto>? BlokList
        {
            get { return _blokList; }
            set
            {
                _blokList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterBlok));
            }
        }
        public bool LoadMasterBlok { get => _blokList == null; }

        private ObservableCollection<MasterSumberAirDto>? _sumberAirList = new();
        public ObservableCollection<MasterSumberAirDto>? SumberAirList
        {
            get { return _sumberAirList; }
            set
            {
                _sumberAirList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterSumberAir));
            }
        }
        public bool LoadMasterSumberAir { get => _sumberAirList == null; }

        private ObservableCollection<MasterJenisBangunanDto>? _jenisBangunanList = new();
        public ObservableCollection<MasterJenisBangunanDto>? JenisBangunanList
        {
            get { return _jenisBangunanList; }
            set
            {
                _jenisBangunanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterJenisBangunan));
            }
        }
        public bool LoadMasterJenisBangunan { get => _jenisBangunanList == null; }

        private ObservableCollection<MasterKepemilikanDto>? _kepemilikanBangunanList = new();
        public ObservableCollection<MasterKepemilikanDto>? KepemilikanBangunanList
        {
            get { return _kepemilikanBangunanList; }
            set
            {
                _kepemilikanBangunanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKepemilikanBangunan));
            }
        }
        public bool LoadMasterKepemilikanBangunan { get => _kepemilikanBangunanList == null; }

        private ObservableCollection<MasterPeruntukanDto>? _peruntukanList = new();
        public ObservableCollection<MasterPeruntukanDto>? PeruntukanList
        {
            get { return _peruntukanList; }
            set
            {
                _peruntukanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPeruntukan));
            }
        }
        public bool LoadMasterPeruntukan { get => _peruntukanList == null; }

        private ObservableCollection<MasterTipePendaftaranSambunganDto>? _tipePendaftaranList = new();
        public ObservableCollection<MasterTipePendaftaranSambunganDto>? TipePendaftaranList
        {
            get { return _tipePendaftaranList; }
            set
            {
                _tipePendaftaranList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterTipePendaftaran));
            }
        }
        public bool LoadMasterTipePendaftaran { get => _tipePendaftaranList == null; }

        private ObservableCollection<MasterDmaDto>? _dmaList = new();
        public ObservableCollection<MasterDmaDto>? DmaList
        {
            get { return _dmaList; }
            set
            {
                _dmaList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDma));
            }
        }
        public bool LoadMasterDma { get => _dmaList == null; }

        private ObservableCollection<MasterDmzDto> _dmzList = new();
        public ObservableCollection<MasterDmzDto> DmzList
        {
            get { return _dmzList; }
            set
            {
                _dmzList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDmz));
            }
        }
        public bool LoadMasterDmz { get => _dmzList == null; }

        private ObservableCollection<MasterPaketDto>? _paketRabList;
        public ObservableCollection<MasterPaketDto>? PaketRabList
        {
            get { return _paketRabList; }
            set
            {
                _paketRabList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPaketRab));
            }
        }
        public bool LoadMasterPaketRab { get => _paketRabList == null; }


        private ObservableCollection<MasterAlasanBatalDto>? _alasanBatalList;
        public ObservableCollection<MasterAlasanBatalDto>? AlasanBatalList
        {
            get { return _alasanBatalList; }
            set
            {
                _alasanBatalList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterAlasanBatal));
            }
        }
        public bool LoadMasterAlasanBatal { get => _alasanBatalList == null; }

        private ObservableCollection<MasterPegawaiDto>? _pegawaiList;
        public ObservableCollection<MasterPegawaiDto>? PegawaiList
        {
            get { return _pegawaiList; }
            set
            {
                _pegawaiList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPegawai));
            }
        }
        public bool LoadMasterPegawai { get => _pegawaiList == null; }

        #endregion

        private PermohonanWpf _formData;
        public PermohonanWpf FormData
        {
            get { return _formData; }
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        private List<ParamPermohonanParameterDto> _formDataDetailParameter;
        public List<ParamPermohonanParameterDto> FormDataDetailParameter
        {
            get { return _formDataDetailParameter; }
            set
            {
                _formDataDetailParameter = value;
                OnPropertyChanged();
            }
        }

        private List<ParamPermohonanBiayaDto> _formDataDetailBiaya;
        public List<ParamPermohonanBiayaDto> FormDataDetailBiaya
        {
            get { return _formDataDetailBiaya; }
            set
            {
                _formDataDetailBiaya = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PermohonanWpf> _dataList;
        public ObservableCollection<PermohonanWpf> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private PermohonanWpf _selectedData;
        public PermohonanWpf SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganGlobalWpf> _dataPelanggan;
        public ObservableCollection<MasterPelangganGlobalWpf> DataPelanggan
        {
            get { return _dataPelanggan; }
            set
            {
                _dataPelanggan = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganGlobalWpf _selectedDataPelanggan;
        public MasterPelangganGlobalWpf SelectedDataPelanggan
        {
            get { return _selectedDataPelanggan; }
            set
            {
                _selectedDataPelanggan = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PermohonanWpf> _dataPermohonan;
        public ObservableCollection<PermohonanWpf> DataPermohonan
        {
            get { return _dataPermohonan; }
            set
            {
                _dataPermohonan = value;
                OnPropertyChanged();
            }
        }

        private PermohonanWpf _selectedDataPermohonan;
        public PermohonanWpf SelectedDataPermohonan
        {
            get { return _selectedDataPermohonan; }
            set
            {
                _selectedDataPermohonan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTipePermohonanDto> _tipePermohonan;
        public ObservableCollection<MasterTipePermohonanDto> TipePermohonan
        {
            get { return _tipePermohonan; }
            set
            {
                _tipePermohonan = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePermohonanDto _selectedTipePermohonan;
        public MasterTipePermohonanDto SelectedTipePermohonan
        {
            get { return _selectedTipePermohonan; }
            set
            {
                _selectedTipePermohonan = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePermohonanDto _selectedTipePermohonanComboBox;
        public MasterTipePermohonanDto SelectedTipePermohonanComboBox
        {
            get { return _selectedTipePermohonanComboBox; }
            set
            {
                _selectedTipePermohonanComboBox = value;
                OnPropertyChanged();
            }
        }





        private MasterPaketDto _selectedPaketRabPipaPersil;
        public MasterPaketDto SelectedPaketRabPipaPersil
        {
            get { return _selectedPaketRabPipaPersil; }
            set
            {
                _selectedPaketRabPipaPersil = value;
                OnPropertyChanged();
            }
        }

        private MasterPaketDto _selectedPaketRabPipaDistribusi;
        public MasterPaketDto SelectedPaketRabPipaDistribusi
        {
            get { return _selectedPaketRabPipaDistribusi; }
            set
            {
                _selectedPaketRabPipaDistribusi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ParamPermohonanList> _dataParamPermohonan;
        public ObservableCollection<ParamPermohonanList> DataParamPermohonan
        {
            get { return _dataParamPermohonan; }
            set
            {
                _dataParamPermohonan = value;
                OnPropertyChanged();
            }
        }

        private MasterMerekMeterDto _merekMeterForm;
        public MasterMerekMeterDto MerekMeterForm
        {
            get { return _merekMeterForm; }
            set
            {
                _merekMeterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterMerekMeterDto _merekMeterForm2;
        public MasterMerekMeterDto MerekMeterForm2
        {
            get { return _merekMeterForm2; }
            set
            {
                _merekMeterForm2 = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _kelurahanForm;
        public MasterKelurahanDto KelurahanForm
        {
            get { return _kelurahanForm; }
            set
            {
                _kelurahanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _rayonForm;
        public MasterRayonDto RayonForm
        {
            get { return _rayonForm; }
            set
            {
                _rayonForm = value;
                OnPropertyChanged();
            }
        }



        private MasterAlasanBatalDto _selectedAlasanBatal;
        public MasterAlasanBatalDto SelectedAlasanBatal
        {
            get { return _selectedAlasanBatal; }
            set
            {
                _selectedAlasanBatal = value;
                OnPropertyChanged();
            }
        }


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

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(10, "10"),
                    new KeyValuePair<int, string>(20, "20"),
                    new KeyValuePair<int, string>(50, "50"),
                    new KeyValuePair<int, string>(100, "100"),
                    new KeyValuePair<int, string>(200, "200"),
                    new KeyValuePair<int, string>(300, "300"),
                    new KeyValuePair<int, string>(500, "500"),
                    new KeyValuePair<int, string>(1000, "1000"),
                };
                return data;
            }
        }

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(20, "20");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitDataPelanggan = new KeyValuePair<int, string>(10, "10");
        public KeyValuePair<int, string> LimitDataPelanggan
        {
            get => _limitDataPelanggan;
            set
            {
                _limitDataPelanggan = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitDataMaterialOngkos = new KeyValuePair<int, string>(10, "10");
        public KeyValuePair<int, string> LimitDataMaterialOngkos
        {
            get => _limitDataMaterialOngkos;
            set
            {
                _limitDataMaterialOngkos = value;
                OnPropertyChanged();
            }
        }


        private ResultKalkulasiAirTangkiDto _hasilHitungRumus;
        public ResultKalkulasiAirTangkiDto HasilHitungRumus
        {
            get { return _hasilHitungRumus; }
            set
            {
                _hasilHitungRumus = value;
                OnPropertyChanged();
            }
        }

        #region prev next page data permohonan
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

        private int _totalPage = 1;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
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
        #endregion

        #region prev next page data pelanggan
        private int _currentPagePelanggan = 1;
        public int CurrentPagePelanggan
        {
            get { return _currentPagePelanggan; }
            set
            {
                _currentPagePelanggan = value;
                OnPropertyChanged();
            }
        }

        private int _totalPagePelanggan = 1;
        public int TotalPagePelanggan
        {
            get { return _totalPagePelanggan; }
            set
            {
                _totalPagePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabledPelanggan;
        public bool IsPreviousButtonEnabledPelanggan
        {
            get { return _isPreviousButtonEnabledPelanggan; }
            set
            {
                _isPreviousButtonEnabledPelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledPelanggan;
        public bool IsNextButtonEnabledPelanggan
        {
            get { return _isNextButtonEnabledPelanggan; }
            set
            {
                _isNextButtonEnabledPelanggan = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecordPelanggan;
        public long TotalRecordPelanggan
        {
            get { return _totalRecordPelanggan; }
            set
            {
                _totalRecordPelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisiblePelanggan;
        public bool IsLeftPageNumberFillerVisiblePelanggan
        {
            get { return _isLeftPageNumberFillerVisiblePelanggan; }
            set
            {
                _isLeftPageNumberFillerVisiblePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisiblePelanggan;
        public bool IsRightPageNumberFillerVisiblePelanggan
        {
            get { return _isRightPageNumberFillerVisiblePelanggan; }
            set
            {
                _isRightPageNumberFillerVisiblePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActivePelanggan;
        public bool IsLeftPageNumberActivePelanggan
        {
            get { return _isLeftPageNumberActivePelanggan; }
            set
            {
                _isLeftPageNumberActivePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActivePelanggan;
        public bool IsRightPageNumberActivePelanggan
        {
            get { return _isRightPageNumberActivePelanggan; }
            set
            {
                _isRightPageNumberActivePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisiblePelanggan;
        public bool IsMiddlePageNumberVisiblePelanggan
        {
            get { return _isMiddlePageNumberVisiblePelanggan; }
            set
            {
                _isMiddlePageNumberVisiblePelanggan = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region prev next page data material dan ongkos
        private int _currentPageMaterialOngkos = 1;
        public int CurrentPageMaterialOngkos
        {
            get { return _currentPageMaterialOngkos; }
            set
            {
                _currentPageMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private int _totalPageMaterialOngkos = 1;
        public int TotalPageMaterialOngkos
        {
            get { return _totalPageMaterialOngkos; }
            set
            {
                _totalPageMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabledMaterialOngkos;
        public bool IsPreviousButtonEnabledMaterialOngkos
        {
            get { return _isPreviousButtonEnabledMaterialOngkos; }
            set
            {
                _isPreviousButtonEnabledMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledMaterialOngkos;
        public bool IsNextButtonEnabledMaterialOngkos
        {
            get { return _isNextButtonEnabledMaterialOngkos; }
            set
            {
                _isNextButtonEnabledMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecordMaterialOngkos;
        public long TotalRecordMaterialOngkos
        {
            get { return _totalRecordMaterialOngkos; }
            set
            {
                _totalRecordMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisibleMaterialOngkos;
        public bool IsLeftPageNumberFillerVisibleMaterialOngkos
        {
            get { return _isLeftPageNumberFillerVisibleMaterialOngkos; }
            set
            {
                _isLeftPageNumberFillerVisibleMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisibleMaterialOngkos;
        public bool IsRightPageNumberFillerVisibleMaterialOngkos
        {
            get { return _isRightPageNumberFillerVisibleMaterialOngkos; }
            set
            {
                _isRightPageNumberFillerVisibleMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActiveMaterialOngkos;
        public bool IsLeftPageNumberActiveMaterialOngkos
        {
            get { return _isLeftPageNumberActiveMaterialOngkos; }
            set
            {
                _isLeftPageNumberActiveMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActiveMaterialOngkos;
        public bool IsRightPageNumberActiveMaterialOngkos
        {
            get { return _isRightPageNumberActiveMaterialOngkos; }
            set
            {
                _isRightPageNumberActiveMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisibleMaterialOngkos;
        public bool IsMiddlePageNumberVisibleMaterialOngkos
        {
            get { return _isMiddlePageNumberVisibleMaterialOngkos; }
            set
            {
                _isMiddlePageNumberVisibleMaterialOngkos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region foto map

        private Uri _fotoBukti1Uri;
        public Uri FotoBukti1Uri
        {
            get => _fotoBukti1Uri;
            set
            {
                _fotoBukti1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti1;
        public bool IsNewFotoBukti1
        {
            get => _isNewFotoBukti1;
            set
            {
                _isNewFotoBukti1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti1FormChecked;
        public bool IsFotoBukti1FormChecked
        {
            get => _isFotoBukti1FormChecked;
            set
            {
                _isFotoBukti1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti2Uri;
        public Uri FotoBukti2Uri
        {
            get => _fotoBukti2Uri;
            set
            {
                _fotoBukti2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti2;
        public bool IsNewFotoBukti2
        {
            get => _isNewFotoBukti2;
            set
            {
                _isNewFotoBukti2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti2FormChecked;
        public bool IsFotoBukti2FormChecked
        {
            get => _isFotoBukti2FormChecked;
            set
            {
                _isFotoBukti2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti3Uri;
        public Uri FotoBukti3Uri
        {
            get => _fotoBukti3Uri;
            set
            {
                _fotoBukti3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti3;
        public bool IsNewFotoBukti3
        {
            get => _isNewFotoBukti3;
            set
            {
                _isNewFotoBukti3 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti3FormChecked;
        public bool IsFotoBukti3FormChecked
        {
            get => _isFotoBukti3FormChecked;
            set
            {
                _isFotoBukti3FormChecked = value;
                OnPropertyChanged();
            }
        }



        private Uri _fotoBuktiSpk1Uri;
        public Uri FotoBuktiSpk1Uri
        {
            get => _fotoBuktiSpk1Uri;
            set
            {
                _fotoBuktiSpk1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiSpk1FormChecked;
        public bool IsFotoBuktiSpk1FormChecked
        {
            get => _isFotoBuktiSpk1FormChecked;
            set
            {
                _isFotoBuktiSpk1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiSpk1;
        public bool IsNewFotoBuktiSpk1
        {
            get => _isNewFotoBuktiSpk1;
            set
            {
                _isNewFotoBuktiSpk1 = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBuktiSpk2Uri;
        public Uri FotoBuktiSpk2Uri
        {
            get => _fotoBuktiSpk2Uri;
            set
            {
                _fotoBuktiSpk2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiSpk2FormChecked;
        public bool IsFotoBuktiSpk2FormChecked
        {
            get => _isFotoBuktiSpk2FormChecked;
            set
            {
                _isFotoBuktiSpk2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiSpk2;
        public bool IsNewFotoBuktiSpk2
        {
            get => _isNewFotoBuktiSpk2;
            set
            {
                _isNewFotoBuktiSpk2 = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBuktiSpk3Uri;
        public Uri FotoBuktiSpk3Uri
        {
            get => _fotoBuktiSpk3Uri;
            set
            {
                _fotoBuktiSpk3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiSpk3FormChecked;
        public bool IsFotoBuktiSpk3FormChecked
        {
            get => _isFotoBuktiSpk3FormChecked;
            set
            {
                _isFotoBuktiSpk3FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiSpk3;
        public bool IsNewFotoBuktiSpk3
        {
            get => _isNewFotoBuktiSpk3;
            set
            {
                _isNewFotoBuktiSpk3 = value;
                OnPropertyChanged();
            }
        }



        private Uri _fotoDenah1Uri;
        public Uri FotoDenah1Uri
        {
            get => _fotoDenah1Uri;
            set
            {
                _fotoDenah1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoDenah1FormChecked;
        public bool IsFotoDenah1FormChecked
        {
            get => _isFotoDenah1FormChecked;
            set
            {
                _isFotoDenah1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoDenah1;
        public bool IsNewFotoDenah1
        {
            get => _isNewFotoDenah1;
            set
            {
                _isNewFotoDenah1 = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoDenah2Uri;
        public Uri FotoDenah2Uri
        {
            get => _fotoDenah2Uri;
            set
            {
                _fotoDenah2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoDenah2FormChecked;
        public bool IsFotoDenah2FormChecked
        {
            get => _isFotoDenah2FormChecked;
            set
            {
                _isFotoDenah2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoDenah2;
        public bool IsNewFotoDenah2
        {
            get => _isNewFotoDenah2;
            set
            {
                _isNewFotoDenah2 = value;
                OnPropertyChanged();
            }
        }


        private Uri _fotoBuktiRab1Uri;
        public Uri FotoBuktiRab1Uri
        {
            get => _fotoBuktiRab1Uri;
            set
            {
                _fotoBuktiRab1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiRab1FormChecked;
        public bool IsFotoBuktiRab1FormChecked
        {
            get => _isFotoBuktiRab1FormChecked;
            set
            {
                _isFotoBuktiRab1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiRab1;
        public bool IsNewFotoBuktiRab1
        {
            get => _isNewFotoBuktiRab1;
            set
            {
                _isNewFotoBuktiRab1 = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBuktiRab2Uri;
        public Uri FotoBuktiRab2Uri
        {
            get => _fotoBuktiRab2Uri;
            set
            {
                _fotoBuktiRab2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiRab2FormChecked;
        public bool IsFotoBuktiRab2FormChecked
        {
            get => _isFotoBuktiRab2FormChecked;
            set
            {
                _isFotoBuktiRab2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiRab2;
        public bool IsNewFotoBuktiRab2
        {
            get => _isNewFotoBuktiRab2;
            set
            {
                _isNewFotoBuktiRab2 = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBuktiRab3Uri;
        public Uri FotoBuktiRab3Uri
        {
            get => _fotoBuktiRab3Uri;
            set
            {
                _fotoBuktiRab3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiRab3FormChecked;
        public bool IsFotoBuktiRab3FormChecked
        {
            get => _isFotoBuktiRab3FormChecked;
            set
            {
                _isFotoBuktiRab3FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiRab3;
        public bool IsNewFotoBuktiRab3
        {
            get => _isNewFotoBuktiRab3;
            set
            {
                _isNewFotoBuktiRab3 = value;
                OnPropertyChanged();
            }
        }





        private Uri _fotoBuktiBeritaAcara1Uri;
        public Uri FotoBuktiBeritaAcara1Uri
        {
            get => _fotoBuktiBeritaAcara1Uri;
            set
            {
                _fotoBuktiBeritaAcara1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiBeritaAcara1FormChecked;
        public bool IsFotoBuktiBeritaAcara1FormChecked
        {
            get => _isFotoBuktiBeritaAcara1FormChecked;
            set
            {
                _isFotoBuktiBeritaAcara1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiBeritaAcara1;
        public bool IsNewFotoBuktiBeritaAcara1
        {
            get => _isNewFotoBuktiBeritaAcara1;
            set
            {
                _isNewFotoBuktiBeritaAcara1 = value;
                OnPropertyChanged();
            }
        }


        private Uri _fotoBuktiBeritaAcara2Uri;
        public Uri FotoBuktiBeritaAcara2Uri
        {
            get => _fotoBuktiBeritaAcara2Uri;
            set
            {
                _fotoBuktiBeritaAcara2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiBeritaAcara2FormChecked;
        public bool IsFotoBuktiBeritaAcara2FormChecked
        {
            get => _isFotoBuktiBeritaAcara2FormChecked;
            set
            {
                _isFotoBuktiBeritaAcara2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiBeritaAcara2;
        public bool IsNewFotoBuktiBeritaAcara2
        {
            get => _isNewFotoBuktiBeritaAcara2;
            set
            {
                _isNewFotoBuktiBeritaAcara2 = value;
                OnPropertyChanged();
            }
        }


        private Uri _fotoBuktiBeritaAcara3Uri;
        public Uri FotoBuktiBeritaAcara3Uri
        {
            get => _fotoBuktiBeritaAcara3Uri;
            set
            {
                _fotoBuktiBeritaAcara3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBuktiBeritaAcara3FormChecked;
        public bool IsFotoBuktiBeritaAcara3FormChecked
        {
            get => _isFotoBuktiBeritaAcara3FormChecked;
            set
            {
                _isFotoBuktiBeritaAcara3FormChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBuktiBeritaAcara3;
        public bool IsNewFotoBuktiBeritaAcara3
        {
            get => _isNewFotoBuktiBeritaAcara3;
            set
            {
                _isNewFotoBuktiBeritaAcara3 = value;
                OnPropertyChanged();
            }
        }


        private bool _isDenahLokasiFormChecked;
        public bool IsDenahLokasiFormChecked
        {
            get => _isDenahLokasiFormChecked;
            set
            {
                _isDenahLokasiFormChecked = value;
                OnPropertyChanged();
            }
        }

        private string _denahLokasiForm;
        public string DenahLokasiForm
        {
            get => _denahLokasiForm;
            set
            {
                _denahLokasiForm = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _previewFile;
        public BitmapImage PreviewFile
        {
            get => _previewFile;
            set
            {
                _previewFile = value;
                OnPropertyChanged();
            }
        }
        #endregion



        private ObservableCollection<MasterPegawaiDto> _selectedPegawai = new ObservableCollection<MasterPegawaiDto>();
        public ObservableCollection<MasterPegawaiDto> SelectedPegawai
        {
            get { return _selectedPegawai; }
            set
            {
                _selectedPegawai = value;
                OnPropertyChanged();
            }
        }

        private ParamPermohonanSpkDto _formSpk;
        public ParamPermohonanSpkDto FormSpk
        {
            get { return _formSpk; }
            set
            {
                _formSpk = value;
                OnPropertyChanged();
            }
        }

        private ParamPermohonanRabDto _formRab;
        public ParamPermohonanRabDto FormRab
        {
            get { return _formRab; }
            set
            {
                _formRab = value;
                OnPropertyChanged();
            }
        }

        private ParamPermohonanBaDto _formBa;
        public ParamPermohonanBaDto FormBa
        {
            get { return _formBa; }
            set
            {
                _formBa = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailRabWpf> _detailRabPipaPersil = new ObservableCollection<DetailRabWpf>();
        public ObservableCollection<DetailRabWpf> DetailRabPipaPersil
        {
            get { return _detailRabPipaPersil; }
            set
            {
                _detailRabPipaPersil = value;
                OnPropertyChanged();
            }
        }

        private DetailRabWpf _selectedDetailRabPipaPersil;
        public DetailRabWpf SelectedDetailRabPipaPersil
        {
            get { return _selectedDetailRabPipaPersil; }
            set
            {
                _selectedDetailRabPipaPersil = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailRabWpf> _detailRabPipaDistribusi = new ObservableCollection<DetailRabWpf>();
        public ObservableCollection<DetailRabWpf> DetailRabPipaDistribusi
        {
            get { return _detailRabPipaDistribusi; }
            set
            {
                _detailRabPipaDistribusi = value;
                OnPropertyChanged();
            }
        }

        private DetailRabWpf _selectedDetailRabPipaDistribusi;
        public DetailRabWpf SelectedDetailRabPipaDistribusi
        {
            get { return _selectedDetailRabPipaDistribusi; }
            set
            {
                _selectedDetailRabPipaDistribusi = value;
                OnPropertyChanged();
            }
        }

        #region Summary Pipa Persil
        private RincianRab _summaryPipaPersil = new RincianRab();
        public RincianRab SummaryPipaPersil
        {
            get { return _summaryPipaPersil; }
            set
            {
                _summaryPipaPersil = value;
                OnPropertyChanged();
            }
        }

        private RincianRab _summaryPipaPersilTambahan = new RincianRab();
        public RincianRab SummaryPipaPersilTambahan
        {
            get { return _summaryPipaPersilTambahan; }
            set
            {
                _summaryPipaPersilTambahan = value;
                OnPropertyChanged();
            }
        }

        private SummaryRab _rekapRabPipaPersil = new SummaryRab();
        public SummaryRab RekapRabPipaPersil
        {
            get { return _rekapRabPipaPersil; }
            set
            {
                _rekapRabPipaPersil = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Summary Pipa Distribusi
        private RincianRab _summaryPipaDistribusi = new RincianRab();
        public RincianRab SummaryPipaDistribusi
        {
            get { return _summaryPipaDistribusi; }
            set
            {
                _summaryPipaDistribusi = value;
                OnPropertyChanged();
            }
        }

        private RincianRab _summaryPipaDistribusiTambahan = new RincianRab();
        public RincianRab SummaryPipaDistribusiTambahan
        {
            get { return _summaryPipaDistribusiTambahan; }
            set
            {
                _summaryPipaDistribusiTambahan = value;
                OnPropertyChanged();
            }
        }

        private SummaryRab _rekapRabPipaDistribusi = new SummaryRab();
        public SummaryRab RekapRabPipaDistribusi
        {
            get { return _rekapRabPipaDistribusi; }
            set
            {
                _rekapRabPipaDistribusi = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Summary Rekap
        private RincianRab _summaryTotal = new RincianRab();
        public RincianRab SummaryTotal
        {
            get { return _summaryTotal; }
            set
            {
                _summaryTotal = value;
                OnPropertyChanged();
            }
        }

        private SummaryRab _rekapRabTotal = new SummaryRab();
        public SummaryRab RekapRabTotal
        {
            get { return _rekapRabTotal; }
            set
            {
                _rekapRabTotal = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region form tambah material & ongkos

        private string _tipeRab = "Pipa Persil";
        public string TipeRab
        {
            get { return _tipeRab; }
            set
            {
                _tipeRab = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterMaterialDto> _daftarMaterial = new ObservableCollection<MasterMaterialDto>();
        public ObservableCollection<MasterMaterialDto> DaftarMaterial
        {
            get { return _daftarMaterial; }
            set
            {
                _daftarMaterial = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyFormMaterial = true;
        public bool IsEmptyFormMaterial
        {
            get { return _isEmptyFormMaterial; }
            set
            {
                _isEmptyFormMaterial = value;
                OnPropertyChanged();
            }
        }

        private MasterMaterialDto _selectedDaftarMaterial;
        public MasterMaterialDto SelectedDaftarMaterial
        {
            get { return _selectedDaftarMaterial; }
            set
            {
                _selectedDaftarMaterial = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterOngkosDto> _daftarOngkos = new ObservableCollection<MasterOngkosDto>();
        public ObservableCollection<MasterOngkosDto> DaftarOngkos
        {
            get { return _daftarOngkos; }
            set
            {
                _daftarOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyFormOngkos = true;
        public bool IsEmptyFormOngkos
        {
            get { return _isEmptyFormOngkos; }
            set
            {
                _isEmptyFormOngkos = value;
                OnPropertyChanged();
            }
        }

        private MasterOngkosDto _selectedDaftarOngkos;
        public MasterOngkosDto SelectedDaftarOngkos
        {
            get { return _selectedDaftarOngkos; }
            set
            {
                _selectedDaftarOngkos = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private MasterTipePendaftaranSambunganDto _tipePendaftaranForm;
        public MasterTipePendaftaranSambunganDto TipePendaftaranForm
        {
            get => _tipePendaftaranForm;
            set
            {
                _tipePendaftaranForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPekerjaanDto _pekerjaanForm;
        public MasterPekerjaanDto PekerjaanForm
        {
            get { return _pekerjaanForm; }
            set
            {
                _pekerjaanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterBlokDto _blokForm;
        public MasterBlokDto BlokForm
        {
            get => _blokForm;
            set
            {
                _blokForm = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBangunanDto _jenisBangunanForm;
        public MasterJenisBangunanDto JenisBangunanForm
        {
            get => _jenisBangunanForm;
            set
            {
                _jenisBangunanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterKepemilikanDto _kepemilikanBangunanForm;
        public MasterKepemilikanDto KepemilikanBangunanForm
        {
            get => _kepemilikanBangunanForm;
            set
            {
                _kepemilikanBangunanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPeruntukanDto _peruntukanForm;
        public MasterPeruntukanDto PeruntukanForm
        {
            get => _peruntukanForm;
            set
            {
                _peruntukanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterSumberAirDto _sumberAirForm;
        public MasterSumberAirDto SumberAirForm
        {
            get => _sumberAirForm;
            set
            {
                _sumberAirForm = value;
                OnPropertyChanged();
            }
        }


        private MasterGolonganDto _golonganForm;
        public MasterGolonganDto GolonganForm
        {
            get => _golonganForm;
            set
            {
                _golonganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _diameterForm;
        public MasterDiameterDto DiameterForm
        {
            get => _diameterForm;
            set
            {
                _diameterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterAdministrasiLainDto _administrasiLainForm;
        public MasterAdministrasiLainDto AdministrasiLainForm
        {
            get => _administrasiLainForm;
            set
            {
                _administrasiLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPemeliharaanLainDto _pemeliharaanLainForm;
        public MasterPemeliharaanLainDto PemeliharaanLainForm
        {
            get => _pemeliharaanLainForm;
            set
            {
                _pemeliharaanLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterRetribusiLainDto _retribusiLainForm;
        public MasterRetribusiLainDto RetribusiLainForm
        {
            get => _retribusiLainForm;
            set
            {
                _retribusiLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDmaDto _dmaForm;
        public MasterDmaDto DmaForm
        {
            get => _dmaForm;
            set
            {
                _dmaForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDmzDto _dmzForm;
        public MasterDmzDto DmzForm
        {
            get => _dmzForm;
            set
            {
                _dmzForm = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _tanggalBppi;
        public DateTime? TanggalBppi
        {
            get => _tanggalBppi;
            set
            {
                _tanggalBppi = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tanggalSpkp;
        public DateTime? TanggalSpkp
        {
            get => _tanggalSpkp;
            set
            {
                _tanggalSpkp = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _tanggalSppb;
        public DateTime? TanggalSppb
        {
            get => _tanggalSppb;
            set
            {
                _tanggalSppb = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmptyDaftarTagihanPiutang
        {
            get => (_daftarTagihanPiutang ?? new ObservableCollection<TagihanPiutangDistribusi>()).Count == 0;
        }
        public decimal? TotalTagihanPiutang
        {
            get => (_daftarTagihanPiutang ?? new ObservableCollection<TagihanPiutangDistribusi>()).Sum(x => x.Total);
        }

        private ObservableCollection<TagihanPiutangDistribusi> _daftarTagihanPiutang;
        public ObservableCollection<TagihanPiutangDistribusi> DaftarTagihanPiutang
        {
            get => _daftarTagihanPiutang;
            set
            {
                _daftarTagihanPiutang = value ?? new ObservableCollection<TagihanPiutangDistribusi>();
                OnPropertyChanged();
                OnPropertyChanged("TotalTagihanPiutang");
                OnPropertyChanged("IsEmptyDaftarTagihanPiutang");
            }
        }

        public async Task<ObservableCollection<TagihanPiutangDistribusi>> GetDataPiutangAsync(int? IdPelangganAir = null)
        {
            if (IdPelangganAir == null)
            {
                return new ObservableCollection<TagihanPiutangDistribusi>();
            }

            var param = new Dictionary<string, dynamic>()
            {
                { "Tanggal", DateTime.Now.ToString("yyyy-MM-dd") },
                { "ListIdPelangganAir", IdPelangganAir.ToString() }
            };
            var resApi = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/payment-tagihan-by-id-pelanggan-air", param);

            var res = new ObservableCollection<TagihanPiutangDistribusi>();
            if (!resApi.IsError)
            {
                var result = resApi.Data;
                if (result.Status && result.Data != null)
                {
                    var temp = result.Data.ToObject<ObservableCollection<PaymentDto>>();
                    foreach (var item in temp)
                    {
                        if (item.RekeningAir.Count > 0)
                        {
                            res.Add(new TagihanPiutangDistribusi()
                            {
                                TipeTagihan = "TAGIHAN AIR",
                                JumlahTagihan = item.RekeningAir.Count,
                                Total = item.RekeningAir.Sum(x => x.Total) ?? decimal.Zero
                            });
                        }
                        if (item.RekeningNonAir.Count > 0)
                        {
                            res.Add(new TagihanPiutangDistribusi()
                            {
                                TipeTagihan = "TAGIHAN NON AIR",
                                JumlahTagihan = item.RekeningNonAir.Count,
                                Total = item.RekeningNonAir.Sum(x => x.Total) ?? decimal.Zero
                            });
                        }
                        if (item.RekeningLimbah.Count > 0)
                        {
                            res.Add(new TagihanPiutangDistribusi()
                            {
                                TipeTagihan = "TAGIHAN LIMBAH",
                                JumlahTagihan = item.RekeningLimbah.Count,
                                Total = item.RekeningLimbah.Sum(x => x.Biaya) ?? decimal.Zero
                            });
                        }
                        if (item.RekeningLltt.Count > 0)
                        {
                            res.Add(new TagihanPiutangDistribusi()
                            {
                                TipeTagihan = "TAGIHAN L2T2",
                                JumlahTagihan = item.RekeningLltt.Count,
                                Total = item.RekeningLltt.Sum(x => x.Biaya) ?? decimal.Zero
                            });
                        }
                    }
                }
            }

            return res;
        }
    }

    public class ParamPermohonanList
    {
        public string Name { get; set; }
        public List<ParamPermohonanData> Data { get; set; }
    }

    public class ParamPermohonanData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class RincianRab : INotifyPropertyChanged
    {
        private decimal? _biayaBahan = 0;
        public decimal? BiayaBahan
        {
            get { return _biayaBahan; }
            set
            {
                _biayaBahan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _biayaPemasangan = 0;
        public decimal? BiayaPemasangan
        {
            get { return _biayaPemasangan; }
            set
            {
                _biayaPemasangan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _biayaPendaftaran = 0;
        public decimal? BiayaPendaftaran
        {
            get { return _biayaPendaftaran; }
            set
            {
                _biayaPendaftaran = value;
                OnPropertyChanged();
            }
        }

        private decimal? _jaminanLangganan = 0;
        public decimal? JaminanLangganan
        {
            get { return _jaminanLangganan; }
            set
            {
                _jaminanLangganan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _perencanaan = 0;
        public decimal? Perencanaan
        {
            get { return _perencanaan; }
            set
            {
                _perencanaan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _materai = 0;
        public decimal? Materai
        {
            get { return _materai; }
            set
            {
                _materai = value;
                OnPropertyChanged();
            }
        }

        private decimal? _lainnya = 0;
        public decimal? Lainnya
        {
            get { return _lainnya; }
            set
            {
                _lainnya = value;
                OnPropertyChanged();
            }
        }

        private decimal? _ppn = 0;
        public decimal? Ppn
        {
            get { return _ppn; }
            set
            {
                _ppn = value;
                OnPropertyChanged();
            }
        }

        private decimal? _keuntungan = 0;
        public decimal? Keuntungan
        {
            get { return _keuntungan; }
            set
            {
                _keuntungan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _jasaDariBahan = 0;
        public decimal? JasaDariBahan
        {
            get { return _jasaDariBahan; }
            set
            {
                _jasaDariBahan = value;
                OnPropertyChanged();
            }
        }

        private decimal? _total = 0;
        public decimal? Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class SummaryRab : INotifyPropertyChanged
    {
        private decimal? _subTotal = 0;
        public decimal? SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                OnPropertyChanged();
            }
        }

        private decimal? _penyesuaian = 0;
        public decimal? Penyesuaian
        {
            get { return _penyesuaian; }
            set
            {
                _penyesuaian = value;
                OnPropertyChanged();
            }
        }

        private decimal? _hargaNetMaterial = 0;
        public decimal? HargaNetMaterial
        {
            get { return _hargaNetMaterial; }
            set
            {
                _hargaNetMaterial = value;
                OnPropertyChanged();
            }
        }

        private decimal? _hargaNetOngkos = 0;
        public decimal? HargaNetOngkos
        {
            get { return _hargaNetOngkos; }
            set
            {
                _hargaNetOngkos = value;
                OnPropertyChanged();
            }
        }

        private decimal? _hargaNetPaket = 0;
        public decimal? HargaNetPaket
        {
            get { return _hargaNetPaket; }
            set
            {
                _hargaNetPaket = value;
                OnPropertyChanged();
            }
        }

        private decimal? _totalRab = 0;
        public decimal? TotalRab
        {
            get { return _totalRab; }
            set
            {
                _totalRab = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class TagihanPiutangDistribusi
    {
        public string TipeTagihan { get; set; }
        public int JumlahTagihan { get; set; }
        public decimal Total { get; set; } = 0;
    }
}
