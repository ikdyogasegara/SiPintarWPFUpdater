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
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(TarifAirTangkiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var Param = new Dictionary<string, dynamic>();
            if (_viewModel.IsKategoriChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterKategori))
                Param.Add("Kategori", _viewModel.FilterKategori);
            if (_viewModel.IsKodeTarifChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterKodeTarif))
                Param.Add("KodeTarifTangki", _viewModel.FilterKodeTarif);
            if (_viewModel.IsNamaTarifChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNamaTarif))
                Param.Add("NamaTarifTangki", _viewModel.FilterNamaTarif);
            Param.Add("currentPage", _viewModel.CurrentPage);
            Param.Add("pageSize", _viewModel.LimitData.Value);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-tangki", Param));
            _viewModel.DataList = new ObservableCollection<MasterTarifTangkiDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<MasterTarifTangkiDto>>();
                    _viewModel.TotalPage = Result.TotalPage;
                    _viewModel.TotalRecord = Result.Record;
                }
                else
                    ShowResult(Response.Data.Ui_msg);
            }
            else
                ShowResult(Response.Error.Message);

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;


            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-non-air", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                    _viewModel.JenisNonAir = Result.Data.ToObject<ObservableCollection<MasterJenisNonAirDto>>();
                else
                    ShowResult(Response.Data.Ui_msg);
            }
            else
                ShowResult(Response.Error.Message);

            TableColumnConfiguration();
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string ErrorMessage)
        {
            if (ErrorMessage != null && !_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                        mainView.ShowSnackbar(ErrorMessage, "danger");
                });
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\atribut_tarif_tangki_air_config.ini";
            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodeTarif = data["show_table_column"]["KodeTarif"] == "1",
                Kategori = data["show_table_column"]["Kategori"] == "1",
                NamaTarif = data["show_table_column"]["NamaTarif"] == "1",
                BiayaAir = data["show_table_column"]["BiayaAir"] == "1",
                BiayaAdministrasi = data["show_table_column"]["BiayaAdministrasi"] == "1",
                BiayaOperasional = data["show_table_column"]["BiayaOperasional"] == "1",
                BiayaTransport = data["show_table_column"]["BiayaTransport"] == "1",
                Ppn = data["show_table_column"]["Ppn"] == "1"
            };
        }
    }
}
