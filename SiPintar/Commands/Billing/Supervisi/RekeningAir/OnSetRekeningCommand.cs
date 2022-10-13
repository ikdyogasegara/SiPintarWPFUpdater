using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnSetRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSetRekeningCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "BillingRootDialog",
                $"Set Rekening {(_viewModel.TanpaDenda ? "Tanpa Denda" : "Normal")}",
                "sedang memproses tindakan ...");

            var param = new
            {
                IdPelangganAir = _viewModel.SelectedData.IdPelangganAir,
                IdPeriode = _viewModel.SelectedDataPeriode.IdPeriode,
                IdRayon = _viewModel.SelectedData.IdRayon,
                IdKelurahan = _viewModel.SelectedData.IdKelurahan,
                IdKolektif = _viewModel.SelectedData.IdKolektif,
                IdFlag = _viewModel.TanpaDenda ? 2 : 1,
                IdStatus = _viewModel.SelectedData.IdStatus,

            };
            var properties = param.GetType().GetProperties();
            var body = new Dictionary<string, dynamic>();

            foreach (var property in properties)
            {
                body.Add(property.Name, property.GetValue(param));
            }

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-update-non-administratif", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (result.Data != null)
                    {
                        var updateData = result.Data.ToObject<ObservableCollection<RekeningAirDto>>();
                        if (updateData?.Count > 0)
                        {
                            DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                            _viewModel.SelectedData.IdFlag = updateData[0].IdFlag;
                            _viewModel.SelectedData.NamaFlag = updateData[0].NamaFlag;
                            _viewModel.SelectedData.Pakai = updateData[0].Pakai;
                            _viewModel.SelectedData.PakaiKalkulasi = updateData[0].PakaiKalkulasi;
                            _viewModel.SelectedData.BiayaPemakaian = updateData[0].BiayaPemakaian;
                            _viewModel.SelectedData.Administrasi = updateData[0].Administrasi;
                            _viewModel.SelectedData.Pemeliharaan = updateData[0].Pemeliharaan;
                            _viewModel.SelectedData.Pelayanan = updateData[0].Pelayanan;
                            _viewModel.SelectedData.Retribusi = updateData[0].Retribusi;
                            _viewModel.SelectedData.AirLimbah = updateData[0].AirLimbah;
                            _viewModel.SelectedData.DendaPakai0 = updateData[0].DendaPakai0;
                            _viewModel.SelectedData.AdministrasiLain = updateData[0].AdministrasiLain;
                            _viewModel.SelectedData.PemeliharaanLain = updateData[0].PemeliharaanLain;
                            _viewModel.SelectedData.RetribusiLain = updateData[0].RetribusiLain;
                            _viewModel.SelectedData.Ppn = updateData[0].Ppn;
                            _viewModel.SelectedData.Meterai = updateData[0].Meterai;
                            _viewModel.SelectedData.Rekair = updateData[0].Rekair;
                            _viewModel.SelectedData.Denda = updateData[0].Denda;
                            _viewModel.SelectedData.Total = updateData[0].Total;
                            _viewModel.SelectedData.FlagMinimumPakai = updateData[0].FlagMinimumPakai;

                            _viewModel.InvokeUpdateDataGrid();
                        }
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }
            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog", dg);
        }
    }
}
