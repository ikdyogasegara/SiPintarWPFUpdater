using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenKoreksiRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenKoreksiRekeningCommand(RekeningAirViewModel viewModel, Dictionary<string, dynamic> testBody = null, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesKoreksi == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (AppSetting.LockVerifikasiBacameter == true)
            {
                if (_viewModel.SelectedData is { FlagVerifikasi: true })
                {
                    OpenDialog(parameter as string);
                }
                else
                {
                    OpenDialogWarning();
                }
            }
            else
            {
                if (_viewModel.SelectedData != null)
                {
                    OpenDialog(parameter as string);
                }
            }

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialog(string parameter)
        {
            if (!_isTest)
            {
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
                    Taksasi = parameter == "taksasi pemakaian" || _viewModel.SelectedData.TaksasiWpf == true,
                    Taksir = false,
                    FlagPublish = _viewModel.SelectedData.FlagPublishWpf
                };
                _ = DialogHost.Show(new KoreksiRekeningView(_viewModel), "BillingRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialogWarning()
        {
            if (!_isTest)
            {
                var message = _viewModel.SelectedData.FlagPublishWpf == true
                    ? "Tidak bisa koreksi data yang sudah dipublish"
                    : "Tidak bisa koreksi data yang belum diverifikasi oleh Bacameter";
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Koreksi Rekening",
                    $"{(_viewModel.SelectedData is null ? "Data belum dipilih!" : message)}",
                    "warning",
                    moduleName: "billing");
            }
        }
    }
}
