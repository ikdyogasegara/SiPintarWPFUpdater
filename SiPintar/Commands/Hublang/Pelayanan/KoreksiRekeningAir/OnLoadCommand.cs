using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly KoreksiRekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(KoreksiRekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var jenisKoreksi = parameter as string;

            await GetJenisPermohonanKoreksiRekeningAsync(jenisKoreksi);

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterGolongan",
                "MasterDiameter",
                "MasterAdministrasiLain",
                "MasterPemeliharaanLain",
                "MasterRetribusiLain",
                "MasterRayon",
                "MasterWilayah",
                "MasterKelurahan",
                "MasterCabang",
                "MasterUser",
                "MasterStatus",
                "MasterMeterai",
            });

            _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.DiameterList = MasterListGlobal.MasterDiameter;
            _viewModel.AdministrasiLainList = MasterListGlobal.MasterAdministrasiLain;
            _viewModel.PemeliharaanLainList = MasterListGlobal.MasterPemeliharaanLain;
            _viewModel.RetribusiLainList = MasterListGlobal.MasterRetribusiLain;
            _viewModel.RayonList = MasterListGlobal.MasterRayon;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.KelurahanList = MasterListGlobal.MasterKelurahan;
            _viewModel.CabangList = MasterListGlobal.MasterCabang;
            _viewModel.UserList = MasterListGlobal.MasterUser;
            _viewModel.StatusList = MasterListGlobal.MasterStatus;
            _viewModel.MeteraiList = MasterListGlobal.MasterMeterai;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetJenisPermohonanKoreksiRekeningAsync(string jenisKoreksi)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "KodeTipePermohonan" , "KREKAIR" },
            };

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tipe-permohonan", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var temp = result.Data.ToObject<ObservableCollection<MasterTipePermohonanDto>>();
                    string[] includeTipe = { "KREKAIR" };

                    var data = new ObservableCollection<MasterTipePermohonanDto>();
                    foreach (var item in temp)
                    {
                        if (Array.IndexOf(includeTipe, item.KodeTipePermohonan) >= 0)
                            data.Add(item);
                    }

                    _viewModel.TipeJenisKoreksiAir = data.Count > 0 ? data[^1] : null;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsEmpty = _viewModel.TipeJenisKoreksiAir == null;

            if (!_viewModel.IsEmpty)
            {
                _viewModel.JenisKoreksi = jenisKoreksi;
            }
        }
    }
}
