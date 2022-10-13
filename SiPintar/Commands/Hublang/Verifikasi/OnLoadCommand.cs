using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang;
using SiPintar.Views;

namespace SiPintar.Commands.Hublang.Verifikasi
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly VerifikasiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(VerifikasiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.IsLoadingJenis = true;
            _viewModel.IsEmpty = true;
            _viewModel.SelectedData = null;
            _viewModel.IsShowListData = false;
            _viewModel.DataList = null;
            _viewModel.JenisList = null;

            _viewModel.OnResetFilterCommand.Execute(null);

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/verifikasi-permohonan", null));
            _viewModel.JenisList = new ObservableCollection<ListPermohonanWpf>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.JenisList = result.Data.ToObject<ObservableCollection<ListPermohonanWpf>>();
                    _viewModel.IsLoading = false;
                    _viewModel.IsLoadingJenis = false;
                    _viewModel.IsEmptyJenis = !_viewModel.JenisList.Any();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");

            _viewModel.IsLoading = false;
            _viewModel.IsLoadingJenis = false;

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterGolongan",
                "MasterTarifLimbah",
                "MasterTarifLltt",
                "MasterRayon",
                "MasterArea",
                "MasterWilayah",
                "MasterKelurahan",
                "MasterKecamatan",
                "MasterCabang",
                "MasterUser",
            });

            _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.TarifLimbahList = MasterListGlobal.MasterTarifLimbah;
            _viewModel.TarifLlttList = MasterListGlobal.MasterTarifLltt;
            _viewModel.RayonList = MasterListGlobal.MasterRayon;
            _viewModel.AreaList = MasterListGlobal.MasterArea;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.KelurahanList = MasterListGlobal.MasterKelurahan;
            _viewModel.KecamatanList = MasterListGlobal.MasterKecamatan;
            _viewModel.CabangList = MasterListGlobal.MasterCabang;
            _viewModel.UserList = MasterListGlobal.MasterUser;

            await Task.FromResult(_isTest);
        }
    }
}
