using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Views.Distribusi.Distribusi
{
    /// <summary>
    /// Interaction logic for GantiMeterView.xaml
    /// </summary>
    public partial class GantiMeterView : UserControl
    {
        private GantiMeterViewModel CurrentViewModel;

        public GantiMeterView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);

            ShowFilter_Click();
            RotasiMeter.IsChecked = true;

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
            CurrentViewModel = (GantiMeterViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is KelainanBacameterViewModel kelainanBacameter)
                kelainanBacameter.OnLoadCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is RotasiMeterViewModel rotasiMeter)
                rotasiMeter.OnLoadCommand.Execute(null);
        }

        private void TableSetting_Click(object sender, RoutedEventArgs e)
        {
            //CurrentViewModel = (GantiMeterViewModel)DataContext;

            //if (CurrentViewModel.PageViewModel == null)
            //    return;

            //if (CurrentViewModel.PageViewModel is KelainanBacameterViewModel kelainanBacameter)
            //    kelainanBacameter.OnOpenSettingTableFormCommand.Execute(null);
            //else if (CurrentViewModel.PageViewModel is RotasiMeterViewModel rotasiMeter)
            //    rotasiMeter.OnOpenSettingTableFormCommand.Execute(null);
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;

            CurrentViewModel = (GantiMeterViewModel)DataContext;
            if (CurrentViewModel == null)
                return;

            CurrentViewModel.CurrentSection = current;
        }

        private void BtnOpenCollapsed_Click(object sender, RoutedEventArgs e)
        {
            InfoPelangganFilter.Visibility = Visibility.Visible;
            BtnOpenCollapsed.Visibility = Visibility.Collapsed;
            BtnCloseCollapsed.Visibility = Visibility.Visible;
        }

        private void BtnCloseCollapsed_Click(object sender, RoutedEventArgs e)
        {
            InfoPelangganFilter.Visibility = Visibility.Collapsed;
            BtnOpenCollapsed.Visibility = Visibility.Visible;
            BtnCloseCollapsed.Visibility = Visibility.Collapsed;
        }
    }
}
