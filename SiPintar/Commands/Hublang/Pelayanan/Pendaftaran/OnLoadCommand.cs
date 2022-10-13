using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PendaftaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var kodeTipePermohonan = parameter as string;

            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "HublangRootDialog", "Mohon tunggu", "sedang memuat data attribute...", "");

            _viewModel.DataList = new ObservableCollection<PermohonanNonPelangganWpf>();

            switch (kodeTipePermohonan)
            {
                case "SAMBUNGAN_BARU_AIR":
                    _viewModel.LabelJudul = "Pendaftaran Sambungan Baru Pelanggan Air";
                    break;
                case "SAMBUNGAN_BARU_LIMBAH":
                    _viewModel.LabelJudul = "Pendaftaran Sambungan Baru Pelanggan Limbah";
                    break;
                case "SAMBUNGAN_BARU_LLTT":
                    _viewModel.LabelJudul = "Pendaftaran Sambungan Baru Pelanggan L2T2";
                    break;
            }

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterTipePermohonan",
                "MasterCabang",
                "MasterWilayah",
                "MasterUser",
                "MasterPekerjaan",
                "MasterKelurahan",
                "MasterRayon",
                "MasterBlok",
                "MasterSumberAir",
                "MasterJenisBangunan",
                "MasterKepemilikan",
                "MasterPeruntukan",
                "MasterTipePendaftaranSambungan",
            });

            _viewModel.SelectedTipePermohonanComboBox = MasterListGlobal.MasterTipePermohonan.FirstOrDefault(c => c.Kategori == "Pendaftaran" && c.KodeTipePermohonan == kodeTipePermohonan);
            _viewModel.CabangList = MasterListGlobal.MasterCabang;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.UserList = MasterListGlobal.MasterUser;
            _viewModel.PekerjaanList = MasterListGlobal.MasterPekerjaan;
            _viewModel.KelurahanList = MasterListGlobal.MasterKelurahan;
            _viewModel.RayonList = MasterListGlobal.MasterRayon;
            _viewModel.BlokList = MasterListGlobal.MasterBlok;
            _viewModel.SumberAirList = MasterListGlobal.MasterSumberAir;
            _viewModel.JenisBangunanList = MasterListGlobal.MasterJenisBangunan;
            _viewModel.KepemilikanBangunanList = MasterListGlobal.MasterKepemilikan;
            _viewModel.PeruntukanList = MasterListGlobal.MasterPeruntukan;
            _viewModel.TipePendaftaranList = MasterListGlobal.MasterTipePendaftaranSambungan;

            await GetMapPelangganAsync(kodeTipePermohonan);

            DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog", dg);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetMapPelangganAsync(string kodeTipePermohonan)
        {
            if (!_isTest)
            {
                var param = new Dictionary<string, dynamic> { { "ForMap", true }, };

                var endPoint = "";

                switch (kodeTipePermohonan)
                {
                    case "SAMBUNGAN_BARU_AIR":
                        endPoint = "master-pelanggan-air";
                        break;
                    case "SAMBUNGAN_BARU_LIMBAH":
                        endPoint = "master-pelanggan-limbah";
                        break;
                    case "SAMBUNGAN_BARU_LLTT":
                        endPoint = "master-pelanggan-limbah";
                        break;
                }

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{endPoint}", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status && result.Data != null)
                    {
                        _viewModel.MapPelangganList = result.Data.ToObject<ObservableCollection<MasterMapPelangganDto>>();
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                }
            }
        }
    }
}
