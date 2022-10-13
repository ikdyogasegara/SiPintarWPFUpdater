using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Views.Perencanaan.Atribut.Material
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly MaterialViewModel _viewModel;
        public DialogFormView(MaterialViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (MaterialViewModel)DataContext;

            Title.Text = ((MaterialViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Material";
            OkButton.Content = ((MaterialViewModel)DataContext).IsEdit ? "Simpan " : "Tambah ";

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
                _viewModel.MaterialForm.MaterialLimbah = _viewModel.SelectedDataKategori.Value;
                _viewModel.MaterialForm.KodeMaterial = KodeMaterial.Text;
                _viewModel.MaterialForm.NamaMaterial = NamaMaterial.Text;
                _viewModel.MaterialForm.Satuan = Satuan.Text;
                _viewModel.MaterialForm.HargaJual = Convert.ToDecimal(HargaJual.Text.Replace(".", ""));

                _viewModel.OnSubmitCommand.Execute(null);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KodeMaterial.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaMaterial.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Satuan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(HargaJual.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
