using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket.Angsuran
{
    public class AngsuranSambunganBaruViewModel : VmBase
    {
        public AngsuranSambunganBaruViewModel(AngsuranViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = OnLoadCommand;
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this);
            OnOpenPublishAngsuranCommand = new OnOpenPublishAngsuranCommand(this);
            OnOpenUnpublishAngsuranCommand = new OnOpenUnpublishAngsuranCommand(this);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitBeritaAcaraCommand = new OnSubmitBeritaAcaraCommand(this, restApi);
            OnSubmitPublishAngsuranCommand = new OnSubmitPublishAngsuranCommand(this, restApi);
            OnOpenDetailAngsuranCommand = new OnOpenDetailAngsuranCommand(this);
            OnCetakSpaCommand = new OnCetakCommand(restApi, "loket",
                "rekening-angsuran-non-air-cetak", "Cetak Surat Pernyataan Angsuran", ErrorHandlingCetak, isTest);
            OnCetakBeritaAcaraCommand = new OnCetakCommand(restApi, "loket",
                "rekening-angsuran-non-air-cetak", "Cetak Berita Acara Angsuran", ErrorHandlingCetak, isTest);
            OnOpenBeritaAcaraCommand = new OnOpenBeritaAcaraCommand(this, isTest);
        }

        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public new ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnOpenPublishAngsuranCommand { get; }
        public ICommand OnOpenUnpublishAngsuranCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnOpenDetailAngsuranCommand { get; }
        public ICommand OnSubmitPublishAngsuranCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitBeritaAcaraCommand { get; }
        public ICommand OnCetakSpaCommand { get; }
        public ICommand OnCetakBeritaAcaraCommand { get; }
        public ICommand OnOpenBeritaAcaraCommand { get; }

        private AngsuranViewModel _parent;
        public AngsuranViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; OnPropertyChanged(); }
        }

        private bool _isDetailAngsuranVisible { get; set; }
        public bool IsDetailAngsuranVisible
        {
            get { return _isDetailAngsuranVisible; }
            set
            {
                _isDetailAngsuranVisible = value;
                OnPropertyChanged("IsDetailAngsuranVisible");
                Parent.IsDetailAngsuranVisible = value;
                Parent.UpdatePage("DetailAngsuran");
                Parent.CurrentSection = "DetailAngsuran";
                Parent.ParentCurrentSection = "SambunganBaru";
                Parent.DataSelected = SelectedData;
            }
        }

        private ObservableCollection<RekeningNonAirAngsuranDto> _dataList { get; set; }
        public ObservableCollection<RekeningNonAirAngsuranDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledPublish;
        public bool IsEnabledPublish
        {
            get => _isEnabledPublish;
            set { _isEnabledPublish = value; OnPropertyChanged(); }
        }

        private bool _isEnabledBa;
        public bool IsEnabledBa
        {
            get => _isEnabledBa;
            set { _isEnabledBa = value; OnPropertyChanged(); }
        }

        private RekeningNonAirAngsuranDto _selectedData { get; set; }
        public RekeningNonAirAngsuranDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsEnabledPublish = _selectedData != null && !(_selectedData.FlagPublish ?? false);
                IsEnabledBa = _selectedData != null && _selectedData.NoBeritaAcara == null;
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
}
