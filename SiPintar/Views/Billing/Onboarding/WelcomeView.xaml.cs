using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Views.Billing.Onboarding
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : UserControl
    {
        public WelcomeView(object dataContext = null)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as OnboardingViewModel;
            if ((context.CurrentPageIndex + 1) == context.PageTotal)
                NextButton.Content = "Oke";
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as OnboardingViewModel;
            if ((context.CurrentPageIndex - 1) < context.PageTotal)
                NextButton.Content = "Selanjutnya";
        }
    }
}
