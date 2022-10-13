using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Onboarding
{
    public class OnLoadFigureCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnLoadFigureCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.OnboardFigure = null;

            var CurrentPageIndex = _viewModel.CurrentPageIndex;

            string imageNumber = null;
            if (CurrentPageIndex > 1)
            {
                imageNumber = (CurrentPageIndex - 1).ToString();
                _viewModel.IsOverlayActive = _viewModel.CurrentPageName != "supervisi" && _viewModel.CurrentPageName != "produktivitas";
            }

            string imageSection = null;
            switch (_viewModel.CurrentPageName)
            {
                case "supervisi":
                    imageSection = "a";
                    break;
                case "produktivitas":
                    imageSection = "b";
                    break;
                default:
                    break;
            }

            string fileUrl = null;
            if (imageSection != null && imageNumber != null)
                fileUrl = $"/SiPintar;component/Assets/Images/Onboarding/Bacameter/Figures/fig.{imageSection}.{imageNumber}.png";

            if (fileUrl != null)
                _viewModel.OnboardFigure = new BitmapImage(new Uri(fileUrl, UriKind.Relative));

            await Task.FromResult(true);
        }
    }
}
