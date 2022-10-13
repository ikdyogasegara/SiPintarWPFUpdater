using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public partial class VerifikasiView : UserControl
    {
        public VerifikasiView(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
            KoreksiStan.IsChecked = true;

            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.StatusVerifikasiForm = 1;
            }

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                if (viewModel.StatusVerifikasiForm == 2 && viewModel.AlasanPenolakanForm == null)
                {
                    if (viewModel.Parent.IsBilling)
                    {
                        _ = DialogHost.Show(new DialogGlobalView(
                            "Alasan Penolakan Harus Diisi !",
                            "Mohon tuliskan alasan penolakan verifikasi.",
                            "warning",
                            "Oke",
                            false,
                            "billing"
                        ), "KoreksiRekeningSubDialog");
                    }
                    else
                    {
                        _ = DialogHost.Show(new DialogGlobalView(
                            "Alasan Penolakan Harus Diisi !",
                            "Mohon tuliskan alasan penolakan verifikasi.",
                            "warning",
                            "Oke",
                            false,
                            "hublang"
                        ), "KoreksiRekeningSubDialog");
                    }
                }
                else
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    _ = Task.Run(() => ((AsyncCommandBase)viewModel.OnSubmitVerifikasiCommand).ExecuteAsync(null));
                }
            }
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            KoreksiStanSection.Visibility = current == "KoreksiStan" ? Visibility.Visible : Visibility.Collapsed;
            FotoBuktiSection.Visibility = current == "FotoBukti" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void StatusVerifikasi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel model)
            {
                if (model.StatusVerifikasiForm == 2)
                {
                    AlasanBorder.Visibility = Visibility.Visible;
                    model.AlasanPenolakanForm = null;
                }
                else
                {
                    AlasanBorder.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
