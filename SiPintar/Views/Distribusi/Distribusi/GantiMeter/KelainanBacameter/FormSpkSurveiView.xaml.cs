using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeter.KelainanBacameter
{
    /// <summary>
    /// Interaction logic for FormSpkSurveiView.xaml
    /// </summary>
    public partial class FormSpkSurveiView : UserControl
    {
        private readonly KelainanBacameterViewModel Vm;
        public FormSpkSurveiView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            Vm = dataContext as KelainanBacameterViewModel;
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
