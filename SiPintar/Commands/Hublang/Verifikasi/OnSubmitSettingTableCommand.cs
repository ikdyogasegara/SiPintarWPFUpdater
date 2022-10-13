using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Commands.Hublang.Verifikasi
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly VerifikasiViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(VerifikasiViewModel viewModel, bool isTest = false)
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
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\verifikasi_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["Jenis"] = CheckValue(param, "Jenis");
                data["show_table_column"]["Status"] = CheckValue(param, "Status");
                data["show_table_column"]["NomorRegister"] = CheckValue(param, "NomorRegister");
                data["show_table_column"]["Wilayah"] = CheckValue(param, "Wilayah");
                data["show_table_column"]["NamaPelanggan"] = CheckValue(param, "NamaPelanggan");
                data["show_table_column"]["NoBeritaAcara"] = CheckValue(param, "NoBeritaAcara");
                data["show_table_column"]["Alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["Kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["Kecamatan"] = CheckValue(param, "Kecamatan");
                data["show_table_column"]["Cabang"] = CheckValue(param, "Cabang");
                data["show_table_column"]["Alasan"] = CheckValue(param, "Alasan");
                data["show_table_column"]["Biaya"] = CheckValue(param, "Biaya");
                data["show_table_column"]["UserInput"] = CheckValue(param, "UserInput");
                data["show_table_column"]["UserBeritaAcara"] = CheckValue(param, "UserBeritaAcara");

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
