using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;
using SiPintar.Views;

namespace SiPintar.Commands.Loket.TutupLoket
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TutupLoketViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(TutupLoketViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTutupLoketToday = false;
            var today = DateTime.Now;
            var tglMulai = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            var TglAkhir = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            var param = new Dictionary<string, dynamic> { { "IdLoket", Application.Current.Resources["IdLoket"]?.ToString() }, { "TglPenerimaanMulai", tglMulai }, { "TglPenerimaanSampaiDengan", TglAkhir }, };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/tutup-loket", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var temp = result.Data.ToObject<ObservableCollection<TutupLoketDto>>();

                    if (temp != null && temp.Count > 0)
                    {
                        _viewModel.IsTutupLoketToday = true;
                    }
                    else
                    {
                        string errorMessage = null;
                        _viewModel.IsLoading = true;
                        _viewModel.PenerimaanForm = 0;
                        var idLoket = Application.Current.Resources["IdLoket"] != null ? Application.Current.Resources["IdLoket"].ToString() : "1";

                        param = new Dictionary<string, dynamic> { { "IdLoket", idLoket }, { "TglPenerimaan", DateTime.Now.ToString("yyyy-MM-dd") }, };

                        if (_isTest)
                            param.Add("TestId", _viewModel.TestId);

                        var response1 = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/tutup-loket-rekap-penerimaan", param);
                        if (!response1.IsError)
                        {
                            var result1 = response1.Data;
                            if (result1.Status && result1.Data != null)
                            {
                                var data = result1.Data.ToObject<ObservableCollection<TutupLoketSummaryDto>>();

                                decimal total = 0;
                                foreach (var item in data)
                                    total += item.Jumlah ?? 0;

                                _viewModel.PenerimaanForm = total;
                            }
                            else
                            {
                                errorMessage = response1.Data.Ui_msg;
                            }
                        }
                        else
                        {
                            errorMessage = response1.Error.Message;
                        }

                        ShowSnackbar(errorMessage);
                        _viewModel.IsLoading = false;
                    }
                }
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbar(string errorMessage)
        {
            if (errorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (LoketView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(errorMessage, "danger");
                });
            }
        }
    }
}
