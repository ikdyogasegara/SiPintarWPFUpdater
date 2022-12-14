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

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi.Kecamatan
{
    public class OnSubmitDeleteCommand : AsyncCommandBase
    {
        private readonly KecamatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitDeleteCommand(KecamatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            if (_viewModel.Section == "cabang")
            {
                url = $"/api/{ApiVersion}/master-cabang";

                entityId.Add("IdCabang", _viewModel.SelectedCabang.IdCabang);
            }
            else if (_viewModel.Section == "kecamatan")
            {
                url = $"/api/{ApiVersion}/master-kecamatan";

                entityId.Add("IdKecamatan", _viewModel.SelectedKecamatan.IdKecamatan);
            }
            else if (_viewModel.Section == "kelurahan")
            {
                url = $"/api/{ApiVersion}/master-kelurahan";

                entityId.Add("IdKelurahan", _viewModel.SelectedKelurahan.IdKelurahan);
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
            if (_viewModel.Section == "cabang")
            {
                _viewModel.SelectedCabang = null;
                _viewModel.CabangList.Clear();
            }
            else if (_viewModel.Section == "kecamatan")
            {
                _viewModel.SelectedKecamatan = null;
                _viewModel.KecamatanList.Clear();
            }
            else if (_viewModel.Section == "kelurahan")
            {
                _viewModel.SelectedKelurahan = null;
                _viewModel.KelurahanList.Clear();
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
