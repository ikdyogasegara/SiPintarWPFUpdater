using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva;
using SiPintar.Utilities;


namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class PersentasePenyusutanAktivaViewModel : ViewModelBase
    {
        public PersentasePenyusutanAktivaViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableFormCommand = new OnSubmitSettingTableFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnOpenAddFormCommand { get; set; }
        public ICommand OnOpenEditFormCommand { get; set; }
        public ICommand OnOpenDeleteFormCommand { get; set; }
        public ICommand OnSubmitDeleteFormCommand { get; set; }
        public ICommand OnOpenSettingTableFormCommand { get; set; }
        public ICommand OnSubmitSettingTableFormCommand { get; set; }
        public ICommand OnSubmitFormCommand { get; set; }


        private object? _tableSetting;
        public object? TableSetting
        {
            get => _tableSetting;
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckTipeAktiva;
        public bool IsCheckTipeAktiva
        {
            get => _isCheckTipeAktiva;
            set
            {
                _isCheckTipeAktiva = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set
            {
                _isAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private MasterPenyusutanAktivaWpf? _selectedData;
        public MasterPenyusutanAktivaWpf? SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ParamMasterPenyusutanAktivaDto? _dumpNewData;
        public ParamMasterPenyusutanAktivaDto? DumpNewData
        {
            get => _dumpNewData;
            set
            {
                _dumpNewData = value;
                OnPropertyChanged();
            }
        }

        private MasterPenyusutanAktivaWpf? _formData;
        public MasterPenyusutanAktivaWpf? FormData
        {
            get => _formData;
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPenyusutanAktivaWpf>? _dataList;
        public ObservableCollection<MasterPenyusutanAktivaWpf>? DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }
    }

    public class MasterPenyusutanAktivaWpf : MasterPenyusutanAktivaDto, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler? PropertyChanged;

        public string KodeGolAktivaWpf
        {
            get => KodeGolAktiva;
            set
            {
                KodeGolAktiva = value;
                OnPropertyChanged();
            }
        }

        public string NamaGolAktivaWpf
        {
            get => NamaGolAktiva;
            set
            {
                NamaGolAktiva = value;
                OnPropertyChanged();
            }
        }

        public int? JmlTahunWpf
        {
            get => JmlTahun;
            set
            {
                JmlTahun = value;
                OnPropertyChanged();
            }
        }

        public decimal? JmlPersenWpf
        {
            get => JmlPersen;
            set
            {
                JmlPersen = value;
                OnPropertyChanged();
            }
        }

        public string KodeSusutWpf
        {
            get => KodeSusut;
            set
            {
                KodeSusut = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewDataWpf;
        public bool IsNewDatawpf
        {
            get => _isNewDataWpf;
            set
            {
                _isNewDataWpf = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
