using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan
{
    public class PersentaseTarifPajakViewModel : VmBase
    {
        public PersentaseTarifPajakViewModel(MasterDataKeuanganViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableFormCommand = new OnSubmitSettingTableFormCommand(this, isTest);
        }

        private dummydatakeuangan _selectedData;
        public dummydatakeuangan SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private List<dummydatakeuangan> _dataList;
        public List<dummydatakeuangan> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
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

    }

    [ExcludeFromCodeCoverage]
    public class dummydatakeuangan
    {
        public int IdPdam { get; set; }
        public int Id { get; set; }
        public string KodePersen { get; set; }
        public string NamaPersen { get; set; }
        public int JumlahPersen { get; set; }
    }
}
