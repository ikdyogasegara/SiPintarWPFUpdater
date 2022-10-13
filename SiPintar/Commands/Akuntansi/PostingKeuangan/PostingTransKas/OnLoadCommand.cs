using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PostingTransKas
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PostingTransKasViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PostingTransKasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsFullPage = false;
            _viewModel.UpdatePage("DataTransKas");


            await Task.FromResult(_isTest);
        }
    }
}
