using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            string successMessage = null;
            string errorMessage = null;

            try
            {
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_tagihan_manual_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["JenisTipePelanggan"] = CheckValue(param, "JenisTipePelanggan");
                data["show_table_column"]["NomorNonAir"] = CheckValue(param, "NomorNonAir");
                data["show_table_column"]["KodeJenisNonAir"] = CheckValue(param, "KodeJenisNonAir");
                data["show_table_column"]["NamaJenisNonAir"] = CheckValue(param, "NamaJenisNonAir");
                data["show_table_column"]["Total"] = CheckValue(param, "Total");
                data["show_table_column"]["NomorPelanggan"] = CheckValue(param, "NomorPelanggan");
                data["show_table_column"]["Nama"] = CheckValue(param, "Nama");
                data["show_table_column"]["Alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["Keterangan"] = CheckValue(param, "Keterangan");
                data["show_table_column"]["KodeTarif"] = CheckValue(param, "KodeTarif");
                data["show_table_column"]["NamaTarif"] = CheckValue(param, "NamaTarif");
                data["show_table_column"]["KodeRayon"] = CheckValue(param, "KodeRayon");
                data["show_table_column"]["NamaRayon"] = CheckValue(param, "NamaRayon");
                data["show_table_column"]["KodeWilayah"] = CheckValue(param, "KodeWilayah");
                data["show_table_column"]["NamaWilayah"] = CheckValue(param, "NamaWilayah");
                data["show_table_column"]["KodeKelurahan"] = CheckValue(param, "KodeKelurahan");
                data["show_table_column"]["NamaKelurahan"] = CheckValue(param, "NamaKelurahan");

                parser.WriteFile(configIni, data);

                successMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                errorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            DialogHelper.ShowSnackbar(_isTest, successMessage == null ? "danger" : "success", successMessage ?? errorMessage);
            if (!string.IsNullOrWhiteSpace(successMessage))
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private string CheckValue(Dictionary<string, bool?> param, string key) => param[key] != null && (bool)param[key] ? "1" : "0";
    }
}
