using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(BaPengembalianViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            try
            {
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_berita_acara_pengembalian_config.ini";
                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["NoBeritaAcara"] = CheckValue(param, "NoBeritaAcara");
                data["show_table_column"]["NomorSambungan"] = CheckValue(param, "NomorSambungan");
                data["show_table_column"]["Bulan"] = CheckValue(param, "Bulan");
                data["show_table_column"]["NamaPelanggan"] = CheckValue(param, "NamaPelanggan");
                data["show_table_column"]["Alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["RekairLama"] = CheckValue(param, "RekairLama");
                data["show_table_column"]["RekairBaru"] = CheckValue(param, "RekairBaru");
                data["show_table_column"]["Gol"] = CheckValue(param, "Gol");
                data["show_table_column"]["Wilayah"] = CheckValue(param, "Wilayah");
                data["show_table_column"]["Rayon"] = CheckValue(param, "Rayon");
                data["show_table_column"]["Kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["Kecamatan"] = CheckValue(param, "Kecamatan");
                data["show_table_column"]["Cabang"] = CheckValue(param, "Cabang");
                data["show_table_column"]["Alasan"] = CheckValue(param, "Alasan");
                data["show_table_column"]["KondisiMeter"] = CheckValue(param, "KondisiMeter");
                data["show_table_column"]["Keterangan"] = CheckValue(param, "Keterangan");
                data["show_table_column"]["TanggalInput"] = CheckValue(param, "TanggalInput");
                data["show_table_column"]["UserInput"] = CheckValue(param, "UserInput");

                parser.WriteFile(ConfigIni, data);

                SuccessMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            DialogHelper.ShowSnackbar(_isTest, SuccessMessage == null ? "danger" : "success", SuccessMessage ?? ErrorMessage);
            if (!string.IsNullOrWhiteSpace(SuccessMessage))
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }

        private string CheckValue(Dictionary<string, bool?> param, string Key) => param[Key] != null && (bool)param[Key] ? "1" : "0";
    }
}
