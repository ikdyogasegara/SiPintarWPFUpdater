using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnGetTglDendaCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetTglDendaCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsCheckedRayon || _viewModel.IsCheckedWilayah)
            {
                var param = new Dictionary<string, dynamic> { { "IdPeriode", _viewModel.SelectedData.IdPeriode } };

                if (_viewModel.SelectedRayon != null || _viewModel.SelectedWilayah != null)
                {
                    if (_viewModel.IsCheckedRayon && _viewModel.SelectedRayon != null)
                    {
                        param.Add("IdRayon", _viewModel.SelectedRayon.IdRayon);
                    }

                    if (_viewModel.IsCheckedWilayah && _viewModel.SelectedWilayah != null)
                    {
                        param.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah);
                    }
                }

                #region tgldenda

                var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening-tgl-denda", param));

                if (!response.IsError)
                {
                    var result = response.Data;

                    if (result.Status && result.Data != null)
                    {
                        var temp = result.Data.ToObject<ObservableCollection<MasterPeriodeTglDendaDto>>();

                        if (temp?.Count > 0)
                        {
                            _viewModel.TglDenda1Form = temp[0].TglMulaiDenda1;
                            _viewModel.TglDenda2Form = temp[0].TglMulaiDenda2;
                            _viewModel.TglDenda3Form = temp[0].TglMulaiDenda3;
                            _viewModel.TglDenda4Form = temp[0].TglMulaiDenda4;
                            _viewModel.TglMulaiDendaForm = temp[0].TglMulaiDendaPerBulan;
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
                }
                #endregion
            }
            else
            {
                _viewModel.TglDenda1Form = _viewModel.SelectedData.TglMulaiDenda1Wpf;
                _viewModel.TglDenda2Form = _viewModel.SelectedData.TglMulaiDenda2Wpf;
                _viewModel.TglDenda3Form = _viewModel.SelectedData.TglMulaiDenda3Wpf;
                _viewModel.TglDenda4Form = _viewModel.SelectedData.TglMulaiDenda4Wpf;
                _viewModel.TglMulaiDendaForm = _viewModel.SelectedData.TglMulaiDendaPerBulanWpf;
            }
        }
    }
}
