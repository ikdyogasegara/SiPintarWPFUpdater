using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnCalculationCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnCalculationCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesKoreksi == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            var isHitungUlangSemua = _viewModel.IsHitungUlangSemua;

            if (_viewModel.RekeningAirList == null || _viewModel.SelectedDataPeriode == null)
                return;

            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;
            object dg = null;
            IEnumerable<int?> idPelanggan;
            if (!isHitungUlangSemua)
                idPelanggan = _viewModel.RekeningAirList.Where(x => x.IdPelangganAir == _viewModel.SelectedData.IdPelangganAir).Select(x => x.IdPelangganAir);
            else
            {
                dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "BillingRootDialog",
                    "Mohon tunggu", "Data Rekening", "sedang diperbarui ...");

                idPelanggan = _viewModel.RekeningAirList.Where(c => c.FlagPublish == false).Select(x => x.IdPelangganAir);
            }

            var selectedPeriode = _viewModel.SelectedDataPeriode;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPeriode", selectedPeriode.IdPeriode},
                { "IdPelangganAir", idPelanggan},
                { "WaktuKoreksi", DateTime.Now}
            };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-hitung-ulang", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (result.Data != null && !isHitungUlangSemua)
                    {
                        var updateData = result.Data.ToObject<ObservableCollection<RekeningAirDto>>();
                        if (updateData?.Count == 1)
                        {
                            selectedData.PakaiWpf = updateData[0].Pakai;
                            selectedData.PakaiKalkulasiWpf = updateData[0].PakaiKalkulasi;
                            selectedData.BiayaPemakaianWpf = updateData[0].BiayaPemakaian;
                            selectedData.AdministrasiWpf = updateData[0].Administrasi;
                            selectedData.PemeliharaanWpf = updateData[0].Pemeliharaan;
                            selectedData.RetribusiWpf = updateData[0].Retribusi;
                            selectedData.PelayananWpf = updateData[0].Pelayanan;
                            selectedData.AirLimbahWpf = updateData[0].AirLimbah;
                            selectedData.DendaPakai0Wpf = updateData[0].DendaPakai0;
                            selectedData.AdministrasiLainWpf = updateData[0].DendaPakai0;
                            selectedData.PemeliharaanLainWpf = updateData[0].PemeliharaanLain;
                            selectedData.RetribusiLainWpf = updateData[0].RetribusiLain;
                            selectedData.PpnWpf = updateData[0].Ppn;
                            selectedData.MeteraiWpf = updateData[0].Meterai;
                            selectedData.RekairWpf = updateData[0].Rekair;
                            selectedData.TotalWpf = updateData[0].Total;
                            selectedData.DendaWpf = updateData[0].Denda;
                            selectedData.WaktuKoreksiWpf = updateData[0].WaktuKoreksi;
                        }
                    }

                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
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

            selectedData.IsUpdatingKoreksi = false;

            if (isHitungUlangSemua)
                DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog", dg);
        }
    }
}

