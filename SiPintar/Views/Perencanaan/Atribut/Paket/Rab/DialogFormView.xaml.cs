using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Views.Perencanaan.Atribut.Paket.Rab
{
    public partial class DialogFormView : UserControl
    {
        private readonly PaketRabViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PaketRabViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Paket RAB";

            WithPpn.IsChecked = _viewModel.WithPpnForm == true;
            NoPpn.IsChecked = _viewModel.NoPpnForm == true;

            CheckButton();
            InputValidation();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text) ||
                string.IsNullOrEmpty(HargaPaket.Text) || PaketMaterial.SelectedItem == null ||
                PaketOngkos.SelectedItem == null || string.IsNullOrEmpty(PersentaseKeuntungan.Text) ||
                string.IsNullOrEmpty(PersentaseJasaDariBahan.Text) || (WithPpn.IsChecked == false && NoPpn.IsChecked == false))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void InputValidation()
        {
            HargaPaket.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            HargaPaket.GotFocus += DecimalValidationHelper.Object_GotFocus;
            HargaPaket.LostFocus += DecimalValidationHelper.Object_LostFocus;

            HargaPaketMaterial.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            HargaPaketMaterial.GotFocus += DecimalValidationHelper.Object_GotFocus;
            HargaPaketMaterial.LostFocus += DecimalValidationHelper.Object_LostFocus;

            HargaPaketOngkos.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            HargaPaketOngkos.GotFocus += DecimalValidationHelper.Object_GotFocus;
            HargaPaketOngkos.LostFocus += DecimalValidationHelper.Object_LostFocus;

            PpnMaterial.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            PpnMaterial.GotFocus += DecimalValidationHelper.Object_GotFocus;
            PpnMaterial.LostFocus += DecimalValidationHelper.Object_LostFocus;

            PPnMaterialTambahan.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            PPnMaterialTambahan.GotFocus += DecimalValidationHelper.Object_GotFocus;
            PPnMaterialTambahan.LostFocus += DecimalValidationHelper.Object_LostFocus;

            PpnOngkos.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            PpnOngkos.GotFocus += DecimalValidationHelper.Object_GotFocus;
            PpnOngkos.LostFocus += DecimalValidationHelper.Object_LostFocus;

            PPnOngkosTambahan.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            PPnOngkosTambahan.GotFocus += DecimalValidationHelper.Object_GotFocus;
            PPnOngkosTambahan.LostFocus += DecimalValidationHelper.Object_LostFocus;

            PersentaseKeuntungan.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            PersentaseKeuntungan.GotFocus += DecimalValidationHelper.Object_GotFocus;
            PersentaseKeuntungan.LostFocus += DecimalValidationHelper.Object_LostFocus;

            PersentaseJasaDariBahan.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            PersentaseJasaDariBahan.GotFocus += DecimalValidationHelper.Object_GotFocus;
            PersentaseJasaDariBahan.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void PaketMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void PaketOngkos_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void HargaTambahan_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Tag.ToString();

            if (current == "ppn1")
                _viewModel.WithPpnForm = true;
            else
                _viewModel.NoPpnForm = true;
        }
    }
}
