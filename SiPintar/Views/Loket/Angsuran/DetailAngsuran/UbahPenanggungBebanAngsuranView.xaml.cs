using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Loket.Angsuran.DetailAngsuran
{
    /// <summary>
    /// Interaction logic for UbahPenanggungBebanAngsuranView.xaml
    /// </summary>
    public partial class UbahPenanggungBebanAngsuranView : UserControl
    {
        public UbahPenanggungBebanAngsuranView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NoHp.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrWhiteSpace(NoTelp.Text))
                OkButton.IsEnabled = false;
            else
            {
                OkButton.IsEnabled = true;
            }
        }
    }
}
