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
using FastReport.Utils;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnFilterCommand(PenghapusanRekeningViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEmpty = false;
            _viewModel.RekeningAirHapusSecaraAkuntansiList = new ObservableCollection<RekeningAirHapusSecaraAkuntansiWpf> { };
            _viewModel.SelectedData = null;

            if (_viewModel.Filter.IdFlag == 0)
            {
                ShowDialogInvalid();
                return;
            }

            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic> { { "pageSize", _viewModel.LimitData.Key }, { "currentPage", _viewModel.CurrentPage } };

            var type = typeof(ParamRekeningAirHapusSecaraAkuntansiDto);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.GetValue(_viewModel.Filter) != null)
                {
                    param.Add(property.Name, property.GetValue(_viewModel.Filter));
                }
            }

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/hps-scr-akuntansi-rekening-air", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.RekeningAirHapusSecaraAkuntansiList = result.Data.ToObject<ObservableCollection<RekeningAirHapusSecaraAkuntansiWpf>>();
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            _viewModel.IsEmpty = !_viewModel.RekeningAirHapusSecaraAkuntansiList.Any();
            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLoading = false;

            TableColumnConfiguration();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialogInvalid()
        {
            if (_isTest)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "BillingRootDialog",
                    "Daftar Hapus Secara Akuntansi",
                    "Tipe Penghapusan belum dipilih!",
                    "warning",
                    "OK",
                    moduleName: "billing");
            }
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_penghapusan_rekening_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NoSamb = data["show_table_column"]["no_samb"] == "1",
                Periode = data["show_table_column"]["periode"] == "1",
                Golongan = data["show_table_column"]["golongan"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Wilayah = data["show_table_column"]["wilayah"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",

                StanLalu = data["show_table_column"]["stan_lalu"] == "1",
                StanSkrg = data["show_table_column"]["stan_skrg"] == "1",
                StanAngkat = data["show_table_column"]["stan_angkat"] == "1",
                Pakai = data["show_table_column"]["pakai"] == "1",
                BiayaPemakaian = data["show_table_column"]["biaya_pemakaian"] == "1",
                Administrasi = data["show_table_column"]["administrasi"] == "1",
                Pemeliharaan = data["show_table_column"]["pemelaiharaan"] == "1",
                Retribusi = data["show_table_column"]["retribusi"] == "1",
                Pelayanan = data["show_table_column"]["pelayanan"] == "1",
                AirLimbah = data["show_table_column"]["air_limbah"] == "1",
                DendaPakai0 = data["show_table_column"]["denda_pakai0"] == "1",
                PemeliharaanLain = data["show_table_column"]["pemeliharaan_lain"] == "1",
                AdministrasiLain = data["show_table_column"]["administrasi_lain"] == "1",
                RetribusiLain = data["show_table_column"]["retribusi_lain"] == "1",
                Ppn = data["show_table_column"]["ppn"] == "1",
                Meterai = data["show_table_column"]["meterai"] == "1",
                Rekair = data["show_table_column"]["rekair"] == "1",
                Denda = data["show_table_column"]["denda"] == "1",
                Total = data["show_table_column"]["total"] == "1",
            };
        }
    }
}
