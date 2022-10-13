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
    public class OnOpenTaksasiPemakaianDialogCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenTaksasiPemakaianDialogCommand(SupervisiViewModel viewModel, bool isTest = false)
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
                StanSkrg = _viewModel.SelectedData.StanSkrg,
                StanLalu = _viewModel.SelectedData.StanLalu,
                StanAngkat = _viewModel.SelectedData.StanAngkat,
                Pakai = _viewModel.SelectedData.Pakai,
                IdRayon = _viewModel.SelectedData.IdRayon,
                IdGolongan = _viewModel.SelectedData.IdGolongan,
                Kelainan = _viewModel.SelectedData.Kelainan,
                PetugasBaca = _viewModel.SelectedData.PetugasBaca
            };

            _viewModel.IsStanAngkatChecked = _viewModel.KoreksiForm.StanAngkat > 0;
            _viewModel.KoreksiSelectedKelainan = _viewModel.KelainanList.FirstOrDefault(i => i.Kelainan == _viewModel.KoreksiForm.Kelainan);
            _viewModel.KoreksiSelectedPetugasBaca = _viewModel.PetugasBacaList.FirstOrDefault(i => i.PetugasBaca == _viewModel.KoreksiForm.PetugasBaca);
            _viewModel.IsTaksasiPemakaian = true;

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
