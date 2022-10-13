using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public partial class KoreksiFormView : UserControl
    {
        private readonly PermohonanKoreksiViewModel _viewModel;

        public KoreksiFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PermohonanKoreksiViewModel;
            DataContext = _viewModel;

            if (_viewModel != null)
            {
                _viewModel.IsFotoBukti1FormChecked = _viewModel.FotoBukti1Uri != null;
                _viewModel.IsFotoBukti2FormChecked = _viewModel.FotoBukti2Uri != null;
                _viewModel.IsFotoBukti3FormChecked = _viewModel.FotoBukti3Uri != null;

                if (_viewModel.IsCanEdit)
                {
                    Title.Text = "Koreksi Permohonan";
                    ButtonOk.Visibility = Visibility.Visible;
                    ButtonBatal.Content = "Batal";
                    ColumnCheckPiutang.IsReadOnly = false;
                }
                else
                {
                    Title.Text = "Detail Permohonan";
                    ButtonOk.Visibility = Visibility.Collapsed;
                    ButtonBatal.Content = "Tutup";
                    ColumnCheckPiutang.IsReadOnly = true;
                }
            }

            InputValidation();
            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void InputValidation()
        {
            Biaya.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Biaya.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Biaya.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.Parent.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var viewModel = (PermohonanKoreksiViewModel)DataContext;

            var isPiutangSelected = viewModel.PiutangAirList.FirstOrDefault(i => i.IsSelected == true) != null;

            if (string.IsNullOrEmpty(viewModel.AlasanForm) || !isPiutangSelected)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var isPiutangSelected = _viewModel.PiutangAirList.FirstOrDefault(i => i.IsSelected == true) != null;

            if (string.IsNullOrEmpty(_viewModel.AlasanForm) || !isPiutangSelected)
            {
                ShowAlert();
                return;
            }

            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null);
        }
        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Form Tidak Lengkap",
                    "Mohon Pilih Piutang & Isikan Alasan Koreksi Rekening.",
                    "warning",
                    "Oke",
                    false,
                    "hublang"
                ), "KoreksiRekeningSubDialog");
        }

        private void BtnOnBrowseFileBukti1Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti1");
        }

        private void BtnOnBrowseFileBukti2Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti2");
        }

        private void BtnOnBrowseFileBukti3Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti3");
        }
    }
}
