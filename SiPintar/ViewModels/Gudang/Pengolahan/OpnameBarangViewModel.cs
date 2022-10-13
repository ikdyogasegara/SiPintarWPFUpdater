using System.Collections.ObjectModel;
using System.Windows.Input;
using SiPintar.Commands.Gudang.Pengolahan.OpnameBarangBulanan;
using SiPintar.Helpers.TableHelper;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.Pengolahan
{
    public class OpnameBarangViewModel : ViewModelBase
    {
        public OpnameBarangViewModel(PengolahanViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parent;
            _isLoading = false;
            Data = new TableHelper(
                    url: "master-barang",
                    restApi: restApi,
                    moduleName: "gudang",
                    isTest: isTest
                );

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = Data.RefreshPageCommand;

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
        public ICommand OnRefreshCommand { get; }

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
