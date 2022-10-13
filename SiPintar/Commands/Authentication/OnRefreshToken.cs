using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Authentication
{
    [ExcludeFromCodeCoverage]
    public static class OnRefreshToken
    {
        public static async Task ExecuteAsync()
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

            var _restApi = new RestApiClientModel();

            bool status = false;

            var body = new Dictionary<string, dynamic>
            {
                { "RefreshToken", Application.Current.Resources["RefreshToken"]?.ToString() }
            };

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/master-token/refresh-token", body, true));
            if (!Response.IsError && Response.HasData)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var Data = Result.Data.ToObject<ObservableCollection<MasterTokenDto>>().FirstOrDefault();

                    if (Data != null)
                    {
                        // re-set token
                        await LiteDBExtension.SetToAppConfigAsync("UserToken", Data.AccessToken);
                        await LiteDBExtension.SetToAppConfigAsync("RefreshToken", Data.RefreshToken);

                        _restApi.SetToken(Data.AccessToken);

                        status = true;
                    }
                }
            }

            if (!status)
            {
                await Task.Delay(1);
                await LiteDBExtension.SetToAppConfigAsync("IsLogin", "0");

                Application.Current.Dispatcher.Invoke(delegate
                {
                    _ = DialogHost.Show(new DialogGlobalView("Informasi", "Sesi Anda telah habis, silahkan login ulang untuk menggunakan Aplikasi.", "warning"), "OptionDialog");

                    ((App)Application.Current).ShowLoginWindow(); // << gak mau jalan setelah auto logout,
                });
            }
        }
    }
}
