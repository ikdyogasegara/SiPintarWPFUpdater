using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    /// <summary>
    /// Interaction logic for DialogFormSpkRotasiMeterView.xaml
    /// </summary>
    public partial class DialogFormSpkRotasiMeterView : UserControl
    {
        private readonly RotasiMeterViewModel Vm;
        public DialogFormSpkRotasiMeterView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            Vm = dataContext as RotasiMeterViewModel;
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
