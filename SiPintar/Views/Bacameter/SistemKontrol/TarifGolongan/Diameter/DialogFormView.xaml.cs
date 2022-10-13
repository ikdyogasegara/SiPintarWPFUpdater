using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Diameter
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly DiameterViewModel _viewModel;

        public DialogFormView(DiameterViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DiameterViewModel)DataContext;

            Title.Text = ((DiameterViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Diameter";
            OkButton.Content = ((DiameterViewModel)DataContext).IsEdit ? "Simpan " : "Tambah ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            PreviewKeyUp += new KeyEventHandler(HandleEnter);

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool status = StatusDiameter.Text == "Aktif";

                _viewModel.DiameterForm.KodeDiameter = KodeDiameter.Text;
                _viewModel.DiameterForm.NamaDiameter = NamaDiameter.Text;
                _viewModel.DiameterForm.NomorSK = NomorSK.Text;
                _viewModel.DiameterForm.Administrasi = Convert.ToDecimal(Administrasi.Text.Replace(".", ""));
                _viewModel.DiameterForm.Pemeliharaan = Convert.ToDecimal(Pemeliharaan.Text.Replace(".", ""));
                _viewModel.DiameterForm.Pelayanan = Convert.ToDecimal(Pelayanan.Text.Replace(".", ""));
                _viewModel.DiameterForm.Retribusi = Convert.ToDecimal(Retribusi.Text.Replace(".", ""));
                _viewModel.DiameterForm.DendaPakai0 = Convert.ToDecimal(DendaPakai0.Text.Replace(".", ""));
                _viewModel.DiameterForm.AirLimbah = Convert.ToDecimal(AirLimbah.Text.Replace(".", ""));
                _viewModel.DiameterForm.Status = status;

                _viewModel.OnSubmitCommand.Execute(null);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeDiameter.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaDiameter.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Administrasi.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Pelayanan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Pemeliharaan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Retribusi.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(DendaPakai0.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(AirLimbah.Text))
                OkButton.IsEnabled = false;
            else if (StatusDiameter.SelectedIndex == -1)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StatusDiameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string textCombo = "";
            if (textCombo != StatusDiameter.Text && _viewModel.IsEdit)
                OkButton.IsEnabled = true;
        }
    }
}
