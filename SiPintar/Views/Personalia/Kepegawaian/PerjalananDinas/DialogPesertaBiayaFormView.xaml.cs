using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas
{
    public partial class DialogPesertaBiayaFormView : UserControl
    {
        private readonly PerjalananDinasViewModel _viewModel;
        public DialogPesertaBiayaFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PerjalananDinasViewModel;
            DataContext = _viewModel;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (KodeBiaya.SelectedValue == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Biaya.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Quantity.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Keterangan.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

    }
}
