
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.DetailTagihan
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly ViewModels.Loket.Tagihan.TagihanKolektifAir.DetailTagihanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private RincianTagihanDetail _rincian;

        public OnRefreshCommand(ViewModels.Loket.Tagihan.TagihanKolektifAir.DetailTagihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Tipe == "ByIdPelangganAir" && _viewModel.Parent.ListSelectedPelangganAir == null)
                return;

            if (_viewModel.Tipe == "ByIdNonAir" && _viewModel.Parent.ListSelectedNonAir == null)
                return;

            _viewModel.ParentPage.IsNamaRoleAdmin = AppSetting.IsNamaRoleAdmin;
            var tanggal = parameter is DateTime ? (DateTime)parameter : default;
            var tanggalParent = _viewModel.ParentPage.TanggalTransaksi.Year == DateTime.Now.Year ? _viewModel.ParentPage.TanggalTransaksi : DateTime.Now;
            _viewModel.ParentPage.TanggalTransaksi = tanggal.Year == DateTime.Now.Year ? tanggal : tanggalParent;
            _viewModel.DataGroup?.Remove(_viewModel.Tagihan);
            _viewModel.DataGroup?.Refresh();

            _viewModel.ShowSideInfo = false;
            _viewModel.Tagihan = new List<TagihanGlobalWpf>();
            _viewModel.ParentPage.IsFullPage = true;
            _viewModel.IsLoading = true;
            _viewModel.ParentPage.IsLoketTutupToday = AppSetting.LoketTutup;

            _rincian = new RincianTagihanDetail();

            var tagihan = new PaymentDto();

            switch (_viewModel.Tipe)
            {
                case "ByIdPelangganAir":
                    tagihan = await GetDataPaymentByIdPelangganAirAsync();
                    break;
                case "ByIdNonAir":
                    tagihan = await GetDataPaymentByIdNonAirAsync();
                    break;
            }

            if (tagihan != null)
            {
                ExtractData(tagihan);
            }

            _viewModel.IsEmpty = _viewModel.IsEmptyTagihan;
            _viewModel.IsLihatKuitansiSebelumCetak = false;
            _viewModel.IsKolektifChecked = false;
            _viewModel.JumlahBayar = 0;
            _viewModel.IsAllowedToProcess = !_viewModel.IsEmpty;

            _viewModel.RincianDetail = _rincian;
            _viewModel.CheckUpdate();
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<PaymentDto> GetDataPaymentByIdPelangganAirAsync()
        {
            PaymentDto resultPayment = null;

            if (!_isTest)
            {
                var listIdPelangganAir = new StringBuilder();

                if (_viewModel.Parent.ListSelectedPelangganAir != null)
                {
                    var delimiter = "";

                    foreach (var item in _viewModel.Parent.ListSelectedPelangganAir)
                    {
                        listIdPelangganAir.Append(delimiter);
                        listIdPelangganAir.Append(item.IdPelangganAir);
                        delimiter = "-";
                    }
                }

                var param = new Dictionary<string, dynamic>
                {
                    {"ListIdPelangganAir", listIdPelangganAir.ToString()},
                    {"Tanggal", _viewModel.ParentPage.TanggalTransaksi.ToString("yyyy-MM-dd")}
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/payment-tagihan-by-id-pelanggan-air", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var temp = result.Data.ToObject<ObservableCollection<PaymentDto>>();
                        if (temp != null && temp.Count > 0)
                        {
                            resultPayment = temp[0];
                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "loket");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "loket");
            }

            return resultPayment;
        }

        [ExcludeFromCodeCoverage]
        private async Task<PaymentDto> GetDataPaymentByIdNonAirAsync()
        {
            PaymentDto resultPayment = null;

            if (!_isTest)
            {
                var listIdNonAir = new StringBuilder();

                if (_viewModel.Parent.ListSelectedNonAir != null)
                {
                    var delimiter = "";

                    foreach (var item in _viewModel.Parent.ListSelectedNonAir)
                    {
                        listIdNonAir.Append(delimiter);
                        listIdNonAir.Append(item.IdNonAir);
                        delimiter = "-";
                    }
                }

                var param = new Dictionary<string, dynamic>
                {
                    {"ListIdNonAir", listIdNonAir.ToString()},
                    {"Tanggal", _viewModel.ParentPage.TanggalTransaksi.ToString("yyyy-MM-dd")}
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/payment-tagihan-by-id-non-air", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var temp = result.Data.ToObject<ObservableCollection<PaymentDto>>();
                        if (temp != null && temp.Count > 0)
                        {
                            resultPayment = temp[0];
                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "loket");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "loket");
            }

            return resultPayment;
        }

        [ExcludeFromCodeCoverage]
        private void ExtractData(PaymentDto data)
        {
            if (!_isTest && data != null)
            {
                var finalResult = new List<TagihanGlobalWpf>();

                if (data.RekeningAir != null && data.RekeningAir.Count > 0)
                {
                    List<TagihanGlobalWpf> listResult;
                    var objectAir = JsonConvert.SerializeObject(data.RekeningAir);
                    listResult = JsonConvert.DeserializeObject<List<TagihanGlobalWpf>>(objectAir);
                    if (listResult != null)
                    {
                        var delimeter = "";

                        foreach (var i in listResult)
                        {
                            var builder = new StringBuilder();

                            builder.Append($"S.Lalu : {Convert.ToInt32(i.StanLalu)}");
                            delimeter = " | ";

                            if (i.StanAngkat > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"S.Angkat : {Convert.ToInt32(i.StanAngkat)}");
                            }

                            builder.Append(delimeter);
                            builder.Append($"S.Kini : {Convert.ToInt32(i.StanSkrg)}");
                            builder.Append(delimeter);
                            builder.Append($"Pakai : {Convert.ToInt32(i.Pakai)}");
                            builder.Append(delimeter);
                            builder.Append($"B.Pakai : {Convert.ToInt32(i.BiayaPemakaian)}");

                            if (i.Administrasi > 0 || i.AdministrasiLain > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Adm : {Convert.ToInt32(i.Administrasi + i.AdministrasiLain)}");
                            }

                            if (i.Pemeliharaan > 0 || i.PemeliharaanLain > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Pem : {Convert.ToInt32(i.Pemeliharaan + i.PemeliharaanLain)}");
                            }

                            if (i.Retribusi > 0 || i.RetribusiLain > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Ret : {Convert.ToInt32(i.Retribusi + i.RetribusiLain)}");
                            }

                            if (i.Pelayanan > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Pel : {Convert.ToInt32(i.Pelayanan)}");
                            }

                            if (i.AirLimbah > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"A.Limbah : {Convert.ToInt32(i.AirLimbah)}");
                            }

                            if (i.DendaPakai0 > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Denda Pk0 : {i.DendaPakai0}");
                            }

                            if (i.Ppn > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Ppn : {Convert.ToInt32(i.Ppn)}");
                            }

                            if (i.Meterai > 0)
                            {
                                builder.Append(delimeter);
                                builder.Append($"Materai : {Convert.ToInt32(i.Meterai)}");
                            }

                            if (!string.IsNullOrWhiteSpace(i.Kelainan))
                            {
                                builder.Append(delimeter);
                                builder.Append($"Kelainan : {i.Kelainan}");
                            }

                            i.JenisTagihan = "Air";
                            i.UraianWpf = builder.ToString();

                            finalResult.Add(i);
                        }
                    }
                }

                if (data.RekeningNonAir != null && data.RekeningNonAir.Count > 0)
                {
                    List<TagihanGlobalWpf> listResult;
                    var objectNonAir = JsonConvert.SerializeObject(data.RekeningNonAir);
                    listResult = JsonConvert.DeserializeObject<List<TagihanGlobalWpf>>(objectNonAir);
                    if (listResult != null)
                    {
                        var delimeter = "";

                        foreach (var i in listResult)
                        {
                            var builder = new StringBuilder();

                            builder.Append($"Jenis Non Air : {i.NamaJenisNonAir}");
                            delimeter = " | ";

                            if (!string.IsNullOrWhiteSpace(i.Keterangan))
                            {
                                builder.Append(delimeter);
                                builder.Append($"Keterangan : {i.Keterangan}");
                            }

                            i.JenisTagihan = "Non Air";
                            i.UraianWpf = builder.ToString();

                            finalResult.Add(i);
                        }
                    }
                }

                var result = finalResult.OrderBy(c => c.Nama).ThenBy(c => c.JenisTagihan).ThenBy(c => c.KodePeriode).ToList();
                _viewModel.Tagihan = result;
                ReloadData();
            }
        }

        private void ReloadData()
        {
            _viewModel.DataGroup = new ListCollectionView(_viewModel.Tagihan);
            using (_viewModel.DataGroup.DeferRefresh())
            {
                _viewModel.DataGroup.GroupDescriptions.Add(new PropertyGroupDescription("Nama"));
            }
        }
    }
}
