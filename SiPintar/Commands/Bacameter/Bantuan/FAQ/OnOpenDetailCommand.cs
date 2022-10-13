using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.Bantuan;
using static SiPintar.ViewModels.Bacameter.Bantuan.FaqViewModel;

namespace SiPintar.Commands.Bacameter.Bantuan.FAQ
{
    [ExcludeFromCodeCoverage]
    public class OnOpenDetailCommand : AsyncCommandBase
    {
        private readonly FaqViewModel _viewModel;

        public OnOpenDetailCommand(FaqViewModel ViewModel)
        {
            _viewModel = ViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null || !(parameter is FaqContentDetailDto))
                return;

            var ContentToLoad = (FaqContentDetailDto)parameter;

            _viewModel.CurrentPageInfo = ContentToLoad.BreadcrumbInfo;
            _viewModel.CurrentContent = ContentToLoad.HtmlString;

            _viewModel.Parent.IsFullPage = false;
            _viewModel.IsDetail = true;

            await Task.FromResult(true);
        }
    }
}
