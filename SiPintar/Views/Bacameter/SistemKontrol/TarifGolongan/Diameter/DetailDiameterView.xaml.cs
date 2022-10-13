using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Diameter
{
    /// <summary>
    /// Interaction logic for DetailDiameterView.xaml
    /// </summary>
    public partial class DetailDiameterView : UserControl
    {
        public DetailDiameterView(DiameterViewModel dataContext)
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
    }
}
