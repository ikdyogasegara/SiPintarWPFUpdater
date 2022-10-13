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
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(SaldoAwalPerkiraanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsCloseDialog)
            {
                _viewModel.SelectedTahun = _viewModel.TahunList[0];
                DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog");
                _viewModel.IsCloseDialog = false;
            }

            TableColumnConfiguration();
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000000 },
                { "CurrentPage", 1 },
                { "Tahun", _viewModel.SelectedTahun.Value },
            };

            if (_viewModel.IsKodePerkiraanChecked && _viewModel.FilterKodePerkiraan != null)
                param.Add("KodePerkiraan", _viewModel.FilterKodePerkiraan);

            if (_viewModel.IsNamaPerkiraanChecked && _viewModel.FilterNamaPerkiraan != null)
                param.Add("NamaPerkiraan", _viewModel.FilterNamaPerkiraan);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/saldo-awal-perkiraan", param);
            _viewModel.DataList = new ObservableCollection<SaldoAwalPerkiraanWpf>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var data = result.Data.ToObject<ObservableCollection<SaldoAwalPerkiraanWpf>>()!;
                    var perkiraan1 = data.GroupBy(x => x.KodePerkiraan1)
                                                        .Select(y => new SaldoAwalPerkiraanWpf
                                                        {
                                                            KodePerkiraanWpf = y.First().KodePerkiraan1,
                                                            NamaPerkiraanWpf = y.First().NamaPerkiraan1,
                                                            SaldoAwalWpf = y.Sum(z => z.DebetAwal - z.KreditAwal),
                                                            SaldoAkhirWpf = y.Sum(z => z.SaldoAkhir),
                                                            TglSaldoWpf = null,
                                                            PerkiraanWpf = "perkiraan1"
                                                        });

                    var perkiraan2 = data.GroupBy(x => x.KodePerkiraan2)
                                                        .Select(y => new SaldoAwalPerkiraanWpf
                                                        {
                                                            KodePerkiraanWpf = y.First().KodePerkiraan2,
                                                            NamaPerkiraanWpf = y.First().NamaPerkiraan2,
                                                            SaldoAwalWpf = y.Sum(z => z.DebetAwal - z.KreditAwal),
                                                            SaldoAkhirWpf = y.Sum(z => z.SaldoAkhir),
                                                            TglSaldoWpf = null,
                                                            PerkiraanWpf = "perkiraan2"
                                                        });

                    var perkiraan3 = data.Select(y => new SaldoAwalPerkiraanWpf
                    {
                        IdSaldoPerkiraan = y.IdSaldoPerkiraan,
                        DebetAwal = y.DebetAwal,
                        KreditAwal = y.KreditAwal,
                        TglSaldo = y.TglSaldo,
                        KodePerkiraanWpf = y.KodePerkiraan3,
                        NamaPerkiraanWpf = y.NamaPerkiraan3,
                        SaldoAwalWpf = y.DebetAwal - y.KreditAwal,
                        SaldoAkhirWpf = y.SaldoAkhir,
                        TglSaldoWpf = y.TglSaldo,
                        PerkiraanWpf = "perkiraan3"
                    });

                    var dataList = perkiraan1.Concat(perkiraan2).Concat(perkiraan3).OrderBy(x => x.KodePerkiraanWpf);

                    _viewModel.TotalSaldoAwal = dataList.Sum(x => x.SaldoAwalWpf);
                    _viewModel.TotalSaldoAkhir = dataList.Sum(x => x.SaldoAkhirWpf);
                    _viewModel.DataList = new ObservableCollection<SaldoAwalPerkiraanWpf>(dataList);

                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg ?? "-");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsEmpty = _viewModel.DataList.Count == 0;
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_saldo_awal_perkiraan_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                KodePerkiraan = data["show_table_column"]["KodePerkiraan"] == "1",
                NamaPerkiraan = data["show_table_column"]["NamaPerkiraan"] == "1",
                SaldoAwal = data["show_table_column"]["SaldoAwal"] == "1",
                SaldoAkhir = data["show_table_column"]["SaldoAkhir"] == "1",
                TanggalSaldo = data["show_table_column"]["TanggalSaldo"] == "1",
            };

        }
    }
}
