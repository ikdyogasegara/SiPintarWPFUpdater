using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.MutasiJabatan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly MutasiJabatanViewModel ViewModel;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as MutasiJabatanViewModel;
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
            if (!ViewModel.IsEdit)
            {
                if (string.IsNullOrEmpty(Sk1.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sk2.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sk3.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sk4.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sk5.Text))
                    OkButton.IsEnabled = false;
            }

            if (TglSk.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (TglBerlaku.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Keterangan.Text))
                OkButton.IsEnabled = false;
            else if (DataGridMutasiJabatanDetail.Items.Count < 1)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

    }
}
