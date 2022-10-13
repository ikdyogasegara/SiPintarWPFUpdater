using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for JenisBangunanView.xaml
    /// </summary>
    public partial class JenisBangunanView : UserControl
    {
        public JenisBangunanView()
        {
            InitializeComponent();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is JenisBangunanViewModel model)
            {
                _ = Task.Run(() => ((AsyncCommandBase)model.OnLimitDataChangeCommand).Execute(null));
            }
        }
    }
}
