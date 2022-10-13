using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut.JenisNonAir
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private readonly JenisNonAirViewModel _viewModel;

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as JenisNonAirViewModel;
            DataContext = _viewModel;

            Title.Text = $"{(_viewModel.IsEdit ? "Koreksi" : "Tambah")} Jenis Non-Air";
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
            if (string.IsNullOrWhiteSpace(Kode.Text) || string.IsNullOrWhiteSpace(Nama.Text) || string.IsNullOrWhiteSpace(Inisial.Text) || Status.SelectedItem == null)
                BtnSubmit.IsEnabled = false;
            //if (DetailBiaya.Items.Count == 0)
            //    BtnSubmit.IsEnabled = false;
            //else
            //{
            //    for (int i = 0; i < DetailBiaya.Items.Count; i++)
            //    {
            //        ContentPresenter cp = DetailBiaya.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
            //        TextBox parameter = cp.ContentTemplate.FindName("Parameter", cp) as TextBox;
            //        ComboBox postBiaya = cp.ContentTemplate.FindName("PostBiaya", cp) as ComboBox;
            //        TextBox biaya = cp.ContentTemplate.FindName("Biaya", cp) as TextBox;
            //        ComboBox isLocked = cp.ContentTemplate.FindName("IsLocked", cp) as ComboBox;
            //        if (string.IsNullOrWhiteSpace(parameter.Text) || string.IsNullOrWhiteSpace(postBiaya.Text) || string.IsNullOrWhiteSpace(biaya.Text) || isLocked.SelectedItem == null)
            //        {
            //            BtnSubmit.IsEnabled = false;
            //        }
            //    }
            //}
        }

        private void JenisNonAir_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

    }
}
