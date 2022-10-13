using System.Windows.Controls;

namespace SiPintar.Views.Billing
{
    /// <summary>
    /// Interaction logic for OnboardingView.xaml
    /// </summary>
    public partial class OnboardingView : UserControl
    {
        public OnboardingView(object dataContext = null)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
