using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Pelayanan.TagihanManual;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.StatusTransaksiWpf == true)
                {
                    await ShowFormWarningAsync();
                }
                else
                {
                    _viewModel.IsEdit = true;
                    _viewModel.FormData = _viewModel.SelectedData;
                    _viewModel.FormDataDetail = new ObservableCollection<RekeningNonAirDetailDto>(_viewModel.SelectedData.DetailWpf);
                    await ShowFormDialogAsync();
                }
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowFormDialogAsync()
        {
            if (!_isTest)
            {
                await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowFormWarningAsync()
        {
            if (!_isTest)
            {
                await DialogHost.Show(new DialogGlobalView("Koreksi Data Tagihan Non Air", "Tagihan Lunas tidak dapat dikoreksi !", "warning", "Ok", false, "hublang"), "HublangRootDialog");
            }
        }
    }
}
