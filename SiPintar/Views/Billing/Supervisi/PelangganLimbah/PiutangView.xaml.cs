using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Billing.Supervisi.PelangganLimbah
{
    public partial class PiutangView : UserControl
    {
        public PiutangView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }
    }
}
