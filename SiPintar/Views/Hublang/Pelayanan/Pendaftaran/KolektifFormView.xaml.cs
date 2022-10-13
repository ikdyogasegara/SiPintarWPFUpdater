using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class KolektifFormView : UserControl
    {
        private readonly PendaftaranViewModel _viewModel;

        public KolektifFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = (PendaftaranViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel == null || _viewModel.KolektifList == null || _viewModel.KolektifList.Count == 0)
                return;

            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitKolektifCommand).ExecuteAsync(null));
        }
    }
}
