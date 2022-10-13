using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.SimulasiTarif
{
    public class OnHitungCommand : AsyncCommandBase
    {
        private readonly SimulasiTarifViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnHitungCommand(SimulasiTarifViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "HublangRootDialog", "Mohon tunggu", "sedang menghitung data...");

            _viewModel.MeterTerpakai = "-";
            _viewModel.BiayaPemakaian = "-";
            _viewModel.BiayaPelayanan = "-";
            _viewModel.Adminstrasi = "-";
            _viewModel.Pemeliharaan = "-";
            _viewModel.Retribusi = "-";
            _viewModel.AdminstrasiLain = "-";
            _viewModel.PemeliharaanLain = "-";
            _viewModel.RetribusiLain = "-";
            _viewModel.RetribusiPakai = "-";
            _viewModel.BiayaAirLimbah = "-";
            _viewModel.Meterai = "-";
            _viewModel.Ppn = "-";
            _viewModel.Tagihan = "-";

            var param = parameter as Dictionary<string, dynamic>;
            double totalPakai = param["Pakai"];
            if (totalPakai < 0)
            {
                DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog", dg);
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "HublangRootDialog",
                    "Simulasi Tarif",
                    "Stan kini harus lebih besar atau sama dengan dari stan lalu",
                    "warning",
                    "OK",
                    false,
                    "hublang");
            }
            else
            {
                var paramSend = new Dictionary<string, dynamic>
                {
                    {"IdGolongan", _viewModel.PilihGolongan.IdGolongan},
                    {"IdDiameter", _viewModel.PilihDiameter.IdDiameter},
                    {"Simulasi", true},
                    {"Pakai", totalPakai}
                };

                var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-kalkulasi", paramSend));
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status && result.Data != null)
                    {
                        var temp = result.Data.ToObject<ObservableCollection<ResultKalkulasiRekeningAirDto>>();
                        if (temp.Count > 0)
                        {
                            _viewModel.MeterTerpakai = $"{DecimalValidationHelper.FormatDecimalToStringId(temp[0].Pakai)} m3";
                            _viewModel.BiayaPemakaian = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].BiayaPemakaian)}";
                            _viewModel.BiayaPelayanan = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Pelayanan)}";
                            _viewModel.Adminstrasi = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Administrasi)}";
                            _viewModel.Pemeliharaan = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Pemeliharaan)}";
                            _viewModel.Retribusi = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Retribusi)}";
                            _viewModel.AdminstrasiLain = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].AdministrasiLain)}";
                            _viewModel.PemeliharaanLain = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].PemeliharaanLain)}";
                            _viewModel.RetribusiLain = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].RetribusiLain)}";
                            _viewModel.BiayaAirLimbah = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].AirLimbah)}";
                            _viewModel.Meterai = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Meterai)}";
                            _viewModel.Ppn = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Ppn)}";
                            _viewModel.Tagihan = $"Rp {DecimalValidationHelper.FormatDecimalToStringId(temp[0].Rekair)}";
                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message);

                DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog", dg);
            }
        }
    }
}
