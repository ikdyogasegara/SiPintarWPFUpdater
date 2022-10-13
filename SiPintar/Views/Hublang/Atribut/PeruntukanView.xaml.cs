using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for PeruntukanView.xaml
    /// </summary>
    public partial class PeruntukanView : UserControl
    {
        public PeruntukanView()
        {
            InitializeComponent();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PeruntukanViewModel model)
            {
                _ = Task.Run(() => ((AsyncCommandBase)model.OnLimitDataChangeCommand).Execute(null));
            }
        }
    }
}
