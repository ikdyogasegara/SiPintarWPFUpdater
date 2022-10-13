using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PenghapusanRekening
{
    public partial class VerifikasiHapusRekeningView : UserControl
    {
        private readonly PenghapusanRekeningViewModel _viewModel;

        public VerifikasiHapusRekeningView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PenghapusanRekeningViewModel)dataContext;

            CheckButton();

        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (TglHapus.SelectedDate == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnOpenConfirmVerifikasiCommand).ExecuteAsync(null));
        }
    }
}
