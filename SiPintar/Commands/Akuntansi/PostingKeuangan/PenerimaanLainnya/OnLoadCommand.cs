using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PenerimaanLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            TableColumnConfiguration();

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriodeAkuntansi",
                "MasterWilayah",
                "MasterPerkiraan3",
            });

            _viewModel.PeriodeList = MasterListGlobal.MasterPeriodeAkuntansi;
            _viewModel.PeriodeTutupBukuList = new(MasterListGlobal.MasterPeriodeAkuntansi.Where(x => x.FlagTutupBuku == true));
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.Perkiraan3List = MasterListGlobal.MasterPerkiraan3;

            await (_viewModel.OnRefreshCommand as AsyncCommandBase).ExecuteAsync(null);

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\posting_keuangan_penerimaanlainnya_config.ini";
            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            _viewModel.TableSetting = new
            {
                NoTrans = data["show_table_column"]["notrans"] == "1",
                KodeWilayah = data["show_table_column"]["kode_wilayah"] == "1",
                NamaWilayah = data["show_table_column"]["nama_wilayah"] == "1",
                Uraian = data["show_table_column"]["uraian"] == "1",
                KodeDebet = data["show_table_column"]["kode_debet"] == "1",
                NamaDebet = data["show_table_column"]["nama_debet"] == "1",
                KodeKredit = data["show_table_column"]["kode_kredit"] == "1",
                NamaKredit = data["show_table_column"]["nama_kredit"] == "1"
            };
        }
    }
}
