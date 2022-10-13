﻿using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PengelompokanAktivaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PengelompokanAktivaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = true;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
