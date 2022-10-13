using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi.Rayon
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly RayonViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(RayonViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var entityId = new Dictionary<string, dynamic>();

            string url = null;
            if (_viewModel.Section == "wilayah")
            {
                url = $"/api/{ApiVersion}/master-wilayah";

                entityId.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah);
            }
            else if (_viewModel.Section == "area")
            {
                url = $"/api/{ApiVersion}/master-area";

                entityId.Add("IdArea", _viewModel.SelectedArea.IdArea);
            }
            else if (_viewModel.Section == "rayon")
            {
                url = $"/api/{ApiVersion}/master-rayon";

                entityId.Add("IdRayon", _viewModel.SelectedRayon.IdRayon);
            }

            var Response = await Task.Run(() => _restApi.DeleteAsync(url, entityId));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowResult(SuccessMessage, ErrorMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void Reload()
        {
            if (_viewModel.Section == "wilayah")
            {
                _viewModel.SelectedWilayah = null;
                _viewModel.WilayahList.Clear();
            }
            else if (_viewModel.Section == "area")
            {
                _viewModel.SelectedArea = null;
                _viewModel.AreaList.Clear();
            }
            else if (_viewModel.Section == "rayon")
            {
                _viewModel.SelectedRayon = null;
                _viewModel.RayonList.Clear();
            }

            _viewModel.OnLoadCommand.Execute(_viewModel.Section);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    CloseDialog();
                });

                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowError(ErrorMessage);
                    });
                }
                else if (SuccessMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowSuccess(SuccessMessage);
                    });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    LoadingDialog();
                });
        }

        [ExcludeFromCodeCoverage]
        private void LoadingDialog()
        {
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), "BacameterRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BacameterRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            Reload();
        }
    }
}
