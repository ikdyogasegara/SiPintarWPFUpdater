using System.Windows.Controls;
using SiPintar.ViewModels.Personalia;

namespace SiPintar.Views.Personalia.Onboarding
{
    public partial class WelcomeView : UserControl
    {
        public WelcomeView(object dataContext = null)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        private void NextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var context = DataContext as OnboardingViewModel;
            if ((context.CurrentPageIndex + 1) == context.PageTotal)
                NextButton.Content = "Oke";
        }

        private void PreviousButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var context = DataContext as OnboardingViewModel;
            if ((context.CurrentPageIndex - 1) < context.PageTotal)
                NextButton.Content = "Selanjutnya";
        }
    }
}
