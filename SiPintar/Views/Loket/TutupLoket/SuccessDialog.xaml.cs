using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Loket.TutupLoket
{
    public partial class SuccessDialog : UserControl
    {
        public SuccessDialog(decimal? penerimaan, decimal? kaskecil)
        {
            InitializeComponent();

            SetDisplay(penerimaan, kaskecil);
        }

        public void SetDisplay(decimal? penerimaan, decimal? kaskecil)
        {
            Penerimaan.Text = penerimaan.HasValue ? string.Format("{0:N0}", penerimaan) : "0";
            KasKecil.Text = kaskecil.HasValue ? string.Format("{0:N0}", kaskecil) : "0";
        }

        private void Close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
