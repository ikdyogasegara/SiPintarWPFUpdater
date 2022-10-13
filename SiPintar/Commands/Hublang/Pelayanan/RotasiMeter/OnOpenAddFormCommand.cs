using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.RotasiMeter;

namespace SiPintar.Commands.Hublang.Pelayanan.RotasiMeter
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(RotasiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFor = "add";
            _viewModel.IsCariPelangganForm = true;

            _viewModel.DataPelanggan.Clear();

            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "DistribusiRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            if (string.IsNullOrWhiteSpace(_viewModel.FilterRutin) || _viewModel.IsRutinChecked == false)
            {
                DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest, false,
                    "DistribusiRootDialog",
                    $"Pilih Filter Rutin/Non-Rutin",
                    $"Filter rutin/non-rutin belum dipilih pada bagian filter.",
                    "warning",
                    "Ok",
                    false,
                    "distribusi");
            }
            else
            {
                _viewModel.DataParamPermohonan.Clear();
                _viewModel.KelurahanForm = new MasterKelurahanDto();
                _viewModel.RayonForm = new MasterRayonDto();
                _viewModel.SelectedDataPelanggan = null;

                DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", GetInstance);
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
