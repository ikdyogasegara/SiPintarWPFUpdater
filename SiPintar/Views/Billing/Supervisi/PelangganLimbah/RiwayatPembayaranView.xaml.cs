using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganLimbah
{
    public partial class RiwayatPembayaranView : UserControl
    {
        private readonly PelangganLimbahViewModel _viewModel;

        public RiwayatPembayaranView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            _viewModel = (PelangganLimbahViewModel)dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = ((AsyncCommandBase)_viewModel.OnSearchRiwayatPembayaranCommand).ExecuteAsync(null);
        }
    }
}
