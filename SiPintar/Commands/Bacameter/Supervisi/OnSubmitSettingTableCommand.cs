using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(SupervisiViewModel viewModel, bool isTest = false)
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
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Bacameter\\supervisi_config.ini";

                if (!File.Exists(ConfigIni))
                    return;

                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["table_supervisi"]["id_pelanggan"] = CheckValue(param, "IdPelanggan");
                data["table_supervisi"]["nama_pelanggan"] = CheckValue(param, "NamaPelanggan");
                data["table_supervisi"]["status_aktif"] = CheckValue(param, "Status");
                data["table_supervisi"]["status_verifikasi"] = CheckValue(param, "Verifikasi");
                data["table_supervisi"]["stan_awal"] = CheckValue(param, "StanAwal");
                data["table_supervisi"]["stan_akhir"] = CheckValue(param, "StanAkhir");
                data["table_supervisi"]["pakai"] = CheckValue(param, "Pakai");
                data["table_supervisi"]["stan_angkat"] = CheckValue(param, "StanAngkat");
                data["table_supervisi"]["biayapemakaian"] = CheckValue(param, "BiayaPemakaian");
                data["table_supervisi"]["administrasi"] = CheckValue(param, "Administrasi");
                data["table_supervisi"]["pemeliharaan"] = CheckValue(param, "Pemeliharaan");
                data["table_supervisi"]["retribusi"] = CheckValue(param, "Retribusi");
                data["table_supervisi"]["pelayanan"] = CheckValue(param, "Pelayanan");
                data["table_supervisi"]["airlimbah"] = CheckValue(param, "AirLimbah");
                data["table_supervisi"]["dendapakai0"] = CheckValue(param, "DendaPakai0");
                data["table_supervisi"]["administrasilain"] = CheckValue(param, "AdministrasiLain");
                data["table_supervisi"]["pemeliharaanlain"] = CheckValue(param, "PemeliharaanLain");
                data["table_supervisi"]["retribusilain"] = CheckValue(param, "RetribusiLain");
                data["table_supervisi"]["ppn"] = CheckValue(param, "Ppn");
                data["table_supervisi"]["meterai"] = CheckValue(param, "Meterai");
                data["table_supervisi"]["rekair"] = CheckValue(param, "Rekair");
                data["table_supervisi"]["denda"] = CheckValue(param, "Denda");
                data["table_supervisi"]["total"] = CheckValue(param, "Total");
                data["table_supervisi"]["merek_meter"] = CheckValue(param, "MerekMeter");
                data["table_supervisi"]["kode_rayon"] = CheckValue(param, "KodeRayon");
                data["table_supervisi"]["rayon"] = CheckValue(param, "Rayon");
                data["table_supervisi"]["alamat"] = CheckValue(param, "Alamat");
                data["table_supervisi"]["no_sambungan"] = CheckValue(param, "NoSambungan");
                data["table_supervisi"]["kode_golongan"] = CheckValue(param, "KodeGolongan");
                data["table_supervisi"]["golongan"] = CheckValue(param, "Golongan");
                data["table_supervisi"]["diameter"] = CheckValue(param, "Diameter");
                data["table_supervisi"]["kelainan"] = CheckValue(param, "Kelainan");
                data["table_supervisi"]["sumber_modul"] = CheckValue(param, "SumberModul");
                data["table_supervisi"]["nama_petugas"] = CheckValue(param, "NamaPetugas");
                data["table_supervisi"]["waktu_verifikasi"] = CheckValue(param, "WaktuVerifikasi");
                data["table_supervisi"]["waktu_baca"] = CheckValue(param, "WaktuBaca");
                data["table_supervisi"]["lampiran"] = CheckValue(param, "Lampiran");
                data["table_supervisi"]["wide_row_height"] = CheckValue(param, "WideRowHeight");
                data["table_supervisi"]["normal_row_height"] = CheckValue(param, "NormalRowHeight");
                data["table_supervisi"]["narrow_row_height"] = CheckValue(param, "NarrowRowHeight");

                parser.WriteFile(ConfigIni, data);

                SuccessMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            if (App.OpenedWindow.ContainsKey("bacameter"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BacameterRootDialog",
                    App.OpenedWindow["bacameter"], false, _viewModel.OnLoadCommand);

            await Task.FromResult(_isTest);
        }

        private string CheckValue(Dictionary<string, bool?> param, string Key) => param[Key] != null && (bool)param[Key] ? "1" : "0";

    }
}
