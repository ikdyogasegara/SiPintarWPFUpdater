using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Views.Perencanaan.Atribut.Paket.Material
{
    public partial class PaketFormView : UserControl
    {
        private readonly PaketMaterialViewModel _viewModel;
        public PaketFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PaketMaterialViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Paket Material";

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
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditPaketCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddPaketCommand).ExecuteAsync(null));
        }

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
    }
}
