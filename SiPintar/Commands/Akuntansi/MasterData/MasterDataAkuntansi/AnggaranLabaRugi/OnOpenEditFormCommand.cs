using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly AnggaranLabaRugiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(AnggaranLabaRugiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;
            _viewModel.AnggaranLabaRugiForm = (AnggaranLabaRugiDto)_viewModel.SelectedDataUraian.Clone();
            _viewModel.IsPerTahun = true;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
