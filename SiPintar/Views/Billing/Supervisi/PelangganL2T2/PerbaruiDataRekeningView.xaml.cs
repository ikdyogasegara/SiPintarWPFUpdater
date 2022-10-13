using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganL2T2
{
    public partial class PerbaruiDataRekeningView : UserControl
    {
        private readonly PelangganL2T2ViewModel _viewModel;

        public PerbaruiDataRekeningView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PelangganL2T2ViewModel)dataContext;

            CheckButton();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (Periode.SelectedIndex < 0)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitPerbaruiDataRekeningCommand).ExecuteAsync(null));
        }
    }
}
