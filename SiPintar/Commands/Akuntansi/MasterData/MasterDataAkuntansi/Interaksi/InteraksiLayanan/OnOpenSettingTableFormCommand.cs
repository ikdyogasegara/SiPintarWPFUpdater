using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using IniFileParser.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnOpenSettingTableFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSettingTableFormCommand(InteraksiLayananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsLoading = true;

            switch (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key)
            {
                case 0:
                    {
                        string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_layanan_air_config.ini";
                        var parser = new IniFileParser.IniFileParser();
                        IniData data = parser.ReadFile(ConfigIni);

                        _viewModel.TableSetting = new
                        {
                            KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                            NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                            KodeGolongan = data["show_table_column"]["KodeGolongan"] == "1",
                            NamaGolongan = data["show_table_column"]["NamaGolongan"] == "1",
                            KodePerkiraan3Debet = data["show_table_column"]["KodePerkiraan3Debet"] == "1",
                            NamaPerkiraan3Debet = data["show_table_column"]["NamaPerkiraan3Debet"] == "1",
                            KodePerkiraan3Kredit = data["show_table_column"]["KodePerkiraan3Kredit"] == "1",
                            NamaPerkiraan3Kredit = data["show_table_column"]["NamaPerkiraan3Kredit"] == "1",
                            FlagPembentukRekair = data["show_table_column"]["FlagPembentukRekair"] == "1",
                            Keterangan = data["show_table_column"]["Keterangan"] == "1",
                        };
                    }
                    break;
                default:
                    {
                        string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_layanan_non_air_config.ini";
                        var parser = new IniFileParser.IniFileParser();
                        IniData data = parser.ReadFile(ConfigIni);

                        _viewModel.TableSetting = new
                        {
                            KodeWilayah = data["show_table_column"]["KodeWilayah"] == "1",
                            NamaWilayah = data["show_table_column"]["NamaWilayah"] == "1",
                            KodeGolongan = data["show_table_column"]["KodeGolongan"] == "1",
                            NamaGolongan = data["show_table_column"]["NamaGolongan"] == "1",
                            KodePerkiraan3 = data["show_table_column"]["KodePerkiraan3"] == "1",
                            NamaPerkiraan3 = data["show_table_column"]["NamaPerkiraan3"] == "1",
                            KodeJenisNonAir = data["show_table_column"]["KodeJenisNonAir"] == "1",
                            NamaJenisNonAir = data["show_table_column"]["NamaJenisNonAir"] == "1",
                            Keterangan = data["show_table_column"]["Keterangan"] == "1",
                        };
                    }
                    break;
            }

            ShowDialog();
            _viewModel.Parent.IsLoading = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) { _ = DialogHost.Show(new SettingTableFormView(_viewModel), "AkuntansiRootDialog"); }
        }
    }
}
