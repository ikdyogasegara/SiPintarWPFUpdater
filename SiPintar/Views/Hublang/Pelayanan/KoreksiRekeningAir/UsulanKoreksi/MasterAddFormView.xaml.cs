using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public partial class MasterAddFormView : UserControl
    {
        private readonly UsulanKoreksiViewModel _viewModel;

        public MasterAddFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as UsulanKoreksiViewModel;
            DataContext = _viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            if (_viewModel != null)
            {
                _viewModel.CurrentStep = 1;
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.Parent.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentStep == 1)
                DialogHost.CloseDialogCommand.Execute(null, null);
            else if (_viewModel.CurrentStep == 2)
                _viewModel.CurrentStep = 1;
            else
                _viewModel.CurrentStep = 2;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentStep == 1)
            {
                _viewModel.CurrentStep = 2;
            }
            else if (_viewModel.CurrentStep == 2)
            {
                var isValid = false;
                foreach (var item in _viewModel.PiutangAirList)
                {
                    if (item.IsInputKoreksi == true)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                {
                    ShowAlert();
                    return;
                }

                Proses();
            }
        }

        private void Proses()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null);
        }

        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Silakan Pilih Piutang",
                    "Mohon pilih atau centang rekening piutang yang akan diusulkan.",
                    "warning",
                    "Oke",
                    false,
                    "hublang"
                ), "KoreksiRekeningSubDialog");
        }
    }
}
