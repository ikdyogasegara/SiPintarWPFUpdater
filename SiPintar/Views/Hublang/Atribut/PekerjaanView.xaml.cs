using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for PekerjaanView.xaml
    /// </summary>
    public partial class PekerjaanView : UserControl
    {
        public PekerjaanView()
        {
            InitializeComponent();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PekerjaanViewModel model)
            {
                _ = Task.Run(() => ((AsyncCommandBase)model.OnLimitDataChangeCommand).Execute(null));
            }
        }
    }
}
