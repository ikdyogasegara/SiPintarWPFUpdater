using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using Serilog;
using SiPintar.Helpers;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public class CurrentUserData
    {
        private readonly IRestApiClientModel _restApi;
        private string _apiVersion = "v1"; // default
        private string _idPlatform;

        private static readonly string s_configEnvPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";
        private static readonly string s_responseMessageIniPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\response_message.ini";

        public CurrentUserData(IRestApiClientModel restApi)
        {
            _restApi = restApi;
            GetConfigurationFromEnv();
        }

        private void GetConfigurationFromEnv()
        {
            DotNetEnv.Env.Load(s_configEnvPath);

            _apiVersion = DotNetEnv.Env.GetString("API_VERSION");
            _ = LiteDBExtension.SetToAppConfigAsync("api_version", _apiVersion);

            _idPlatform = DotNetEnv.Env.GetString("ID_PLATFORM");
            _ = LiteDBExtension.SetToAppConfigAsync("IdPlatform", _idPlatform);

            //Lokasi Foto Bacameter disimpen di AppSetting Static
            AppSetting.AksesFotoMeter = DotNetEnv.Env.GetString("AKSES_FOTO_BACAMETER") == "TRUE";
            AppSetting.LokasiFotoMeter = DotNetEnv.Env.GetString("LOKASI_FOTO_BACAMETER") ?? "D:\\Foto";
        }

        public async Task<string> GetUserDataAsync()
        {
            var status = "";
            try
            {
                var idUser = Application.Current.Resources["IdUser"].ToString(); // already set when get token

                var param = new Dictionary<string, dynamic> { { "IdUser", idUser }, { "IncludeAkses", true }, };

                var response = await _restApi.GetAsync($"/api/{_apiVersion}/master-user", param);
                if (!response.IsError && response.HasData)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<MasterUserDto>>().FirstOrDefault();

                        if (data != null)
                        {
                            if (data.Aktif == false)
                            {
                                status = "Pengguna tidak aktif !";
                            }
                            else
                            {
                                var dataList = new ObservableCollection<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("FullName", data.Nama),
                                    new KeyValuePair<string, string>("UserName", data.NamaUser),
                                    new KeyValuePair<string, string>("NoIdentitas", data.NoIdentitas),
                                    new KeyValuePair<string, string>("IdRole", data.IdRole.ToString()),
                                    new KeyValuePair<string, string>("NamaRole", data.NamaRole),
                                    new KeyValuePair<string, string>("IdPdam", data.IdPdam.ToString()),
                                    new KeyValuePair<string, string>("IdLoket", data.IdLoket != null ? data.IdLoket.ToString() : "1"),
                                    new KeyValuePair<string, string>("KodeLoket", data.KodeLoket),
                                    new KeyValuePair<string, string>("NamaLoket", data.NamaLoket)
                                };

                                var permission = new List<string>();
                                var moduleAkses = new List<string>();
                                foreach (var item in data.Akses)
                                {
                                    // per menu
                                    if (!permission.Contains($"{item.NamaModule}.{item.NamaAccess}") && item.Value == true)
                                        permission.Add($"{item.NamaModule}.{item.NamaAccess}");

                                    // per module
                                    if (!moduleAkses.Contains(item.IdModule.ToString()) && item.Value == true)
                                        moduleAkses.Add(item.IdModule.ToString());
                                }

                                dataList.Add(new KeyValuePair<string, string>("Permissions", string.Join(',', permission.Select(x => x.ToString()).ToArray())));
                                dataList.Add(new KeyValuePair<string, string>("ModuleAkses", string.Join(',', moduleAkses.Select(x => x.ToString()).ToArray())));

                                var namaRole = Application.Current.Resources["NamaRole"]?.ToString();
                                if (namaRole != null)
                                {
                                    AppSetting.IsNamaRoleAdmin = namaRole.ToLower().Contains("admin");
                                }

                                await LiteDBExtension.SetToAppConfigAsync(null, null, dataList);
                                status = "sukses";
                            }
                        }
                    }
                    else
                    {
                        status = response.Data.Ui_msg;
                        DialogHelper.ShowSnackbar(false, "danger", response.Data.Ui_msg, "main", true);
                    }
                }
                else
                {
                    status = response.Error.Message;
                    DialogHelper.ShowSnackbar(false, "danger", response.Error.Message, "main", true);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("error get userdata: " + e);
                Log.Logger.Error(e.Message);
                DialogHelper.ShowSnackbar(false, "danger", "error get userdata: " + e, "main", true);
            }

            return status;
        }

        public async Task<ObservableCollection<MasterResponseUiDto>> GetResponseMessageDataAsync()
        {
            var resultData = new ObservableCollection<MasterResponseUiDto>();
            var _errorMessage = "";

            try
            {
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(s_responseMessageIniPath);

                // set default first..
                foreach (var item in data["default"])
                {
                    await LiteDBExtension.SetToAppConfigAsync(item.KeyName, item.Value);
                }

                var idPdam = Application.Current.Resources["IdPdam"].ToString();
                var idPlatform = Application.Current.Resources["IdPlatform"].ToString();

                var param = new Dictionary<string, dynamic> { { "IdPdam", idPdam }, { "IdPlatform", idPlatform } };

                var response = await Task.Run(() => _restApi.GetAsync($"/api/{_apiVersion}/master-response-ui", param, true));
                if (!response.IsError && response.HasData)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        resultData = result.Data.ToObject<ObservableCollection<MasterResponseUiDto>>();

                        if (resultData != null)
                        {
                            // set in file + app resources..
                            foreach (var item in resultData)
                            {
                                data["sync"][item.Key] = item.Message;
                                await LiteDBExtension.SetToAppConfigAsync(item.Key, item.Message);
                            }

                            parser.WriteFile(s_responseMessageIniPath, data);
                        }
                    }
                    else
                        _errorMessage = response.Data.Ui_msg;
                }
                else
                {
                    _errorMessage = response.Error.Message;
                }

                if (_errorMessage != null)
                {
                    Debug.WriteLine("response message: " + _errorMessage);
                    Log.Logger.Error("Failed to get response message from server. " + _errorMessage);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("response message: " + e);
                Log.Logger.Error("Failed to setup response message");
            }

            return resultData;
        }
    }
}
