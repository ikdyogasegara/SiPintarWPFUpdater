using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;
using SiPintar.Views;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.RiwayatTransaksi
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly RiwayatTransaksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(RiwayatTransaksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IdPelangganAir == null && _viewModel.IdNonAir == null)
            {
                return;
            }

            string errorMessage = null;
            _viewModel.DataList = null;
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic> { { "PageSize", _viewModel.LimitData.Key }, { "CurrentPage", _viewModel.CurrentPage }, { "StatusTransaksi", true }, };

            switch (_viewModel.FilterTipeTransaksi.Tipe)
            {
                case "Pembayaran Rek. Air":
                    _viewModel.EndPoint = "rekening-air-history-pelunasan-pembatalan";
                    if (_viewModel.IdPelangganAir != null)
                    {
                        param.Add("IdPelangganAir", _viewModel.IdPelangganAir);
                    }
                    else
                    {
                        param.Add("IdPelangganAir", -1);
                    }

                    break;
                case "Pembayaran Rek. Limbah":
                    _viewModel.EndPoint = "rekening-limbah-history-pelunasan-pembatalan";
                    if (_viewModel.IdPelangganAir != null)
                    {
                        param.Add("IdPelangganAir", _viewModel.IdPelangganAir);
                    }
                    else
                    {
                        param.Add("IdPelangganAir", -1);
                    }

                    break;
                case "Pembayaran Rek. L2T2":
                    _viewModel.EndPoint = "rekening-lltt-history-pelunasan-pembatalan";
                    if (_viewModel.IdPelangganAir != null)
                    {
                        param.Add("IdPelangganAir", _viewModel.IdPelangganAir);
                    }
                    else
                    {
                        param.Add("IdPelangganAir", -1);
                    }

                    break;
                case "Pembayaran Rek. Non Air":
                    _viewModel.EndPoint = "rekening-non-air-history-pelunasan-pembatalan";
                    if (_viewModel.IdPelangganAir != null)
                    {
                        param.Add("IdPelangganAir", _viewModel.IdPelangganAir);
                    }

                    if (_viewModel.IdNonAir != null)
                    {
                        param.Add("IdNonAir", _viewModel.IdNonAir);
                    }

                    break;
            }

            if (_viewModel.IsKasirChecked && _viewModel.FilterKasir != null)
                param.Add("IdUserTransaksi", _viewModel.FilterKasir.IdUser);
            if (_viewModel.IsLoketChecked && _viewModel.FilterLoket != null)
                param.Add("IdLoket", _viewModel.FilterLoket.IdLoket);
            if (_viewModel.IsTahunChecked && _viewModel.FilterTahun != null)
                param.Add("TahunTransaksi", _viewModel.FilterTahun.Tahun);
            if (_viewModel.IsPeriodeChecked && _viewModel.FilterPeriode != null)
                param.Add("IdPeriode", _viewModel.FilterPeriode.IdPeriode);
            if (_viewModel.IsKolektifChecked && _viewModel.FilterKolektif != null)
                param.Add("IdKolektiTransaksi", _viewModel.FilterKolektif.IdKolektif);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", param);

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<RiwayatPelunasanDanPembatalanWpf>>();
                    _viewModel.TotalRecord = (int)response.Data.Record;
                    _viewModel.TotalPage = result.TotalPage;
                }
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
                errorMessage = response.Error.Message;

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = !_viewModel.DataList.Any();
            _viewModel.IsLoading = false;
            await LoadSettingAsync();
            ShowSnackbarIfError(errorMessage);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowSnackbarIfError(string errorMessage)
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

        [ExcludeFromCodeCoverage]
        private Task LoadSettingAsync()
        {
            _viewModel.TableSetting = new
            {
                KodeJenisNonAir = _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Non Air",
                NamaJenisNonAir = _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Non Air",
                Rekair = _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Air",
                Denda = _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Air",
                Total = _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Air" || _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Non Air",
                Biaya = _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. Limbah" || _viewModel.FilterTipeTransaksi.Tipe == "Pembayaran Rek. L2T2",
            };

            return Task.CompletedTask;
        }
    }
}
