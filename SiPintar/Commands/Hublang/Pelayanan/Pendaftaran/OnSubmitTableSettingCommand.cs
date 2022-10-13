using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitTableSettingCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitTableSettingCommand(PendaftaranViewModel viewModel, bool isTest = false)
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
                var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Hublang\\pelayanan_pendaftaran_sambungan_baru_config.ini";
                var parser = new IniFileParser.IniFileParser();
                var data = parser.ReadFile(configIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["flag"] = CheckValue(param, "Flag");
                data["show_table_column"]["nomor_pendaftaran"] = CheckValue(param, "NomorPendaftaran");
                data["show_table_column"]["tanggal"] = CheckValue(param, "Tanggal");
                data["show_table_column"]["nama"] = CheckValue(param, "Nama");
                data["show_table_column"]["alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["no_handphone"] = CheckValue(param, "NoHandphone");
                data["show_table_column"]["no_samb_depan"] = CheckValue(param, "NoSambDepan");
                data["show_table_column"]["no_samb_belakang"] = CheckValue(param, "NoSambBelakang");
                data["show_table_column"]["no_samb_kiri"] = CheckValue(param, "NoSambKiri");
                data["show_table_column"]["no_samb_kanan"] = CheckValue(param, "NoSambKanan");
                data["show_table_column"]["cabang"] = CheckValue(param, "Cabang");
                data["show_table_column"]["kelurahan"] = CheckValue(param, "Kelurahan");
                data["show_table_column"]["penghuni"] = CheckValue(param, "Penghuni");
                data["show_table_column"]["jenis_bangunan"] = CheckValue(param, "JenisBangunan");
                data["show_table_column"]["pekerjaan"] = CheckValue(param, "Pekerjaan");
                data["show_table_column"]["biaya"] = CheckValue(param, "Biaya");
                data["show_table_column"]["tipe"] = CheckValue(param, "Tipe");
                data["show_table_column"]["user"] = CheckValue(param, "User");
                data["show_table_column"]["nomor_RAB"] = CheckValue(param, "NomorRAB");
                data["show_table_column"]["tanggal_RAB"] = CheckValue(param, "TanggalRAB");
                data["show_table_column"]["nomor_SPPRAB"] = CheckValue(param, "NomorSPPRAB");
                data["show_table_column"]["tanggal_SPPRAB"] = CheckValue(param, "TanggalSPPRAB");
                data["show_table_column"]["nomor_BA"] = CheckValue(param, "NomorBA");
                data["show_table_column"]["nomor_SPKO"] = CheckValue(param, "NomorSPKO");
                data["show_table_column"]["nomor_SPKP"] = CheckValue(param, "NomorSPKP");
                data["show_table_column"]["nomor_SPA"] = CheckValue(param, "NomorSPA");

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
