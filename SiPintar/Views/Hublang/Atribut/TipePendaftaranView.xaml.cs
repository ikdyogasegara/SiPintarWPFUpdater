using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for TipePendaftaranView.xaml
    /// </summary>
    public partial class TipePendaftaranView : UserControl
    {
        public TipePendaftaranView()
        {
            InitializeComponent();
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is TipePendaftaranViewModel model)
            {
                _ = Task.Run(() => ((AsyncCommandBase)model.OnLimitDataChangeCommand).Execute(null));
            }
        }
    }
}
