using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PengelompokanAktivaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PengelompokanAktivaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _ = _viewModel;
            TableColumnConfiguration();
            _ = _restApi;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_pengelompokan_aktiva_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);
            _viewModel.TableSetting = new
            {
                GolAktiva = data["show_table_column"]["GolAktiva"] == "1",
                UraianAktiva = data["show_table_column"]["UraianAktiva"] == "1",
                JumlahTahun = data["show_table_column"]["JumlahTahun"] == "1",
                TipeAktiva = data["show_table_column"]["TipeAktiva"] == "1",
            };

        }
    }
}
