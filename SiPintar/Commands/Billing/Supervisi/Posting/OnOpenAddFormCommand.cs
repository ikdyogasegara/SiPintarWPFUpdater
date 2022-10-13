using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.Posting;

namespace SiPintar.Commands.Billing.Supervisi.Posting
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public OnOpenAddFormCommand(PostingViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>();
            param.Add("status", true);

            var ResponsePeriode = await _restApi.GetAsync("/api/v1/master-periode-rekening", param);
            _viewModel.MasterPeriodeList = new ObservableCollection<MasterPeriodeDto>();

            if (!ResponsePeriode.IsError)
            {
                var Result = ResponsePeriode.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.MasterPeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();
                    _viewModel.SelectedDataPeriode = _viewModel.MasterPeriodeList[0];
                }
            }

            ShowDialog();
            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}

