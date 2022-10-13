using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;


namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(InteraksiLayananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            if (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Value == null)
                return;

            TableColumnConfiguration();
            var param = new Dictionary<string, dynamic>
                {
                    {"PageSize" ,  _viewModel.LimitData.Value},
                    { "CurrentPage" , _viewModel.CurrentPage },
                };

            _viewModel.Parent.IsLoading = true;
            //add filter
            if (_viewModel.Parent.IsGolonganChecked && _viewModel.Parent.FilterGolongan != null)
                param.Add("IdGolongan", _viewModel.Parent.FilterGolongan.IdGolongan ?? 0);

            if (_viewModel.Parent.IsWilayahChecked && _viewModel.Parent.FilterWilayah != null)
                param.Add("IdWilayah", _viewModel.Parent.FilterWilayah.IdWilayah ?? 0);

            if (_viewModel.Parent.IsPerkiraanDebetChecked && _viewModel.Parent.FilterPerkiraan3Debet != null)
                param.Add("IdPerkiraan3Debet", _viewModel.Parent.FilterPerkiraan3Debet.IdPerkiraan3 ?? 0);

            if (_viewModel.Parent.IsPerkiraanKreditChecked && _viewModel.Parent.FilterPerkiraan3Kredit != null)
                param.Add("IdPerkiraan3Kredit", _viewModel.Parent.FilterPerkiraan3Kredit.IdPerkiraan3 ?? 0);

            if (_viewModel.Parent.IsFlagChecked && _viewModel.Parent.FlagPembentukRekair != null)
                param.Add("FlagPembentukRekair", _viewModel.Parent.FlagPembentukRekair);

            if (_viewModel.Parent.IsPembentukRekairChecked && _viewModel.Parent.FilterPembentukRekAir.Value != null)
                param.Add("Keterangan", _viewModel.Parent.FilterPembentukRekAir.Value);

            if (_viewModel.Parent.IsJenisNonAirChecked && _viewModel.Parent.FilterJenisNonAir != null)
                param.Add("IdJenisNonAir", _viewModel.Parent.FilterJenisNonAir.IdJenisNonAir);

            if (_viewModel.Parent.IsPerkiraanChecked && _viewModel.Parent.FilterPerkiraan3 != null)
                param.Add("IdPerkiraan3", _viewModel.Parent.FilterPerkiraan3.IdPerkiraan3);

            _viewModel.DataList = new();


            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.Parent.Url}", param);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var data = result.Data.ToObject<ObservableCollection<MasterInPelayananWpf>>().OrderByDescending(x => x.WaktuUpdate);

                    _viewModel.DataList = new ObservableCollection<MasterInPelayananWpf>(data);
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
            _viewModel.Parent.IsEmpty = _viewModel.DataList.Count == 0;
            _ = _viewModel.TotalRecord > 100 ? _viewModel.IsOverLimit == true : _viewModel.IsOverLimit == false;

            _viewModel.Parent.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;


            switch (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key)
            {
                case 0:
                    {
                        string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_layanan_air_config.ini";

                        if (!File.Exists(ConfigIni))
                            return;

                        var parser = new IniFileParser.IniFileParser();
                        IniData data = parser.ReadFile(ConfigIni);

                        _viewModel.TableSetting = new
                        {
                            KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                            NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                            KodeGolongan = data["show_table_column"]["KodeGolongan"] == "1",
                            NamaGolongan = data["show_table_column"]["NamaGolongan"] == "1",
                            KodePerkiraan3Debet = data["show_table_column"]["KodePerkiraan3Debet"] == "1",
                            NamaPerkiraan3Debet = data["show_table_column"]["NamaPerkiraan3Debet"] == "1",
                            KodePerkiraan3Kredit = data["show_table_column"]["KodePerkiraan3Kredit"] == "1",
                            NamaPerkiraan3Kredit = data["show_table_column"]["NamaPerkiraan3Kredit"] == "1",
                            FlagPembentukRekair = data["show_table_column"]["FlagPembentukRekair"] == "1",
                            Keterangan = data["show_table_column"]["Keterangan"] == "1",
                        };
                    }
                    break;
                default:
                    {
                        string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_layanan_non_air_config.ini";

                        if (!File.Exists(ConfigIni))
                            return;

                        var parser = new IniFileParser.IniFileParser();
                        IniData data = parser.ReadFile(ConfigIni);

                        _viewModel.TableSetting = new
                        {
                            KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                            NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                            KodeGolongan = data["show_table_column"]["KodeGolongan"] == "1",
                            NamaGolongan = data["show_table_column"]["NamaGolongan"] == "1",
                            KodePerkiraan3 = data["show_table_column"]["KodePerkiraan3"] == "1",
                            NamaPerkiraan3 = data["show_table_column"]["NamaPerkiraan3"] == "1",
                            KodeJenisNonAir = data["show_table_column"]["KodeJenisNonAir"] == "1",
                            NamaJenisNonAir = data["show_table_column"]["NamaJenisNonAir"] == "1",
                            Keterangan = data["show_table_column"]["Keterangan"] == "1",
                        };

                    }
                    break;
            }
        }
    }
}
