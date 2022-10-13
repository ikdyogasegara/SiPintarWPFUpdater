using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Views.Personalia.DataMaster.Keluarga
{
    public partial class DialogFormView : UserControl
    {
        private readonly KeluargaViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (KeluargaViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                CloseDialog();
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e) => CloseDialog();

        private void CloseDialog()
        {
            _viewModel.IsCariDariPegawai = false;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(NamaKeluarga.Text))
                OkButton.IsEnabled = false;
            else if (Status.SelectedItem == null)
                OkButton.IsEnabled = false;
            else if (Pekerjaan.SelectedItem == null)
                OkButton.IsEnabled = false;
            else if (JenisKelamin.SelectedItem == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NoPenduduk.Text))
                OkButton.IsEnabled = false;
            else if (TglLahir.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NoBpjsKes.Text))
                OkButton.IsEnabled = false;
            else if (FlagKawin.SelectedItem == null)
                OkButton.IsEnabled = false;
            else if (FlagTertanggung.SelectedItem == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }
    }
}
