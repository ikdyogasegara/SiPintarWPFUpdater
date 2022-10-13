using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Langganan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(LanggananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            var tipe = parameter as string; // for detail or edit
            _viewModel.IsEdit = tipe == "edit";
            _viewModel.IsDetail = tipe == "detail";

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            _viewModel.IsLoadingForm = true;

            _viewModel.IsNewFotoRumah1 = false;
            _viewModel.IsNewFotoRumah2 = false;
            _viewModel.IsNewFotoRumah3 = false;

            _viewModel.FormData = JsonConvert.DeserializeObject<MasterPelangganGlobalForm>(JsonConvert.SerializeObject(_viewModel.SelectedData));

            await GetFotoAsync();

            _viewModel.IsLoadingForm = false;

            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);

            if (!_isTest)
            {
                switch (_viewModel.SelectedJenisPelanggan)
                {
                    case "Pelanggan Air":
                        await DialogHost.Show(new KoreksiPelangganAirFormView(_viewModel), "HublangRootDialog");
                        break;
                    case "Pelanggan Limbah":
                        await DialogHost.Show(new KoreksiPelangganLimbahFormView(_viewModel), "HublangRootDialog");
                        break;
                    case "Pelanggan L2T2":
                        await DialogHost.Show(new KoreksiPelangganLlttFormView(_viewModel), "HublangRootDialog");
                        break;
                }
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFotoAsync()
        {
            if (_viewModel.FormData.IdPelangganAir == null && _viewModel.FormData.IdPelangganLimbah == null && _viewModel.FormData.IdPelangganLltt == null)
                return;

            var detail = _viewModel.FormData;

            var param = new Dictionary<string, dynamic>();

            switch (_viewModel.SelectedJenisPelanggan)
            {
                case "Pelanggan Air":
                    param.Add("IdPelangganAir", _viewModel.SelectedData.IdPelangganAir);
                    break;
                case "Pelanggan Limbah":
                    param.Add("IdPelangganLimbah", _viewModel.SelectedData.IdPelangganLimbah);
                    break;
                case "Pelanggan L2T2":
                    param.Add("IdPelangganLltt", _viewModel.SelectedData.IdPelangganLltt);
                    break;
            }

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPoint}-link-foto", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var data = result.Data.ToObject<ObservableCollection<MasterPelangganGlobalDto>>().FirstOrDefault();

                    if (data != null)
                    {
                        detail.FotoRumah1 = data.FotoRumah1;
                        detail.FotoRumah2 = data.FotoRumah2;
                        detail.FotoRumah3 = data.FotoRumah3;

                        _viewModel.FormData = detail;

                        _viewModel.FotoRumah1Uri = await ImageUriHelper.SetUriAsync(detail.FotoRumah1);
                        _viewModel.FotoRumah2Uri = await ImageUriHelper.SetUriAsync(detail.FotoRumah2);
                        _viewModel.FotoRumah3Uri = await ImageUriHelper.SetUriAsync(detail.FotoRumah3);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
            }
        }
    }
}
