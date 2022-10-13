using System.Windows.Controls;
using System.Windows.Markup;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    /// <summary>
    /// Interaction logic for DialogFormBaRotasiMeterView.xaml
    /// </summary>
    public partial class DialogFormBaRotasiMeterView : UserControl
    {
        private readonly RotasiMeterNonRutinViewModel Vm;
        public DialogFormBaRotasiMeterView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            Vm = dataContext as RotasiMeterNonRutinViewModel;

            Language = XmlLanguage.GetLanguage("id-ID");
        }
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckButton()
        {

        }
    }
}
