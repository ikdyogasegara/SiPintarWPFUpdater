using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Distribusi.Distribusi.GantiMeter.KelainanBacameter;
using SiPintar.Utilities;


namespace SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter
{
    [ExcludeFromCodeCoverage]
    public class KelainanBacameterViewModel : ViewModelBase
    {
        public KelainanBacameterViewModel(GantiMeterViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenSpkSurveiCommand = new OnOpenSpkSurveiCommand(this, restApi);
            OnAddPetugasCommand = new OnAddPetugasCommand(this);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnOpenSpkSurveiCommand { get; }
        public ICommand OnAddPetugasCommand { get; }

        private GantiMeterViewModel _parent;
        public GantiMeterViewModel Parent
        {
            get { return _parent; }
            set { _parent = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _petugasListForm;
        public ObservableCollection<MasterPegawaiDto> PetugasListForm
        {
            get { return _petugasListForm; }
            set { _petugasListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _formPetugasTambahan = new ObservableCollection<MasterPegawaiDto>();
        public ObservableCollection<MasterPegawaiDto> FormPetugasTambahan
        {
            get { return _formPetugasTambahan; }
            set { _formPetugasTambahan = value; OnPropertyChanged(); }
        }


    }
}
