using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly AnggaranPerputaranUangViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(AnggaranPerputaranUangViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedDataUraian.IdPerputaranUang != null)
            {
                _viewModel.AnggaranPerputaranUangForm = (LaporanPerputaranUangDto)_viewModel.SelectedDataUraian.Clone();
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "AkuntansiRootDialog", "Koreksi Saldo Anggaran Perputaran Uang", "Silahkan pilih data", "warning", "Ok", false, "akuntansi");
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogFormView(_viewModel);

    }
}
