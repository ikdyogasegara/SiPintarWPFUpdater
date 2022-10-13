using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly bool _isTest;
        private readonly LanggananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;

        public OnRefreshCommand(LanggananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.DataList = null;
            _viewModel.IsEmpty = true;
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                {"pageSize", _viewModel.LimitData.Key},
                {"currentPage", _viewModel.CurrentPage}
            };

            if (_viewModel.IsNomorSambunganChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNomorSambungan))
            {
                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        param.Add("NoSamb", _viewModel.FilterNomorSambungan);
                        break;
                    case "Pelanggan Limbah":
                        param.Add("NomorLimbah", _viewModel.FilterNomorSambungan);
                        break;
                    case "Pelanggan L2T2":
                        param.Add("NomorLltt", _viewModel.FilterNomorSambungan);
                        break;
                }
            }

            if (_viewModel.IsNamaChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNama))
                param.Add("Nama", _viewModel.FilterNama);
            if (_viewModel.IsAlamatChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterAlamat))
                param.Add("Alamat", _viewModel.FilterAlamat);
            if (_viewModel.IsRayonChecked && _viewModel.FilterRayon != null)
                param.Add("IdRayon", _viewModel.FilterRayon.IdRayon);
            if (_viewModel.IsAreaChecked && _viewModel.FilterArea != null)
                param.Add("IdArea", _viewModel.FilterArea.IdArea);
            if (_viewModel.IsWilayahChecked && _viewModel.FilterWilayah != null)
                param.Add("IdWilayah", _viewModel.FilterWilayah.IdWilayah);
            if (_viewModel.IsKelurahanChecked && _viewModel.FilterKelurahan != null)
                param.Add("IdKelurahan", _viewModel.FilterKelurahan.IdKelurahan);
            if (_viewModel.IsKecamatanChecked && _viewModel.FilterKecamatan != null)
                param.Add("IdKecamatan", _viewModel.FilterKecamatan.IdKecamatan);
            if (_viewModel.IsCabangChecked && _viewModel.FilterCabang != null)
                param.Add("IdCabang", _viewModel.FilterCabang.IdCabang);
            if (_viewModel.IsStatusChecked && _viewModel.FilterStatus != null)
                param.Add("IdStatus", _viewModel.FilterStatus.IdStatus);
            if (_viewModel.IsFlagChecked && _viewModel.FilterFlag != null)
                param.Add("IdFlag", _viewModel.FilterFlag.IdFlag);
            if (_viewModel.IsGolonganChecked && _viewModel.FilterGolongan != null)
                param.Add("IdGolongan", _viewModel.FilterGolongan.IdGolongan);
            if (_viewModel.IsTarifLimbahChecked && _viewModel.FilterTarifLimbah != null)
                param.Add("IdTarifLimbah", _viewModel.FilterTarifLimbah.IdTarifLimbah);
            if (_viewModel.IsTarifLlttChecked && _viewModel.FilterTarifLltt != null)
                param.Add("IdTarifLltt", _viewModel.FilterTarifLltt.IdTarifLltt);
            if (_viewModel.IsDiameterChecked && _viewModel.FilterDiameter != null)
                param.Add("IdDiameter", _viewModel.FilterDiameter.IdDiameter);
            if (_viewModel.IsKolektifChecked && _viewModel.FilterKolektif != null)
                param.Add("IdKolektif", _viewModel.FilterKolektif.IdKolektif);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}", param);
            _viewModel.DataList = new ObservableCollection<MasterPelangganGlobalWpf>();
            _viewModel.DataKoreksiPelangganList = new ObservableCollection<MasterPelangganGlobalKoreksiWpf>();

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    if (_viewModel.IsTabKoreksi)
                    {
                        _viewModel.DataKoreksiPelangganList = result.Data.ToObject<ObservableCollection<MasterPelangganGlobalKoreksiWpf>>();
                        _viewModel.IsEmpty = !_viewModel.DataKoreksiPelangganList.Any();
                    }
                    else
                    {
                        _viewModel.DataList = result.Data.ToObject<ObservableCollection<MasterPelangganGlobalWpf>>();
                        _viewModel.IsEmpty = !_viewModel.DataList.Any();
                    }

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

            _viewModel.IsLoading = false;
            TableColumnConfiguration();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            var configIni =
                $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_pelanggan_config.ini";

            if (!File.Exists(configIni))
            {
                return;
            }

            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                Nama = data["show_table_column"]["nama"] == "1",
                Alamat = data["show_table_column"]["alamat"] == "1",
                NoHandphone = data["show_table_column"]["no_hp"] == "1",
                NoTelp = data["show_table_column"]["no_telp"] == "1",
                Rayon = data["show_table_column"]["rayon"] == "1",
                Area = data["show_table_column"]["area"] == "1",
                Wilayah = data["show_table_column"]["wilayah"] == "1",
                Kecamatan = data["show_table_column"]["kecamatan"] == "1",
                Kelurahan = data["show_table_column"]["kelurahan"] == "1",
                Cabang = data["show_table_column"]["cabang"] == "1",
                Kolektif = data["show_table_column"]["kolektif"] == "1",
                Flag = data["show_table_column"]["flag"] == "1",
            };
        }
    }
}
