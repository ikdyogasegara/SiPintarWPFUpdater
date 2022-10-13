using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Billing.Supervisi.PenghapusanRekening
{
    public partial class TambahHapusRekeningView : UserControl
    {
        public TambahHapusRekeningView(object dataContext)
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
