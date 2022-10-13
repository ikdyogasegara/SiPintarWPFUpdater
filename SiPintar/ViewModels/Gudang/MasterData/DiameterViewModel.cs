using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.Diameter;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class DiameterViewModel : VmBase
    {
        public DiameterViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
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

        private ObservableCollection<MasterDiameterBarangDto> _data;
        public ObservableCollection<MasterDiameterBarangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterBarangDto _selectedData;
        public MasterDiameterBarangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaDiameterBarangChecked;
        public bool IsNamaDiameterBarangChecked
        {
            get { return _isNamaDiameterBarangChecked; }
            set
            {
                _isNamaDiameterBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaDiameterBarang;
        public string FilterNamaDiameterBarang
        {
            get { return _filterNamaDiameterBarang; }
            set
            {
                _filterNamaDiameterBarang = value;
                OnPropertyChanged();
            }
        }
    }
}
