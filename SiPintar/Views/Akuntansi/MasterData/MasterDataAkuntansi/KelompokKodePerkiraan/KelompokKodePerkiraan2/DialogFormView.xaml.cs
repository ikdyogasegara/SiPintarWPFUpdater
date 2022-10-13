using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly KelompokKodePerkiraan2ViewModel _viewModel;
        public DialogFormView(KelompokKodePerkiraan2ViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (KelompokKodePerkiraan2ViewModel)DataContext;

            Title.Text = ((KelompokKodePerkiraan2ViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Kode Perkiraan (XX.YY)";
            OkButton.Content = ((KelompokKodePerkiraan2ViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Simpan ";

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
            if (_viewModel.Parent.SelectedDataPerkiraan1?.IdPerkiraan1.HasValue == false)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(KodePerkiraan2.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaPerkiraan2.Text))
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedNeracaMaster?.IdNeracaMaster.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedArusKasMaster?.IdArusKasMaster.HasValue == false)
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
            var data = new ParamMasterPerkiraan2Dto();
            if (!_viewModel.Parent.IsAdd)
            {
                data.IdPerkiraan2 = _viewModel.SelectedData.IdPerkiraan2;
                data.IdPdam = _viewModel.SelectedData.IdPdam;
            }

            data.KodePerkiraan2 = _viewModel.Parent.SelectedDataPerkiraan1.KodePerkiraan1 + "." + KodePerkiraan2.Text;
            data.NamaPerkiraan2 = _viewModel.FormPerkiraan.NamaPerkiraan2;
            data.IdPerkiraan1 = _viewModel.Parent.SelectedDataPerkiraan1.IdPerkiraan1;
            data.IdNeracaMaster = _viewModel.Parent.SelectedNeracaMaster.IdNeracaMaster;
            data.IdArusKasMaster = _viewModel.Parent.SelectedArusKasMaster.IdArusKasMaster;

            _viewModel.OnSubmitFormCommand.Execute(data);

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = (_viewModel.DataList.Where(x => x.KodePerkiraan1 == _viewModel.Parent.SelectedDataPerkiraan1.KodePerkiraan1).Count() + 1).ToString();
            KodePerkiraan2.Text = data.Length == 1 ? "0" + data : data;
        }


    }
}
