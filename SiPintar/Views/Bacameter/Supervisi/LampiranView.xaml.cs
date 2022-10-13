using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class LampiranView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;
        public LampiranView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

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
            if (string.IsNullOrEmpty(Lampiran.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void Lampiran_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckButton();
    }
}
