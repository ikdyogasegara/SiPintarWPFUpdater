using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.Supplier;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class SupplierViewModel : VmBase
    {
        public SupplierViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
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
        }

        private ObservableCollection<MasterSuplierDto> _data;
        public ObservableCollection<MasterSuplierDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterSuplierDto _selectedData;
        public MasterSuplierDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaCpChecked;
        public bool IsNamaCpChecked
        {
            get { return _isNamaCpChecked; }
            set
            {
                _isNamaCpChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaPerusahaanChecked;
        public bool IsNamaPerusahaanChecked
        {
            get { return _isNamaPerusahaanChecked; }
            set
            {
                _isNamaPerusahaanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isJabatanChecked;
        public bool IsJabatanChecked
        {
            get { return _isJabatanChecked; }
            set
            {
                _isJabatanChecked = value;
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

        private string _filterNamaCp;
        public string FilterNamaCp
        {
            get { return _filterNamaCp; }
            set
            {
                _filterNamaCp = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaPerusahaan;
        public string FilterNamaPerusahaan
        {
            get { return _filterNamaPerusahaan; }
            set
            {
                _filterNamaPerusahaan = value;
                OnPropertyChanged();
            }
        }

        private string _filterJabatan;
        public string FilterJabatan
        {
            get { return _filterJabatan; }
            set
            {
                _filterJabatan = value;
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
    }
}
