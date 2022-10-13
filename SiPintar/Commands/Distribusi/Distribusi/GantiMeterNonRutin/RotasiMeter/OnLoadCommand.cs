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
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(RotasiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var item = parameter as ParamJadwalGantiMeterFilterDto;
            _viewModel.IsLoading = true;
            _viewModel.Parent.IsLoading = true;
            TableColumnConfiguration();
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" ,  _viewModel.LimitData.Value},
                { "CurrentPage" , _viewModel.CurrentPage },
                { "Rutin" , false },
            };

            if (item?.Tahun != null)
                param.Add("Tahun", item.Tahun);
            if (item?.Kelainan != null)
                param.Add("Kelainan", item.Kelainan);
            if (item?.NomorSpk != null)
                param.Add("NomorSpk", item.NomorSpk);
            if (item?.NomorBa != null)
                param.Add("NomorBa", item.NomorBa);
            if (!string.IsNullOrEmpty(item?.Nama))
                param.Add("Nama", item.Nama);
            if (!string.IsNullOrEmpty(item?.NoSamb))
                param.Add("NoSamb", item.NoSamb);
            if (item?.StatusData != null && item.StatusData == "Sudah SPK Pengecekan")
                param.Add("AdaSPK", true);
            if (item?.StatusData != null && item.StatusData == "Sudah BA Pengecekan")
                param.Add("AdaBeritaAcara", true);
            if (item?.IdWilayah != null)
                param.Add("IdWilayah", item.IdWilayah);
            if (item?.IdGolongan != null)
                param.Add("IdGolongan", item.IdGolongan);
            if (item?.IdRayon != null)
                param.Add("IdRayon", item.IdRayon);
            if (item?.IdArea != null)
                param.Add("IdArea", item.IdArea);



            _viewModel.DataList = new ObservableCollection<JadwalGantiMeterDto>();
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/jadwal-ganti-meter", param);
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
