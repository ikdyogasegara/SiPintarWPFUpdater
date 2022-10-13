using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.Barang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class BarangViewModel : VmBase
    {
        public BarangViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
        {
            _ = _parent;
            OnLoadCommand = new OnLoadCommand(this, _restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, _restApi, _isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, _isTest);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, _restApi, _isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, _isTest);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, _restApi, _isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, _isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, _restApi, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
            OnGetFotoBarangCommand = new OnGetFotoBarangCommand(this, _restApi, _isTest);
            OnGetKodeBarangCommand = new OnGetKodeBarangCommand(this, _restApi, _isTest);
            OnPilihFotoBarangCommand = new OnPilihFotoBarangCommand(this, _isTest);
        }

        public ICommand OnGetFotoBarangCommand;
        public ICommand OnGetKodeBarangCommand;
        public ICommand OnPilihFotoBarangCommand;

        private ObservableCollection<MasterBarangDto> _data;
        public ObservableCollection<MasterBarangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangDto _selectedData;
        public MasterBarangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
                if (!string.IsNullOrWhiteSpace(value?.Foto))
                {
                    OnGetFotoBarangCommand.Execute(null);
                }
                else
                {
                    FotoBarangUri = null;
                }
            }
        }

        private string _kodeBarang;
        public string KodeBarang
        {
            get { return _kodeBarang; }
            set
            {
                _kodeBarang = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBarangUri;
        public Uri FotoBarangUri
        {
            get { return _fotoBarangUri; }
            set
            {
                _fotoBarangUri = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBarangFormUri;
        public Uri FotoBarangFormUri
        {
            get { return _fotoBarangFormUri; }
            set
            {
                _fotoBarangFormUri = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterJenisBarangDto> _daftarJenisBarang;
        public ObservableCollection<MasterJenisBarangDto> DaftarJenisBarang
        {
            get { return _daftarJenisBarang; }
            set
            {
                _daftarJenisBarang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDiameterBarangDto> _daftarDiameterBarang;
        public ObservableCollection<MasterDiameterBarangDto> DaftarDiameterBarang
        {
            get { return _daftarDiameterBarang; }
            set
            {
                _daftarDiameterBarang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterSatuanBarangDto> _daftarSatuanBarang;
        public ObservableCollection<MasterSatuanBarangDto> DaftarSatuanBarang
        {
            get { return _daftarSatuanBarang; }
            set
            {
                _daftarSatuanBarang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKodeAkunDto> _daftarKodeAkun;
        public ObservableCollection<MasterKodeAkunDto> DaftarKodeAkun
        {
            get { return _daftarKodeAkun; }
            set
            {
                _daftarKodeAkun = value;
                OnPropertyChanged();
            }
        }

        private bool _isKodeBarangChecked;
        public bool IsKodeBarangChecked
        {
            get { return _isKodeBarangChecked; }
            set
            {
                _isKodeBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isSeriBarangChecked;
        public bool IsSeriBarangChecked
        {
            get { return _isSeriBarangChecked; }
            set
            {
                _isSeriBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaBarangChecked;
        public bool IsNamaBarangChecked
        {
            get { return _isNamaBarangChecked; }
            set
            {
                _isNamaBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isJenisBarangChecked;
        public bool IsJenisBarangChecked
        {
            get { return _isJenisBarangChecked; }
            set
            {
                _isJenisBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isDiameterBarangChecked;
        public bool IsDiameterBarangChecked
        {
            get { return _isDiameterBarangChecked; }
            set
            {
                _isDiameterBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isSatuanBarangChecked;
        public bool IsSatuanBarangChecked
        {
            get { return _isSatuanBarangChecked; }
            set
            {
                _isSatuanBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterKodeBarang;
        public string FilterKodeBarang
        {
            get { return _filterKodeBarang; }
            set
            {
                _filterKodeBarang = value;
                OnPropertyChanged();
            }
        }

        private string _filterSeriBarang;
        public string FilterSeriBarang
        {
            get { return _filterSeriBarang; }
            set
            {
                _filterSeriBarang = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaBarang;
        public string FilterNamaBarang
        {
            get { return _filterNamaBarang; }
            set
            {
                _filterNamaBarang = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBarangDto _filterJenisBarang;
        public MasterJenisBarangDto FilterJenisBarang
        {
            get { return _filterJenisBarang; }
            set
            {
                _filterJenisBarang = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterBarangDto _filterDiameterBarang;
        public MasterDiameterBarangDto FilterDiameterBarang
        {
            get { return _filterDiameterBarang; }
            set
            {
                _filterDiameterBarang = value;
                OnPropertyChanged();
            }
        }

        private MasterSatuanBarangDto _filterSatuanBarang;
        public MasterSatuanBarangDto FilterSatuanBarang
        {
            get { return _filterSatuanBarang; }
            set
            {
                _filterSatuanBarang = value;
                OnPropertyChanged();
            }
        }
    }
}
