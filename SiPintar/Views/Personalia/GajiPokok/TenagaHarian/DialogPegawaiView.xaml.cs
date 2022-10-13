using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Views.Personalia.GajiPokok.TenagaHarian
{
    /// <summary>
    /// Interaction logic for DialogPegawaiView.xaml
    /// </summary>
    public partial class DialogPegawaiView : UserControl
    {
        private readonly TenagaHarianViewModel ViewModel;

        public DialogPegawaiView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as TenagaHarianViewModel;
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
