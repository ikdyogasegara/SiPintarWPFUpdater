using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.DetailTagihan
{
    public class OnCloseDetailTagihanCommand : AsyncCommandBase
    {
        private readonly DetailTagihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnCloseDetailTagihanCommand(DetailTagihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.ListSelectedPelangganAir = null;
            _viewModel.Parent.ListSelectedNonAir = null;
            _viewModel.ParentPage.IsFullPage = false;
            _viewModel.Parent.UpdatePage("SearchPage");
            await Task.FromResult(_isTest);
        }
    }
}
