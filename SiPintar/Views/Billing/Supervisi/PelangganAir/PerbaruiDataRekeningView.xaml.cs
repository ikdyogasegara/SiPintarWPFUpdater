using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Billing.Supervisi.PelangganAir
{
    public partial class PerbaruiDataRekeningView : UserControl
    {
        public PerbaruiDataRekeningView(object dataContext)
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
