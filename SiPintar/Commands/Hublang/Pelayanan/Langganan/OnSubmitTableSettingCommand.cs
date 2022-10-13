using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitTableSettingCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitTableSettingCommand(LanggananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            if (!_isTest)
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_pelanggan_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["nama"] = CheckValue(param, "Nama");
                data["show_table_column"]["alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["rayon"] = CheckValue(param, "Rayon");
                data["show_table_column"]["area"] = CheckValue(param, "Area");
                data["show_table_column"]["wilayah"] = CheckValue(param, "Wilayah");
                data["show_table_column"]["kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["kecamatan"] = CheckValue(param, "Kecamatan");
                data["show_table_column"]["cabang"] = CheckValue(param, "Cabang");
                data["show_table_column"]["kolektif"] = CheckValue(param, "Kolektif");
                data["show_table_column"]["flag"] = CheckValue(param, "Flag");

                parser.WriteFile(configIni, data);

                DialogHelper.ShowSnackbar(_isTest, "success", "Konfigurasi berhasil tersimpan !");
                await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
            }
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key] ? "1" : "0";
    }
}
