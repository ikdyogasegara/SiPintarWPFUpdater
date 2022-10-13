using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    public partial class LampiranView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;
        public LampiranView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;

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
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitLampiranCommand).ExecuteAsync(null));
        }

        private void CheckButton()
        {
            OkButton.IsEnabled = !string.IsNullOrEmpty(Lampiran.Text);
        }

        private void Lampiran_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckButton();
    }
}
