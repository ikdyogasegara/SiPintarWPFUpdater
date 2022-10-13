using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Bacameter.Supervisi;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnOpenKoreksiDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenKoreksiDialogCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null || _viewModel.SelectedData.FlagVerifikasi == true)
                return;

            _viewModel.KoreksiForm = new RekeningAirDto()
            {
                IdPelangganAir = _viewModel.SelectedData.IdPelangganAir,
                StanSkrg = _viewModel.SelectedData.StanSkrgWpf,
                StanLalu = _viewModel.SelectedData.StanLaluWpf,
                StanAngkat = _viewModel.SelectedData.StanAngkatWpf,
                Pakai = _viewModel.SelectedData.PakaiWpf,
                IdRayon = _viewModel.SelectedData.IdRayon,
                IdGolongan = _viewModel.SelectedData.IdGolongan,
                Kelainan = _viewModel.SelectedData.KelainanWpf,
                PetugasBaca = _viewModel.SelectedData.PetugasBacaWpf,
                Taksasi = _viewModel.SelectedData.TaksasiWpf,
                Taksir = _viewModel.SelectedData.TaksirWpf
            };

            _viewModel.IsTaksasiPemakaian = false;

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest && _viewModel.SelectedData.IsVerifikasi == false)
                _ = DialogHost.Show(new KoreksiHasilBacaView(_viewModel), "BacameterRootDialog");
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BacameterRootDialog",
                    "Koreksi Hasil Baca",
                    $"{(_viewModel.SelectedData is null ? "Data belum dipilih!" : "Tidak bisa koreksi data yang sudah diverifikasi")}",
                    "warning",
                    moduleName: "bacameter");
            }
        }
    }
}
