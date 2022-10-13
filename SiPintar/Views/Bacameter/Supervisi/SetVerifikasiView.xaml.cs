using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class SetVerifikasiView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isVerifikasi;
        private readonly bool _ignoreCheckbox;
        public SetVerifikasiView(object dataContext, bool IsVerifikasi = true, bool IgnoreCheckbox = true)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

            _isVerifikasi = IsVerifikasi;
            _ignoreCheckbox = IgnoreCheckbox;
            SetDisplay();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void SetDisplay()
        {
            VerifikasiSection.Visibility = _isVerifikasi ? Visibility.Visible : Visibility.Collapsed;
            UnverifikasiSection.Visibility = !_isVerifikasi ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_isVerifikasi)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitVerifikasiCommand).ExecuteAsync(_ignoreCheckbox));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitUnverifikasiCommand).ExecuteAsync(_ignoreCheckbox));
        }
    }
}
