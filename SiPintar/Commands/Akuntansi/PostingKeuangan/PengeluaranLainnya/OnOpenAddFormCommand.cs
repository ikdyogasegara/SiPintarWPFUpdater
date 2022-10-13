using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views.Akuntansi.PostingKeuangan.PengeluaranLainnya;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PengeluaranLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = true;
            _viewModel.PengeluaranLainnyaForm = new();
            _viewModel.SelectedDebet = new();
            _viewModel.SelectedKredit = new();
            _viewModel.SelectedWilayah = new();
            _viewModel.PengeluaranLainnyaForm.WaktuInput = DateTime.Now;
            _viewModel.PengeluaranLainnyaForm.Kategori = 0;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);

        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
