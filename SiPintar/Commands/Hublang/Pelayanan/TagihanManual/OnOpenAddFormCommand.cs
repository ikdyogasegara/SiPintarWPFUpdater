using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.TagihanManual;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            if (parameter == null)
            {
                _viewModel.JenisPelangganSelected = null;
                _viewModel.SelectedPelanggan = null;
            }

            _viewModel.IsEdit = false;
            _viewModel.IsLoading = false;
            _viewModel.FormData = new RekeningNonAirDto();
            _viewModel.FormDataDetail = new ObservableCollection<RekeningNonAirDetailDto> { new RekeningNonAirDetailDto() };

            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogFormView(_viewModel);

    }
}
