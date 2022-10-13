using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class WilayahAdministrasiViewModel : ViewModelBase
    {
        public readonly RayonViewModel _rayon;
        public readonly KecamatanViewModel _kecamatan;

        public WilayahAdministrasiViewModel(IRestApiClientModel restApi)
        {
            _rayon = new RayonViewModel(this, restApi);
            _kecamatan = new KecamatanViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnExportCommand { get; }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private string _currentSection;
        public string CurrentSection
        {
            get { return _currentSection; }
            set
            {
                _currentSection = value;
                OnPropertyChanged();

                if (_currentSection != null)
                    UpdatePage(_currentSection);
            }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "rayon" => _rayon,
                "kecamatan" => _kecamatan,
                "kelurahan" => _kecamatan,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "rayon":
                        ((RayonViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "kecamatan":
                    case "kelurahan":
                        ((KecamatanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }
    }
}
