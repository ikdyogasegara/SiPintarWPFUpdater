using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin
{
    [ExcludeFromCodeCoverage]
    public class KelainanBacameterNonRutinViewModel : ViewModelBase
    {
        public KelainanBacameterNonRutinViewModel(GantiMeterNonRutinViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;
        }

        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnOpenSpkSurveiCommand { get; }
        public ICommand OnAddPetugasCommand { get; }

        private GantiMeterNonRutinViewModel _parent;
        public GantiMeterNonRutinViewModel Parent
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
