using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEmptyPeriode = true;
            _viewModel.TotalRecord = 0;
            _viewModel.RekeningAirList = new ObservableCollection<RekeningAirWpf>();
            _viewModel.RekeningAirDetailList = new List<dynamic>();
            _viewModel.SelectedDataPeriode = new MasterPeriodeDto();
            _viewModel.MasterPeriodeList = new List<MasterPeriodeDto>();

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                 "MasterPeriode",
            });

            _viewModel.MasterPeriodeList = MasterListGlobal.MasterPeriode.OrderByDescending(c => c.KodePeriode).ToList();

            _viewModel.IsLoading = false;

            SetComboBox();
            SetFilterLainnya();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void SetComboBox()
        {
            if (!_isTest)
            {
                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                 "MasterStatus",
            }).ContinueWith(result =>
            {
                _viewModel.MasterStatusList = JArray.FromObject(MasterListGlobal.MasterStatus).ToObject<ObservableCollection<MasterStatusDto>>();
                _viewModel.MasterStatusList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                 "MasterGolongan",
            }).ContinueWith(result =>
            {
                _viewModel.MasterGolonganList = JArray.FromObject(MasterListGlobal.MasterGolongan).ToObject<ObservableCollection<MasterGolonganDto>>();
                _viewModel.MasterGolonganList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterDiameter",
            }).ContinueWith(result =>
            {
                _viewModel.MasterDiameterList = JArray.FromObject(MasterListGlobal.MasterDiameter).ToObject<ObservableCollection<MasterDiameterDto>>();
                _viewModel.MasterDiameterList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterUser",
            }).ContinueWith(result =>
            {
                _viewModel.MasterUserList = JArray.FromObject(MasterListGlobal.MasterUser).ToObject<ObservableCollection<MasterUserDto>>();
                _viewModel.MasterUserList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterKelurahan",
            }).ContinueWith(result =>
            {
                _viewModel.MasterKelurahanList = JArray.FromObject(MasterListGlobal.MasterKelurahan).ToObject<ObservableCollection<MasterKelurahanDto>>();
                _viewModel.MasterKelurahanList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterCabang",
            }).ContinueWith(result =>
            {
                _viewModel.MasterCabangList = JArray.FromObject(MasterListGlobal.MasterCabang).ToObject<ObservableCollection<MasterCabangDto>>();
                _viewModel.MasterCabangList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterRayon",
            }).ContinueWith(result =>
            {
                _viewModel.MasterRayonList = JArray.FromObject(MasterListGlobal.MasterRayon).ToObject<ObservableCollection<MasterRayonDto>>();
                _viewModel.MasterRayonList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterWilayah",
            }).ContinueWith(result =>
            {
                _viewModel.MasterWilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahDto>>();
                _viewModel.MasterWilayahList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterKolektif",
            }).ContinueWith(result =>
            {
                _viewModel.MasterKolektifList = JArray.FromObject(MasterListGlobal.MasterKolektif).ToObject<ObservableCollection<MasterKolektifDto>>();
                _viewModel.MasterKolektifList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterFlag",
            }).ContinueWith(result =>
            {
                _viewModel.MasterFlagList = JArray.FromObject(MasterListGlobal.MasterFlag).ToObject<ObservableCollection<MasterFlagDto>>();
                _viewModel.MasterFlagList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterKondisiMeter",
            }).ContinueWith(result =>
            {
                _viewModel.MasterKondisiMeterList = JArray.FromObject(MasterListGlobal.MasterKondisiMeter).ToObject<ObservableCollection<MasterKondisiMeterDto>>();
                _viewModel.MasterKondisiMeterList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterAdministrasiLain",
            }).ContinueWith(result =>
            {
                _viewModel.MasterAdministrasiLainList = JArray.FromObject(MasterListGlobal.MasterAdministrasiLain).ToObject<ObservableCollection<MasterAdministrasiLainDto>>();
                _viewModel.MasterAdministrasiLainList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPemeliharaanLain",
            }).ContinueWith(result =>
            {
                _viewModel.MasterPemeliharaanLainList = JArray.FromObject(MasterListGlobal.MasterPemeliharaanLain).ToObject<ObservableCollection<MasterPemeliharaanLainDto>>();
                _viewModel.MasterPemeliharaanLainList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterRetribusiLain",
            }).ContinueWith(result =>
            {
                _viewModel.MasterRetribusiLainList = JArray.FromObject(MasterListGlobal.MasterRetribusiLain).ToObject<ObservableCollection<MasterRetribusiLainDto>>();
                _viewModel.MasterRetribusiLainList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterMeterai",
            }).ContinueWith(result =>
            {
                _viewModel.MasterMeteraiList = JArray.FromObject(MasterListGlobal.MasterMeterai).ToObject<ObservableCollection<MasterMeteraiDto>>();
                _viewModel.MasterMeteraiList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterBlok",
            }).ContinueWith(result =>
            {
                _viewModel.MasterBlokList = JArray.FromObject(MasterListGlobal.MasterBlok).ToObject<ObservableCollection<MasterBlokDto>>();
                _viewModel.MasterBlokList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterMerekMeter",
            }).ContinueWith(result =>
            {
                _viewModel.MasterMerekMeterList = JArray.FromObject(MasterListGlobal.MasterMerekMeter).ToObject<ObservableCollection<MasterMerekMeterDto>>();
                _viewModel.MasterMerekMeterList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterLoket",
            }).ContinueWith(result =>
            {
                _viewModel.MasterLoketList = JArray.FromObject(MasterListGlobal.MasterLoket).ToObject<ObservableCollection<MasterLoketDto>>();
                _viewModel.MasterLoketList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterKelainan",
            }).ContinueWith(result =>
            {
                _viewModel.MasterKelainanList = JArray.FromObject(MasterListGlobal.MasterKelainan).ToObject<ObservableCollection<MasterKelainanDto>>();
                _viewModel.MasterKelainanList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPetugasBaca",
            }).ContinueWith(result =>
            {
                _viewModel.MasterPetugasBacaList = JArray.FromObject(MasterListGlobal.MasterPetugasBaca).ToObject<ObservableCollection<MasterPetugasBacaDto>>();
                _viewModel.MasterPetugasBacaList ??= new();
            });
            }
        }

        [ExcludeFromCodeCoverage]
        private void SetFilterLainnya()
        {
            _viewModel.MasterLainnyaList = new ObservableCollection<FilterLainnyaDto>
            {
                new FilterLainnyaDto() { Lainnya = "Sudah Baca" },
                new FilterLainnyaDto() { Lainnya = "Belum Baca" },
                new FilterLainnyaDto() { Lainnya = "Sudah Koreksi" },
                new FilterLainnyaDto() { Lainnya = "Belum Koreksi" },
                new FilterLainnyaDto() { Lainnya = "Sudah Publish" },
                new FilterLainnyaDto() { Lainnya = "Belum Publish" },
                new FilterLainnyaDto() { Lainnya = "Sudah Lunas" },
                new FilterLainnyaDto() { Lainnya = "Belum Lunas" },
                new FilterLainnyaDto() { Lainnya = "Sudah Upload" },
                new FilterLainnyaDto() { Lainnya = "Belum Upload" },
                new FilterLainnyaDto() { Lainnya = "Pemakaian Minus" },
                new FilterLainnyaDto() { Lainnya = "Terdapat Stan Angkat" },
                new FilterLainnyaDto() { Lainnya = "Terdapat Kelainan" },
                new FilterLainnyaDto() { Lainnya = "Hanya Yang Di-Taksir" },
                new FilterLainnyaDto() { Lainnya = "Hanya Yang Di-Taksasi" },
                new FilterLainnyaDto() { Lainnya = "Di-Taksir Selama 2 Bulan" },
                new FilterLainnyaDto() { Lainnya = "Di-Taksir Selama 3 Bulan" },
                new FilterLainnyaDto() { Lainnya = "Stan Awal = 0" },
                new FilterLainnyaDto() { Lainnya = "Stan Awal = Stan Akhir" },
                new FilterLainnyaDto() { Lainnya = "Stan Awal = Stan Akhir, Ada Pakai" },
                new FilterLainnyaDto() { Lainnya = "Total Rekening Tdk Sama" },
                new FilterLainnyaDto() { Lainnya = "Rek.Air Rekening Tdk Sama" },
                new FilterLainnyaDto() { Lainnya = "Pemakaian Minimum Selama 3 Bln" },
                new FilterLainnyaDto() { Lainnya = "Pemakaian Minimum Selama 6 Bln" },
                new FilterLainnyaDto() { Lainnya = "Kelainan Sama 3 Bulan" }
            };
        }
    }
}
