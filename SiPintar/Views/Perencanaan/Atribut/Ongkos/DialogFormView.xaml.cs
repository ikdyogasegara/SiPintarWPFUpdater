using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Views.Perencanaan.Atribut.Ongkos
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly OngkosViewModel _viewModel;
        public DialogFormView(OngkosViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (OngkosViewModel)DataContext;

            Title.Text = ((OngkosViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Ongkos";
            OkButton.Content = ((OngkosViewModel)DataContext).IsEdit ? "Simpan " : "Tambah ";

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

                _viewModel.OnSubmitCommand.Execute(null);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeOngkos.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaOngkos.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Satuan.Text))
                OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(HargaJual.Text))
            //    OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(_viewModel.SelectedDataPerhitungan.Key))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(_viewModel.SelectedDataPaketMaterial?.NamaPaketMaterial) && _viewModel.IsEnablePerhitungan)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(JumlahPersen.Text) && _viewModel.IsEnablePerhitungan)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(_viewModel.SelectedDataPostBiaya.Value))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(_viewModel.SelectedDataTipe.Value))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CapitalizeTextBox(object sender, TextCompositionEventArgs e)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            textInfo.ToTitleCase(Kategori.Text);
        }

        private void Perhitungan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

    }
}
