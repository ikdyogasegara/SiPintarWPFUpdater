using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut.JenisBangunan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly JenisBangunanViewModel _viewModel;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as JenisBangunanViewModel;
            DataContext = _viewModel;

            Title.Text = $"{(_viewModel.IsEdit ? "Koreksi" : "Tambah")} JenisBangunan";
            BtnSubmit.Content = "Simpan";

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            CheckButton();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitFormCommand).ExecuteAsync(null));
        }

        private void CheckButton()
        {
            BtnSubmit.IsEnabled = true;
            if (string.IsNullOrWhiteSpace(NamaJenisBangunan.Text))
                BtnSubmit.IsEnabled = false;
        }

        private void JenisBangunan_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

    }
}
