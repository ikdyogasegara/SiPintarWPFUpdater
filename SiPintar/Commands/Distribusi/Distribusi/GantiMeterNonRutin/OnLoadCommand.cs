using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(GantiMeterNonRutinViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.CurrentSection = "RotasiMeter";
            _ = GetAreaAsync();
            _ = GetKelainanAsync();
            _ = GetWilayahAsync();
            _ = GetGolonganAsync();
            _ = GetRayonAsync();
            _ = GetTahunAsync();
            _ = GetDiameterAsync();
            _ = GetMerekMeterAsync();

            await Task.FromResult(_isTest);
        }

        public async Task GetAreaAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-area", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.AreaList = Result.Data.ToObject<List<MasterAreaDto>>();
                }
            }
        }

        public async Task GetKelainanAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-ganti-meter", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.KelainanList = Result.Data.ToObject<List<MasterKelainanDto>>();
                }
            }
        }

        public async Task GetWilayahAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-wilayah", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.WilayahList = Result.Data.ToObject<List<MasterWilayahDto>>();
                }
            }
        }

        public async Task GetTahunAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.TahunList = Result.Data.ToObject<List<MasterPeriodeDto>>().GroupBy(x => x.Tahun);

                }
            }
        }

        public async Task GetGolonganAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-golongan", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.GolonganList = Result.Data.ToObject<List<MasterGolonganDto>>();
                }
            }
        }

        public async Task GetRayonAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-rayon", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.RayonList = Result.Data.ToObject<List<MasterRayonDto>>();
                }
            }
        }

        public async Task GetDiameterAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tarif-diameter", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.DiameterList = Result.Data.ToObject<List<MasterDiameterDto>>();
                }
            }
        }

        public async Task GetMerekMeterAsync()
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-merek-meter", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _viewModel.MerekMeterList = Result.Data.ToObject<List<MasterMerekMeterDto>>();
                }
            }
        }
    }
}
