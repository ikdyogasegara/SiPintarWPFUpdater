using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnOpenPublishListCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenPublishListCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesPublishUnpublish == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.SelectedDataPeriode.IdPeriode == null)
                return;

            if (AppSetting.LockVerifikasiBacameter == true)
            {
                _viewModel.IdPelangganAirList = _viewModel.RekeningAirList.Where(c => c.FlagKoreksiWpf == true && c.FlagVerifikasiWpf == true && c.FlagPublishWpf == false && c.PakaiWpf >= 0).Select(c => c.IdPelangganAir).ToList();
            }
            else
            {
                _viewModel.IdPelangganAirList = _viewModel.RekeningAirList.Where(c => c.FlagKoreksiWpf == true && c.FlagPublishWpf == false && c.PakaiWpf >= 0).Select(c => c.IdPelangganAir).ToList();
            }


            if (_viewModel.IdPelangganAirList.Count == 0)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Publish Rekening Tampil",
                    $"Tidak ada rekening tampil yang siap dipublish",
                    "warning",
                    moduleName: "billing");
                return;
            }
            else
            {
                if (!_isTest) _ = await DialogHost.Show(new KonfirmasiPasswordPublishView(_viewModel), "BillingRootDialog");
            }
        }
    }
}
