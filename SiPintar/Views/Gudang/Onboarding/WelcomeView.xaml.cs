using System.Windows.Controls;
using SiPintar.ViewModels.Gudang;

namespace SiPintar.Views.Gudang.Onboarding
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
