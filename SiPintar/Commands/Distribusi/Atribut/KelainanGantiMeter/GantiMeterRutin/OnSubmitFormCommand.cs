using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;

namespace SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterRutin
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly GantiMeterRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(GantiMeterRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog");
            _viewModel.IsLoading = true;
            _viewModel.JenisGantiMeterList?.Clear();
            _viewModel.FormGantiMeter.IdWarnaGantiMeter = _viewModel.WarnaMeter.IdWarnaGantiMeter;
            if (_viewModel.IsCheckedDenganBiaya == false)
                _viewModel.FormGantiMeter.IdJenisNonAir = null;
            else
                _viewModel.FormGantiMeter.IdJenisNonAir = _viewModel.JenisNonair.IdJenisNonAir;

            Type type = typeof(ParamMasterJenisGantiMeterDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormGantiMeter));
                }
            }
            RestApiResponse Response = null;
            if (_viewModel.IsAdd)
            {
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-ganti-meter", body));
            }
            else
            {
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-ganti-meter", null, body));
            }
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "distribusi");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "distribusi");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "distribusi");
            }
            //End - Send Data

            _ = ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
