using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Materai
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly MateraiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(MateraiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _testBody = testBody;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ErrorMessage = null;

            _viewModel.ListYear = new List<int>();

            _viewModel.IsLoading = true;

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-meterai", _testBody);

            _viewModel.DataMeterai = new ObservableCollection<MasterMeteraiDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DataMeterai = Result.Data.ToObject<ObservableCollection<MasterMeteraiDto>>();

                    GeneratePeriodeAkhir();

                    _viewModel.DataMeterai = new ObservableCollection<MasterMeteraiDto>(_viewModel.DataMeterai.OrderBy(x => x.KodePeriodeMulaiBerlaku).ToList());
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;

            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            ShowSnackbar(ErrorMessage);

            if (_viewModel.DataMeterai.Count == 0)
                _viewModel.IsEmpty = true;

            int minYear = 0;
            if (!_viewModel.IsEmpty)
            {
                minYear = (_viewModel.DataMeterai.Min(g => g.KodePeriodeMulaiBerlaku) ?? ((DateTime.Now.Year * 100) + DateTime.Now.Month)) / 100;
            }
            else
            {
                minYear = DateTime.Now.Year - 10;
            }

            _viewModel.ListYear = new List<int>(Enumerable.Range(minYear, DateTime.Now.Year - minYear + 1));
            _viewModel.IsLoading = false;
        }

        private void GeneratePeriodeAkhir()
        {
            foreach (var item in _viewModel.DataMeterai)
            {
                var temp = _viewModel.DataMeterai.Where(x => x.KodePeriodeMulaiBerlaku > item.KodePeriodeMulaiBerlaku)
                    .Min(x => x.KodePeriodeMulaiBerlaku);
                if (temp.HasValue)
                {
                    var temp1 = temp.Value.ToString();
                    var temp2 = DateTime.ParseExact(temp1, "yyyyMM", CultureInfo.InvariantCulture);
                    temp2 = temp2.AddMonths(-1);
                    item.NamaPeriodeAkhir = temp2.ToString("MMMM yyyy", new CultureInfo("id-ID"));
                }
                else
                    item.NamaPeriodeAkhir = "Sampai Dengan Sekarang";
            }
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
