using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Views.Personalia.Tunjangan.PengecualianTunjangan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PengecualianTunjanganViewModel ViewModel;

        public DialogFormView(object dataContext)
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

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckForm_TargetUpdated(object sender, DataTransferEventArgs e) => CheckButton();

        private void CheckButton()
        {
            //if (!ViewModel.IsEdit && string.IsNullOrEmpty(NoSertifikat.Text))
            //    OkButton.IsEnabled = false;

            //if (TglAwal.SelectedDate == null)
            //    OkButton.IsEnabled = false;
            //else if (TglAkhir.SelectedDate == null)
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(Uraian.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(Tempat.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(Penyelenggara.Text))
            //    OkButton.IsEnabled = false;
            //else if (DataGridPengecualianTunjanganDetail.Items.Count < 1)
            //    OkButton.IsEnabled = false;
            //else
            OkButton.IsEnabled = true;
        }

    }
}
