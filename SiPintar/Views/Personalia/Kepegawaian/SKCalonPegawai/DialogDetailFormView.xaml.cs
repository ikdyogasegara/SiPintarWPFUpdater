using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.SKCalonPegawai
{
    public partial class DialogDetailFormView : UserControl
    {
        private readonly SKCalonPegawaiViewModel _viewModel;
        public DialogDetailFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as SKCalonPegawaiViewModel;
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
            if (string.IsNullOrEmpty(Nik.Text))
                OkButton.IsEnabled = false;
            else if (Golongan.SelectedValue == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(MkgThn.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(MkgBln.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

    }
}
