using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Billing.Supervisi.PelangganL2T2
{
    public partial class RiwayatPembayaranView : UserControl
    {
        public RiwayatPembayaranView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }
    }
}
