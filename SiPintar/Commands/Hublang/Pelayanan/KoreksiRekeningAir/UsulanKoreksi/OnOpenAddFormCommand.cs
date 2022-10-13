using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(UsulanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.IsLoading || _viewModel.Parent.TipeJenisKoreksiAir == null)
                return;

            _viewModel.IsDetail = false;
            _viewModel.IsEdit = false;

            _viewModel.FotoBukti1PermohonanUri = null;
            _viewModel.FotoBukti2PermohonanUri = null;
            _viewModel.FotoBukti3PermohonanUri = null;
            _viewModel.IsFotoBukti1PermohonanFormChecked = false;
            _viewModel.IsFotoBukti2PermohonanFormChecked = false;
            _viewModel.IsFotoBukti3PermohonanFormChecked = false;

            _viewModel.CurrentStep = 1;

            _viewModel.SelectedPermohonanAir = null;
            _viewModel.SelectedTempPermohonanAir = null;
            _viewModel.NamaPelangganForm = null;
            _viewModel.NoSambForm = null;
            _viewModel.NoPermohonanForm = null;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new MasterAddFormView(_viewModel), "HublangRootDialog");
        }
    }
}
