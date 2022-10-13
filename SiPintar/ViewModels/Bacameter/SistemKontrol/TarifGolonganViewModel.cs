using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class TarifGolonganViewModel : ViewModelBase
    {
        public readonly GolonganViewModel _golongan;
        public readonly DiameterViewModel _diameter;
        public TarifGolonganViewModel(IRestApiClientModel restApi)
        {
            _golongan = new GolonganViewModel(this, restApi);
            _diameter = new DiameterViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }

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
                "golongan" => _golongan,
                "diameter" => _diameter,
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
                    case "golongan":
                        ((GolonganViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "diameter":
                        ((DiameterViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var ListOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return data;
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
