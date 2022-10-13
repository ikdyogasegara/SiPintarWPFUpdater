using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnSearchRiwayatPembayaranCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSearchRiwayatPembayaranCommand(PelangganLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            string ErrorMessage = null;
            _viewModel.IsEmptyDialog = false;

            _viewModel.PembayaranList = new ObservableCollection<RekeningLimbahPelunasanDanPembatalanDto>();

            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPelangganLimbah" , _viewModel.SelectedData.IdPelangganLimbah },
                { "StatusTransaksi", true },
            };

            if (!_viewModel.TahunPembayaranForm.Equals(default(KeyValuePair<string, string>)))
            {
                param.Add("TahunTransaksi", _viewModel.TahunPembayaranForm.Value);
            }

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-limbah-history-pelunasan-pembatalan", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.PembayaranList = Result.Data.ToObject<ObservableCollection<RekeningLimbahPelunasanDanPembatalanDto>>();
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
    }
}
