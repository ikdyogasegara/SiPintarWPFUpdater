using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Loket;
using SiPintar.Views.Loket.Angsuran.BuatAngsuran;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class OnOpenAddNonAirLainnyaCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddNonAirLainnyaCommand(AngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.CurrentStep = 1;

            _viewModel.NamaPelangganForm = null;
            _viewModel.NoSambForm = null;
            _viewModel.AlamatForm = null;
            _viewModel.SelectedPelanggan = null;
            _viewModel.NoTelpForm = null;
            _viewModel.NoHpForm = null;
            _viewModel.DpMaxForm = 0;

            _viewModel.JumlahPokok = 0;
            _viewModel.JumlahDpMinForm = 0;
            _viewModel.SisaAngsur = 0;
            _viewModel.UangMukaForm = 0;
            _viewModel.TerminForm = 0;
            _viewModel.TglTerminForm = DateTime.Now;
            _viewModel.JenisAngsuran = "NonAirLainnya";

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                _ = DialogHost.Show(new FormNonAirLainnyaView(_viewModel), "LoketRootDialog");
                _ = ((AsyncCommandBase)_viewModel.GetNonAirListCommand).ExecuteAsync(null);
            }
        }
    }
}
