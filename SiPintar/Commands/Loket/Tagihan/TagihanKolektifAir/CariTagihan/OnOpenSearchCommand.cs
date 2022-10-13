using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Loket.Tagihan;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;
using SiPintar.Views.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.CariTagihan
{
    public class OnOpenSearchCommand : AsyncCommandBase
    {
        private readonly CariTagihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSearchCommand(CariTagihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.FilterStatusPelunasan = "Belum Lunas";
            _viewModel.ListSearchNonAir = null;
            _viewModel.ListSearchPelangganAir = null;
            _viewModel.KolektifForm = null;
            _viewModel.JenisNonAirForm = null;

            OpenDialog(parameter as string);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialog(string jenis)
        {
            if (!_isTest)
            {
                if (jenis == "Pelanggan Air")
                {
                    _ = DialogHost.Show(new SearchDialog(_viewModel), "LoketRootDialog");
                }
                else if (jenis == "Non Air")
                {
                    _ = DialogHost.Show(new SearchDialogNonAir(_viewModel), "LoketRootDialog");
                }
            }
        }
    }
}
