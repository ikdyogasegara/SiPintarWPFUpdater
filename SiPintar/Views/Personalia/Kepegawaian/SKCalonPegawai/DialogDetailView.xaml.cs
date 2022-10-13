using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.SKCalonPegawai
{
    /// <summary>
    /// Interaction logic for DialogDetailView.xaml
    /// </summary>
    public partial class DialogDetailView : UserControl
    {
        private readonly SKCalonPegawaiViewModel ViewModel;

        public DialogDetailView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as SKCalonPegawaiViewModel;
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
