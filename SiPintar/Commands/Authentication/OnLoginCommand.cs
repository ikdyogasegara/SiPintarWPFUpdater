using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using DeviceId;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Utilities;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Authentication
{
    [ExcludeFromCodeCoverage]
    public class OnLoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private string _errorMessage;

        public OnLoginCommand(LoginViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            if (string.IsNullOrEmpty(_viewModel.NamaUser))
            {
                _viewModel.ErrorUsername = "Masukkan nama user";
                return;
            }
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                _viewModel.ErrorPassword = "Masukkan password";
                return;
            }

            _viewModel.ErrorUsername = string.Empty;
            _viewModel.ErrorPassword = string.Empty;
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;

            _errorMessage = null;

            var isSuccessGetToken = await GetTokenAsync(passwordBox);

            CheckIfError();

            // if all process success, set login flag
            if (isSuccessGetToken && !_isTest)
            {
                await LiteDBExtension.SetToAppConfigAsync("IsLogin", "1");
                var getData = new CurrentUserData(_restApi);
                var statusRespone = await getData.GetUserDataAsync();

                if (statusRespone == "sukses")
                {
                    ShowMainWindow();
                }
                else
                {
                    _viewModel.ErrorMessage = statusRespone;
                }
            }

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        private async Task<bool> GetTokenAsync(PasswordBox passwordBox)
        {
            var status = false;

            try
            {
                var body = new Dictionary<string, dynamic>
                {
                    { "IdComputer", GetComputerId() },
                    { "NamaUser", _viewModel.NamaUser },
                    { "Password", passwordBox.Password }
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
                            await LiteDBExtension.SetToAppConfigAsync("UserName", _viewModel.NamaUser);

                            _restApi.SetToken(data.AccessToken);

                            status = true;
                        }
                    }
                    else
                        _errorMessage = response.Data.Ui_msg;
                }
                else
                    _errorMessage = response.Error.Message;
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

        private void CheckIfError()
        {
            if (_errorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate { _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", _errorMessage, "error"), "LoginViewDialog"); });
        }

        private void ShowMainWindow()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { ((App)Application.Current).ShowMainWindow(); });
        }
    }
}
