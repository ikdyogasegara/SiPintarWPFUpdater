using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.JenisBarang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class JenisBarangViewModel : VmBase
    {
        public JenisBarangViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
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

        private ObservableCollection<MasterJenisBarangDto> _data;
        public ObservableCollection<MasterJenisBarangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBarangDto _selectedData;
        public MasterJenisBarangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTipeBarangDto> _dataTipeBarang;
        public ObservableCollection<MasterTipeBarangDto> DataTipeBarang
        {
            get { return _dataTipeBarang; }
            set
            {
                _dataTipeBarang = value;
                OnPropertyChanged();
            }
        }

        private MasterTipeBarangDto _filterTipeBarang;
        public MasterTipeBarangDto FilterTipeBarang
        {
            get { return _filterTipeBarang; }
            set
            {
                _filterTipeBarang = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaJenisBarang;
        public string FilterNamaJenisBarang
        {
            get { return _filterNamaJenisBarang; }
            set
            {
                _filterNamaJenisBarang = value;
                OnPropertyChanged();
            }
        }

        private bool _isTipeBarangChecked;
        public bool IsTipeBarangChecked
        {
            get { return _isTipeBarangChecked; }
            set
            {
                _isTipeBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaJenisBarangChecked;
        public bool IsNamaJenisBarangChecked
        {
            get { return _isNamaJenisBarangChecked; }
            set
            {
                _isNamaJenisBarangChecked = value;
                OnPropertyChanged();
            }
        }
    }
}
