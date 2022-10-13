using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.DetailTagihan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir
{
    public class DetailTagihanViewModel : ViewModelBase
    {
        public DetailTagihanViewModel(TagihanKolektifAirViewModel parent, TagihanViewModel parentPage, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;
            ParentPage = parentPage;

            OnRefreshCommand = new OnRefreshCommand(this, restApi);
            OnSubmitBayarCommand = new OnSubmitBayarCommand(this, restApi);
            OnCloseDetailTagihanCommand = new OnCloseDetailTagihanCommand(this);
            OnCetakCommand = new OnCetakCommand(restApi, "loket", "payment-cetak", "Cetak Tagihan", ErrorHandlingCetak, isTest);
        }

        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitBayarCommand { get; }
        public ICommand OnCloseDetailTagihanCommand { get; }
        public ICommand OnCetakCommand { get; }


        private TagihanKolektifAirViewModel _parent;
        public TagihanKolektifAirViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private TagihanViewModel _parentPage;
        public TagihanViewModel ParentPage
        {
            get => _parentPage;
            set { _parentPage = value; OnPropertyChanged(); }
        }

        private string _tipe;
        public string Tipe
        {
            get => _tipe;
            set { _tipe = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isAllowedToProcess;
        public bool IsAllowedToProcess
        {
            get => _isAllowedToProcess;
            set { _isAllowedToProcess = value; OnPropertyChanged(); }
        }

        private bool _isCetakKuitansi = true;
        public bool IsCetakKuitansi
        {
            get => _isCetakKuitansi;
            set { _isCetakKuitansi = value; OnPropertyChanged(); }
        }

        private bool _isLihatKuitansiSebelumCetak;
        public bool IsLihatKuitansiSebelumCetak
        {
            get => _isLihatKuitansiSebelumCetak;
            set { _isLihatKuitansiSebelumCetak = value; OnPropertyChanged(); }
        }

        private bool _isEmptyTagihan;
        public bool IsEmptyTagihan
        {
            get => _isEmptyTagihan;
            set { _isEmptyTagihan = value; OnPropertyChanged(); }
        }

        private bool _showSideInfo;
        public bool ShowSideInfo
        {
            get => _showSideInfo;
            set { _showSideInfo = value; OnPropertyChanged(); }
        }

        private List<TagihanGlobalWpf> _tagihan;
        public List<TagihanGlobalWpf> Tagihan
        {
            get => _tagihan;
            set
            {
                _tagihan = value;
                OnPropertyChanged();

                if (_tagihan == null || _tagihan.Count == 0)
                {
                    IsEmptyTagihan = true;
                }
                else
                {
                    IsEmptyTagihan = false;
                }
            }
        }

        private TagihanGlobalWpf _selectedTagihan;
        public TagihanGlobalWpf SelectedTagihan
        {
            get => _selectedTagihan;
            set { _selectedTagihan = value; OnPropertyChanged(); }
        }

        private ListCollectionView _dataGroup;
        public ListCollectionView DataGroup
        {
            get { return _dataGroup; }
            set
            {
                _dataGroup = value;
                OnPropertyChanged();
            }
        }

        private decimal _jumlahBayar = 0;
        public decimal JumlahBayar
        {
            get => _jumlahBayar;
            set { _jumlahBayar = value; OnPropertyChanged(); }
        }

        private RincianTagihanDetail _rincianDetail = new RincianTagihanDetail();
        public RincianTagihanDetail RincianDetail
        {
            get => _rincianDetail;
            set { _rincianDetail = value; OnPropertyChanged(); }
        }

        private bool _isKolektifChecked;
        public bool IsKolektifChecked
        {
            get => _isKolektifChecked;
            set
            {
                _isKolektifChecked = value;
                OnPropertyChanged();

                if (!value)
                {
                    SelectedKolektif = null;
                }
            }
        }

        private MasterKolektifDto _selectedKolektif;
        public MasterKolektifDto SelectedKolektif
        {
            get => _selectedKolektif;
            set
            {
                _selectedKolektif = value;
                OnPropertyChanged();
            }
        }


        private bool _isDibayarPetugas;
        public bool IsDibayarPetugas
        {
            get => _isDibayarPetugas;
            set { _isDibayarPetugas = value; OnPropertyChanged(); }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }

        private RekeningAirPelunasanDanPembatalanDto _dataBuktiHasilBaca;
        public RekeningAirPelunasanDanPembatalanDto DataBuktiHasilBaca
        {
            get { return _dataBuktiHasilBaca; }
            set { _dataBuktiHasilBaca = value; OnPropertyChanged(); }
        }

        private bool _hasFotoHasilBaca;
        public bool HasFotoHasilBaca
        {
            get => _hasFotoHasilBaca;
            set { _hasFotoHasilBaca = value; OnPropertyChanged(); }
        }

        private BitmapImage _fileFotoHasilBaca = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage FileFotoHasilBaca
        {
            get => _fileFotoHasilBaca;
            set { _fileFotoHasilBaca = value; OnPropertyChanged(); }
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

        public bool IsAllSelected
        {
            get
            {
                if (Tagihan == null)
                {
                    return true;
                }
                var totalSelect = Tagihan.Count(x => x.IsSelected);
                return totalSelect == Tagihan.Count;
            }
            set
            {
                if (Tagihan == null)
                {
                    return;
                }

                Tagihan.ForEach(x => x.IsSelected = value);
                SetRincian();
                OnPropertyChanged();
            }
        }

        public decimal GetBiayaPemakaian
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.BiayaPemakaian ?? decimal.Zero);
            }
        }

        public decimal GetAdministrasi
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Administrasi + item.AdministrasiLain ?? decimal.Zero);
            }
        }

        public decimal GetPemeliharaan
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Pemeliharaan + item.PemeliharaanLain ?? decimal.Zero);
            }
        }

        public decimal GetRetribusi
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Retribusi + item.RetribusiLain ?? decimal.Zero);
            }
        }

        public decimal GetPelayanan
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Pelayanan ?? decimal.Zero);
            }
        }

        public decimal GetAirLimbah
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.AirLimbah ?? decimal.Zero);
            }
        }

        public decimal GetDendaPakai0
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.DendaPakai0 ?? decimal.Zero);
            }
        }

        public decimal GetPpn
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Ppn ?? decimal.Zero);
            }
        }

        public decimal GetMeterai
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Meterai ?? decimal.Zero);
            }
        }

        public decimal GetRekAir
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Rekair ?? decimal.Zero);
            }
        }

        public decimal GetDenda
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected).Sum(item => item.Denda ?? decimal.Zero);
            }
        }

        public decimal GetTotalRekeningAir
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected && item.JenisTagihan == "Air").Sum(item => item.Total ?? decimal.Zero);
            }
        }

        public decimal GetLembarTagihan
        {
            get
            {
                return Tagihan?.Count(item => item.IsSelected) ?? decimal.Zero;
            }
        }

        public decimal GetTotalRekeningNonAir
        {
            get
            {
                return Tagihan == null ? decimal.Zero : Tagihan.Where(item => item.IsSelected && item.JenisTagihan == "Non Air").Sum(item => item.Total ?? decimal.Zero);
            }
        }


        public void SetRincian()
        {
            RincianDetail = new RincianTagihanDetail
            {
                BiayaPemakaian = GetBiayaPemakaian,
                Administrasi = GetAdministrasi,
                Pemeliharaan = GetPemeliharaan,
                Retribusi = GetRetribusi,
                Pelayanan = GetPelayanan,
                AirLimbah = GetAirLimbah,
                DendaPakai0 = GetDendaPakai0,
                Ppn = GetPpn,
                Meterai = GetMeterai,
                RekAir = GetRekAir,
                Denda = GetDenda,
                Diskon = 0,

                LembarTagihan = GetLembarTagihan,

                TotalRekeningAir = GetTotalRekeningAir,
                TotalRekeningLimbah = 0,
                TotalRekeningLltt = 0,
                TotalRekeningNonAir = GetTotalRekeningNonAir,
                TotalAngsuranAir = 0,
                TotalAngsuranNonAir = 0,
            };
            DataGroup?.Refresh();
            OnPropertyChanged("IsAllSelected");
        }

        public void CheckUpdate()
        {
            OnPropertyChanged("IsAllSelected");
            SetRincian();
        }
    }
}
