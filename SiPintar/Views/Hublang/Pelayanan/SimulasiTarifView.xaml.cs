using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan
{
    /// <summary>
    /// Interaction logic for SimulasiTarifView.xaml
    /// </summary>
    public partial class SimulasiTarifView : UserControl
    {
        public SimulasiTarifView()
        {
            InitializeComponent();
            CheckButtonHitung();

            StanLalu.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            StanLalu.GotFocus += DecimalValidationHelper.Object_GotFocus;
            StanLalu.LostFocus += DecimalValidationHelper.Object_LostFocus;
            StanKini.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            StanKini.GotFocus += DecimalValidationHelper.Object_GotFocus;
            StanKini.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void CheckButtonHitung()
        {
            bool Status = true;
            Status = !string.IsNullOrWhiteSpace(StanLalu.Text) && Status;
            Status = !string.IsNullOrWhiteSpace(StanKini.Text) && Status;
            Status = Golongan.SelectedItem != null && Status;
            Status = Diameter.SelectedItem != null && Status;
            BtnHitung.IsEnabled = Status;
        }

        private void Stan_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButtonHitung();
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButtonHitung();

        private void BtnHitung_Click(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, dynamic>();
            param.Add("Pakai", Convert.ToDouble(
                DecimalValidationHelper.FormatStringIdToDecimal(StanKini.Text) - DecimalValidationHelper.FormatStringIdToDecimal(StanLalu.Text
                )));
            var Vm = DataContext as SimulasiTarifViewModel;
            if (Vm != null)
                _ = ((AsyncCommandBase)Vm.OnHitungCommand).ExecuteAsync(param);
        }
    }
}
