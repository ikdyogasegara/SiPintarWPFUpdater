using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(TagihanManualViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.DataList = null;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage" , _viewModel.CurrentPage },
                { "FlagManual" , true },
            };

            if (_viewModel.IsStatusChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterStatus))
            {
                switch (_viewModel.FilterStatus)
                {
                    case "Belum Lunas":
                        param.Add("StatusTransaksi", false);
                        break;
                    case "Sudah Lunas":
                        param.Add("StatusTransaksi", true);
                        break;
                }
            }

            if (_viewModel.IsNomorNonAirChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorNonAir))
                param.Add("NomorNonAir", _viewModel.FilterNomorNonAir);

            if (_viewModel.IsJenisTipePelangganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterJenisTipePelanggan.NamaJenisPelanggan))
                param.Add("JenisTipePelanggan", _viewModel.FilterJenisTipePelanggan.NamaJenisPelanggan);

            if (_viewModel.IsJenisNonAirChecked && _viewModel.FilterJenisNonAir.IdJenisNonAir.HasValue)
                param.Add("IdJenisNonAir", _viewModel.FilterJenisNonAir.IdJenisNonAir);

            if (_viewModel.IsNomorPelangganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorPelanggan))
            {
                switch (_viewModel.JenisPelangganSelected.NamaJenisPelanggan)
                {
                    case "Pelanggan Air":
                        param.Add("NoSamb", _viewModel.FilterNomorPelanggan);
                        break;
                    case "Pelanggan Limbah":
                        param.Add("NomorLimbah", _viewModel.FilterNomorPelanggan);
                        break;
                    case "Pelanggan L2T2":
                        param.Add("NomorLltt", _viewModel.FilterNomorPelanggan);
                        break;
                }
            }

            if (_viewModel.IsNamaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNama))
                param.Add("Nama", _viewModel.FilterNama);

            if (_viewModel.IsAlamatChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterAlamat))
                param.Add("Alamat", _viewModel.FilterAlamat);

            if (_viewModel.IsRayonChecked && _viewModel.FilterRayon.IdRayon.HasValue)
                param.Add("IdRayon", _viewModel.FilterRayon.IdRayon);

            if (_viewModel.IsWilayahChecked && _viewModel.FilterWilayah.IdWilayah.HasValue)
                param.Add("IdWilayah", _viewModel.FilterWilayah.IdWilayah);

            if (_viewModel.IsKelurahanChecked && _viewModel.FilterKelurahan.IdKelurahan.HasValue)
                param.Add("IdKelurahan", _viewModel.FilterKelurahan.IdKelurahan);

            if (_viewModel.IsGolonganChecked && _viewModel.FilterGolongan.IdGolongan.HasValue)
                param.Add("IdGolongan", _viewModel.FilterGolongan.IdGolongan);

            if (_viewModel.IsTarifLimbahChecked && _viewModel.FilterTarifLimbah.IdTarifLimbah.HasValue)
                param.Add("IdTarifLimbah", _viewModel.FilterTarifLimbah.IdTarifLimbah);

            if (_viewModel.IsTarifLlttChecked && _viewModel.FilterTarifLltt.IdTarifLltt.HasValue)
                param.Add("IdTarifLltt", _viewModel.FilterTarifLltt.IdTarifLltt);

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-non-air", param));
            _viewModel.DataList = new ObservableCollection<RekeningNonAirWpf>();

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<RekeningNonAirWpf>>();
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
            _viewModel.IsPageNumberVisible = _viewModel.TotalPage > 1;

            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;
            _viewModel.IsLoading = false;

            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_tagihan_manual_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                JenisTipePelanggan = data["show_table_column"]["JenisTipePelanggan"] == "1",
                NomorNonAir = data["show_table_column"]["NomorNonAir"] == "1",
                KodeJenisNonAir = data["show_table_column"]["KodeJenisNonAir"] == "1",
                NamaJenisNonAir = data["show_table_column"]["NamaJenisNonAir"] == "1",
                Total = data["show_table_column"]["Total"] == "1",
                NomorPelanggan = data["show_table_column"]["NomorPelanggan"] == "1",
                Nama = data["show_table_column"]["Nama"] == "1",
                Alamat = data["show_table_column"]["Alamat"] == "1",
                Keterangan = data["show_table_column"]["Keterangan"] == "1",
                KodeTarif = data["show_table_column"]["KodeTarif"] == "1",
                NamaTarif = data["show_table_column"]["NamaTarif"] == "1",
                KodeRayon = data["show_table_column"]["KodeRayon"] == "1",
                NamaRayon = data["show_table_column"]["NamaRayon"] == "1",
                KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                KodeKelurahan = data["show_table_column"]["KodeKelurahan"] == "1",
                NamaKelurahan = data["show_table_column"]["NamaKelurahan"] == "1"
            };
        }
    }
}
