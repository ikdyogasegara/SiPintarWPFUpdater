using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.DiklatPelatihan
{
    /// <summary>
    /// Interaction logic for DialogPegawaiView.xaml
    /// </summary>
    public partial class DialogPegawaiView : UserControl
    {
        private readonly DiklatPelatihanViewModel ViewModel;

        public DialogPegawaiView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as DiklatPelatihanViewModel;
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
