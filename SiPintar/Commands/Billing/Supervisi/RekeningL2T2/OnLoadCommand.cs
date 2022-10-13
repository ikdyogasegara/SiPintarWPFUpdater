using System;
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
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(RekeningL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            TableColumnConfiguration();

            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.RekeningList = new ObservableCollection<RekeningLlttDto> { };

            _viewModel.IsLoading = true;

            await GetPeriodeAsync(parameter);

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            Type type = typeof(RekeningLlttDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.Name != "IdPeriode" && property.GetValue(_viewModel.RekeningFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.RekeningFilter));
                }

                if (property.Name == "IdPeriode" && _viewModel.PeriodeFilter != null)
                    param.Add(property.Name, _viewModel.PeriodeFilter.IdPeriode);
            }

            param = AdditionalParameter(param);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/rekening-lltt", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RekeningList = Result.Data.ToObject<ObservableCollection<RekeningLlttDto>>();
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

            _ = GetStatusAsync();
            _ = GetTarifL2T2Async();
            _ = GetKolektifAsync();
            _ = GetWilayahAsync();
            _ = GetKelurahanAsync();
            _ = GetRayonAsync();
            _ = GetCabangAsync();
            _ = GetKasirAsync();
            _ = GetLoketAsync();

            _viewModel.IsLoading = false;
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_rekening_lltt_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                Publish = data["show_table_column"]["publish"] == "1",
                NoLltt = data["show_table_column"]["no_lltt"] == "1",
                NoSamb = data["show_table_column"]["no_samb"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                Biaya = data["show_table_column"]["biaya"] == "1",
                Upload = data["show_table_column"]["upload"] == "1",
                KodeLltt = data["show_table_column"]["kode_lltt"] == "1",
                TarifLltt = data["show_table_column"]["tarif_lltt"] == "1",
                KodeRayon = data["show_table_column"]["kode_rayon"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Lunas = data["show_table_column"]["lunas"] == "1",
                TglBayar = data["show_table_column"]["tgl_bayar"] == "1",
                KodeWilayah = data["show_table_column"]["kode_wilayah"] == "1",
                Wilayah = data["show_table_column"]["wilayah"] == "1",
                WaktuPublish = data["show_table_column"]["waktu_publish"] == "1",
                Flag = data["show_table_column"]["flag"] == "1",
                Status = data["show_table_column"]["status"] == "1"
            };
        }

        private Dictionary<string, dynamic> AdditionalParameter(Dictionary<string, dynamic> param)
        {
            if (_viewModel.TglBayarAwalFilter != null)
                param.Add("TglBayarAwal", _viewModel.TglBayarAwalFilter);
            if (_viewModel.TglBayarAkhirFilter != null)
                param.Add("TglBayarAkhir", _viewModel.TglBayarAkhirFilter);
            if (_viewModel.TglUploadAwalFilter != null)
                param.Add("TglUploadAwal", _viewModel.TglUploadAwalFilter);
            if (_viewModel.TglUploadAkhirFilter != null)
                param.Add("TglUploadAkhir", _viewModel.TglUploadAkhirFilter);
            if (_viewModel.TglPublishAwalFilter != null)
                param.Add("TglPublishAwal", _viewModel.TglPublishAwalFilter);
            if (_viewModel.TglPublishAkhirFilter != null)
                param.Add("TglPublishAkhir", _viewModel.TglPublishAkhirFilter);
            if (_viewModel.BiayaAwalFilter != null)
                param.Add("BiayaAwal", _viewModel.BiayaAwalFilter);
            if (_viewModel.BiayaAkhirFilter != null)
                param.Add("BiayaAkhir", _viewModel.BiayaAkhirFilter);

            return param;
        }

        [ExcludeFromCodeCoverage]
        public void ShowSnackbar(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    mainView.ShowSnackbar(ErrorMessage, "danger");
                });
            }
        }

        public async Task GetPeriodeAsync(object parameter)
        {
            bool IsRefresh = parameter != null && (bool)parameter;

            if (!IsRefresh)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "status", true }
                };

                var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-periode-rekening", param);
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                    {
                        _viewModel.PeriodeList = Result.Data.ToObject<ObservableCollection<MasterPeriodeDto>>();

                        _viewModel.PeriodeFilter = _viewModel.PeriodeList?[0];
                    }
                }
            }
        }

        public async Task GetStatusAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-status");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.StatusList = Result.Data.ToObject<ObservableCollection<MasterStatusDto>>();
                }
            }
        }

        public async Task GetTarifL2T2Async()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-lltt");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TarifL2T2List = Result.Data.ToObject<ObservableCollection<MasterTarifLlttDto>>();
                }
            }
        }

        public async Task GetKolektifAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-kolektif");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KolektifList = Result.Data.ToObject<ObservableCollection<MasterKolektifDto>>();
                }
            }
        }

        public async Task GetWilayahAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-wilayah");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.WilayahList = Result.Data.ToObject<ObservableCollection<MasterWilayahDto>>();
                }
            }
        }

        public async Task GetKelurahanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-kelurahan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KelurahanList = Result.Data.ToObject<ObservableCollection<MasterKelurahanDto>>();
                }
            }
        }

        public async Task GetKasirAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-user");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KasirList = Result.Data.ToObject<ObservableCollection<MasterUserDto>>();
                }
            }
        }

        public async Task GetRayonAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-rayon");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RayonList = Result.Data.ToObject<ObservableCollection<MasterRayonDto>>();
                }
            }
        }

        public async Task GetLoketAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-loket");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.LoketList = Result.Data.ToObject<ObservableCollection<MasterLoketDto>>();
                }
            }
        }

        public async Task GetCabangAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-cabang");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.CabangList = Result.Data.ToObject<ObservableCollection<MasterCabangDto>>();
                }
            }
        }
    }
}
