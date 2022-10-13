using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.BAPengembalian
{
    /// <summary>
    /// Interaction logic for DialogView.xaml
    /// </summary>
    public partial class DialogView : UserControl
    {
        private readonly BaPengembalianViewModel Vm;

        public DialogView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as BaPengembalianViewModel;
            DataContext = Vm;
        }

        private void CetakPermohonan(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
