using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;
using UserControl = System.Windows.Controls.UserControl;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public partial class DialogFormSpkRotasiMeterView : UserControl
    {
        private readonly RotasiMeterNonRutinViewModel Vm;
        public DialogFormSpkRotasiMeterView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            Vm = dataContext as RotasiMeterNonRutinViewModel;
        }
        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckBox_Click(object sender, RoutedEventArgs e) => CheckButton();
        private void CheckForm_TextChanged(object sender, TextChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }
    }
}

