using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands.Gudang.Pengolahan.PenyesuaianStock;
using SiPintar.Helpers.TableHelper;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.Pengolahan
{
    public class PenyesuaianStockViewModel : ViewModelBase
    {
        public PenyesuaianStockViewModel(PengolahanViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parent;
            Data = new TableHelper(
                    url: "master-blok",
                    restApi: restApi,
                    moduleName: "gudang"
                );

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddCommand = new OnOpenAddCommand(this, isTest);
            OnOpenCariBarangCommand = new OnOpenCariBarangCommand(this, restApi, isTest);

            StaticBulan = new ObservableCollection<string>
            {
                "Januari",
                "Februari",
                "September"
            };
            StaticTahun = new ObservableCollection<string>
            {
                "2021",
                "2022"
            };
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddCommand { get; }
        public ICommand OnOpenCariBarangCommand { get; }

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

        public TableHelper Data { get; set; }

        public ObservableCollection<string> StaticBulan { get; }
        public ObservableCollection<string> StaticTahun { get; }
    }
}
