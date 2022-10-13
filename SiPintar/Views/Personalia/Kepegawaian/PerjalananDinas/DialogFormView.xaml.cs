using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PerjalananDinasViewModel ViewModel;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as PerjalananDinasViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckForm_TargetUpdated(object sender, DataTransferEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (!ViewModel.IsEdit)
            {
                if (string.IsNullOrEmpty(Spt1.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Spt2.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Spt3.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Spt4.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Spt5.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sppd1.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sppd2.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sppd3.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sppd4.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Sppd5.Text))
                    OkButton.IsEnabled = false;
            }

            if (string.IsNullOrEmpty(Dasar.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Keperluan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(TempatBerangkat.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(TempatTujuan.Text))
                OkButton.IsEnabled = false;
            else if (TglBerangkat.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (TglKembali.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(LamaDinas.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Transportasi.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Keterangan.Text))
                OkButton.IsEnabled = false;
            else if (DataGridPerjalananDinasDetail.Items.Count < 1)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }
    }
}
