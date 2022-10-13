using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.Posting
{
    public class OnConfirmPostingCommand : AsyncCommandBase
    {

        private readonly PostingViewModel _viewModel;
        private readonly bool _isTest;

        public OnConfirmPostingCommand(PostingViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if (_viewModel.IsLoading || _viewModel.SelectedDataPeriode == null)
                return;

            var SelectedDate = _viewModel.SelectedDataPeriode.NamaPeriode;


            ShowDialog(SelectedDate);

            await Task.FromResult(true);
        }


        [ExcludeFromCodeCoverage]
        private void ValidasiPostingUlang()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Catatan) || _viewModel.Catatan == "-")
                _ = DialogHost.Show(new DialogGlobalView("Posting Ulang", "Kolom catatan tidak boleh kosong atau - !", "error"), "BillingRootDialog");

        }

        [ExcludeFromCodeCoverage]
        private void PenamaanDialog(IEnumerable<RiwayatPostingDto> note, string SelectedDate)
        {
            if (!_viewModel.IsPostingRekeningAir && !_viewModel.IsPostingRekeningLimbah && !_viewModel.IsPostingRekeningLltt && !_viewModel.IsPostingPelangganAir &&
                !_viewModel.IsPostingPelangganLimbah && !_viewModel.IsPostingPelangganLltt)
                _ = DialogHost.Show(new DialogGlobalView("Posting Data", "Jenis yang mau di posting belum di pilih", "error"), "BillingRootDialog");

            string HeaderInfo = note.Any() ? "Posting Ulang Data DRD & Data Pelanggan" : "Posting Data DRD & Data Pelanggan";
            _ = DialogHost.Show(new DialogGlobalYesCancelView(
                                HeaderInfo,
                                $@"Anda akan memposting {HeaderInfo} periode {SelectedDate}",
                                "confirmation",
                                _viewModel.OnSubmitPostingCommand,
                                "Posting",
                                "Batal",
                                false,
                                true
                            ), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string SelectedDate)
        {

            if (!_isTest)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                var note = _viewModel.RiwayatPosting.Where(x => x.IdPeriode == _viewModel.SelectedDataPeriode?.IdPeriode);
                if (note.Any())
                {
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        ValidasiPostingUlang();
                    });
                }

                App.Current.Dispatcher.Invoke(delegate
                {
                    PenamaanDialog(note, SelectedDate);
                });


            }
        }
    }
}
