using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using DeviceId;
using Serilog;

namespace SiPintar.Utilities.Services.Filter;

[ExcludeFromCodeCoverage]
public class LoadSettingService
{
    private readonly IRestApiClientModel _restApi;
    private string _apiVersion = "v1";

    public LoadSettingService()
    {
        _restApi = new RestApiClientModel();
    }

    public async Task<bool> GetSpecialTokenAsync()
    {
        var status = false;

        try
        {
            var body = new Dictionary<string, dynamic>
            {
                { "IdComputer", GetComputerId() },
                { "NamaUser", "bsa-token-force" },
                { "Password", "bsa-token-force66521vs!^-!" }
            };

            var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-token/authenticate", body, true));
            if (!response.IsError && response.HasData)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var data = result.Data.ToObject<ObservableCollection<MasterTokenDto>>().FirstOrDefault();

                    if (data != null)
                    {
                        // set token
                        await LiteDBExtension.SetToAppConfigAsync("IdUser", data.IdUser.ToString());
                        await LiteDBExtension.SetToAppConfigAsync("UserToken", data.AccessToken);
                        await LiteDBExtension.SetToAppConfigAsync("RefreshToken", data.RefreshToken);
                        await LiteDBExtension.SetToAppConfigAsync("UserName", "bsa-token-force");

                        _restApi.SetToken(data.AccessToken);

                        status = true;
                    }
                }
                else
                    Log.Logger.Error(response.Data.Ui_msg);
            }
            else
                Log.Logger.Error(response.Error.Message);
        }
        catch (Exception e)
        {
            Debug.WriteLine("error get token: " + e);
            Log.Logger.Error(e.ToString());
        }

        return status;
    }

    private string GetComputerId()
    {
        var computerId = "UNKNOWN-DEVICEID";

        try
        {
            computerId = new DeviceIdBuilder()
                .AddMachineName()
                .AddOsVersion()
                .AddMacAddress()
                .OnWindows(windows => windows
                    .AddProcessorId()
                    .AddMotherboardSerialNumber()
                    .AddSystemDriveSerialNumber())
                .ToString();

        }
        catch (Exception e)
        {
            Debug.WriteLine("error get deviceId " + e);
            Log.Logger.Error("error get deviceId " + e);
        }

        return computerId;
    }

    public async Task GetPengaturanAsync()
    {
        var param = new Dictionary<string, dynamic>();

        var response = await _restApi.GetAsync($"/api/pengaturan", param);
        if (!response.IsError)
        {
            var result = response.Data;

            if (result.Status && result.Data != null && result.Data.Count > 0)
            {
                MasterListGlobal.Pengaturan = result.Data.ToObject<ObservableCollection<PengaturanDto>>();

                var apiData = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "api_version");

                _apiVersion = apiData != null ? apiData.Value : _apiVersion;
                _apiVersion = !_apiVersion.ToLower().Contains("v") ? $"v{_apiVersion}" : _apiVersion;
                await LiteDBExtension.SetToAppConfigAsync("api_version", _apiVersion);

                #region Cek Lock Verifikasi Bacameter

                var lockVerifikasiBacameter = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "lock_verifikasi_bacameter");
                if (lockVerifikasiBacameter != null)
                {
                    AppSetting.LockVerifikasiBacameter = lockVerifikasiBacameter.Value == "1";
                }

                #endregion

                #region Cek Pelanggan Limbah

                var pelangganLimbah = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "pelanggan_limbah");
                if (pelangganLimbah != null)
                {
                    AppSetting.PelangganLimbah = pelangganLimbah.Value == "1";
                }

                #endregion

                #region Cek Pelanggan L2T2

                var pelangganLltt = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "pelanggan_lltt");
                if (pelangganLltt != null)
                {
                    AppSetting.PelangganLltt = pelangganLltt.Value == "1";
                }

                #endregion

                #region Premium Module

                var premiumModule = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "premium_module");
                if (pelangganLltt != null)
                {
                    AppSetting.PremiumModule = premiumModule.Value == "1";
                }

                #endregion
            }
        }
    }

    public async Task GetPdamAsync()
    {
        var param = new Dictionary<string, dynamic>();

        var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pdam", param);
        if (!response.IsError)
        {
            var result = response.Data;

            if (result.Status && result.Data != null && result.Data.Count > 0)
            {
                var data = result.Data.ToObject<ObservableCollection<MasterPdamDto>>().FirstOrDefault();

                if (data != null)
                {
                    await LiteDBExtension.SetToAppConfigAsync("NamaPdam", data.NamaPdam);
                    await LiteDBExtension.SetToAppConfigAsync("Provinsi", data.Provinsi);
                    await LiteDBExtension.SetToAppConfigAsync("Kota", data.Kota);
                    await LiteDBExtension.SetToAppConfigAsync("AlamatLengkap", data.AlamatLengkap);

                    // detail
                    foreach (var item in data.Detail)
                        await LiteDBExtension.SetToAppConfigAsync(item.Key.ToString().Trim(), item.Value?.ToString().Trim());
                }
            }
        }
    }
}
