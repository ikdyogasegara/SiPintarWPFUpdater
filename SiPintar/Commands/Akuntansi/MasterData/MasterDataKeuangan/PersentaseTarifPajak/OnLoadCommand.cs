using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PersentaseTarifPajakViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _ = _viewModel;
            _ = _restApi;

            GetDummyData();
            TableColumnConfiguration();
            _viewModel.IsEmpty = _viewModel.DataList == null;


            await Task.FromResult(_isTest);
        }

        public void GetDummyData()
        {
            _viewModel.DataList = new List<dummydatakeuangan>()
            {
                new dummydatakeuangan()
                {
                    Id = 1,
                    IdPdam = 1,
                    KodePersen ="01",
                    NamaPersen ="%Utang%pajak%Ppn",
                    JumlahPersen = 10

                },
                new dummydatakeuangan()
                {
                    Id = 2,
                    IdPdam = 1,
                    KodePersen ="01",
                    NamaPersen ="%Utang%PPh%pasal 21%",
                    JumlahPersen = 10
                },
                new dummydatakeuangan()
                {
                    Id = 3,
                    IdPdam = 1,
                    KodePersen ="01",
                    NamaPersen ="%Utang%PPh%pasal 22%",
                    JumlahPersen = 10
                }
            };
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            if (_isTest)
                return;

            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_keuangan_persentase_tarif_pajak_config.ini";

            if (!File.Exists(ConfigIni))
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);
            _viewModel.TableSetting = new
            {
                KodePersen = data["show_table_column"]["KodePersen"] == "1",
                NamaPersen = data["show_table_column"]["NamaPersen"] == "1",
                JumlahPersen = data["show_table_column"]["JumlahPersen"] == "1",
            };

        }
    }
}
