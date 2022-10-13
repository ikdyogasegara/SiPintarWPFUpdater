using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.ProsesTransaksi.DistribusiBarang;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class DistribusiBarangViewModel : VmBase
    {
        public DistribusiBarangViewModel(ProsesTransaksiViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
        {
            _ = _parent;
            OnLoadCommand = new OnLoadCommand(this, _restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, _restApi, _isTest);
        }

        private ObservableCollection<MasterGudangDto> _daftarGudang;
        public ObservableCollection<MasterGudangDto> DaftarGudang
        {
            get { return _daftarGudang; }
            set
            {
                _daftarGudang = value;
                OnPropertyChanged();
            }
        }
    }
}
