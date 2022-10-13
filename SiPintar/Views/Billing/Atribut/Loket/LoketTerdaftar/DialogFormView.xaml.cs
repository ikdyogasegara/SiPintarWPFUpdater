using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.Loket;

namespace SiPintar.Views.Billing.Atribut.Loket.LoketTerdaftar
{
    public partial class DialogFormView : UserControl
    {
        private readonly LoketTerdaftarViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (LoketTerdaftarViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Loket";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void Wilayah_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeLoket.Text) || string.IsNullOrEmpty(NamaLoket.Text) ||
                KodeWilayah.SelectedItem == null || NamaWilayah.SelectedItem == null || Status.SelectedItem == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;

            mitra.IsChecked = _viewModel.FlagMitraForm;
            bukanMitra.IsChecked = !_viewModel.FlagMitraForm;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.FlagMitraForm = (bool)((RadioButton)sender).IsChecked && ((RadioButton)sender).Tag.ToString() == "mitra";
        }
    }
}
