using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public partial class MasterAddFormView : UserControl
    {
        private readonly PermohonanKoreksiViewModel _viewModel;

        public MasterAddFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PermohonanKoreksiViewModel;
            DataContext = _viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            _viewModel.CurrentStep = 1;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.Parent.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PermohonanKoreksiViewModel)DataContext;

            if (viewModel.CurrentStep == 1)
                DialogHost.CloseDialogCommand.Execute(null, null);
            else if (viewModel.CurrentStep == 2)
                viewModel.CurrentStep = 1;
            else
                viewModel.CurrentStep = 2;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PermohonanKoreksiViewModel)DataContext;

            if (viewModel.CurrentStep == 1)
            {
                viewModel.CurrentStep = 2;

                _ = ((AsyncCommandBase)viewModel.GetPiutangListCommand).ExecuteAsync(null);
            }
            else if (viewModel.CurrentStep == 2)
            {
                var isPiutangSelected = viewModel.PiutangAirList.FirstOrDefault(i => i.IsSelected == true) != null;

                if (string.IsNullOrEmpty(_viewModel.AlasanForm) || !isPiutangSelected)
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
            var viewModel = (PermohonanKoreksiViewModel)DataContext;
            _ = ((AsyncCommandBase)viewModel.OnSubmitAddCommand).ExecuteAsync(null);
        }

        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Form Tidak Lengkap",
                    "Mohon Pilih Piutang & Isikan Alasan Koreksi Rekening.",
                    "warning",
                    "Oke",
                    false,
                    "hublang"
                ), "KoreksiRekeningSubDialog");
        }
    }
}
