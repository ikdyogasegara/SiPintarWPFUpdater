using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Distribusi;

namespace SiPintar.Views.Distribusi
{
    /// <summary>
    /// Interaction logic for AtributView.xaml
    /// </summary>
    public partial class AtributView : UserControl
    {
        public AtributView()
        {
            InitializeComponent();
        }
        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (AtributViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
