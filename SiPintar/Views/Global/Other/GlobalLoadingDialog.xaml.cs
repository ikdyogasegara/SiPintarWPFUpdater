using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SiPintar.Views.Global.Other
{
    /// <summary>
    /// Interaction logic for GlobalLoadingDialog.xaml
    /// </summary>
    public partial class GlobalLoadingDialog : UserControl
    {
        public GlobalLoadingDialog(
            string header, string message1, string message2,
            bool isCircleLoading = true, bool isUnknowHowLongTime = true, double estimatedTimeInSecond = 20)
        {
            InitializeComponent();

            _ = SetDisplayAsync(header, message1, message2, isCircleLoading, isUnknowHowLongTime, estimatedTimeInSecond);
        }

        public async Task SetDisplayAsync(string header, string message1, string message2, bool isCircleLoading, bool isUnknowHowLongTime, double estimatedTimeInSecond)
        {
            DialogHeader.Text = header ?? "Mohon tunggu sebentar";
            DialogMessage1.Text = message1 ?? "Tindakan sedang diproses";
            DialogMessage2.Text = message2 ?? "";

            IndeterminateCircle.Visibility = isCircleLoading ? Visibility.Visible : Visibility.Collapsed;
            DetermineWrapperLinear.Visibility = isCircleLoading ? Visibility.Collapsed : Visibility.Visible;

            if (!isUnknowHowLongTime)
            {
                var determinationCalc = Math.Round((double)100 / estimatedTimeInSecond, 2);

                double current = 0;
                while (current < 100)
                {
                    PercentageInfo.Text = Math.Round(current, 0).ToString();
                    DeterminateLinear.Value = current;
                    current = Math.Round(current + determinationCalc, 2);

                    await Task.Delay(1000);
                }

                IndeterminateCircle.Visibility = Visibility.Visible;
                DetermineWrapperLinear.Visibility = Visibility.Collapsed;
            }
        }
    }
}
