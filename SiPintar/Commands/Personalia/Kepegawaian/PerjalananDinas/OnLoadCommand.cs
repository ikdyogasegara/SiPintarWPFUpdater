using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PerjalananDinasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>();

            if (_viewModel.IsNoPegawaiChecked)
                param.Add("NoPegawai", _viewModel.FilterNoPegawai);
            if (_viewModel.IsNamaPegawaiChecked)
                param.Add("NamaPegawai", _viewModel.FilterNamaPegawai);
            if (_viewModel.IsNoSptChecked)
                param.Add("NoSpt", _viewModel.FilterNoSpt);
            if (_viewModel.IsNoSppdChecked)
                param.Add("NoSppd", _viewModel.FilterNoSppd);
            if (_viewModel.IsKeperluanChecked)
                param.Add("Keperluan", _viewModel.FilterKeperluan);
            if (_viewModel.IsTempatTujuanChecked)
                param.Add("TempatTujuan", _viewModel.FilterTempatTujuan);
            if (_viewModel.IsTglBerangkatChecked)
                param.Add("TglBerangkat", _viewModel.FilterTglBerangkat);
            if (_viewModel.IsTglKembaliChecked)
                param.Add("TglKembali", _viewModel.FilterTglKembali);
            if (_viewModel.IsBebanAnggaranChecked)
                param.Add("BebanAnggaran", _viewModel.FilterBebanAnggaran);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/sppd", param));
            _viewModel.PerjalananDinasList = new ObservableCollection<SppdDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.PerjalananDinasList = Result.Data.ToObject<ObservableCollection<SppdDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.PerjalananDinasList.Any();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
