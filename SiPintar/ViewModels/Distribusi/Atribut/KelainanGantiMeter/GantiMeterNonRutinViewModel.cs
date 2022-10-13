using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter
{
    public class GantiMeterNonRutinViewModel : VmBase
    {
        public GantiMeterNonRutinViewModel(KelainanGantiMeterViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);

        }

        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitFormCommand { get; }

        private ObservableCollection<MasterJenisGantiMeterDto> _jenisGantiMeterList;
        public ObservableCollection<MasterJenisGantiMeterDto> JenisGantiMeterList
        {
            get { return _jenisGantiMeterList; }
            set { _jenisGantiMeterList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisGantiMeterWpf> _dataGantiMeterList;
        public ObservableCollection<MasterJenisGantiMeterWpf> DataGantiMeterList
        {
            get { return _dataGantiMeterList; }
            set
            {
                _dataGantiMeterList = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisGantiMeterDto _selectedData;
        public MasterJenisGantiMeterDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterWarnaGantiMeterDto> _warnaMeterList;
        public ObservableCollection<MasterWarnaGantiMeterDto> WarnaMeterList
        {
            get { return _warnaMeterList; }
            set { _warnaMeterList = value; OnPropertyChanged(); }
        }

        private MasterWarnaGantiMeterDto _warnaMeter;
        public MasterWarnaGantiMeterDto WarnaMeter
        {
            get { return _warnaMeter; }
            set { _warnaMeter = value; OnPropertyChanged(); }
        }

        private ParamMasterJenisGantiMeterDto _formGantiMeter;
        public ParamMasterJenisGantiMeterDto FormGantiMeter
        {
            get { return _formGantiMeter; }
            set { _formGantiMeter = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisNonAirDto> _jenisNonairList;
        public ObservableCollection<MasterJenisNonAirDto> JenisNonairList
        {
            get { return _jenisNonairList; }
            set { _jenisNonairList = value; OnPropertyChanged(); }
        }

        private MasterJenisNonAirDto _jenisNonair;
        public MasterJenisNonAirDto JenisNonair
        {
            get { return _jenisNonair; }
            set { _jenisNonair = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public new bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }


        private bool _isCheckedDenganBiaya;
        public bool IsCheckedDenganBiaya
        {
            get { return _isCheckedDenganBiaya; }
            set
            {
                _isCheckedDenganBiaya = value;
                OnPropertyChanged();
            }
        }

    }
}
