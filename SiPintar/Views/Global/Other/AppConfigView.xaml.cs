using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.ViewModels;

namespace SiPintar.Views.Global.Other
{
    public partial class AppConfigView : Window
    {
        private LoginViewModel _loginContext;
        private readonly AppConfigViewModel _vm;

        public AppConfigView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as AppConfigViewModel;
            DataContext = _vm;
            SetupPengaturanModule();
            PreviewKeyUp += HandleEsc;
        }

        private void SetupPengaturanModule()
        {
            ApiLoket.IsChecked = _vm.ApiLoket == "CLOUD";
            ApiBilling.IsChecked = _vm.ApiBilling == "CLOUD";
            ApiBacameter.IsChecked = _vm.ApiBacameter == "CLOUD";
            ApiHublang.IsChecked = _vm.ApiHublang == "CLOUD";
            ApiPerencanaan.IsChecked = _vm.ApiPerencanaan == "CLOUD";
            ApiDistribusi.IsChecked = _vm.ApiDistribusi == "CLOUD";
            ApiAkuntansiKeuangan.IsChecked = _vm.ApiAkuntansiKeuangan == "CLOUD";

            ApiLoketReport.IsChecked = _vm.ApiLoketReport == "CLOUD";
            ApiBillingReport.IsChecked = _vm.ApiBillingReport == "CLOUD";
            ApiBacameterReport.IsChecked = _vm.ApiBacameterReport == "CLOUD";
            ApiHublangReport.IsChecked = _vm.ApiHublangReport == "CLOUD";
            ApiPerencanaanReport.IsChecked = _vm.ApiPerencanaanReport == "CLOUD";
            ApiDistribusiReport.IsChecked = _vm.ApiDistribusiReport == "CLOUD";
            ApiAkuntansiKeuanganReport.IsChecked = _vm.ApiAkuntansiKeuanganReport == "CLOUD";
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                HideHandlerDialog();
        }

        public void ShowHandlerDialog(object loginContext)
        {
            _loginContext = (LoginViewModel)loginContext;
            _ = ((AppConfigViewModel)DataContext).SetInitConfigAsync();
            SetupPengaturanModule();
            ShowDialog();
        }

        public void HideHandlerDialog()
        {
            _loginContext.UpdateState();
            Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => HideHandlerDialog();

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, bool?>
            {
                { "Config1", true },
                { "Config2", true },
                { "Config3", true },
            };

            _ = ((AsyncCommandBase)((AppConfigViewModel)DataContext).OnSaveCommand).ExecuteAsync(param);
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppConfigViewModel vm)
            {
                var prop = vm.GetType().GetProperty(((ToggleButton)sender).Name);
                if (prop != null)
                {
                    prop.SetValue(vm, "CLOUD");
                }
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppConfigViewModel vm)
            {
                var prop = vm.GetType().GetProperty(((ToggleButton)sender).Name);
                if (prop != null)
                {
                    prop.SetValue(vm, "LOCAL");
                }
            }
        }
    }
}
