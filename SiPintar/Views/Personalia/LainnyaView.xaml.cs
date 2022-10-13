using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Personalia;

namespace SiPintar.Views.Personalia
{
    public partial class LainnyaView : UserControl
    {
        public LainnyaView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (LainnyaViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
