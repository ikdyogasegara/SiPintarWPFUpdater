using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.KategoriBarangKeluar;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class KategoriBarangKeluarViewModel : VmBase
    {
        public KategoriBarangKeluarViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
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

        private ObservableCollection<MasterKategoriBarangKeluarDto> _data;
        public ObservableCollection<MasterKategoriBarangKeluarDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterKategoriBarangKeluarDto _selectedData;
        public MasterKategoriBarangKeluarDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaKategoriBarangKeluarChecked;
        public bool IsNamaKategoriBarangKeluarChecked
        {
            get { return _isNamaKategoriBarangKeluarChecked; }
            set
            {
                _isNamaKategoriBarangKeluarChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaKategoriBarangKeluar;
        public string FilterNamaKategoriBarangKeluar
        {
            get { return _filterNamaKategoriBarangKeluar; }
            set
            {
                _filterNamaKategoriBarangKeluar = value;
                OnPropertyChanged();
            }
        }
    }
}
