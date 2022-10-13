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

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PersentasePenyusutanAktivaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.DataList = new();

            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" , 10000000 },
                { "CurrentPage", 1},
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-penyusutan-aktiva", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<MasterPenyusutanAktivaWpf>>();
                    MarkNewlyCreated();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg!);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsEmpty = _viewModel.DataList!.Count == 0;

            TableColumnConfiguration();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void MarkNewlyCreated()
        {
            if (_viewModel.DumpNewData != null)
            {
                _viewModel.DataList!
                    .Where(x => x.KodeGolAktiva == _viewModel.DumpNewData.KodeGolAktiva && x.NamaGolAktiva == _viewModel.DumpNewData.NamaGolAktiva)
                    .ForEach(i => i.IsNewDatawpf = true);

                _viewModel.DumpNewData = null;
            }
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_persentase_penyusutan_aktiva_config.ini";

            if (!File.Exists(configIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);
            _viewModel.TableSetting = new
            {
                GolAktiva = data["show_table_column"]["GolAktiva"] == "1",
                NamaGolAktiva = data["show_table_column"]["NamaGolAktiva"] == "1",
                JumlahTahun = data["show_table_column"]["JumlahTahun"] == "1",
                Persen = data["show_table_column"]["Persen"] == "1",
                TipeAktiva = data["show_table_column"]["TipeAktiva"] == "1",
            };

        }
    }
}
