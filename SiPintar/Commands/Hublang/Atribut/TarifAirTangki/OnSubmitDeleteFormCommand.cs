using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(TarifAirTangkiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = null;
            if (!_isTest)
            {
                DialogHost.Close("HublangRootDialog");
                dg = DialogHost.Show(new GlobalLoadingDialog("Mohon tunggu", "sedang memproses tindakan...", ""), "HublangRootDialog");
            }

            var body = new Dictionary<string, dynamic>();
            body.Add("IdTarifTangki", _viewModel.SelectedData.IdTarifTangki);
            RestApiResponse Response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-tangki", body));


            if (!Response.IsError)
            {
                if (!_isTest)
                    DialogHost.Close("HublangRootDialog", dg);
                var Result = Response.Data;
                ShowResult(Result.Ui_msg, Result.Status);
            }
            else
                ShowResult(Response.Error.Message, false);

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string Msg, bool Status)
        {
            if (Msg != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(Msg, Status ? "success" : "danger");
                });
        }
    }
}
