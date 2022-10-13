using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.Tarif;

namespace SiPintar.Views.Billing.Atribut.Tarif.RetribusiLain
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly RetribusiViewModel _viewModel;
        public DialogFormView(RetribusiViewModel dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
            _viewModel = (RetribusiViewModel)DataContext;

            Title.Text = ((RetribusiViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Retribusi";

            OkButton.Content = ((RetribusiViewModel)DataContext).IsEdit ? "Simpan " : "Tambah ";
            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            PreviewKeyUp += new KeyEventHandler(HandleEnter);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.KodeRetribusiForm = KodeRetribusiLain.Text;
                _viewModel.NamaRetribusiForm = NamaRetribusiLain.Text;
                _viewModel.BiayaRetribusiForm = decimal.Parse(Retribusi.Text);

                OkButton_Click(null, null);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeRetribusiLain.Text) || string.IsNullOrEmpty(NamaRetribusiLain.Text))
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
