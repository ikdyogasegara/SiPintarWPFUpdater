using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly SaldoAwalViewModel _viewModel;
        public DialogFormView(SaldoAwalViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SaldoAwalViewModel)DataContext;

            HitungTotal();
            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            //if (string.IsNullOrEmpty(KodeMaterial.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(NamaMaterial.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(Satuan.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(HargaJual.Text))
            //    OkButton.IsEnabled = false;
            //else
            //    OkButton.IsEnabled = true;

        }

        private void Dynamic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Helpers.DecimalValidationHelper.DecimalValidationTextBox(sender, e);
        }

        private void Dynamic_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Helpers.DecimalValidationHelper.Object_GotFocus(sender, e);
            HitungTotal();
        }

        private void Dynamic_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Helpers.DecimalValidationHelper.Object_LostFocus(sender, e);
        }

        private void HitungTotal()
        {
            var total = _viewModel.SaldoAwalForm.SaldoAwalRekon.Sum(x => x.JumlahSaldo ?? 0) + _viewModel.SaldoAwalForm.TotalSaldoKasBank ?? 0;
            TotalSaldoAwal.Text = Helpers.DecimalValidationHelper.FormatDecimalToStringId(total);
        }
    }
}
