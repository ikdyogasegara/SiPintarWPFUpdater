using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;

namespace SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin
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


            _viewModel.IsLoading = true;
            _viewModel.DataGantiMeterList = new ObservableCollection<MasterJenisGantiMeterWpf>();
            var Param = new Dictionary<string, dynamic>();
            Param.Add("Kategori", "Non Rutin");


            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-ganti-meter", Param));
            _viewModel.JenisGantiMeterList = new ObservableCollection<MasterJenisGantiMeterDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.JenisGantiMeterList = Result.Data.ToObject<ObservableCollection<MasterJenisGantiMeterDto>>();
                    foreach (var item in _viewModel.JenisGantiMeterList)
                    {
                        _viewModel.JenisGantiMeterList = Result.Data.ToObject<ObservableCollection<MasterJenisGantiMeterDto>>();
                        var data = _viewModel.JenisGantiMeterList.Select(item => new MasterJenisGantiMeterWpf
                        {
                            IdPdam = item.IdPdam,
                            IdJenisGantiMeter = item.IdJenisGantiMeter,
                            JenisGantiMeter = item.JenisGantiMeter,
                            Kategori = item.Kategori,
                            IdJenisNonAir = item.IdJenisNonAir,
                            IdWarnaGantiMeter = item.IdWarnaGantiMeter,
                            KodeJenisNonAir = item.KodeJenisNonAir,
                            KodeWarnaGantiMeter = item.KodeWarnaGantiMeter,
                            WarnaGantiMeter = item.WarnaGantiMeter,
                            WaktuUpdate = item.WaktuUpdate,
                            FlagDenganBiaya = item.IdJenisNonAir != null ? true : false,
                        }).ToList();
                        _viewModel.DataGantiMeterList = new ObservableCollection<MasterJenisGantiMeterWpf>(data);
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "distribusi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "distribusi");

            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-warna-ganti-meter", Param));
            _viewModel.WarnaMeterList = new ObservableCollection<MasterWarnaGantiMeterDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.WarnaMeterList = Result.Data.ToObject<ObservableCollection<MasterWarnaGantiMeterDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "distribusi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "distribusi");

            Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-non-air", Param));
            _viewModel.JenisNonairList = new ObservableCollection<MasterJenisNonAirDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.JenisNonairList = Result.Data.ToObject<ObservableCollection<MasterJenisNonAirDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "distribusi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "distribusi");

            _viewModel.IsEmpty = !_viewModel.JenisGantiMeterList.Any();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
