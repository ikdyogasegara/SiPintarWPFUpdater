using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(PersentasePenyusutanAktivaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...");

            var data = parameter as ParamMasterPenyusutanAktivaDto;
            var type = typeof(ParamMasterPenyusutanAktivaDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(data)!);
                }
            }

            RestApiResponse response;
            if (_viewModel.IsAdd)
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-penyusutan-aktiva", body);
            else
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-penyusutan-aktiva", null!, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (_viewModel.IsAdd)
                    {
                        _viewModel.DumpNewData = data;
                        _viewModel.OnLoadCommand.Execute(null);
                    }
                    else
                        UpdateRecord(_viewModel.SelectedData!, result.Data.ToObject<ObservableCollection<MasterPenyusutanAktivaDto>>()!);

                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg!, "akuntansi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg!, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message!, "akuntansi");

            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(MasterPenyusutanAktivaWpf selectedData, ObservableCollection<MasterPenyusutanAktivaDto> resultData)
        {
            if (!_isTest)
            {
                selectedData.KodeGolAktivaWpf = resultData[0].KodeGolAktiva;
                selectedData.NamaGolAktivaWpf = resultData[0].NamaGolAktiva;
                selectedData.JmlTahunWpf = resultData[0].JmlTahun;
                selectedData.JmlPersenWpf = resultData[0].JmlPersen;
                selectedData.KodeSusutWpf = resultData[0].KodeSusut;
            }
        }
    }
}
