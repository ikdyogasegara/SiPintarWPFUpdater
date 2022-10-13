using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.TipeBarang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class TipeBarangViewModel : VmBase
    {
        public TipeBarangViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
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

        private ObservableCollection<MasterTipeBarangDto> _data;
        public ObservableCollection<MasterTipeBarangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterTipeBarangDto _selectedData;
        public MasterTipeBarangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaTipeBarangChecked;
        public bool IsNamaTipeBarangChecked
        {
            get { return _isNamaTipeBarangChecked; }
            set
            {
                _isNamaTipeBarangChecked = value;
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
    }
}
