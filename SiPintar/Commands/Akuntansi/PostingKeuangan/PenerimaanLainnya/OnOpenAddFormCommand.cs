using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
using SiPintar.Views.Akuntansi.PostingKeuangan.PenerimaanLainnya;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PenerimaanLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;
            _viewModel.IsAdd = true;

            _viewModel.WaktuInputForm = DateTime.Now;
            _viewModel.NotransForm = string.Empty;
            _viewModel.SelectedWilayah = new();
            _viewModel.SelectedDebet = new();
            _viewModel.SelectedKredit = new();
            _viewModel.UraianForm = string.Empty;

            _viewModel.PenerimaanLainnyaForm = new()
            {
                SumberData = 2
            };

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
