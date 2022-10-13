using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningL2T2
{
    public partial class KoreksiRekeningL2T2View : UserControl
    {
        private readonly RekeningL2T2ViewModel _viewModel;

        public KoreksiRekeningL2T2View(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningL2T2ViewModel)dataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Biaya.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitKoreksiRekeningCommand).ExecuteAsync(null));
        }
    }
}
