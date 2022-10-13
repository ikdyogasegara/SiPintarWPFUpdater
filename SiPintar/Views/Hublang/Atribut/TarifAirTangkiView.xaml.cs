using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for TarifAirTangkiView.xaml
    /// </summary>
    public partial class TarifAirTangkiView : UserControl
    {
        public TarifAirTangkiView()
        {
            InitializeComponent();
            HideFilter_Click(null, null);
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(240);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is TarifAirTangkiViewModel model)
                _ = Task.Run(() => ((AsyncCommandBase)model.OnLimitDataChangeCommand).Execute(null));
        }
    }
}
