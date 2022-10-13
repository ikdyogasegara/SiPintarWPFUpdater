using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.Bantuan;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.Bantuan.SaranPengaduan
{
    public class OnSubmitCommand : AsyncCommandBase
    {
        private readonly SaranPengaduanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnSubmitCommand(SaranPengaduanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;
            bool FlagPerluPerbaikan = false;


            _ = SaranPengaduanAsync(SuccessMessage, ErrorMessage);

            if (_viewModel.Komentar == null)
                return;

            _viewModel.IsLoadingForm = true;

            var selected = new ObservableCollection<MasterSaranPertanyaanDto>();
            var data = new ObservableCollection<MasterSaranPertanyaanDto>(_viewModel.MasterSaranPertanyaan);

            foreach (var item in data)
            {
                if (item.IsSelected && !selected.Any(i => i.IdPertanyaan == item.IdPertanyaan))
                    selected.Add(item);
                else if (!item.IsSelected && selected.Any(i => i.IdPertanyaan == item.IdPertanyaan))
                    selected.Remove(item);
            }

            _viewModel.SelectedMasterSaranPertanyaan = selected;

            if (_viewModel.SelectedMasterSaranPertanyaan.Count > 0)
            {
                FlagPerluPerbaikan = true;
                foreach (var item in _viewModel.SelectedMasterSaranPertanyaan)
                    _ = SaranPerbaikanAsync(SuccessMessage, ErrorMessage, (int)item.IdPertanyaan, FlagPerluPerbaikan);
            }
            else
                _ = SaranPerbaikanAsync(SuccessMessage, ErrorMessage, 0, FlagPerluPerbaikan);



            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
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

        private async Task SaranPerbaikanAsync(string SuccessMessage, string ErrorMessage, int IdPertanyaan, bool FlagPerluPerbaikan)
        {

            var ResponseSaranPertanyaan = await _restApi.GetAsync($"/api/{ApiVersion}/saran-pengaduan");
            var SaranPengaduan = new List<SaranPengaduanDto>();
            string maxidsaranpengaduan = "";
            if (!ResponseSaranPertanyaan.IsError)
            {
                var Result = ResponseSaranPertanyaan.Data;

                if (Result.Status && Result.Data != null)
                    SaranPengaduan = Result.Data.ToObject<List<SaranPengaduanDto>>();

                if (ResponseSaranPertanyaan.HasData)
                    maxidsaranpengaduan = SaranPengaduan.Max(x => x.IdSaranPengaduan) + 1.ToString();

                await Task.Run(() =>
                {
                    var body = new Dictionary<string, dynamic>();
                    body.Add("IdSaranPengaduan", maxidsaranpengaduan);
                    body.Add("IdPertanyaan", IdPertanyaan);
                    body.Add("FlagPerluPerbaikan", FlagPerluPerbaikan);
                    RestApiResponse Response = _restApi.PostAsync($"/api/{ApiVersion}/saran-perbaikan", body).Result;
                    if (!Response.IsError)
                    {
                        var Result = Response.Data;
                        if (Result.Status)
                            SuccessMessage = Response.Data.Ui_msg;
                        else
                            ErrorMessage = Response.Data.Ui_msg;
                    }
                    else
                        ErrorMessage = Response.Error.Message;

                });

                ShowResult(SuccessMessage, ErrorMessage);
            }
        }

        private async Task SaranPengaduanAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdModule", "1");
                body.Add("NamaPetugas", _viewModel.NamaPetugas);
                body.Add("Bagian", _viewModel.Bagian);
                body.Add("NamaPdam", _viewModel.NamaPDAM);
                body.Add("NoHp", _viewModel.NoHp);
                body.Add("Email", _viewModel.Email);
                body.Add("Rating", _viewModel.Rating.ToString());
                body.Add("Komentar", _viewModel.Komentar);
                RestApiResponse Response = _restApi.PostAsync($"/api/{ApiVersion}/saran-pengaduan", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;

            });

            ShowResult(SuccessMessage, ErrorMessage);
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
            SuccessMessage = "Saran & Pengaduan Berhasil Dikirim";
            var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnResetCommand.Execute(null);
        }
    }
}
