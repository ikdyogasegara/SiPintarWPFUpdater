using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Verifikasi
{
    public class OnSubmitVerifikasiCommand : AsyncCommandBase
    {
        private readonly VerifikasiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitVerifikasiCommand(VerifikasiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog");
            _viewModel.IsLoadingForm = true;
            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var body = new Dictionary<string, dynamic>
            {
                { "IdTipePermohonan", _viewModel.SelectedData.IdTipePermohonan },
                { "IdPermohonan", _viewModel.SelectedData.IdPermohonan },
                { "JenisPelanggan", _viewModel.JenisPelanggan },
            };

            var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.EndPointVerifikasi}", null, body);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    await UpdateRecordAsync(selectedData);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "hublang");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "hublang");

            selectedData.IsUpdatingKoreksi = false;
            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private async Task UpdateRecordAsync(PermohonanWpf selectedData)
        {
            if (!_isTest)
            {
                var temp = _viewModel.JenisList.FirstOrDefault(c => c.IdTipePermohonan == selectedData.IdTipePermohonan);

                switch (_viewModel.JenisPelanggan)
                {
                    case "Pelanggan Air":
                        temp.JumlahPelangganAirWpf = temp.JumlahPelangganAirWpf - 1;
                        temp.JumlahWpf = temp.JumlahWpf - 1;
                        break;
                    case "Pelanggan Limbah":
                        temp.JumlahPelangganLimbahWpf = temp.JumlahPelangganLimbahWpf - 1;
                        temp.JumlahWpf = temp.JumlahWpf - 1;
                        break;
                    case "Pelanggan L2T2":
                        temp.JumlahPelangganLlttWpf = temp.JumlahPelangganLlttWpf - 1;
                        temp.JumlahWpf = temp.JumlahWpf - 1;
                        break;
                    case "Non Pelanggan":
                        temp.JumlahNonPelangganWpf = temp.JumlahNonPelangganWpf - 1;
                        temp.JumlahWpf = temp.JumlahWpf - 1;
                        break;
                }

                selectedData.IsUpdatingKoreksi = false;
                DialogHelper.ShowSnackbar(_isTest, "success", "Verifikasi Sukses", "hublang");
                await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null);
            }
        }
    }
}
