using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(InteraksiPenyusutanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsAdd = true;

            _viewModel.InPenyusustanForm = new();
            _viewModel.SelectedAkunAktiva = new();
            _viewModel.SelectedAkunAkumPenyusutan = new();
            _viewModel.SelectedAkunBiaya = new();

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
