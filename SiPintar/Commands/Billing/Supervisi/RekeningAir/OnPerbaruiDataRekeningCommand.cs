using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnPerbaruiDataRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnPerbaruiDataRekeningCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedData = new RekeningAirWpf();

            if (_viewModel.SelectedDataPeriode != null && _viewModel.SelectedDataPeriode.IdPeriode.HasValue && _viewModel.RekeningAirList?.Count > 0)
            {
                #region akses
                if (_viewModel.AksesPerbaruiData == false)
                {
                    DialogHelper.ShowInvalidAkses(_isTest);
                    return;
                }
                #endregion

                object dg = null;
                IEnumerable<int?> idPelanggan;

                if (!_viewModel.IsPerbaruiSemua)
                {
                    selectedData = _viewModel.SelectedData;
                    selectedData.IsUpdatingKoreksi = true;
                    idPelanggan = _viewModel.RekeningAirList.Where(x => x.IdPelangganAir == _viewModel.SelectedData.IdPelangganAir).Select(x => x.IdPelangganAir);
                }
                else
                {
                    dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "BillingRootDialog",
                        "Mohon tunggu", "Data Rekening", "sedang diperbarui ...");

                    idPelanggan = _viewModel.RekeningAirList.Where(c => c.FlagPublishWpf == false).Select(x => x.IdPelangganAir);
                }

                var param = new Dictionary<string, dynamic>
                {
                    {"IdPeriode" , _viewModel.SelectedDataPeriode.IdPeriode},
                    {"IdPelangganAir" , idPelanggan},
                };

                var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-perbarui-data", null, param));
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        if (!_viewModel.IsPerbaruiSemua)
                        {
                            if (result.Data != null)
                            {
                                var updateData = result.Data.ToObject<ObservableCollection<RekeningAirDto>>();
                                UpdareRecord(selectedData, updateData);
                                selectedData.IsUpdatingKoreksi = false;
                            }

                            DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");

                            await ((AsyncCommandBase)_viewModel.OnFilterCommand).ExecuteAsync(null);

                            if (dg != null)
                                DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog", dg);
                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");

                selectedData.IsUpdatingKoreksi = false;
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "BillingRootDialog", "Data Rekening",
                    "Tidak ada data!", "warning", "OK", moduleName: "billing");
            }
        }

        [ExcludeFromCodeCoverage]
        private void UpdareRecord(RekeningAirWpf selectedData, ObservableCollection<RekeningAirDto> updateData)
        {
            if (!_isTest)
            {
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
                }
            }
        }
    }
}
