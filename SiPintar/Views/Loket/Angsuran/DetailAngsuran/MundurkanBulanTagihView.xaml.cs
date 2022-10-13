using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Loket.Angsuran.DetailAngsuran
{
    /// <summary>
    /// Interaction logic for MundurkanBulanTagihView.xaml
    /// </summary>
    public partial class MundurkanBulanTagihView : UserControl
    {
        public MundurkanBulanTagihView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
