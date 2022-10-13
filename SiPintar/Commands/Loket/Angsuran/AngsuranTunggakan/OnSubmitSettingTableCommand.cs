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
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitSettingTableCommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitSettingTableCommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
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
                string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Loket\\angsuran_config.ini";
                var parser = new IniFileParser.IniFileParser();
                IniData data = parser.ReadFile(ConfigIni);

                var param = (Dictionary<string, bool?>)parameter;

                data["tunggakan"]["nama"] = CheckValue(param, "Nama");
                data["tunggakan"]["jenis_angsuran"] = CheckValue(param, "JenisAngsuran");
                data["tunggakan"]["no_angsuran"] = CheckValue(param, "NoAngsuran");
                data["tunggakan"]["termin"] = CheckValue(param, "Termin");
                data["tunggakan"]["jumlah"] = CheckValue(param, "Jumlah");
                data["tunggakan"]["alamat"] = CheckValue(param, "Alamat");
                data["tunggakan"]["waktu_daftar"] = CheckValue(param, "WaktuDaftar");
                data["tunggakan"]["dibebankan_kepada"] = CheckValue(param, "DibebankanKepada");
                data["tunggakan"]["no_samb"] = CheckValue(param, "NoSamb");
                data["tunggakan"]["no_berita_acara"] = CheckValue(param, "NomorBA");

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
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "HublangRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (LoketView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
