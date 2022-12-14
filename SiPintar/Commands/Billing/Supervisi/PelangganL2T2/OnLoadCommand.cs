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
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly Dictionary<string, dynamic> _testBody;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnLoadCommand(PelangganL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, Dictionary<string, dynamic> testBody = null)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _testBody = testBody;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            TableColumnConfiguration();

            string ErrorMessage = null;
            _viewModel.IsEmpty = false;

            _viewModel.MasterPelangganLlttList = new ObservableCollection<MasterPelangganLlttDto> { };
            _viewModel.SelectedData = null;

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
            };

            Type type = typeof(MasterPelangganLlttDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.PelangganFilter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.PelangganFilter));
                }
            }

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-pelanggan-lltt", _testBody ?? param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.MasterPelangganLlttList = Result.Data.ToObject<ObservableCollection<MasterPelangganLlttDto>>();
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

            _viewModel.IsEmpty = !_viewModel.MasterPelangganLlttList.Any();

            _ = GetStatusAsync();
            _ = GetFlagAsync();
            _ = GetTarifLlttAsync();
            _ = GetKolektifAsync();
            _ = GetWilayahAsync();
            _ = GetKelurahanAsync();
            _ = GetKecamatanAsync();
            _ = GetRayonAsync();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
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

        public async Task GetFlagAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-flag", _testBody);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.FlagList = Result.Data.ToObject<ObservableCollection<MasterFlagDto>>();
                }
            }
        }

        public async Task GetTarifLlttAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-tarif-lltt");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TarifLlttList = Result.Data.ToObject<ObservableCollection<MasterTarifLlttDto>>();
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

        public async Task GetKecamatanAsync()
        {
            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-kecamatan");
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KecamatanList = Result.Data.ToObject<ObservableCollection<MasterKecamatanDto>>();
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

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_pelanggan_lltt_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                NoLltt = data["show_table_column"]["no_lltt"] == "1",
                NoSamb = data["show_table_column"]["no_samb"] == "1",
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                KodeLltt = data["show_table_column"]["kode_lltt"] == "1",
                GolLltt = data["show_table_column"]["gol_lltt"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Kolektif = data["show_table_column"]["kolektif"] == "1",
                NoHp = data["show_table_column"]["no_hp"] == "1",
                NoTelp = data["show_table_column"]["no_telp"] == "1",
                Ktp = data["show_table_column"]["ktp"] == "1",
                Email = data["show_table_column"]["email"] == "1",
                Keterangan = data["show_table_column"]["keterangan"] == "1",
                Status = data["show_table_column"]["status"] == "1",
                Flag = data["show_table_column"]["flag"] == "1"
            };
        }
    }
}
