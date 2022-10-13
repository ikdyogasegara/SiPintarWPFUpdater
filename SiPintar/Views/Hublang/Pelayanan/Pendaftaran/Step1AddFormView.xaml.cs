using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class Step1AddFormView : UserControl
    {
        public Step1AddFormView()
        {
            InitializeComponent();
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void KodePair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
