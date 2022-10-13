using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    [ExcludeFromCodeCoverage]
    public class GetJadwalListCommand : AsyncCommandBase
    {
        private readonly PerPetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetJadwalListCommand(PerPetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEmptyJadwal = true;

            _viewModel.JadwalList = new ObservableCollection<MasterJadwalBacaDto>();

            if (_viewModel.SelectedPetugas == null)
                return;

            _viewModel.IsLoadingJadwal = true;

            string ErrorMessage = null;
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitDataJadwal.Key },
                { "CurrentPage" , _viewModel.CurrentPageJadwal },
                { "IdPetugasBaca" , _viewModel.SelectedPetugas.IdPetugasBaca },
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-jadwal-baca", param));

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var ListData = Result.Data.ToObject<ObservableCollection<MasterJadwalBacaDto>>();

                    _viewModel.JadwalList = ListData;
                    _viewModel.TotalPageJadwal = Result.TotalPage;
                    _viewModel.TotalRecordJadwal = Convert.ToInt32(Result.Record);
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

            ShowResult(ErrorMessage);

            _viewModel.IsPreviousButtonEnabledJadwal = _viewModel.CurrentPageJadwal > 1;
            _viewModel.IsNextButtonEnabledJadwal = _viewModel.CurrentPageJadwal < _viewModel.TotalPageJadwal;
            _viewModel.IsLeftPageNumberActiveJadwal = _viewModel.CurrentPageJadwal == 1;
            _viewModel.IsRightPageNumberActiveJadwal = _viewModel.CurrentPageJadwal == _viewModel.TotalPageJadwal;
            _viewModel.IsLeftPageNumberFillerVisibleJadwal = _viewModel.CurrentPageJadwal != 2;
            _viewModel.IsRightPageNumberFillerVisibleJadwal = _viewModel.CurrentPageJadwal < _viewModel.TotalPageJadwal - 1;
            _viewModel.IsMiddlePageNumberVisibleJadwal = _viewModel.CurrentPageJadwal > 1 && _viewModel.CurrentPageJadwal < _viewModel.TotalPageJadwal;

            _viewModel.IsEmptyJadwal = _viewModel.JadwalList.Count == 0;

            _viewModel.IsLoadingJadwal = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    if (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) is BacameterView mainView)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }
    }
}
