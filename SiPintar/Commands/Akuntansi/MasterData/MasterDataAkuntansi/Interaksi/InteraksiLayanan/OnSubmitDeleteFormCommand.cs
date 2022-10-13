using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(InteraksiLayananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                "", true, true, 20);

            try
            {
                //Start - Submit Delete
                var param = new Dictionary<string, dynamic>();
                param.Add("IdPdam", _viewModel.SelectedData.IdPdam ?? 0);
                param.Add("IdUserRequest", 1);
                switch (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key)
                {
                    case 0:
                        param.Add("IdInPelayananAir", _viewModel.SelectedData.IdInPelayananAir ?? 0);
                        break;
                    default:
                        param.Add("IdInPelayananNonAir", _viewModel.SelectedData.IdInPelayananNonAir ?? 0);
                        break;
                };

                var Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.Parent.Url}", param));
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "akuntansi");
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "akuntansi");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "akuntansi");
                }

                //Start - Submit Delete
                DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

                //End - Get Filter Data
                await ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync("");
                await Task.FromResult(_isTest);
            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
            }
        }
    }
}
