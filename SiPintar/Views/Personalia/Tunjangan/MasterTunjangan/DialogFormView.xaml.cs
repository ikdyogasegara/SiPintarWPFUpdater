using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Views.Personalia.Tunjangan.MasterTunjangan
{
    public partial class DialogFormView : UserControl
    {
        private readonly MasterTunjanganViewModel ViewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as MasterTunjanganViewModel;
            DataContext = ViewModel;

            Title.Text = ((MasterTunjanganViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Master Tunjangan";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
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

        private void CheckButton()
        {
            if (!ViewModel.IsEdit && string.IsNullOrEmpty(Kode.Text))
                OkButton.IsEnabled = false;

            if (string.IsNullOrEmpty(NamaTunjangan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Urutan.Text))
                OkButton.IsEnabled = false;
            else if (Tipe.SelectedValue == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

    }
}
