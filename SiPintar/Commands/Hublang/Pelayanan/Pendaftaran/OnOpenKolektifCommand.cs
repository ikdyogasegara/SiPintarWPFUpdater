using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Pendaftaran;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnOpenKolektifCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenKolektifCommand(PendaftaranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.JenisPelangganForm = _viewModel.JenisPelangganList.FirstOrDefault(i => i.Value == "Pelanggan Air");
            _viewModel.KolektifList = new ObservableCollection<CalonPelangganCsvObject>();
            _viewModel.IsEmptyDialog = true;
            _viewModel.FilenameCsvForm = null;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new KolektifFormView(_viewModel), "HublangRootDialog");
        }
    }
}
