using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Loket;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Views.Loket
{
    public partial class AngsuranView : UserControl
    {
        private AngsuranViewModel CurrentViewModel;
        public AngsuranView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);

            ShowFilter_Click();
            Tunggakan.IsChecked = true;
        }

        private void HideFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(204);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (AngsuranViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is AngsuranTunggakanViewModel tunggakan)
                tunggakan.OnRefreshCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is AngsuranSambunganBaruViewModel sambungan)
                sambungan.OnRefreshCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is AngsuranNonAirViewModel nonair)
                nonair.OnRefreshCommand.Execute(null);
        }

        private void TableSetting_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (AngsuranViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is AngsuranTunggakanViewModel tunggakan)
                tunggakan.OnOpenSettingTableFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is AngsuranSambunganBaruViewModel sambungan)
                sambungan.OnOpenSettingTableFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is AngsuranNonAirViewModel nonair)
                nonair.OnOpenSettingTableFormCommand.Execute(null);
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;

            CurrentViewModel = (AngsuranViewModel)DataContext;
            if (CurrentViewModel == null)
                return;

            CurrentViewModel.CurrentSection = current;
        }
    }
}
