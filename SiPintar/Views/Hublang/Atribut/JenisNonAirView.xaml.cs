using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for JenisNonAirView.xaml
    /// </summary>
    public partial class JenisNonAirView : UserControl
    {
        public JenisNonAirView()
        {
            InitializeComponent();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is JenisNonAirViewModel model)
            {
                _ = Task.Run(() => ((AsyncCommandBase)model.OnLimitDataChangeCommand).Execute(null));
            }
        }
    }
}
