using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    [ExcludeFromCodeCoverage]
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(InteraksiJenisPersediaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsAdd = true;

            _viewModel.InJenisPersediaanForm = new();
            _viewModel.SelectedJenis = new();
            _viewModel.SelectedKeperluan = new();
            _viewModel.SelectedDebet = new();
            _viewModel.SelectedKredit = new();

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);

            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
