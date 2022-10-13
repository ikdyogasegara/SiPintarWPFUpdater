using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Views.Personalia.DataMaster.Keluarga
{
    /// <summary>
    /// Interaction logic for DialogPegawaiView.xaml
    /// </summary>
    public partial class DialogPegawaiView : UserControl
    {
        private readonly KeluargaViewModel ViewModel;

        public DialogPegawaiView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as KeluargaViewModel;
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
