using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.Tarif;

namespace SiPintar.Views.Billing.Atribut.Tarif.GolonganLimbah
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly GolonganLimbahViewModel _viewModel;
        public DialogFormView(GolonganLimbahViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (GolonganLimbahViewModel)DataContext;

            Title.Text = ((GolonganLimbahViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Tarif Golongan Limbah";
            OkButton.Content = ((GolonganLimbahViewModel)DataContext).IsEdit ? "Simpan " : "Tambah ";

            CheckButton();
            OkButton.IsEnabled = ((GolonganLimbahViewModel)DataContext).IsEdit;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeTarif.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaTarifLimbah.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Biaya.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitCommand).ExecuteAsync(null));
        }
    }
}
