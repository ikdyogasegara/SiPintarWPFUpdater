using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnSubmitUbahPenanggungBebanCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitUbahPenanggungBebanCommand(DetailAngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog");

                var param = new Dictionary<string, dynamic>
                {
                    { "idAngsuran", _viewModel.Parent._nonair.SelectedData.IdAngsuran},
                    { "idPelangganAir", _viewModel.PelangganSelected.IdPelangganAir},
                    { "idPelangganLimbah", 0},
                    { "idPelangganLltt", 0},
                    { "nama", _viewModel.PelangganSelected.Nama},
                    { "alamat", _viewModel.PelangganSelected.Alamat},
                    { "noTelp", _viewModel.Parent.NoTelpForm},
                    { "noHp", _viewModel.Parent.NoHpForm},
                    { "idRayon", _viewModel.PelangganSelected.IdRayon},
                    { "idKelurahan", _viewModel.PelangganSelected.IdKelurahan},
                    { "idGolongan", _viewModel.PelangganSelected.IdGolongan},
                    { "idDiameter", _viewModel.PelangganSelected.IdDiameter},
                };

                var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air-update-penanggung-beban", param));

                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", "Ganti Penanggung Beban Berhasil", "loket");
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "loket");
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "loket");
                }

            }
            catch (Exception) { throw; }

            _ = ((AsyncCommandBase)_viewModel.OnLoadCommandAngsuran).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
