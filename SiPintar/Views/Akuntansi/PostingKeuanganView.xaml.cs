using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Views.Akuntansi
{
    /// <summary>
    /// Interaction logic for PostingKeuanganView.xaml
    /// </summary>
    public partial class PostingKeuanganView : UserControl
    {
        public PostingKeuanganView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PostingKeuanganViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }
    }
}
