using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using Dapper;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using SiPintar.Helpers;
using SiPintar.Helpers.BacameterV4;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    [ExcludeFromCodeCoverage]
    public class OnDownloadBacameterCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnDownloadBacameterCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            CloseDialog();

            ShowLoading();

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterGolongan",
                "MasterDiameter",
                "MasterAdministrasiLain",
                "MasterPemeliharaanLain",
                "MasterRetribusiLain",
                "MasterStatus",
                "MasterPeriode",
            });

            _viewModel.MasterGolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.MasterDiameterList = MasterListGlobal.MasterDiameter;
            _viewModel.MasterAdministrasiLainList = MasterListGlobal.MasterAdministrasiLain;
            _viewModel.MasterPemeliharaanLainList = MasterListGlobal.MasterPemeliharaanLain;
            _viewModel.MasterRetribusiLainList = MasterListGlobal.MasterRetribusiLain;
            _viewModel.MasterStatusList = MasterListGlobal.MasterStatus;
            _viewModel.MasterPeriodeList = MasterListGlobal.MasterPeriode?.OrderByDescending(c => c.KodePeriode).ToList();

            await ProcessAsync();

            _viewModel.OnFilterCommand.Execute(null);

            AppDispatcherHelper.Run(_isTest, () =>
            {
                DialogHost.Close("BillingRootDialog");
            });

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ProcessAsync()
        {
            if (!_isTest)
            {
                var host = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "bacameter_host")?.Value ?? "localhost";
                var port = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "bacameter_port")?.Value ?? "3309";
                var password = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "bacameter_pass")?.Value ?? "admin";
                var userId = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "bacameter_user")?.Value ?? "root";
                var database = MasterListGlobal.Pengaturan.FirstOrDefault(i => i.Key == "bacameter_db")?.Value ?? "bacameter";

                await using var bacameter = new MySqlConnection(new MySqlConnectionStringBuilder
                {
                    Server = host,
                    Port = uint.Parse(port),
                    Password = password,
                    UserID = userId,
                    Database = database,
                    ConnectionTimeout = 240

                }.ConnectionString);

                var listRekening = _viewModel.RekeningAirList.Where(c => c.FlagPublishWpf == false).ToList();
                var periode = string.Concat(_viewModel.SelectedDataPeriode.KodePeriode.ToString().Right(2), _viewModel.SelectedDataPeriode.KodePeriode.ToString().Mid(2, 2));
                var table = $"hasilbaca{periode}";

                var pageSize = 1000;
                int currentPage = 1;
                var total = listRekening.Count;

                await bacameter.CloseAsync();
                await bacameter.OpenAsync();

                while (true)
                {
                    try
                    {
                        var listData = new List<ParamDownloadHasilBacaListDataDto>();

                        var listNosamb = listRekening.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();

                        if (listNosamb.Count > 0)
                        {
                            Console.WriteLine($"Proses => {currentPage} [ {listNosamb.Count} ]");

                            if (bacameter.State == ConnectionState.Closed) await bacameter.OpenAsync();

                            var data = await bacameter.QueryAsync<HasilBaca>($"select idpelanggan,stanskrg,stanlalu,stanangkat,pakaiskrg," +
                                                 $"kelainan,waktubaca,IF(sudahbaca='1',1,0) as flagbaca,kodepetugas as petugasbaca," +
                                                 $"IF(flagkoreksi='1',1,0) as taksasi,waktuverifikasi,IF(taksir='1',1,0) as taksir,latitude," +
                                                 $"longitude,IF(adafotorumah='1',1,0) as adafotorumah,IF(adavideo='1',1,0) as adavideo,memolapangan as memo from {table} " +
                                                 $"WHERE verifikasi='1' AND idpelanggan IN @nosamb", new { nosamb = listNosamb.Select(c => c.NoSamb) });

                            if (data.Any())
                            {
                                var tempRekening = listNosamb.Where(c => data.Select(n => n.IdPelanggan).Contains(c.NoSamb));


                                foreach (var i in tempRekening)
                                {
                                    var meterai = _viewModel.MasterMeteraiList.Where(c => c.KodePeriodeMulaiBerlaku <= i.KodePeriode).OrderByDescending(c => c.KodePeriodeMulaiBerlaku).FirstOrDefault();
                                    var golongan = _viewModel.MasterGolonganList.FirstOrDefault(c => c.IdGolongan == i.IdGolongan);
                                    var diameter = _viewModel.MasterDiameterList.FirstOrDefault(c => c.IdDiameter == i.IdDiameter);
                                    var administrasiLain = _viewModel.MasterAdministrasiLainList.FirstOrDefault(c => c.IdAdministrasiLain == i.IdAdministrasiLain);
                                    var pemeliharaanLain = _viewModel.MasterPemeliharaanLainList.FirstOrDefault(c => c.IdPemeliharaanLain == i.IdPemeliharaanLain);
                                    var retribusiLain = _viewModel.MasterRetribusiLainList.FirstOrDefault(c => c.IdRetribusiLain == i.IdRetribusiLain);
                                    var status = _viewModel.MasterStatusList.FirstOrDefault(c => c.IdStatus == i.IdStatus);

                                    var hasilBaca = data.FirstOrDefault(c => c.IdPelanggan == i.NoSamb);

                                    if (hasilBaca != null)
                                    {
                                        var resultKalkulasi = KalkulasiRekeningAirHelper.Hitung(
                                                hasilBaca.PakaiSkrg ?? 0,
                                                golongan,
                                                diameter,
                                                administrasiLain,
                                                pemeliharaanLain,
                                                retribusiLain,
                                                meterai,
                                                status,
                                                i.IdFlag,
                                                i.TglMulaiDenda1,
                                                i.TglMulaiDenda2,
                                                i.TglMulaiDenda3,
                                                i.TglMulaiDenda4,
                                                i.TglMulaiDendaPerBulan);

                                        listData.Add(new ParamDownloadHasilBacaListDataDto
                                        {
                                            IdPelangganAir = i.IdPelangganAir,
                                            StanSkrg = hasilBaca.StanSkrg,
                                            StanLalu = hasilBaca.StanLalu,
                                            StanAngkat = hasilBaca.StanAngkat,
                                            Pakai = resultKalkulasi.Pakai,
                                            PakaiKalkulasi = resultKalkulasi.PakaiKalkulasi,
                                            BiayaPemakaian = resultKalkulasi.BiayaPemakaian,
                                            Administrasi = resultKalkulasi.Administrasi,
                                            Pemeliharaan = resultKalkulasi.Pemeliharaan,
                                            Retribusi = resultKalkulasi.Retribusi,
                                            Pelayanan = resultKalkulasi.Pelayanan,
                                            AirLimbah = resultKalkulasi.AirLimbah,
                                            DendaPakai0 = resultKalkulasi.DendaPakai0,
                                            AdministrasiLain = resultKalkulasi.AdministrasiLain,
                                            PemeliharaanLain = resultKalkulasi.PemeliharaanLain,
                                            RetribusiLain = resultKalkulasi.RetribusiLain,
                                            Ppn = resultKalkulasi.Ppn,
                                            Meterai = resultKalkulasi.Meterai,
                                            Rekair = resultKalkulasi.Rekair,
                                            Denda = resultKalkulasi.Denda,
                                            Total = resultKalkulasi.Total,
                                            Taksir = hasilBaca.Taksir,
                                            Taksasi = hasilBaca.Taksasi,
                                            FlagBaca = true,
                                            PetugasBaca = hasilBaca.PetugasBaca,
                                            Kelainan = hasilBaca.Kelainan,
                                            WaktuBaca = hasilBaca.WaktuBaca,
                                            FlagKoreksi = true,
                                            WaktuKoreksi = DateTime.Now,
                                            FlagVerifikasi = true,
                                            WaktuVerifikasi = hasilBaca.WaktuVerifikasi,
                                            Blok1 = resultKalkulasi.Blok1,
                                            Blok2 = resultKalkulasi.Blok2,
                                            Blok3 = resultKalkulasi.Blok3,
                                            Blok4 = resultKalkulasi.Blok4,
                                            Blok5 = resultKalkulasi.Blok5,
                                            Prog1 = resultKalkulasi.Prog1,
                                            Prog2 = resultKalkulasi.Prog2,
                                            Prog3 = resultKalkulasi.Prog3,
                                            Prog4 = resultKalkulasi.Prog4,
                                            Prog5 = resultKalkulasi.Prog5,
                                            Latitude = hasilBaca.Latitude,
                                            Longitude = hasilBaca.Longitude,
                                            AdaFotoRumah = hasilBaca.AdaFotoRumah,
                                            AdaVideo = hasilBaca.AdaVideo,
                                            Memo = hasilBaca.Memo
                                        });
                                    }
                                }

                                if (listData.Count > 0)
                                {
                                    #region API
                                    var param = new Dictionary<string, dynamic> { { "IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode }, { "LangsungPublish", _viewModel.IsLangsungPublish }, { "ListData", listData }, };

                                    var response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/download-hasil-baca", param);
                                    if (!response.IsError)
                                    {
                                        var result = response.Data;
                                        if (result.Status)
                                        {
                                            currentPage++;
                                            DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, moduleName: "billing");
                                        }
                                        else
                                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, moduleName: "billing");
                                    }
                                    else
                                        DialogHelper.ShowSnackbar(_isTest, "success", response.Error.Message, moduleName: "billing");
                                    #endregion
                                }
                                else
                                {
                                    currentPage++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Download Bacameter ==> {e.Message}");
                        DialogHelper.ShowSnackbar(_isTest, "danger", e.Message, moduleName: "billing");
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(LoadingDialog);
        }

        [ExcludeFromCodeCoverage]
        private void LoadingDialog()
        {
            double EstimationTimeInSecond = 60;
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, "sekitar 3-5 menit tergantung koneksi", false, false, EstimationTimeInSecond), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (!_isTest)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
