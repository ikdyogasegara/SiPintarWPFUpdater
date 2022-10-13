using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Views.Billing
{
    public partial class SupervisiView : UserControl
    {
        public SupervisiView()
        {
            InitializeComponent();
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Error;
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var viewModel = (SupervisiViewModel)DataContext;

            var label = ((RadioButton)sender).Tag.ToString();
            if (viewModel != null && label != null)
                viewModel.UpdatePage(label);
        }
    }
}
