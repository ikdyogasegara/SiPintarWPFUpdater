using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Views.Loket
{
    public partial class TutupLoketView : UserControl
    {
        public TutupLoketView()
        {
            InitializeComponent();
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            //if (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text) ||
            //    string.IsNullOrEmpty(HargaPaket.Text) || PaketMaterial.SelectedItem == null ||
            //    PaketOngkos.SelectedItem == null || string.IsNullOrEmpty(PersentaseKeuntungan.Text) ||
            //    string.IsNullOrEmpty(PersentaseJasaDariBahan.Text) || (WithPpn.IsChecked == false && NoPpn.IsChecked == false))
            //    OkButton.IsEnabled = false;
            //else
            //    OkButton.IsEnabled = true;
        }
    }
}
