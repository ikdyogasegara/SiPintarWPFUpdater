using System.Windows.Controls;
using System.Windows.Markup;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    public partial class DialogFormBaRotasiMeterView : UserControl
    {
        private readonly RotasiMeterViewModel Vm;
        public DialogFormBaRotasiMeterView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            Vm = dataContext as RotasiMeterViewModel;
            Language = XmlLanguage.GetLanguage("id-ID");
        }

        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckButton()
        {

        }

    }
}

