using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var dictionary = parameter as Dictionary<string, dynamic>;
            _viewModel.FiturName = dictionary["FiturName"];
            _viewModel.SelectedJenisPelanggan = dictionary["SelectedJenisPelanggan"];
            _viewModel.TipePermohonan = null;
            _viewModel.DataList = null;

            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, _viewModel.Identfire, "Mohon tunggu", "sedang memuat data attribute...", "");

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize", 1000000 }
            };

            if (_viewModel.IsPelangganAir)
            {
                param.Add("FlagPelangganAir", true);
            }
            if (_viewModel.IsPelangganLimbah)
            {
                param.Add("FlagPelangganLimbah", true);
            }
            if (_viewModel.IsPelangganLltt)
            {
                param.Add("FlagPelangganLltt", true);
            }
            if (_viewModel.IsNonPelanggan)
            {
                param.Add("FlagNonPelanggan", true);
            }
            if (_viewModel.FiturName != "Berita Acara" && _viewModel.FiturName != "Berita Acara View Only" && _viewModel.FiturName != "SPK" && _viewModel.FiturName != "RAB" && _viewModel.FiturName != "Rotasi Meter")
            {
                param.Add("Kategori", _viewModel.FiturName);
            }
            if (_viewModel.FiturName == "SPK")
            {
                param.Add("StepSpk", true);
            }
            if (_viewModel.FiturName == "RAB")
            {
                param.Add("StepRab", true);
            }
            if (_viewModel.FiturName == "Berita Acara")
            {
                param.Add("StepBeritaAcara", true);
            }
            if (_viewModel.FiturName == "Berita Acara View Only")
            {
                param.Add("StepBeritaAcara", true);
            }
            if (_viewModel.FiturName == "Rotasi Meter")
            {
                param.Add("KodeTipePermohonan", "GANTI_METER");
            }

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-tipe-permohonan", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var temp = result.Data.ToObject<ObservableCollection<MasterTipePermohonanDto>>();

                    _viewModel.TipePermohonan = new ObservableCollection<MasterTipePermohonanDto>()
                    {
                        new MasterTipePermohonanDto()
                        {
                            NamaTipePermohonan = "Semua",
                            IdTipePermohonan = null
                        }
                    };

                    foreach (var item in temp)
                    {
                        _viewModel.TipePermohonan.Add(item);
                    }

                    if (_viewModel.IsBeritaAcara)
                    {
                        var deleteList = _viewModel.TipePermohonan.FirstOrDefault(c => c.KodeTipePermohonan == "KREKAIR");
                        _viewModel.TipePermohonan.Remove(deleteList);
                    }

                    var obj = _viewModel.TipePermohonan.FirstOrDefault(x => x.NamaTipePermohonan.Trim().ToLower() == "air tangki");
                    if (obj != null)
                    {
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "tanggal pemesanan"), 0);
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "estimasi waktu pengiriman"), 1);
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "tarif air tangki"), 2);
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "jumlah m3"), 3);
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "jumlah armada tangki air"), 4);
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "jarak tujuan (km)"), 5);
                        MoveItem(obj.DetailParameter, obj.DetailParameter.Find(x => x.Parameter.ToLower().Trim() == "waktu penagihan"), 6);
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            DialogHelper.CloseDialog(_isTest, true, _viewModel.Identfire, dg);

            SetComboBox();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private static void MoveItem(IList<MasterTipePermohonanDetailDto> data, MasterTipePermohonanDetailDto target, int idx)
        {
            if (target != null)
            {
                var findIdx = data.IndexOf(target);
                data.RemoveAt(findIdx);
                data.Insert(idx, target);
            }
        }

        private void SetComboBox()
        {
            if (!_isTest)
            {
                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterGolongan",
                }).ContinueWith(result =>
                {
                    _viewModel.GolonganList = JArray.FromObject(MasterListGlobal.MasterGolongan).ToObject<ObservableCollection<MasterGolonganDto>>();
                    _viewModel.GolonganList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterDiameter",
                }).ContinueWith(result =>
                {
                    _viewModel.DiameterList = JArray.FromObject(MasterListGlobal.MasterDiameter).ToObject<ObservableCollection<MasterDiameterDto>>();
                    _viewModel.DiameterList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterTarifLimbah",
                }).ContinueWith(result =>
                {
                    _viewModel.TarifLimbahList = JArray.FromObject(MasterListGlobal.MasterTarifLimbah).ToObject<ObservableCollection<MasterTarifLimbahDto>>();
                    _viewModel.TarifLimbahList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterTarifLltt",
                }).ContinueWith(result =>
                {
                    _viewModel.TarifLlttList = JArray.FromObject(MasterListGlobal.MasterTarifLltt).ToObject<ObservableCollection<MasterTarifLlttDto>>();
                    _viewModel.TarifLlttList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKelurahan",
                }).ContinueWith(result =>
                {
                    _viewModel.KelurahanList = JArray.FromObject(MasterListGlobal.MasterKelurahan).ToObject<ObservableCollection<MasterKelurahanDto>>();
                    _viewModel.KelurahanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterCabang",
                }).ContinueWith(result =>
                {
                    _viewModel.CabangList = JArray.FromObject(MasterListGlobal.MasterCabang).ToObject<ObservableCollection<MasterCabangDto>>();
                    _viewModel.CabangList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKecamatan",
                }).ContinueWith(result =>
                {
                    _viewModel.KecamatanList = JArray.FromObject(MasterListGlobal.MasterKecamatan).ToObject<ObservableCollection<MasterKecamatanDto>>();
                    _viewModel.KecamatanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterRayon",
                }).ContinueWith(result =>
                {
                    _viewModel.RayonList = JArray.FromObject(MasterListGlobal.MasterRayon).ToObject<ObservableCollection<MasterRayonDto>>();
                    _viewModel.RayonList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterArea",
                }).ContinueWith(result =>
                {
                    _viewModel.AreaList = JArray.FromObject(MasterListGlobal.MasterArea).ToObject<ObservableCollection<MasterAreaDto>>();
                    _viewModel.AreaList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterWilayah",
                }).ContinueWith(result =>
                {
                    _viewModel.WilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahDto>>();
                    _viewModel.WilayahList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterAdministrasiLain",
                }).ContinueWith(result =>
                {
                    _viewModel.AdministrasiLainList = JArray.FromObject(MasterListGlobal.MasterAdministrasiLain).ToObject<ObservableCollection<MasterAdministrasiLainDto>>();
                    _viewModel.AdministrasiLainList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterPemeliharaanLain",
                }).ContinueWith(result =>
                {
                    _viewModel.PemeliharaanLainList = JArray.FromObject(MasterListGlobal.MasterPemeliharaanLain).ToObject<ObservableCollection<MasterPemeliharaanLainDto>>();
                    _viewModel.PemeliharaanLainList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterRetribusiLain",
                }).ContinueWith(result =>
                {
                    _viewModel.RetribusiLainList = JArray.FromObject(MasterListGlobal.MasterRetribusiLain).ToObject<ObservableCollection<MasterRetribusiLainDto>>();
                    _viewModel.RetribusiLainList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterBlok",
                }).ContinueWith(result =>
                {
                    _viewModel.BlokList = JArray.FromObject(MasterListGlobal.MasterBlok).ToObject<ObservableCollection<MasterBlokDto>>();
                    _viewModel.BlokList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterMerekMeter",
                }).ContinueWith(result =>
                {
                    _viewModel.MerekMeterList = JArray.FromObject(MasterListGlobal.MasterMerekMeter).ToObject<ObservableCollection<MasterMerekMeterDto>>();
                    _viewModel.MerekMeterList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterSumberAir",
                }).ContinueWith(result =>
                {
                    _viewModel.SumberAirList = JArray.FromObject(MasterListGlobal.MasterSumberAir).ToObject<ObservableCollection<MasterSumberAirDto>>();
                    _viewModel.SumberAirList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterDma",
                }).ContinueWith(result =>
                {
                    _viewModel.DmaList = JArray.FromObject(MasterListGlobal.MasterDma).ToObject<ObservableCollection<MasterDmaDto>>();
                    _viewModel.DmaList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterDmz",
                }).ContinueWith(result =>
                {
                    _viewModel.DmzList = JArray.FromObject(MasterListGlobal.MasterDmz).ToObject<ObservableCollection<MasterDmzDto>>();
                    _viewModel.DmzList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterUser",
                }).ContinueWith(result =>
                {
                    _viewModel.UserList = JArray.FromObject(MasterListGlobal.MasterUser).ToObject<ObservableCollection<MasterUserDto>>();
                    _viewModel.UserList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterPegawai",
                }).ContinueWith(result =>
                {
                    _viewModel.PegawaiList = JArray.FromObject(MasterListGlobal.MasterPegawai).ToObject<ObservableCollection<MasterPegawaiDto>>();
                    _viewModel.PegawaiList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterAlasanBatal",
                }).ContinueWith(result =>
                {
                    _viewModel.AlasanBatalList = JArray.FromObject(MasterListGlobal.MasterAlasanBatal).ToObject<ObservableCollection<MasterAlasanBatalDto>>();
                    _viewModel.AlasanBatalList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterPaketRab",
                }).ContinueWith(result =>
                {
                    _viewModel.PaketRabList = JArray.FromObject(MasterListGlobal.MasterPaketRab).ToObject<ObservableCollection<MasterPaketDto>>();
                    _viewModel.PaketRabList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterPekerjaan",
                }).ContinueWith(result =>
                {
                    _viewModel.PekerjaanList = JArray.FromObject(MasterListGlobal.MasterPekerjaan).ToObject<ObservableCollection<MasterPekerjaanDto>>();
                    _viewModel.PekerjaanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterJenisBangunan",
                }).ContinueWith(result =>
                {
                    _viewModel.JenisBangunanList = JArray.FromObject(MasterListGlobal.MasterJenisBangunan).ToObject<ObservableCollection<MasterJenisBangunanDto>>();
                    _viewModel.JenisBangunanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterKepemilikan",
                }).ContinueWith(result =>
                {
                    _viewModel.KepemilikanBangunanList = JArray.FromObject(MasterListGlobal.MasterKepemilikan).ToObject<ObservableCollection<MasterKepemilikanDto>>();
                    _viewModel.KepemilikanBangunanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterPeruntukan",
                }).ContinueWith(result =>
                {
                    _viewModel.PeruntukanList = JArray.FromObject(MasterListGlobal.MasterPeruntukan).ToObject<ObservableCollection<MasterPeruntukanDto>>();
                    _viewModel.PeruntukanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterTipePendaftaranSambungan",
                }).ContinueWith(result =>
                {
                    _viewModel.TipePendaftaranList = JArray.FromObject(MasterListGlobal.MasterTipePendaftaranSambungan).ToObject<ObservableCollection<MasterTipePendaftaranSambunganDto>>();
                    _viewModel.TipePendaftaranList ??= new();
                });
            }
        }
    }
}
