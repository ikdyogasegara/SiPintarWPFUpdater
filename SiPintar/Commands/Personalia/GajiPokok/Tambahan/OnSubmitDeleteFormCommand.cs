﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.Tambahan
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly TambahanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(TambahanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic> { { "IdGajiTambahan", _viewModel.SelectedData.IdGajiTambahan } };

            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-gaji-pegawai-tambahan", body));

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            await Task.FromResult(_isTest);
        }
    }
}
