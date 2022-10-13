using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Gudang.Bantuan
{
    public partial class SaranPengaduanView : UserControl
    {
        public SaranPengaduanView()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
