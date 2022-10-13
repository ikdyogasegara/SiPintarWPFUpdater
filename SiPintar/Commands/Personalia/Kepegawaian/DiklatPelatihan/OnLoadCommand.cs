using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(DiklatPelatihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
            if (_viewModel.IsNoSertifikatChecked)
                param.Add("NoSertifikat", _viewModel.FilterNoSertifikat);
            if (_viewModel.IsUraianChecked)
                param.Add("Uraian", _viewModel.FilterUraian);
            if (_viewModel.IsTempatChecked)
                param.Add("Tempat", _viewModel.FilterTempat);
            if (_viewModel.IsPenyelenggaraChecked)
                param.Add("Penyelenggara", _viewModel.FilterPenyelenggara);
            if (_viewModel.IsTglAwalChecked)
                param.Add("TglAwal", _viewModel.FilterTglAwal);
            if (_viewModel.IsTglAkhirChecked)
                param.Add("TglAkhir", _viewModel.FilterTglAkhir);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/diklat", param));
            _viewModel.DiklatPelatihanList = new ObservableCollection<DiklatDto>();
            _viewModel.DiklatPelatihanListTable = new ObservableCollection<DiklatWpf>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.DiklatPelatihanList = Result.Data.ToObject<ObservableCollection<DiklatDto>>();
                    _viewModel.DiklatPelatihanListTable = new ObservableCollection<DiklatWpf>(
                        _viewModel.DiklatPelatihanList.SelectMany(d => d.Detail
                            .Select(t => new DiklatWpf
                            {
                                IdDiklat = d.IdDiklat,
                                NamaPegawai = t.NamaPegawai,
                                Uraian = d.Uraian,
                                Tempat = d.Tempat,
                                Penyelenggara = d.Penyelenggara,
                                TglAwal = d.TglAwal,
                                TglAkhir = d.TglAkhir,
                                Tahun = d.Tahun,
                                NoSertifikat = d.NoSertifikat,
                            })
                        ).OrderBy(d => d.NamaPegawai)
                    );
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.DiklatPelatihanList.Any();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
