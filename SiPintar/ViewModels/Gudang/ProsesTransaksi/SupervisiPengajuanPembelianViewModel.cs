using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class SupervisiPengajuanPembelianViewModel : VmBase
    {
        public delegate void CheckSubmitHandler();
        public event CheckSubmitHandler CheckSubmit;

        public SupervisiPengajuanPembelianViewModel(ProsesTransaksiViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
        {
            _ = _parent;
            CheckSubmit = null!;
            _data = null!;
            _dataBarang = null!;
            _dataBarangPengajuanPembelianDetail = null!;
            _dataCabangGudang = null!;
            _dataDetail = null!;
            _dataKategoriBarangMasuk = null!;
            _dataLpb = null!;
            _dataPengajuanPembelian = null!;
            _dataPeriode = null!;
            _dataSuplier = null!;
            _selectedData = null!;
            _selectedDataBarang = null!;
            _selectedDataBarangKoreksiVerif = null!;
            _selectedDataCabangGudang = null!;
            _selectedDataDetail = null!;
            _selectedDataLpb = null!;
            _spk = null!;
            _noSpk = null!;
            _noPengajuan = null!;
            _noOrderPembelian = null!;
            _barangTambahSatuan = null!;
            _orderPembelian = null!;
            _prosesData = null!;
            OnLoadCommand = new OnLoadCommand(this, _restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, _restApi, _isTest);
            OnGetDataPengajuanDetailCommand = new OnGetDataPengajuanDetailCommand(this, _restApi, _isTest);
            OnCariBarangCommand = new OnCariBarangCommand(this, _restApi, _isTest);
            OnTambahBarangPengajuanCommand = new OnTambahBarangPengajuanCommand(this, _isTest);
            OnPindahBarangKeLpbCommand = new OnPindahBarangKeLpbCommand(this, _restApi, _isTest);
            OnPindahBarangKeDpbCommand = new OnPindahBarangKeDpbCommand(this, _restApi, _isTest);
            OnOpenKoreksiHargaFormCommand = new OnOpenKoreksiHargaFormCommand(this, _restApi, _isTest);
            OnSubmitKoreksiHargaCommand = new OnSubmitKoreksiHargaCommand(this, _restApi, _isTest);
            OnOpenTambahBarangSatuanCommand = new OnOpenTambahBarangSatuanCommand(this, _isTest);
            OnTambahBarangSatuanCommand = new OnTambahBarangSatuanCommand(this, _restApi, _isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, _isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, _restApi, _isTest);
            OnOpenVerifikasiBarangCommand = new OnOpenVerifikasiBarangCommand(this, _isTest);
            OnVerifikasiBarangCommand = new OnVerifikasiBarangCommand(this, _restApi, _isTest);
            OnOpenUnVerifikasiBarangCommand = new OnOpenUnVerifikasiBarangCommand(this, _isTest);
            OnUnVerifikasiBarangCommand = new OnUnVerifikasiBarangCommand(this, _restApi, _isTest);
            OnOpenPilihLaporanBarangCommand = new OnOpenPilihLaporanBarangCommand(this, _isTest);
            OnOpenLaporanOrderPembelianCommand = new OnOpenLaporanOrderPembelianCommand(this, _restApi, _isTest);
            OnGenerateNoOrderPembelianCommand = new OnGenerateNoOrderPembelianCommand(this, _restApi, _isTest);
            OnOpenConfirmationOrderPembelianCommand = new OnOpenConfirmationOrderPembelianCommand(this, _isTest);
            OnSubmitOrderPembelianCommand = new OnSubmitOrderPembelianCommand(this, _restApi, _isTest);
            OnOpenLaporanSuratPerjanjianKerjaCommand = new OnOpenLaporanSuratPerjanjianKerjaCommand(this, _restApi, _isTest);
            OnGenerateNoSpkCommand = new OnGenerateNoSpkCommand(this, _restApi, _isTest);
            OnOpenConfirmationSpkCommand = new OnOpenConfirmationSpkCommand(this, _isTest);
            OnSubmitSpkCommand = new OnSubmitSpkCommand(this, _restApi, _isTest);
            OnOpenConfirmationProsesDataCommand = new OnOpenConfirmationProsesDataCommand(this, _isTest);
            OnOpenProsesDataDialogCommand = new OnOpenProsesDataDialogCommand(this, _restApi, _isTest);
            OnOpenConfirmationSubmitProsesDataCommand = new OnOpenConfirmationSubmitProsesDataCommand(this, _isTest);
            OnSubmitProsesDataCommand = new OnSubmitProsesDataCommand(this, _restApi, _isTest);
        }

        #region Commands
        public ICommand OnGetDataPengajuanDetailCommand { get; }
        public ICommand OnCariBarangCommand { get; }
        public ICommand OnTambahBarangPengajuanCommand { get; }
        public ICommand OnPindahBarangKeLpbCommand { get; }
        public ICommand OnPindahBarangKeDpbCommand { get; }
        public ICommand OnOpenKoreksiHargaFormCommand { get; }
        public ICommand OnSubmitKoreksiHargaCommand { get; }
        public ICommand OnOpenTambahBarangSatuanCommand { get; }
        public ICommand OnTambahBarangSatuanCommand { get; }
        public ICommand OnOpenVerifikasiBarangCommand { get; }
        public ICommand OnVerifikasiBarangCommand { get; }
        public ICommand OnOpenUnVerifikasiBarangCommand { get; }
        public ICommand OnUnVerifikasiBarangCommand { get; }
        public ICommand OnOpenPilihLaporanBarangCommand { get; }
        public ICommand OnOpenLaporanOrderPembelianCommand { get; }
        public ICommand OnOpenLaporanSuratPerjanjianKerjaCommand { get; }
        public ICommand OnGenerateNoOrderPembelianCommand { get; }
        public ICommand OnOpenConfirmationOrderPembelianCommand { get; }
        public ICommand OnSubmitOrderPembelianCommand { get; }
        public ICommand OnGenerateNoSpkCommand { get; }
        public ICommand OnOpenConfirmationSpkCommand { get; }
        public ICommand OnSubmitSpkCommand { get; }
        public ICommand OnOpenConfirmationProsesDataCommand { get; }
        public ICommand OnOpenProsesDataDialogCommand { get; }
        public ICommand OnOpenConfirmationSubmitProsesDataCommand { get; }
        public ICommand OnSubmitProsesDataCommand { get; }
        #endregion

        private ObservableCollection<PengajuanPembelianBarangDto> _data;
        public ObservableCollection<PengajuanPembelianBarangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private PengajuanPembelianBarangDto _selectedData;
        public PengajuanPembelianBarangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
                if (value != null)
                {
                    OnGetDataPengajuanDetailCommand.Execute(null);
                }
                else
                {
                    DataDetail?.Clear();
                    DataLpb?.Clear();
                    IsEmptyDetail = true;
                    IsEmptyLpb = true;
                }
            }
        }

        private ObservableCollection<PengajuanPembelianBarangDetailDto> _dataDetail;
        public ObservableCollection<PengajuanPembelianBarangDetailDto> DataDetail
        {
            get { return _dataDetail; }
            set
            {
                _dataDetail = value;
                OnPropertyChanged();
            }
        }

        private PengajuanPembelianBarangDetailDto _selectedDataDetail;
        public PengajuanPembelianBarangDetailDto SelectedDataDetail
        {
            get { return _selectedDataDetail; }
            set
            {
                _selectedDataDetail = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PengajuanPembelianBarangDiterimaDraftDto> _dataLpb;
        public ObservableCollection<PengajuanPembelianBarangDiterimaDraftDto> DataLpb
        {
            get { return _dataLpb; }
            set
            {
                _dataLpb = value;
                OnPropertyChanged();
            }
        }

        private PengajuanPembelianBarangDiterimaDraftDto _selectedDataLpb;
        public PengajuanPembelianBarangDiterimaDraftDto SelectedDataLpb
        {
            get { return _selectedDataLpb; }
            set
            {
                _selectedDataLpb = value;
                OnPropertyChanged();
            }
        }

        private bool _isDeletePengajuan { get; set; }
        public bool IsDeletePengajuan
        {
            get { return _isDeletePengajuan; }
            set
            {
                _isDeletePengajuan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingDetail { get; set; }
        public bool IsLoadingDetail
        {
            get { return _isLoadingDetail; }
            set
            {
                _isLoadingDetail = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyDetail = true;
        public bool IsEmptyDetail
        {
            get { return _isEmptyDetail; }
            set
            {
                _isEmptyDetail = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyLpb = true;
        public bool IsEmptyLpb
        {
            get { return _isEmptyLpb; }
            set
            {
                _isEmptyLpb = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm1 { get; set; }
        public bool IsLoadingForm1
        {
            get { return _isLoadingForm1; }
            set
            {
                _isLoadingForm1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyForm1 = true;
        public bool IsEmptyForm1
        {
            get { return _isEmptyForm1; }
            set
            {
                _isEmptyForm1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterGudangDto> _dataCabangGudang;
        public ObservableCollection<MasterGudangDto> DataCabangGudang
        {
            get { return _dataCabangGudang; }
            set
            {
                _dataCabangGudang = value;
                OnPropertyChanged();
            }
        }

        private MasterGudangDto _selectedDataCabangGudang;
        public MasterGudangDto SelectedDataCabangGudang
        {
            get { return _selectedDataCabangGudang; }
            set
            {
                _selectedDataCabangGudang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKategoriBarangMasukDto> _dataKategoriBarangMasuk;
        public ObservableCollection<MasterKategoriBarangMasukDto> DataKategoriBarangMasuk
        {
            get { return _dataKategoriBarangMasuk; }
            set
            {
                _dataKategoriBarangMasuk = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tanggalPengajuan;
        public DateTime? TanggalPengajuan
        {
            get { return _tanggalPengajuan; }
            set
            {
                _tanggalPengajuan = value;
                OnPropertyChanged();
            }
        }

        private string _noPengajuan;
        public string NoPengajuan
        {
            get { return _noPengajuan; }
            set
            {
                _noPengajuan = value;
                OnPropertyChanged();
            }
        }

        private string _noOrderPembelian;
        public string NoOrderPembelian
        {
            get { return _noOrderPembelian; }
            set
            {
                _noOrderPembelian = value;
                OnPropertyChanged();
            }
        }

        private string _noSpk;
        public string NoSpk
        {
            get { return _noSpk; }
            set
            {
                _noSpk = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBarangDto> _dataBarang;
        public ObservableCollection<MasterBarangDto> DataBarang
        {
            get { return _dataBarang; }
            set
            {
                _dataBarang = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangDto _selectedDataBarang;
        public MasterBarangDto SelectedDataBarang
        {
            get { return _selectedDataBarang; }
            set
            {
                _selectedDataBarang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterSuplierDto> _dataSuplier;
        public ObservableCollection<MasterSuplierDto> DataSuplier
        {
            get { return _dataSuplier; }
            set
            {
                _dataSuplier = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeGudangDto> _dataPeriode;
        public ObservableCollection<MasterPeriodeGudangDto> DataPeriode
        {
            get { return _dataPeriode; }
            set
            {
                _dataPeriode = value;
                OnPropertyChanged();
            }
        }


        private ParamPengajuanPembelianBarangDto _dataPengajuanPembelian;
        public ParamPengajuanPembelianBarangDto DataPengajuanPembelian
        {
            get { return _dataPengajuanPembelian; }
            set
            {
                _dataPengajuanPembelian = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ParamBarangPengajuanPembelianWpf> _dataBarangPengajuanPembelianDetail;
        public ObservableCollection<ParamBarangPengajuanPembelianWpf> DataBarangPengajuanPembelianDetail
        {
            get { return _dataBarangPengajuanPembelianDetail; }
            set
            {
                _dataBarangPengajuanPembelianDetail = value;
                OnPropertyChanged();
                CheckSubmit?.Invoke();
            }
        }

        private bool _isEmptyBarangPengajuanPembelianDetail = true;
        public bool IsEmptyBarangPengajuanPembelianDetail
        {
            get { return _isEmptyBarangPengajuanPembelianDetail; }
            set
            {
                _isEmptyBarangPengajuanPembelianDetail = value;
                OnPropertyChanged();
                CheckSubmit?.Invoke();
            }
        }

        private MasterBarangDto _selectedDataBarangKoreksiVerif;
        public MasterBarangDto SelectedDataBarangKoreksiVerif
        {
            get { return _selectedDataBarangKoreksiVerif; }
            set
            {
                _selectedDataBarangKoreksiVerif = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPembelianBarangTambahBarangWpf _barangTambahSatuan;
        public ParamPengajuanPembelianBarangTambahBarangWpf BarangTambahSatuan
        {
            get { return _barangTambahSatuan; }
            set
            {
                _barangTambahSatuan = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPembelianBarangOPDto _orderPembelian;
        public ParamPengajuanPembelianBarangOPDto OrderPembelian
        {
            get { return _orderPembelian; }
            set
            {
                _orderPembelian = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPembelianBarangSuratPerjanjianDto _spk;
        public ParamPengajuanPembelianBarangSuratPerjanjianDto Spk
        {
            get { return _spk; }
            set
            {
                _spk = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPembelianBarangProsesKePersediaanDto _prosesData;
        public ParamPengajuanPembelianBarangProsesKePersediaanDto ProsesData
        {
            get { return _prosesData; }
            set
            {
                _prosesData = value;
                OnPropertyChanged();
            }
        }
    }

    public class ParamBarangPengajuanPembelianWpf : ParamPengajuanPembelianBarangDetailDto
    {
        public string NamaBarang { get; set; } = "";
        public string SatuanBarang { get; set; } = "";
    }

    public class ParamPengajuanPembelianBarangTambahBarangWpf : ParamPengajuanPembelianBarangTambahBarangDto
    {
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
    }
}
