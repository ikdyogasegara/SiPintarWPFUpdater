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
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(RekeningAirViewModel viewModel, bool isTest = false)
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
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Billing\\supervisi_rekening_air_config.ini";
                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["show_table_column"]["flag_publish"] = CheckValue(param, "FlagPublish");
                data["show_table_column"]["flag_verifikasi"] = CheckValue(param, "FlagVerifikasi");
                data["show_table_column"]["nosamb"] = CheckValue(param, "Nosamb");
                data["show_table_column"]["nama"] = CheckValue(param, "Nama");
                data["show_table_column"]["nama_golongan"] = CheckValue(param, "NamaGolongan");
                data["show_table_column"]["nama_diameter"] = CheckValue(param, "NamaDiameter");
                data["show_table_column"]["kode_cabang"] = CheckValue(param, "KodeCabang");
                data["show_table_column"]["nama_cabang"] = CheckValue(param, "NamaCabang");
                data["show_table_column"]["kode_rayon"] = CheckValue(param, "KodeRayon");
                data["show_table_column"]["nama_rayon"] = CheckValue(param, "NamaRayon");
                data["show_table_column"]["kode_kelurahan"] = CheckValue(param, "KodeKelurahan");
                data["show_table_column"]["nama_kelurahan"] = CheckValue(param, "NamaKelurahan");
                data["show_table_column"]["id_flag"] = CheckValue(param, "IdFlag");
                data["show_table_column"]["nama_flag"] = CheckValue(param, "NamaFlag");
                data["show_table_column"]["kelainan"] = CheckValue(param, "Kelainan");
                data["show_table_column"]["taksasi"] = CheckValue(param, "Taksasi");
                data["show_table_column"]["petugas_baca"] = CheckValue(param, "PetugasBaca");
                data["show_table_column"]["kode_kolektif"] = CheckValue(param, "KodeKolektif");
                data["show_table_column"]["nama_kolektif"] = CheckValue(param, "NamaKolektif");
                data["show_table_column"]["no_rekening"] = CheckValue(param, "NoRekening");
                data["show_table_column"]["nama_status"] = CheckValue(param, "NamaStatus");

                data["show_table_column"]["stan_lalu"] = CheckValue(param, "StanLalu");
                data["show_table_column"]["stan_skrg"] = CheckValue(param, "StanSkrg");
                data["show_table_column"]["stan_angkat"] = CheckValue(param, "StanAngkat");
                data["show_table_column"]["pakai"] = CheckValue(param, "Pakai");
                data["show_table_column"]["pakai_kalkulasi"] = CheckValue(param, "PakaiHitung");
                data["show_table_column"]["biaya_pemakaian"] = CheckValue(param, "BiayaPemakaian");
                data["show_table_column"]["administrasi"] = CheckValue(param, "Administrasi");
                data["show_table_column"]["pemelaiharaan"] = CheckValue(param, "Pemeliharaan");
                data["show_table_column"]["retribusi"] = CheckValue(param, "Retribusi");
                data["show_table_column"]["pemeliharaan_lain"] = CheckValue(param, "PemeliharaanLain");
                data["show_table_column"]["administrasi_lain"] = CheckValue(param, "AdministrasiLain");
                data["show_table_column"]["retribusi_lain"] = CheckValue(param, "RetribusiLain");
                data["show_table_column"]["meterai"] = CheckValue(param, "Meterai");
                data["show_table_column"]["rekair"] = CheckValue(param, "Rekair");
                data["show_table_column"]["air_limbah"] = CheckValue(param, "AirLimbah");
                data["show_table_column"]["denda_pakai0"] = CheckValue(param, "DendaPakai0");
                data["show_table_column"]["denda"] = CheckValue(param, "Denda");
                data["show_table_column"]["pelayanan"] = CheckValue(param, "Pelayanan");
                data["show_table_column"]["ppn"] = CheckValue(param, "Ppn");
                data["show_table_column"]["total"] = CheckValue(param, "Total");
                data["show_table_column"]["waktu_koreksi"] = CheckValue(param, "WaktuKoreksi");
                data["show_table_column"]["waktu_publish"] = CheckValue(param, "WaktuPublish");
                data["show_table_column"]["flag_upload"] = CheckValue(param, "FlagUpload");
                data["show_table_column"]["kode_wilayah"] = CheckValue(param, "KodeWilayah");
                data["show_table_column"]["nama_wilayah"] = CheckValue(param, "NamaWilayah");
                data["show_table_column"]["flag_koreksi"] = CheckValue(param, "FlagKoreksi");
                data["show_table_column"]["kode_golongan"] = CheckValue(param, "KodeGolongan");
                data["show_table_column"]["kode_diameter"] = CheckValue(param, "KodeDiameter");
                data["show_table_column"]["alamat"] = CheckValue(param, "Alamat");
                data["show_table_column"]["nama_user"] = CheckValue(param, "NamaUser");
                data["show_table_column"]["nama_loket"] = CheckValue(param, "NamaLoket");
                data["show_table_column"]["waktu_transaksi"] = CheckValue(param, "WaktuTransaksi");


                parser.WriteFile(ConfigIni, data);

                SuccessMessage = "Konfigurasi berhasil disimpan";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Gagal menyimpan konfigurasi. {e.Message}";
            }

            ShowResult(SuccessMessage, ErrorMessage);

            await Task.FromResult(_isTest);

        }

        private string CheckValue(Dictionary<string, bool?> param, string Key) => param[Key] != null && (bool)param[Key] ? "1" : "0";

        [ExcludeFromCodeCoverage]
        private void ShowResult(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowError(ErrorMessage); });
                }
                else if (SuccessMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate { ShowSuccess(SuccessMessage); });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnFilterCommand.Execute(null);
        }
    }
}
