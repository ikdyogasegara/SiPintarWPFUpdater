using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class GetListCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();
        private int IdStatusAktif = 1;
        private int IdStatusNonAktif;

        public GetListCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            GetStatus();

            if (_viewModel.SelectedPeriode == null)
                return;

            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            param.Add("IdPeriode", _viewModel.SelectedPeriode.IdPeriode);

            Type type = typeof(ParamRekeningAirFilterDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.RekeningFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.RekeningFilter));
                }
            }

            param = AdditionalParameter(param);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-air", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<ObservableCollection<RekeningAirWpf>>();

                    foreach (var item in data)
                    {
                        item.IsAktif = item.IdStatus == IdStatusAktif;
                        item.IsVerifikasi = item.FlagVerifikasi == true;
                        item.IsUnverifikasi = item.FlagVerifikasi != true;

                        item.IsSelected = false;
                    }

                    _viewModel.IsSelectedAll = false;
                    _viewModel.RekeningList = data;
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
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

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;

            ShowSnackbar(ErrorMessage);

            _viewModel.IsEmpty = !_viewModel.RekeningList.Any();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void GetStatus()
        {
            if (_viewModel.StatusList == null || _viewModel.StatusList.Count == 0)
                return;

            var getNonAktif = _viewModel.StatusList.FirstOrDefault(i => i.NamaStatus.ToLower() == "non aktif" || i.NamaStatus.ToLower() == "tidak aktif");
            var getAktif = _viewModel.StatusList.FirstOrDefault(i => i.NamaStatus.ToLower() == "aktif");

            IdStatusNonAktif = getNonAktif != null ? (int)getNonAktif.IdStatus : 0;
            IdStatusAktif = getAktif != null ? (int)getAktif.IdStatus : 0;
        }

        [ExcludeFromCodeCoverage]
        private Dictionary<string, dynamic> AdditionalParameter(Dictionary<string, dynamic> param)
        {
            if (_viewModel.JenisDataFilter != null)
            {
                var FieldAwal = "FieldAwal";
                var FieldAkhir = "FieldAkhir";
                switch (_viewModel.JenisDataFilter?.Key)
                {
                    case 1:
                        FieldAwal = "WaktuKoreksiAwal";
                        FieldAkhir = "WaktuKoreksiAkhir";
                        break;
                    case 2:
                        FieldAwal = "WaktuPublishAwal";
                        FieldAkhir = "WaktuPublishAkhir";
                        break;
                    case 3:
                        FieldAwal = "WaktuUploadAwal";
                        FieldAkhir = "WaktuUploadAkhir";
                        break;
                    case 4:
                        FieldAwal = "WaktuTransaksiAwal";
                        FieldAkhir = "WaktuTransaksiAkhir";
                        break;
                    case 5:
                        FieldAwal = "WaktuVerifikasiAwal";
                        FieldAkhir = "WaktuVerifikasiAkhir";
                        break;
                    case 6:
                        FieldAwal = "WaktuBacaAwal";
                        FieldAkhir = "WaktuBacaAkhir";
                        break;
                    default:
                        break;
                }

                if (_viewModel.RentangWaktu1Filter != null)
                    param.Add(FieldAwal, _viewModel.RentangWaktu1Filter);
                if (_viewModel.RentangWaktu2Filter != null)
                    param.Add(FieldAkhir, _viewModel.RentangWaktu2Filter);
            }

            if (_viewModel.JumlahPakai1Filter != null)
                param.Add("PakaiAwal", _viewModel.JumlahPakai1Filter);
            if (_viewModel.JumlahPakai2Filter != null)
                param.Add("PakaiAkhir", _viewModel.JumlahPakai2Filter);

            if (_viewModel.StanAwal1Filter != null)
                param.Add("StanLaluAwal", _viewModel.StanAwal1Filter);
            if (_viewModel.StanAwal2Filter != null)
                param.Add("StanLaluAkhir", _viewModel.StanAwal2Filter);
            if (_viewModel.StanAkhir1Filter != null)
                param.Add("StanSkrgAwal", _viewModel.StanAkhir1Filter);
            if (_viewModel.StanAkhir2Filter != null)
                param.Add("StanSkrgAkhir", _viewModel.StanAkhir2Filter);
            if (_viewModel.LuasRumah1Filter != null)
                param.Add("LuasRumahAwal", _viewModel.LuasRumah1Filter);
            if (_viewModel.LuasRumah2Filter != null)
                param.Add("LuasRumahAkhir", _viewModel.LuasRumah2Filter);



            if (_viewModel.KeakuratanFilter != null)
                param.Add("Keakuratan", _viewModel.KeakuratanFilter);

            if (_viewModel.JenisVerifikasiFilter != null)
            {
                switch (_viewModel.JenisVerifikasiFilter?.Key)
                {
                    case 2:
                        param.Add("FlagVerifikasi", true);
                        break;
                    case 3:
                        param.Add("FlagVerifikasi", false);
                        break;
                    case 4:
                        param.Add("IdStatus", IdStatusAktif);
                        break;
                    case 5:
                        param.Add("IdStatus", IdStatusAktif);
                        param.Add("FlagBaca", false);
                        break;
                    case 6:
                        param.Add("IdStatus", IdStatusNonAktif);
                        break;
                    case 7:
                        param.Add("FlagUpload", true);
                        break;
                    case 8:
                        param.Add("FlagUpload", false);
                        break;
                    default:
                        break;
                }
            }

            if (_viewModel.LainnyaFilter != null)
            {
                switch (_viewModel.LainnyaFilter?.Key)
                {
                    case 1:
                        param.Add("Lainnya", "Sudah dibaca ulang");
                        break;
                    case 2:
                        param.Add("Lainnya", "Request dibaca ulang");
                        break;
                    case 3:
                        param.Add("Lainnya", "Terdapat stan angkat");
                        break;
                    case 4:
                        param.Add("Lainnya", "Terdapat foto rumah");
                        break;
                    case 5:
                        param.Add("Lainnya", "Terdapat video");
                        break;
                    case 6:
                        param.Add("Lainnya", "Terdapat kelainan");
                        break;
                    case 7:
                        param.Add("Lainnya", "UNKNOWN");
                        break;
                    case 8:
                        param.Add("Lainnya", "Hanya yang di-taksir");
                        break;
                    case 9:
                        param.Add("Lainnya", "Di-taksir selama 2 bulan");
                        break;
                    case 10:
                        param.Add("Lainnya", "Di-taksir selama 3 bulan");
                        break;
                    case 11:
                        param.Add("Lainnya", "Pemakaian minus");
                        break;
                    case 12:
                        param.Add("Lainnya", "Belum lunas");
                        break;
                    case 13:
                        param.Add("Lainnya", "Sudah lunas");
                        break;
                    case 14:
                        param.Add("Lainnya", "Stan awal = 0");
                        break;
                    case 15:
                        param.Add("Lainnya", "Stan awal = stan akhir");
                        break;
                    case 16:
                        param.Add("Lainnya", "Stan awal = stan akhir, ada pakai");
                        break;
                    case 17:
                        param.Add("Lainnya", "UNKNOWN");
                        break;
                    case 18:
                        param.Add("Lainnya", "Total rekening tdk sama");
                        break;
                    case 19:
                        param.Add("Lainnya", "Tidak dapat dibaca");
                        break;
                    case 20:
                        param.Add("Lainnya", "Data lapangan != stan skrg");
                        break;
                    case 21:
                        param.Add("Lainnya", "Terdapat memo lapangan");
                        break;
                    case 22:
                        param.Add("Lainnya", "Tukar hasil baca ulang");
                        break;
                    case 23:
                        param.Add("Lainnya", "Pemakaian sama 3 bln");
                        break;
                    case 24:
                        param.Add("Lainnya", "Taksasi");
                        break;
                    case 25:
                        param.Add("Lainnya", "Meter terbalik");
                        break;
                    case 26:
                        param.Add("Lainnya", "Terdapat lampiran");
                        break;
                    case 27:
                        param.Add("Lainnya", "Pemakaian minimum selama 3 bln");
                        break;
                    case 28:
                        param.Add("Lainnya", "Pemakaian minimum selama 6 bln");
                        break;
                    case 29:
                        param.Add("Lainnya", "UNKNOWN");
                        break;
                    case 30:
                        param.Add("Lainnya", "Kelainan sama 3 bulan");
                        break;
                    case 31:
                        param.Add("Lainnya", "Dibaca manual dari Supervisi");
                        break;
                    default:
                        break;
                }
            }

            return param;
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }
    }
}
