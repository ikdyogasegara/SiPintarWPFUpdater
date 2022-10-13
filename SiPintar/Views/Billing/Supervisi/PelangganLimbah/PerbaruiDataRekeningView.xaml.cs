using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganLimbah
{
    public partial class PerbaruiDataRekeningView : UserControl
    {
        private readonly PelangganLimbahViewModel _viewModel;

        public PerbaruiDataRekeningView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PelangganLimbahViewModel)dataContext;

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
