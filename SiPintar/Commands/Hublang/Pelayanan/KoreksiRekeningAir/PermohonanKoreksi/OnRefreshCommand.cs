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
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(PermohonanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.TipeJenisKoreksiAir == null)
                return;

            _viewModel.DataList = null;
            _viewModel.IsEmpty = true;
            _viewModel.Parent.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
                { "IdTipePermohonan" , _viewModel.Parent.TipeJenisKoreksiAir.IdTipePermohonan },
                { "IncludeRabDetail" , false },
                { "KodeTipePermohonan" , "KREKAIR" }, // perlu kode karena untuk join ke table permohonan koreksi rekening untuk dapat status
            };

            //add filter
            if (_viewModel.IsNoSambunganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNoSambungan))
                param.Add("NoSamb", _viewModel.FilterNoSambungan);
            if (_viewModel.IsStatusPermohonanChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterStatusPermohonan))
                param.Add("StatusPermohonanText", _viewModel.FilterStatusPermohonan);
            if (_viewModel.IsNomorRegisterChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorRegister))
                param.Add("NomorPermohonan", _viewModel.FilterNomorRegister);
            if (_viewModel.IsNamaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNama))
                param.Add("Nama", _viewModel.FilterNama);
            if (_viewModel.IsAlamatChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterAlamat))
                param.Add("Alamat", _viewModel.FilterAlamat);
            if (_viewModel.IsGolonganChecked && _viewModel.FilterGolongan != null)
                param.Add("IdGolongan", _viewModel.FilterGolongan.IdGolongan);
            if (_viewModel.IsRayonChecked && _viewModel.FilterRayon != null)
                param.Add("IdRayon", _viewModel.FilterRayon.IdRayon);
            if (_viewModel.IsWilayahChecked && _viewModel.FilterWilayah != null)
                param.Add("IdWilayah", _viewModel.FilterWilayah.IdWilayah);
            if (_viewModel.IsKelurahanChecked && _viewModel.FilterKelurahan != null)
                param.Add("IdKelurahan", _viewModel.FilterKelurahan.IdKelurahan);
            if (_viewModel.IsCabangChecked && _viewModel.FilterCabang != null)
                param.Add("IdCabang", _viewModel.FilterCabang.IdCabang);
            if (_viewModel.IsTanggalInputChecked && _viewModel.FilterTanggalInputAwal.HasValue)
                param.Add("TanggalMulaiPermohonan", _viewModel.FilterTanggalInputAwal.Value);
            if (_viewModel.IsTanggalInputChecked && _viewModel.FilterTanggalInputAkhir.HasValue)
                param.Add("TanggalSampaiDenganPermohonan", _viewModel.FilterTanggalInputAkhir.Value);
            if (_viewModel.IsUserInputChecked && _viewModel.FilterUserInput != null)
                param.Add("IdUserPermohonan", _viewModel.FilterUserInput.IdUser);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/permohonan-pelanggan-air", param);

            _viewModel.DataList = new ObservableCollection<PermohonanWpf>();

            if (!response.IsError)
            {
                var result = response.Data;

                if (result.Status && result.Data != null)
                {
                    var listData = result.Data.ToObject<ObservableCollection<PermohonanWpf>>();

                    _viewModel.DataList = listData;
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;
            _viewModel.Parent.IsLoading = false;

            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_permohonan_koreksi_rekening_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Jenis = data["show_table_column"]["Jenis"] == "1",
                Status = data["show_table_column"]["Status"] == "1",
                NomorRegister = data["show_table_column"]["NomorRegister"] == "1",
                NamaPelanggan = data["show_table_column"]["NamaPelanggan"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                Alasan = data["show_table_column"]["Alasan"] == "1",
                Rayon = data["show_table_column"]["Rayon"] == "1",
                Wilayah = data["show_table_column"]["Wilayah"] == "1",
                Kelurahan = data["show_table_column"]["Kelurahan"] == "1",
                Kecamatan = data["show_table_column"]["Kecamatan"] == "1",
                Cabang = data["show_table_column"]["Cabang"] == "1",
                NoBeritaAcara = data["show_table_column"]["NoBeritaAcara"] == "1"
            };
        }
    }
}
