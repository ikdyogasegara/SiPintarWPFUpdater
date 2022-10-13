using System.Windows.Input;
using SiPintar.Commands.Loket.TutupLoket;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket
{
    public class TutupLoketViewModel : ViewModelBase
    {
        public TutupLoketViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnSubmitFormCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private string _kodeLoket;
        public string KodeLoket
        {
            get => _kodeLoket;
            set { _kodeLoket = value; OnPropertyChanged(); }
        }

        private decimal _penerimaanForm;
        public decimal PenerimaanForm
        {
            get => _penerimaanForm;
            set { _penerimaanForm = value; OnPropertyChanged(); }
        }

        private decimal _kasKecil;
        public decimal KasKecil
        {
            get => _kasKecil;
            set { _kasKecil = value; OnPropertyChanged(); }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }

        private bool _isTutupLoketToday;
        public bool IsTutupLoketToday
        {
            get { return _isTutupLoketToday; }
            set { _isTutupLoketToday = value; OnPropertyChanged(); }
        }
    }
}
