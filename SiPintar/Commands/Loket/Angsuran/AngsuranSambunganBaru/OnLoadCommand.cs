using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(AngsuranSambunganBaruViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var Param = parameter as RekeningNonAirAngsuranDto;
            _viewModel.IsLoading = true;
            TableColumnConfiguration();
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" ,  _viewModel.LimitData.Value},
                { "CurrentPage" , _viewModel.CurrentPage },
                { "IdJenisNonAir", -1 },
            };

            if (!string.IsNullOrEmpty(Param?.Nama))
                param.Add("Nama", Param.Nama);
            if (!string.IsNullOrEmpty(Param?.NoSamb))
                param.Add("NoSamb", Param?.NoSamb);
            if (Param?.FlagPublish != null)
                param.Add("FlagPublish", Param.FlagPublish);
            if (Param?.FlagLunas != null)
                param.Add("FlagLunas", Param.FlagLunas);
            if (Param?.IdUser != null)
                param.Add("IdUserYangMendaftarkan", Param.IdUser);


            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air", param);

            _viewModel.DataList = new ObservableCollection<RekeningNonAirAngsuranDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<RekeningNonAirAngsuranDto>>();
                    _viewModel.TotalRecord = (int)Response.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);


            _viewModel.IsLoading = false;

            if (_viewModel.DataList == null)
                _viewModel.IsEmpty = true;
            _viewModel.IsEmpty = !_viewModel.DataList.Any();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Loket\\angsuran_config.ini";

            if (!File.Exists(ConfigIni) || _isTest)
                return;

            var parser = new IniFileParser.IniFileParser();
            IniData data = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
                Nama = data["sambungan_baru"]["nama"] == "1",
                JenisAngsuran = data["sambungan_baru"]["jenis_angsuran"] == "1",
                NoAngsuran = data["sambungan_baru"]["no_angsuran"] == "1",
                Termin = data["sambungan_baru"]["termin"] == "1",
                Jumlah = data["sambungan_baru"]["jumlah"] == "1",
                Alamat = data["sambungan_baru"]["alamat"] == "1",
                WaktuDaftar = data["sambungan_baru"]["waktu_daftar"] == "1",
                DibebankanKepada = data["sambungan_baru"]["dibebankan_kepada"] == "1",
                NoSamb = data["sambungan_baru"]["no_samb"] == "1",
                NomorBA = data["sambungan_baru"]["no_berita_acara"] == "1",
            };
        }
    }
}
