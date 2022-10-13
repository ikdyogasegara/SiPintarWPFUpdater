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
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(RotasiMeterViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var Param = parameter as ParamJadwalGantiMeterFilterDto;

            _viewModel.IsLoading = true;
            _viewModel.Parent.IsLoading = true;

            TableColumnConfiguration();
            var param = new Dictionary<string, dynamic>
            {
                {"PageSize" ,  _viewModel.LimitData.Value},
                { "CurrentPage" , _viewModel.CurrentPage },
                { "Rutin" , true },
            };

            if (Param?.Tahun != null)
                param.Add("Tahun", Param?.Tahun);
            if (Param?.Kelainan != null)
                param.Add("Kelainan", Param.Kelainan);
            if (Param?.NomorSpk != null)
                param.Add("NomorSpk", Param.NomorSpk);
            if (Param?.NomorBa != null)
                param.Add("NomorBa", Param.NomorBa);
            if (!string.IsNullOrEmpty(Param?.Nama))
                param.Add("Nama", Param.Nama);
            if (!string.IsNullOrEmpty(Param?.NoSamb))
                param.Add("NoSamb", Param?.NoSamb);
            if (Param?.StatusData != null && Param?.StatusData == "Sudah SPK Pengecekan")
                param.Add("AdaSPK", true);
            if (Param?.StatusData != null && Param?.StatusData == "Sudah BA Pengecekan")
                param.Add("AdaBeritaAcara", true);
            if (Param?.IdWilayah != null)
                param.Add("IdWilayah", Param?.IdWilayah);
            if (Param?.IdGolongan != null)
                param.Add("IdGolongan", Param?.IdGolongan);
            if (Param?.IdRayon != null)
                param.Add("IdRayon", Param?.IdRayon);
            if (Param?.IdArea != null)
                param.Add("IdArea", Param?.IdArea);



            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", param);

            _viewModel.DataList = new ObservableCollection<JadwalGantiMeterDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DataList = Result.Data.ToObject<ObservableCollection<JadwalGantiMeterDto>>();
                    _viewModel.TotalRecord = (int)Response.Data.Record;
                    _viewModel.TotalPage = Result.TotalPage;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);

            _viewModel.IsLoading = false;
            _viewModel.Parent.IsLoading = false;


            if (_viewModel.DataList == null)
                _viewModel.IsEmpty = true;
            _viewModel.IsEmpty = !_viewModel.DataList.Any();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void TableColumnConfiguration()
        {
            string ConfigIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Loket\\jadwal_ganti_meter_config.ini";

            if (!File.Exists(ConfigIni) || _isTest)
                return;

            var parser = new IniFileParser.IniFileParser();
            _ = parser.ReadFile(ConfigIni);

            _viewModel.TableSetting = new
            {
            };
        }
    }
}
