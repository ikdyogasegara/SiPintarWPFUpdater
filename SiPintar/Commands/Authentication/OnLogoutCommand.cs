using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Authentication
{
    [ExcludeFromCodeCoverage]
    public class OnLogoutCommand : AsyncCommandBase
    {
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLogoutCommand(IRestApiClientModel restApi, bool isTest = false)
        {
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var body = new Dictionary<string, dynamic>
            {
                { "RefreshToken", Application.Current.Resources["RefreshToken"]?.ToString()}
            };

            var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-token/revoke-token", body, true));

            DialogHelper.CloseDialog(_isTest, true, "MainRootDialog");

            //sementara di disable pengecekan revoke token..
            if (!response.IsError)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    _ = LiteDBExtension.SetToAppConfigAsync("IsLogin", "0");
                    ((App)Application.Current).ShowLoginWindow();
                });
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, moduleName: "main", true);
            }
            await Task.FromResult(_isTest);
        }
    }
}
