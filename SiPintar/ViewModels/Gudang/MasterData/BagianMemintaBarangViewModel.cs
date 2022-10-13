using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.BagianMemintaBarang;
using SiPintar.Utilities;


namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class BagianMemintaBarangViewModel : VmBase
    {
        public BagianMemintaBarangViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
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

        private ObservableCollection<MasterBagianMemintaBarangDto> _data;
        public ObservableCollection<MasterBagianMemintaBarangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterBagianMemintaBarangDto _selectedData;
        public MasterBagianMemintaBarangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaBagianChecked;
        public bool IsNamaBagianChecked
        {
            get { return _isNamaBagianChecked; }
            set
            {
                _isNamaBagianChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isDivisiChecked;
        public bool IsDivisiChecked
        {
            get { return _isDivisiChecked; }
            set
            {
                _isDivisiChecked = value;
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


        private string _filterNamaBagian;
        public string FilterNamaBagian
        {
            get { return _filterNamaBagian; }
            set
            {
                _filterNamaBagian = value;
                OnPropertyChanged();
            }
        }

        private MasterDivisiDto _filterDivisi;
        public MasterDivisiDto FilterDivisi
        {
            get { return _filterDivisi; }
            set
            {
                _filterDivisi = value;
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

        private ObservableCollection<MasterDivisiDto> _daftarDivisi;
        public ObservableCollection<MasterDivisiDto> DaftarDivisi
        {
            get { return _daftarDivisi; }
            set
            {
                _daftarDivisi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _daftarWilayah;
        public ObservableCollection<MasterWilayahDto> DaftarWilayah
        {
            get { return _daftarWilayah; }
            set
            {
                _daftarWilayah = value;
                OnPropertyChanged();
            }
        }
    }
}
