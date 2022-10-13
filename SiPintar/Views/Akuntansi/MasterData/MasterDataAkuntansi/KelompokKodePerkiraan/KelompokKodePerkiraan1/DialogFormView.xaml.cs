using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1
{
    /// <summary>
    /// Interaction logic for DialogForm.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly KelompokKodePerkiraan1ViewModel _viewModel;

        public DialogFormView(KelompokKodePerkiraan1ViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (KelompokKodePerkiraan1ViewModel)DataContext;

            Title.Text = ((KelompokKodePerkiraan1ViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Kode Perkiraan (XX)";
            OkButton.Content = ((KelompokKodePerkiraan1ViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(KelompokPerkiraan.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaPerkiraan.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void KelompokKode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = new ParamMasterPerkiraan1Dto();
            if (_viewModel.Parent.IsAdd == false)
            {
                data.IdPerkiraan1 = _viewModel.SelectedData.IdPerkiraan1;
                data.IdPdam = _viewModel.SelectedData.IdPdam;
            }

            data.KodePerkiraan1 = _viewModel.FormPerkiraan.KodePerkiraan1;
            data.NamaPerkiraan1 = _viewModel.FormPerkiraan.NamaPerkiraan1;

            _viewModel.OnSubmitFormCommand.Execute(data);

        }
    }
}
