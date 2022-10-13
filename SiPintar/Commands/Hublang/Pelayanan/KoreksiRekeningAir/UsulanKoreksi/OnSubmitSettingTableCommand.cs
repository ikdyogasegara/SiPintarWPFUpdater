using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(UsulanKoreksiViewModel viewModel, bool isTest = false)
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
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_usulan_koreksi_rekening_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["NamaPelanggan"] = CheckValue(param, "NamaPelanggan");
                data["show_table_column"]["Alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["NomorRegister"] = CheckValue(param, "NomorRegister");
                data["show_table_column"]["Alasan"] = CheckValue(param, "Alasan");
                data["show_table_column"]["Rayon"] = CheckValue(param, "Rayon");
                data["show_table_column"]["Wilayah"] = CheckValue(param, "Wilayah");
                data["show_table_column"]["Kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["NomorBeritaAcara"] = CheckValue(param, "NomorBeritaAcara");
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
