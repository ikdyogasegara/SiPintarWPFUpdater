using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using LiteDB;
using Serilog;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class LiteDBExtension
    {
        private static readonly string s_configPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";
        private static LiteDatabase s_db;
        private static bool s_dbIsBeingUsed;
        private static readonly List<string> s_moduleConfigKey = new List<string>() {
            "BACAMETER_MODULE_ID",
            "BILLING_MODULE_ID",
            "LOKET_MODULE_ID",
            "HUBLANG_MODULE_ID",
            "DISTRIBUSI_MODULE_ID",
            "PERENCANAAN_MODULE_ID",
            "GUDANG_MODULE_ID",
            "PERSONALIA_MODULE_ID",
            "AKUNTANSIKEUANGAN_MODULE_ID",
        };
        private static readonly List<string> s_moduleConfigApi = new List<string>() {
            "BACAMETER_MODULE_API",
            "BILLING_MODULE_API",
            "LOKET_MODULE_API",
            "HUBLANG_MODULE_API",
            "DISTRIBUSI_MODULE_API",
            "PERENCANAAN_MODULE_API",
            "GUDANG_MODULE_API",
            "PERSONALIA_MODULE_API",
            "AKUNTANSIKEUANGAN_MODULE_API",
        };

        private static readonly List<string> s_moduleConfigReportApi = new List<string>() {
            "BACAMETER_MODULE_REPORT_API",
            "BILLING_MODULE_REPORT_API",
            "LOKET_MODULE_REPORT_API",
            "HUBLANG_MODULE_REPORT_API",
            "DISTRIBUSI_MODULE_REPORT_API",
            "PERENCANAAN_MODULE_REPORT_API",
            "GUDANG_MODULE_REPORT_API",
            "PERSONALIA_MODULE_REPORT_API",
            "AKUNTANSIKEUANGAN_MODULE_REPORT_API",
        };

        private static string GetLiteDBPath()
        {
            DotNetEnv.Env.Load(s_configPath);
            var password = DotNetEnv.Env.GetString("LITEDB_PASSWORD");

            return "Filename=" + AppDomain.CurrentDomain.BaseDirectory + $"data.db;Password={password}";
        }

        public static async Task SetupAppAsync()
        {
            if (s_db == null)
                await OpenDbAsync();
            var appConfigColl = s_db.GetCollection<AppConfig>("AppConfig");

            //create if not exist
            if (appConfigColl.Count() == 0)
                CreateAppConfig(appConfigColl);

            //set version
            var dataVersion = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "versi") ??
                              new AppConfig() { Key = "Versi" };
            dataVersion.Value = AppVersion.Version;
            appConfigColl.Upsert(dataVersion);

            // set id module
            foreach (var item in s_moduleConfigKey)
            {
                var key = item.Split('_')[0];
                var configKey = "idmodule" + item.Split('_')[0].ToLower();
                var appKey = "IdModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                var idModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ??
                               new AppConfig() { Key = appKey };
                idModule.Value = DotNetEnv.Env.GetString(item);
                appConfigColl.Upsert(idModule);
            }

            // set API module
            foreach (var item in s_moduleConfigApi)
            {
                var key = item.Split('_')[0];
                var configKey = "apimodule" + item.Split('_')[0].ToLower();
                var appKey = "ApiModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ??
                                new AppConfig() { Key = appKey };
                apiModule.Value = DotNetEnv.Env.GetString(item);
                appConfigColl.Upsert(apiModule);
            }

            // set API Report module
            foreach (var item in s_moduleConfigReportApi)
            {
                var key = item.Split('_')[0];
                var configKey = "apireportmodule" + item.Split('_')[0].ToLower();
                var appKey = "ApiReportModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ??
                                new AppConfig() { Key = appKey };
                apiModule.Value = DotNetEnv.Env.GetString(item);
                appConfigColl.Upsert(apiModule);
            }

            // Set to app resource (data from local LiteDB)
            await DisposeDbAsync();
            await SetAppResourcesAsync();

            return;
        }

        private static void CreateAppConfig(ILiteCollection<AppConfig> collection)
        {
            DotNetEnv.Env.Load(s_configPath);
            var loginExpired = DotNetEnv.Env.GetString("CLIENT_EXPIRE_ACCESS");

            collection.Insert(new AppConfig() { Key = "IsLogin", Value = "0" });
            collection.Insert(new AppConfig() { Key = "LoginExpired", Value = loginExpired });
            collection.Insert(new AppConfig() { Key = "IdUser", Value = null });
            collection.Insert(new AppConfig() { Key = "UserToken", Value = null });
            collection.Insert(new AppConfig() { Key = "RefreshToken", Value = null });
            collection.Insert(new AppConfig() { Key = "UserName", Value = null });
            collection.Insert(new AppConfig() { Key = "FullName", Value = null });
            collection.Insert(new AppConfig() { Key = "NoIdentitas", Value = null });
            collection.Insert(new AppConfig() { Key = "IdRole", Value = null });
            collection.Insert(new AppConfig() { Key = "NamaRole", Value = null });
            collection.Insert(new AppConfig() { Key = "ModuleAkses", Value = null });
            collection.Insert(new AppConfig() { Key = "Permissions", Value = null });
            collection.Insert(new AppConfig() { Key = "OnBoardBacameterFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardBillingFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardHublangFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardLoketFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardDistribusiFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardPerencanaanFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardGudangFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardPersonaliaFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardAkuntansiFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "OnBoardKeuanganFlag", Value = "0" });
            collection.Insert(new AppConfig() { Key = "LoginTimeout", Value = null });
            collection.Insert(new AppConfig() { Key = "Versi", Value = null });
            collection.Insert(new AppConfig() { Key = "IdPdam", Value = null });

            foreach (var item in s_moduleConfigKey)
            {
                var key = item.Split('_')[0];
                var appKey = "IdModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                collection.Insert(new AppConfig() { Key = appKey, Value = null });
            }
        }

        public static async Task SetAppResourcesAsync()
        {
            if (s_db == null)
                await OpenDbAsync();

            var appConfigColl = s_db.GetCollection<AppConfig>("AppConfig");

            var isLogin = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "islogin") ?? null;
            var loginExpired = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "loginexpired") ?? null;
            var idUser = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "iduser") ?? null;
            var userToken = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "usertoken") ?? null;
            var refreshToken = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "refreshtoken") ?? null;
            var userName = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "username") ?? null;
            var fullName = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "fullname") ?? null;
            var noIdentitas = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "noidentitas") ?? null;
            var idRole = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "idrole") ?? null;
            var namaRole = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "namarole") ?? null;
            var moduleAkses = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "moduleakses") ?? null;
            var permissions = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "permissions") ?? null;
            var onBoardBacameterFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardbacameterflag") ?? null;
            var onBoardBillingFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardbillingflag") ?? null;
            var onBoardHublangFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardhublangflag") ?? null;
            var onBoardLoketFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardloketflag") ?? null;
            var onBoardDistribusiFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboarddistribusiflag") ?? null;
            var onBoardPerencanaanFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardperencanaanflag") ?? null;
            var onBoardGudangFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardgudangflag") ?? null;
            var onBoardPersonaliaFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardpersonaliaflag") ?? null;
            var onBoardAkuntansiFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardakuntansiflag") ?? null;
            var onBoardKeuanganFlag = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "onboardkeuanganflag") ?? null;
            var loginTimeout = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "logintimeout") ?? null;
            var versi = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "versi") ?? null;
            var idPdam = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == "idpdam") ?? null;

            // Save to "Application.Current.Resources"
            var dataToSave = new Dictionary<string, string>
            {
                {"IsLogin", isLogin != null ? isLogin.Value : "0"},
                {"LoginExpired", loginExpired != null ? loginExpired.Value : "300"},
                {"IdUser", idUser?.Value},
                {"UserToken", userToken?.Value},
                {"RefreshToken", refreshToken?.Value},
                {"UserName", userName?.Value},
                {"FullName", fullName?.Value},
                {"NoIdentitas", noIdentitas?.Value},
                {"IdRole", idRole?.Value},
                {"NamaRole", namaRole?.Value},
                {"ModuleAkses", moduleAkses?.Value},
                {"Permissions", permissions?.Value},
                {"OnBoardBacameterFlag", onBoardBacameterFlag != null ? onBoardBacameterFlag.Value : "0"},
                {"OnBoardBillingFlag", onBoardBillingFlag != null ? onBoardBillingFlag.Value : "0"},
                {"OnBoardHublangFlag", onBoardHublangFlag != null ? onBoardHublangFlag.Value : "0"},
                {"OnBoardLoketFlag", onBoardLoketFlag != null ? onBoardLoketFlag.Value : "0"},
                {"OnBoardDistribusiFlag", onBoardDistribusiFlag != null ? onBoardDistribusiFlag.Value : "0"},
                {"OnBoardPerencanaanFlag", onBoardPerencanaanFlag != null ? onBoardPerencanaanFlag.Value : "0"},
                {"OnBoardGudangFlag", onBoardGudangFlag != null ? onBoardGudangFlag.Value : "0"},
                {"OnBoardPersonaliaFlag", onBoardPersonaliaFlag != null ? onBoardPersonaliaFlag.Value : "0"},
                {"OnBoardAkuntansiFlag", onBoardAkuntansiFlag != null ? onBoardAkuntansiFlag.Value : "0"},
                {"OnBoardKeuanganFlag", onBoardKeuanganFlag != null ? onBoardKeuanganFlag.Value : "0"},
                {"LoginTimeout", loginTimeout?.Value},
                {"Versi", versi?.Value},
                {"IdPdam", idPdam?.Value}
            };

            foreach (var item in s_moduleConfigKey)
            {
                var key = item.Split('_')[0];
                var configKey = "idmodule" + item.Split('_')[0].ToLower();
                var appKey = "IdModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                var idModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ?? null;
                dataToSave.Add(appKey, idModule?.Value);
            }

            foreach (var item in s_moduleConfigApi)
            {
                var key = item.Split('_')[0];
                var configKey = "apimodule" + item.Split('_')[0].ToLower();
                var appKey = "ApiModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ?? null;
                dataToSave.Add(appKey, apiModule?.Value);
            }

            foreach (var item in s_moduleConfigReportApi)
            {
                var key = item.Split('_')[0];
                var configKey = "apireportmodule" + item.Split('_')[0].ToLower();
                var appKey = "ApiReportModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ?? null;
                dataToSave.Add(appKey, apiModule?.Value);
            }

            foreach (var item in dataToSave)
            {
                if (!Application.Current.Resources.Contains(item.Key))
                    Application.Current.Resources.Add(item.Key, item.Value);
                else
                    Application.Current.Resources[item.Key] = item.Value;
            }
            await DisposeDbAsync();
        }

        public static async Task<string> GetAppConfigAsync(string key)
        {
            if (s_db == null)
                await OpenDbAsync();

            var appConfigColl = s_db.GetCollection<AppConfig>("AppConfig");
            var data = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == key.ToLower());
            var result = data?.Value;
            await DisposeDbAsync();
            return result;
        }

        public static async Task SetToAppConfigAsync(string key, string value, ObservableCollection<KeyValuePair<string, string>> dataList = null)
        {
            while (s_dbIsBeingUsed)
            {
                await Task.Delay(500);
            }
            // set to db
            if (s_db == null)
                await OpenDbAsync();

            var appConfigColl = s_db.GetCollection<AppConfig>("AppConfig");
            if (dataList == null)
            {
                // set to app resources
                if (Application.Current != null)
                {
                    if (Application.Current.Resources.Contains(key))
                        Application.Current.Resources[key] = value;
                    else
                        Application.Current.Resources.Add(key, value);
                }
                var data = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == key.ToLower()) ??
                           new AppConfig() { Key = key };
                data.Value = value;
                appConfigColl.Upsert(data);
                await DisposeDbAsync();
            }
            else
            {
                foreach (var item in dataList)
                {
                    // set to app resources
                    if (Application.Current != null)
                    {
                        if (Application.Current.Resources.Contains(item.Key))
                            Application.Current.Resources[item.Key] = item.Value;
                        else
                            Application.Current.Resources.Add(item.Key, item.Value);
                    }
                    var data = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == item.Key.ToLower()) ??
                               new AppConfig() { Key = item.Key };
                    data.Value = item.Value;
                    appConfigColl.Upsert(data);
                }
                await DisposeDbAsync();
            }
        }

        public static async Task ReSetupApiModuleAsync()
        {
            await OpenDbAsync();
            var appConfigColl = s_db.GetCollection<AppConfig>("AppConfig");
            if (appConfigColl.Count() > 0)
            {
                // set API module
                foreach (var item in s_moduleConfigApi)
                {
                    var key = item.Split('_')[0];
                    var configKey = "apimodule" + item.Split('_')[0].ToLower();
                    var appKey = "ApiModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                    var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ?? new AppConfig() { Key = appKey };
                    apiModule.Value = DotNetEnv.Env.GetString(item);
                    appConfigColl.Upsert(apiModule);
                }

                var dataToSave = new Dictionary<string, string>();

                foreach (var item in s_moduleConfigApi)
                {
                    var key = item.Split('_')[0];
                    var configKey = "apimodule" + item.Split('_')[0].ToLower();
                    var appKey = "ApiModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                    var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ?? new AppConfig();
                    dataToSave.Add(appKey, apiModule?.Value);
                }

                foreach (var item in s_moduleConfigReportApi)
                {
                    var key = item.Split('_')[0];
                    var configKey = "apireportmodule" + item.Split('_')[0].ToLower();
                    var appKey = "ApiReportModule" + key.Substring(0, 1).ToUpper() + key[1..].ToLower();
                    var apiModule = appConfigColl.FindOne(x => x.Key.Trim().ToLower() == configKey) ?? new AppConfig();
                    dataToSave.Add(appKey, apiModule?.Value);
                }

                foreach (var item in dataToSave)
                {
                    if (!Application.Current.Resources.Contains(item.Key))
                        Application.Current.Resources.Add(item.Key, item.Value);
                    else
                        Application.Current.Resources[item.Key] = item.Value;
                }
            }

            await DisposeDbAsync();
        }

        private static async Task OpenDbAsync()
        {
            //Debug.WriteLine("Opening LiteDB ...") use to trace LiteDB
            try
            {
                s_db = new LiteDatabase(GetLiteDBPath());
            }
            catch (Exception ex)
            {
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "data.db")))
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "data.db"));
                s_db = new LiteDatabase(GetLiteDBPath());
                Log.Logger.Error(ex.ToString());
                //Debug.WriteLine($"Error Opening LiteDB : {ex.Message}") use to trace LiteDB
            }

            s_dbIsBeingUsed = true;
            await Task.FromResult(true);
        }

        private static async Task DisposeDbAsync()
        {
            //Debug.WriteLine("Closing LiteDB ...") use to trace LiteDB
            try
            {
                if (s_db != null)
                    s_db.Dispose();

                s_db = null;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                //Debug.WriteLine($"Error Closing LiteDB : {ex.Message}") use to trace LiteDB
            }
            s_dbIsBeingUsed = false;
            await Task.FromResult(true);
        }
    }
}
