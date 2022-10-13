using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Views.Personalia.Tunjangan.PengecualianTunjangan
{
    /// <summary>
    /// Interaction logic for DialogPegawaiView.xaml
    /// </summary>
    public partial class DialogPegawaiView : UserControl
    {
        private readonly PengecualianTunjanganViewModel ViewModel;

        public DialogPegawaiView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as PengecualianTunjanganViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
