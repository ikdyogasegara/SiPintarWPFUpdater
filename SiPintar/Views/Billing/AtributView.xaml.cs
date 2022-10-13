using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Views.Billing
{
    public partial class AtributView : UserControl
    {
        public AtributView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var viewModel = (AtributViewModel)DataContext;

            var label = ((RadioButton)sender).Tag.ToString();
            if (viewModel != null && label != null)
                viewModel.UpdatePage(label);
        }
    }
}
