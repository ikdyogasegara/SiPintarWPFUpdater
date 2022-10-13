using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public partial class Step2AddFormView : UserControl
    {
        public Step2AddFormView()
        {
            InitializeComponent();
            InputValidation();
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void InputValidation()
        {
            Biaya.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Biaya.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Biaya.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void BtnOnBrowseFileBukti1Command_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanKoreksiViewModel viewModel)
            {
                viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti1");
            }
        }

        private void BtnOnBrowseFileBukti2Command_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanKoreksiViewModel viewModel)
            {
                viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti2");
            }
        }

        private void BtnOnBrowseFileBukti3Command_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PermohonanKoreksiViewModel viewModel)
            {
                viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti3");
            }
        }
    }
}
