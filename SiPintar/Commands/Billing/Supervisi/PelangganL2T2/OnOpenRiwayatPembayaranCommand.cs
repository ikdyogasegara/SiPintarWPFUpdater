using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Billing.Supervisi.PelangganL2T2;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnOpenRiwayatPembayaranCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnOpenRiwayatPembayaranCommand(PelangganL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _testBody = testBody;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            string ErrorMessage = null;
            _viewModel.IsEmptyDialog = false;

            _viewModel.PembayaranList = new ObservableCollection<RekeningLlttPelunasanDanPembatalanDto>();

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir" , _viewModel.SelectedData.IdPelangganLltt },
                { "StatusTransaksi", true },
            };

            if (_viewModel.YearPembayaran.HasValue)
            {
                param.Add("TahunTransaksi", _viewModel.YearPembayaran);
            }

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-lltt-history-pelunasan-pembatalan", _testBody ?? param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PembayaranList = Result.Data.ToObject<ObservableCollection<RekeningLlttPelunasanDanPembatalanDto>>();
                }
                else
                {
                    ErrorMessage = Response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);
            _viewModel.IsEmptyDialog = !_viewModel.PembayaranList.Any();
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        private void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null)
            {
                AppDispatcherHelper.Run(_isTest, () =>
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest && !DialogHost.IsDialogOpen("BillingRootDialog"))
            {
                int minYear = DateTime.Now.Year - 10;
                _viewModel.ListYearPembayaran = new List<int>(Enumerable.Range(minYear, 10 + 1));

                _ = DialogHost.Show(new RiwayatPembayaranView(_viewModel), "BillingRootDialog");
            }
        }

    }
}
