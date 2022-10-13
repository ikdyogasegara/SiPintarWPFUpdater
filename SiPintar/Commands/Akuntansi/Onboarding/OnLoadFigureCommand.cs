using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Commands.Akuntansi.Onboarding
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
                _viewModel.IsOverlayActive = _viewModel.CurrentPageName != "menu_1" && _viewModel.CurrentPageName != "menu_2";
            }

            string imageSection = null;
            switch (_viewModel.CurrentPageName)
            {
                case "menu_1":
                    imageSection = "a";
                    break;
                case "menu_2":
                    imageSection = "b";
                    break;
                default:
                    break;
            }

            string fileUrl = null;
            if (imageSection != null && imageNumber != null)
                fileUrl = $"/SiPintar;component/Assets/Images/Onboarding/Akuntansi/Figures/fig.{imageSection}.{imageNumber}.png";

            if (fileUrl != null)
                _viewModel.OnboardFigure = new BitmapImage(new Uri(fileUrl, UriKind.Relative));

            await Task.FromResult(true);
        }
    }
}
